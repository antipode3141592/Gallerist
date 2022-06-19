using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Bag
    {
        List<int> contents = new List<int>();

        public Bag()
        {
        }

        public void ResetBag(int total)
        {
            contents.Clear();
            for (int i = 0; i < total; i++)
                contents.Add(i);
        }

        public int DrawFromBag()
        {
            int randomIndex = Random.Range(0, contents.Count);
            int retval = contents[randomIndex];
            contents.RemoveAt(randomIndex);
            return retval;
        }
    }
}