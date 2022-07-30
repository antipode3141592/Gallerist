using System.Collections.Generic;

namespace Gallerist
{
    public interface IModifier
    {
        public TraitType TypeToModify { get; } 
        public List<string> IdsToModify { get; }
    }
}