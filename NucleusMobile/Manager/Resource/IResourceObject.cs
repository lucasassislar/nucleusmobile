using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    /// <summary>
    /// An interface for loaded shared-resources, that can be disposed on-demand
    /// </summary>
    public interface IResourceObject : IDisposable
    {
        bool IsDisposed();
    }
}