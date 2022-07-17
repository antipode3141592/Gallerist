using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.Data
{
    public class MainEventDataSource : MonoBehaviour, IDataSource<string>
    {
        [SerializeField] string dataPath = "Gallerist - MainEvent";

        Bag dataBag;

        public List<string> DataList { get; } = new();

        public string DataPath => dataPath;

        void Awake()
        {
            var _dataList = Resources.Load<TextAsset>(DataPath);
            DataList.AddRange(_dataList.text.Split(',', '\n'));
            dataBag = new Bag(DataList.Count);

            if (Debug.isDebugBuild)
                Debug.Log($"{gameObject.name} has {DataList.Count} items");
        }

        public string GetRandomItem()
        {
            return DataList[dataBag.DrawFromBag()];
        }
    }
}