using System;
using System.Collections.Generic;

namespace Gallerist
{
    public interface ITraitDataSource
    {
        List<string> AestheticTraits { get; set; }
        List<string> EmotiveTraits { get; set; }

        List<ITrait> GenerateAestheticTraits(int totalTraits, Type traitType, List<string> requiredTraits = null, int bonus = 0);
        List<ITrait> GenerateEmotiveTraits(int totalTraits, Type traitType, List<string> requiredTraits = null, int bonus = 0);
        List<string> GetRandomTraitNames(int totalTraits, TraitType traitType);
    }
}