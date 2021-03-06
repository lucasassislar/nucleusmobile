using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Diagnostics;

#if ANDROID
using Android.Graphics;
using Android.Content.Res;
using Android;
using Android.Content;
using Android.Widget;
#elif IOS
using UIKit;
#endif

namespace Nucleus
{
    /// <summary>
    /// Manages loading and unloading of shared resources
    /// </summary>
    public class ResourceManager : IDisposable
    {
        /// <summary>
        /// Maximum number of loadings that can happen at the same time
        /// </summary>
        public int MaxConcurrentLoads = 4;

        private Dictionary<string, IResourceObject> resources;
        private Timer timer;
        private List<DownloadingInstance> downloading;
        private List<DownloadingInstance> toDownload;
        private object locker = new object();

        public ResourceManager()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;

            resources = new Dictionary<string, IResourceObject>();
            downloading = new List<DownloadingInstance>();
            toDownload = new List<DownloadingInstance>();
        }


        /// <summary>
        /// Removes all resources that we're already disposed
        /// </summary>
        public void Clean()
        {
            List<string> toDel = new List<string>();

            foreach (var res in resources)
            {
                if (res.Value.IsDisposed())
                {
                    toDel.Add(res.Key);
                }
            }

            for (int i = 0; i < toDel.Count; i++)
            {
                resources.Remove(toDel[i]);
            }
        }

        public void Cancel()
        {
            for (int i = 0; i < downloading.Count; i++)
            {
                downloading[i].Task.Dispose();
            }
        }


        /// <summary>
        /// Disposes all resources loaded by this ResourceManager and frees all references
        /// </summary>
        public void Dispose()
        {
            // cancel everything and dispose all resources
            foreach (var res in resources)
            {
                res.Value.Dispose();
            }

            for (int i = 0; i < downloading.Count; i++)
            {
                downloading[i].Task.Dispose();
            }

            resources.Clear();
            downloading.Clear();
            toDownload.Clear();

            resources = null;
            downloading = null;
            toDownload = null;
        }

        /// <summary>
        /// Returns a DownloadingInstance if the object you are requesting is downloading/queued
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DownloadingInstance IsDownloading(string str)
        {
            string lower = str.ToLower();
            for (int i = 0; i < downloading.Count; i++)
            {
                var down = downloading[i];
                if (lower == downloading[i].path.ToString().ToLower())
                {
                    return down;
                }
            }
            for (int i = 0; i < toDownload.Count; i++)
            {
                var down = toDownload[i];
                if (lower == toDownload[i].path.ToString().ToLower())
                {
                    return down;
                }
            }
            return null;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            update();
            timer.Enabled = true;
        }

        /// <summary>
        /// Starts all the downloads that were queued if there's room
        /// </summary>
        private void startQueued()
        {
            if (downloading.Count < MaxConcurrentLoads)
            {
                for (int i = 0; i < toDownload.Count; i++)
                {
                    DownloadingInstance to = toDownload[i];

                    switch (to.dtype)
                    {
                        case LoadType.Download:
                            {
                                Task<byte[]> task;
                                WebClient client = new WebClient();
                                if (to.post == null)
                                {
                                    task = client.DownloadDataTaskAsync(to.path);
                                }
                                else
                                {
                                    task = client.UploadValuesTaskAsync(to.path, to.post);
                                }
                                to.client = client;
                                to.Task = task;
                                downloading.Add(to);
                                toDownload.RemoveAt(i);
                                i--;
                            }
                            break;
                        case LoadType.File:
                            {
#if ANDROID
                                switch (to.type)
                                {
                                    case ResourceType.Image:
                                        {
                                            Task<Bitmap> task = Task.Run<Bitmap>(delegate
                                            {
                                                string path = to.path.OriginalString;
                                                Bitmap bitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(Core.Instance.PlatformManager.Activity.ContentResolver, Android.Net.Uri.Parse(path));
                                                return bitmap;
                                            });

                                            to.Task = task;
                                            downloading.Add(to);
                                            toDownload.RemoveAt(i);
                                            i--;
                                        }
                                        break;
                                }
#elif IOS
                                switch (to.type)
                                {
                                    case ResourceType.Image:
                                        {
                                            Task<UIImage> task = Task.Run<UIImage>(delegate
                                            {
                                                string path = to.path.OriginalString;
                                                if (path.StartsWith(Uri.UriSchemeFile))
                                                {
                                                    path = path.Replace(Uri.UriSchemeFile + Uri.SchemeDelimiter, "");
                                                }

                                                UIImage image = UIImage.FromFile(path);
                                                return image;
                                            });

                                            to.Task = task;
                                            downloading.Add(to);
                                            toDownload.RemoveAt(i);
                                            i--;
                                        }
                                        break;
                                }
#else
                                                throw new NotImplementedException();
#endif
                            }
                            break;
                    }

                    if (downloading.Count >= MaxConcurrentLoads)
                    {
                        break;
                    }
                }
            }
        }

