using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus
{
    /// <summary>
    /// Contains data about a loading/downloading operation
    /// </summary>
    public class DownloadingInstance
    {
        public Uri path;
        public List<Action> failureCallbacks;
        public NameValueCollection post;
        public ResourceType type;
        public LoadType dtype;
        public bool store;
        public WebClient client;

        protected Task task;
        public Task Task
        {
            get { return task; }
            set { task = value; }
        }

        protected List<object> callbacks;
        public List<object> Callbacks
        {
            get { return callbacks; }
        }

        public DownloadingInstance(Task ts, Uri pt, List<object> backs, List<Action> onFailure, NameValueCollection post,
             ResourceType type, LoadType dtype, bool store)
        {
            path = pt;
            task = ts;
            callbacks = backs;
            this.post = post;
            this.type = type;
            this.failureCallbacks = onFailure;
            this.dtype = dtype;
            this.store = store;
        }
    }
}