using System;
using System.Collections.Generic;

namespace Gallerist
{
    public interface IObjectManager<T>
    {
        public List<T> CurrentObjects { get; }
        public List<T> PastObjects { get; }

        public T SelectedObject { get; set; }

        public T GetObjectAt(int index);

        public event EventHandler ObjectsGenerated;
    }
}
