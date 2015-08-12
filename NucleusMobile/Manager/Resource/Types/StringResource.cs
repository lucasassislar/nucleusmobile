using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    /// <summary>
    /// A Resource holding a string
    /// </summary>
    public class StringResource : IResourceObject
    {
        public string String;

        public StringResource(byte[] daa)
        {
            String = WebUtil.GetASCIIString(daa);
        }

        public void Dispose()
        {
        }

        public bool IsDisposed()
        {
            // can't dispose a string
            return false;
        }
    }
}