        private void update()
        {
            lock (locker)
            {
                try
                {
                    startQueued();

                    for (int i = 0; i < downloading.Count; i++)
                    {
                        var down = downloading[i];
                        switch (down.dtype)
                        {
                            case LoadType.File:
                                {
                                    Task task = down.Task;
                                    IResourceObject res = null;

                                    if (task.Status == TaskStatus.RanToCompletion)
                                    {
                                        switch (down.type)
                                        {
                                            case ResourceType.Image:
#if ANDROID
                                                Task<Bitmap> t = (Task<Bitmap>)task;
                                                res = new ImageResource(t.Result);
#elif IOS
                                                Task<UIImage> t = (Task<UIImage>)task;
                                                res = new ImageResource(t.Result);
#else
                                                throw new NotImplementedException();
#endif
                                                if (down.store)
                                                {
                                                    resources.Add(down.path.OriginalString, res);
                                                }

                                                downloading.RemoveAt(i);
                                                i--;
                                                for (int j = 0; j < down.Callbacks.Count; j++)
                                                {
                                                    Delegate del = (Delegate)down.Callbacks[j];
                                                    del.DynamicInvoke(res);
                                                    //down.Callbacks[j](res);
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            case LoadType.Download:
                                {
                                    down.client.Dispose();
                                    var task = (Task<byte[]>)down.Task;
                                    if (task.Status == TaskStatus.RanToCompletion)
                                    {
                                        // success
                                        IResourceObject res = null;
                                        switch (down.type)
                                        {
                                            case ResourceType.Image:
                                                res = new ImageResource(task.Result);
                                                if (task.Result.Length < 10)
                                                {
                                                    // decode error
                                                    string error = WebUtil.GetASCIIString(task.Result);
                                                    continue;
                                                }

                                                break;
                                            case ResourceType.String:
                                                res = new StringResource(task.Result);
                                                break;
                                        }

                                        if (down.store)
                                        {
                                            resources.Add(down.path.OriginalString, res);
                                        }

                                        downloading.RemoveAt(i);
                                        i--;

                                        for (int j = 0; j < down.Callbacks.Count; j++)
                                        {
                                            Delegate del = (Delegate)down.Callbacks[j];
                                            del.DynamicInvoke(res);
                                            //down.Callbacks[j](res);
                                        }
                                    }
                                    else if (task.Status == TaskStatus.Canceled ||
                                             task.Status == TaskStatus.Faulted)
                                    {
                                        // failure
                                        downloading.RemoveAt(i);
                                        i--;

                                        for (int j = 0; j < down.failureCallbacks.Count; j++)
                                        {
                                            down.failureCallbacks[j]();
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    if (ex is WebException)
                    {
                        WebException web = (WebException)ex;
                        using (Stream stream = web.Response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream);
                            string data = reader.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Loads an image from the device's internal memory/application data for shared use
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <param name="failure"></param>
        /// <param name="store"></param>
        public virtual void LoadImage(string path, Action<ImageResource> callback, Action failure, bool store = true)
        {
            LoadFile(path, ResourceType.Image, callback, failure, store);
        }

        /// <summary>
        /// Loads a file from the device's internal memory/application data for shared use
        /// </summary>
        /// <param name="path"></param>
        /// <param name="resource"></param>
        /// <param name="callback"></param>
        /// <param name="failure"></param>
        /// <param name="store"></param>
        public virtual void LoadFile<T>(string path, ResourceType resource, Action<T> callback, Action failure, bool store = true) where T : IResourceObject
        {
#if ANDROID
            string package = Core.Instance.PlatformManager.Activity.PackageName;
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            path = "android.resource://" + package + "/drawable/" + name;
#endif

            lock (locker)
            {
                IResourceObject res;
                if (resources.TryGetValue(path, out res))
                {
                    if (res.IsDisposed())
                    {
                        res.Dispose();

                        resources.Remove(path);
                    }
                    else
                    {
                        if (callback != null)
                        {
                            callback((T)res);
                        }
                        return;
                    }
                }
                var download = IsDownloading(path);
                if (download == null)
                {
                    toDownload.Add(new DownloadingInstance(null, new Uri(path),
                        callback == null ? new List<object>() : new List<object>() { callback },
                        failure == null ? new List<Action>() : new List<Action>() { failure }, null, resource, LoadType.File, store));
                }
                else
                {
                    if (callback != null)
                    {
                        download.Callbacks.Add(callback);
                    }
                    if (failure != null)
                    {
                        download.failureCallbacks.Add(failure);
                    }
                }
            }
        }

        /// <summary>
        /// Downloads a string and by default do not store it as a resource
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="callback"></param>
        /// <param name="failure"></param>
        /// <param name="postData"></param>
        /// <param name="store"></param>
        public virtual void DownloadString(Uri uri, Action<StringResource> callback, Action failure, NameValueCollection postData = null, bool store = false)
        {
            Download(uri, callback, failure, ResourceType.String, postData, false);
        }

        /// <summary>
        /// Downloads an image and by default store it as a shared-resource
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="callback"></param>
        /// <param name="failure"></param>
        /// <param name="postData"></param>
        public virtual void DownloadImage(Uri uri, Action<ImageResource> callback, Action failure = null, NameValueCollection postData = null, bool store = true)
        {
            Download(uri, callback, failure, ResourceType.Image, postData);
        }

        /// <summary>
        /// Downloads a Resource from the internet for shared use
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="callback"></param>
        /// <param name="failure"></param>
        /// <param name="resType"></param>
        /// <param name="postData"></param>
        /// <param name="store"></param>
        public virtual void Download<T>(Uri uri, Action<T> callback, Action failure, ResourceType resType, NameValueCollection postData = null, bool store = true) where T : IResourceObject
        {
            string path = uri.ToString();
            string lower = path.ToLower();

            lock (locker)
            {
                IResourceObject res;
                if (resources.TryGetValue(lower, out res))
                {
                    if (res.IsDisposed())
                    {
                        resources.Remove(lower);
                    }
                    else
                    {
                        if (callback != null)
                        {
                            callback((T)res);
                        }
                        return;
                    }
                }

                var download = IsDownloading(lower);

                if (download == null)
                {
                    toDownload.Add(new DownloadingInstance(null, uri,
                        callback == null ? new List<object>() : new List<object>() { callback },
                        failure == null ? new List<Action>() : new List<Action>() { failure }, postData, resType, LoadType.Download, store));
                }
                else
                {
                    if (callback != null)
                    {
                        download.Callbacks.Add(callback);
                    }
                    if (failure != null)
                    {
                        download.failureCallbacks.Add(failure);
                    }
                }
            }
        }
    }
}