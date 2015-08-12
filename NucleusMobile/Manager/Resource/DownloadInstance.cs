using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

        protected Task task;
        public Task Task
        {
            get { return task; }
            set { task = value; }
        }

        protected List<Action<IResourceObject>> callbacks;
        public List<Action<IResourceObject>> Callbacks
        {
            get { return callbacks; }
        }

        public DownloadingInstance(Task ts, Uri pt, List<Action<IResourceObject>> backs, List<Action> onFailure, NameValueCollection post,
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