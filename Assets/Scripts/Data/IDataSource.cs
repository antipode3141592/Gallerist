using System.Collections.Generic;

namespace Gallerist.Data
{
    public interface IDataSource<T>
    {
        public string DataPath { get; }

        public List<T> DataList { get; }
        public T GetRandomItem();


    }
}