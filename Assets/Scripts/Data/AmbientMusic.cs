using System.Collections.Generic;

namespace Gallerist
{
    public class AmbientMusic : IModifier
    {
        public string Name;
        public string Description;
        public TraitType TypeToModify => TraitType.Aesthetic;
        public List<string> IdsToModify { get; }
        public string Modifiers => $"({string.Join(", ", IdsToModify)})";

        public AmbientMusic(string name, string description, List<string> idsToModify)
        {
            Name = name;
            Description = description;
            IdsToModify = idsToModify;
        }
    }
}