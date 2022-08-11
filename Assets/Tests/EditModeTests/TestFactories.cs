using Gallerist;

namespace Testing.EditMode
{
    public class TestFactories
    {
        public static string defaultArtistName = "Arty McArtist";

        public static Patron GetPatron()
        {
            return new Patron(firstName: "Polly",
                lastInitial: "P.",
                portrait: null,
                isSubscriber: false,
                aestheticTraits: new()
                {
                    new PatronTrait(
                        name: "ATrait1",
                        value: 5,
                        isKnown: false,
                        traitType: TraitType.Aesthetic)
                    ,
                    new PatronTrait(
                        name: "ATrait2",
                        value: 5,
                        isKnown: false,
                        traitType: TraitType.Aesthetic),
                    new PatronTrait(
                        name: "ATrait3",
                        value: 5,
                        isKnown: false,
                        traitType: TraitType.Aesthetic)
                },
                emotiveTraits: new()
                {
                    new PatronTrait(
                        name: "ETrait1",
                        value: 5,
                        isKnown: false,
                        traitType: TraitType.Emotive)
                    ,
                    new PatronTrait(
                        name: "ETrait2",
                        value: 5,
                        isKnown: false,
                        traitType: TraitType.Emotive),
                    new PatronTrait(
                        name: "ETrait3",
                        value: 5,
                        isKnown: false,
                        traitType: TraitType.Emotive)
                },
                acquisitions: new()
                {

                },
                aestheticThreshold: 8,
                emotiveThreshold: 8,
                boredomThreshold: 2);
        }

        public static Artist GetArtist()
        {
            return new Artist(name: defaultArtistName,
                bio: "",
                favoredAestheticTraits: new()
                {
                    new ArtistTrait(
                    name: "ATrait1",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Aesthetic),
                    new ArtistTrait(
                    name: "ATrait2",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Aesthetic),
                    new ArtistTrait(name: "ATrait3",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Aesthetic)
                },
                favoredEmotiveTraits: new()
                {
                    new ArtistTrait(
                    name: "ETrait1",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Emotive),
                    new ArtistTrait(
                    name: "ETrait2",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Emotive),
                    new ArtistTrait(name: "ETrait3",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Emotive)
                },
                portrait: null,
                experience: 0);
        }

        public static Art GetArt()
        {
            return new Art(
                name: "Example Art #1",
                description: "An incredible, innovative feat of daring!",
                artistName: defaultArtistName,
                aestheticQualities: new()
                {
                    new ArtTrait(
                    name: "ATrait1",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Aesthetic),
                    new ArtTrait(
                    name: "ATrait2",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Aesthetic),
                    new ArtTrait(name: "ATrait3",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Aesthetic)
                },
                emotiveQualities: new()
                {
                    new ArtTrait(
                    name: "ETrait1",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Emotive),
                    new ArtTrait(
                    name: "ETrait2",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Emotive),
                    new ArtTrait(name: "ETrait3",
                    value: 5,
                    isKnown: false,
                    traitType: TraitType.Emotive)
                },
                image: null);
        }

    }
}
