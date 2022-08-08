using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallerist.Data
{
    public class FinalReportDescriptions
    {

        public static Dictionary<int, string> ReportsByRenown = new()
        {
            { -3, $"[GalleryName] had an abysmal year with many showings that didn't connect with the local crowds of [TownName]."},
            { -2, $""},
            { -1, $"" },
            { 0, $""},
            { 1, $""},
            { 2, $""},
            { 3, $""}
        };

        
    }
}
