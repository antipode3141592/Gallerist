using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.UI
{
    public class PreparationItems : MonoBehaviour
    {
        public List<PreparationItem> PreparationItemsList;

        public PreparationItem SelectedItem;

        public event EventHandler SelectedItemChanged;

        void Awake()
        {
            foreach(var item in PreparationItemsList)
            {

                item.ItemClicked += PreparationItemSelected;
                item.Highlight(false);
            }
        }

        void PreparationItemSelected(object sender, EventArgs e)
        {
            PreparationItem _sender = sender as PreparationItem;
            foreach (var item in PreparationItemsList)
            {
                if (_sender == item)
                {
                    SelectedItem = item;
                    SelectedItemChanged?.Invoke(this, EventArgs.Empty);
                    item.Highlight(true);
                } else
                {
                    item.Highlight(false);
                }
            }
        }
    }
}