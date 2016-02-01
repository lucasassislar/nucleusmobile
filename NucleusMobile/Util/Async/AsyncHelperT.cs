using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Nucleus
{
    public class AsyncHelper<T>
    {
        protected Timer timer;
        protected bool running;
        protected Task<T> task;

        /// <summary>
        /// If set to True, the Core's Platform must have been set with the current Activity
        /// </summary>
        public bool RunOnUIThread = true;
        public event Action OnUpdate;
        public event Action OnFinished;
        public event Action<T> OnSuccess;
        public event Action<Exception, TaskStatus> OnFail;

        public bool Running
        {
            get { return running; }
        }

        internal AsyncHelper()
        {
            timer = new Timer();
            timer.Interval = 250;
            timer.Elapsed += timer_Elapsed;
        }

        public void Start(Task<T> task)
        {
            if (running)
            {
                return;
            }

            running = true;

            this.task = task;
            timer.Enabled = true;
        }

        private void finished(T data)
        {
            timer.Enabled = false;
            running = false;

            if (OnFinished != null)
            {
                OnFinished();
            }
            onFinished(data);
        }

        protected virtual void onFinished(T data)
        {
            // success or fail, it finished
        }

        protected virtual void onSuccess(T data)
        {
            if (OnSuccess != null)
            {
                if (RunOnUIThread)
                {
                    Core.Instance.PlatformManager.RunOnUIThread(delegate
                    {
                        OnSuccess(task.Result);
                    });
                }
                else
                {
                    OnSuccess(task.Result);
                }
            }
        }

        protected virtual void onFail()
        {
            if (OnFail != null)
            {
                if (RunOnUIThread)
                {
                    Core.Instance.PlatformManager.RunOnUIThread(delegate
                    {
                        OnFail(task.Exception, task.Status);
                    });
                }
                else
                {
                    OnFail(task.Exception, task.Status);
                }
            }
        }


        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }

            if (task.Status == TaskStatus.RanToCompletion)
            {
                finished(task.Result);
                onSuccess(task.Result);
            }
            else if (task.Status == TaskStatus.Canceled ||
                task.Status == TaskStatus.Faulted)
            {
                finished(default(T));
                onFail();
            }
        }
    }
}