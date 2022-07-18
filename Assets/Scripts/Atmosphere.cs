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
        public TraitType TypeToModify { get; } 
        public List<string> IdsToModify { get; }
    }
}