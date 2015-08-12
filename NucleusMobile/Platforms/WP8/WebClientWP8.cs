#if WP8

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
    public class WebClientWP8 : WebClient, IDisposable
    {
        private List<Task<byte>> runningTasks;

        public WebClientWP8()
        {
            runningTasks = new List<Task<byte>>();
        }

        public void Dispose()
        {
            runningTasks.Clear();
            runningTasks = null;
        }

        public Task<byte[]> DownloadDataTaskAsync(string address)
        {
            return Task<byte[]>.Factory.StartNew(() =>
            {
                WebRequest request = this.GetWebRequest(new Uri(address));
                byte[] data = null;

                AsyncCallback callback = new AsyncCallback((IAsyncResult res) =>
                {
                    using (Stream str = request.EndGetRequestStream(res))
                    {
                        data = new byte[str.Length];
                        str.Write(data, 0, data.Length);
                    }
                });

                request.BeginGetRequestStream(callback, null);

                while (data == null)
                {

                }
                return data;
            });
        }

        public Task<byte[]> UploadValuesTaskAsync(string address, NameValueCollection names)
        {
            return Task<byte[]>.Factory.StartNew(() =>
            {
                WebRequest request = this.GetWebRequest(new Uri(address));
                request.ContentType = "text/plain; charset=utf-8";
                request.Method = "POST";

                byte[] data = null;

                request.BeginGetRequestStream(new AsyncCallback((IAsyncResult res) =>
                {
                    using (Stream postStream = request.EndGetRequestStream(res))
                    {
                        StringBuilder postParamBuilder = new StringBuilder();
                        foreach (var key in names.Keys)
                        {
                            postParamBuilder.Append(String.Format("{0}={1}&", key, names[key]));
                        }

                        byte[] byteArray = Encoding.UTF8.GetBytes(postParamBuilder.ToString());
                        postStream.Write(byteArray, 0, byteArray.Length);
                        postStream.Close();

                        request.BeginGetResponse(new AsyncCallback((IAsyncResult nres) =>
                            {
                                using (Stream respStream = request.EndGetResponse(nres).GetResponseStream())
                                {
                                    data = new byte[respStream.Length];
                                    respStream.Write(data, 0, data.Length);
                                }
                            }), null);
                    }
                }), null);

                while (data == null)
                {

                }
                return data;
            });
        }

    }
}
#endif