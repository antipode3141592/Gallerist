using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Patron
    {
        public string Name { get; private set; }
        public bool IsSubscriber { get; private set; }
        public IList<string> Acquisitions { get; private set; }
        public IList<ITrait> Preferences { get; private set; }
    }
}