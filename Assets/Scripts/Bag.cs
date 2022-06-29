using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Bag
    {
        List<int> contents = new List<int>();
        int _total;

        public Bag(int total)
        {
            _total = total;
            for (int i = 0; i < _total; i++)
                contents.Add(i);
        }

        public void ResetBag()
        {
            contents.Clear();
            for (int i = 0; i < _total; i++)
                contents.Add(i);
        }

        public int DrawFromBag()
        {
            //check bag size, if 0 (empty), reset bag before drawing
            if (contents.Count == 0) ResetBag();
            int randomIndex = Random.Range(0, contents.Count);
            int retval = contents[randomIndex];
            contents.RemoveAt(randomIndex);
            return retval;
        }
    }
}