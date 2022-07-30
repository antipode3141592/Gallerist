using System;
using System.Collections.Generic;

namespace Gallerist
{
    public interface IObjectManager<T>
    {
        public List<T> CurrentObjects { get; }
        public List<T> PastObjects { get; }

        public T CurrentObject { get; set; }

        public T GetObjectAt(int index);

        public void SetCurrentObject(T obj);

        public event EventHandler ObjectsGenerated;
        public event EventHandler SelectedObjectChanged;
    }
}
