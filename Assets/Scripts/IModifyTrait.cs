using Gallerist.Events;
using System;

namespace Gallerist
{
    public interface IModifyTrait
    {
        event EventHandler<TraitModified> TraitModified;

        bool ModifyTrait(string traitName, int modifier);
    }
}