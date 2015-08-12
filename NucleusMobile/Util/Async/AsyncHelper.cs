using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nucleus;
using System.Threading.Tasks;

namespace Nucleus
{
    public class AsyncHelper : AsyncHelper<byte[]>
    {
        public int Type
        {
            get { return workType; }
        }
        protected int workType;

        public AsyncHelper()
        {
        }
    }
}