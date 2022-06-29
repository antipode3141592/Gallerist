using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Atmosphere
    {
        public List<IModifier> Modifiers;


    }

    public interface IModifier
    {
        public Type TypeToModify { get; } 
        public IList<string> IdsToModify { get; }
    }
}