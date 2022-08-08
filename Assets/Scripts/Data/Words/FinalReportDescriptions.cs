using System.Collections.Generic;

namespace Gallerist.Data
{
    public class FinalReportDescriptions
    {

        public static Dictionary<int, string> ReportsByRenown = new()
        {
            { -3, $"[GalleryName] had an abysmal year with many showings that didn't connect with the local crowds of [TownName]."},
            { -2, $"There were significant setbacks in [GalleryName]'s opening year, but there's still plenty of opportunity to learn and grow for next year!"},
            { -1, $"The shows at [GalleryName] did not always connect with the crowds of [TownName], but you were able to move enough art to stay afloat.  Maybe next year's slate of artists will better suit the crowds of [TownName]." },
            { 0, $"This year had some ups and downs but [GalleryName] will perservere into the next year!"},
            { 1, $"[GalleryName]'s shows this year connected with patrons, but rarely did you move originals.  Print sales have sustained the gallery and its artists, but you'll have to work harder to garner acclaim in [TownName] next year."},
            { 2, $"A few successful shows and consistent sales have made the locals of [TownName] take note.  Next year, [GalleryName]'s shows will have bigger crowds and the artists vying for shows will be even more experienced."},
            { 3, $"Every one of [GalleryName]'s shows went well and you adapted well to the rising expectations as the gallery's renown grew.  Each show connected with the inhabitants of [TownName] and more artists want to work with you."}
        };

        
    }
}
