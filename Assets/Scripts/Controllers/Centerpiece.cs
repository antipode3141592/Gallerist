using System.Collections.Generic;

namespace Gallerist
{
    public class Centerpiece : IModifier
    {
        public string Name;
        public string Description;
        public TraitType TypeToModify => TraitType.Emotive;
        public List<string> IdsToModify { get; }
        public string Modifiers => $"({string.Join(", ", IdsToModify)})";

        public Centerpiece(string name, string description, List<string> idsToModify)
        {
            Name = name;
            Description = description;
            IdsToModify = idsToModify;
        }
    }
}