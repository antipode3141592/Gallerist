using System;

namespace Gallerist.Events
{
    public class TraitModified : EventArgs
    {
        public string TraitName;
        public int Modifier;

        public TraitModified(string traitName, int modifier)
        {
            TraitName = traitName;
            Modifier = modifier;
        }
    }
}