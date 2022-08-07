using System.Collections.Generic;

namespace Gallerist.Data
{
    public class MonthStartingReportDescriptions
    {

        public static string GetStartingReport(int month, int reputationChange)
        {
            return MonthStartingReports[month][reputationChange];
        }

        public static Dictionary<int, Dictionary<int, string>> MonthStartingReports = new()
        {
            { 0, new Dictionary<int, string>()
                {
                    {0, $"[GalleryName] is nearing its grand opening.  As a [RenownDescription] gallery in the town of [TownName], you are aiming to make a splash and draw the attention of more renowned artists and bigger crowds. Time to finalize preparations for the first opening night!" }
                }
            },
            { 1, new Dictionary<int, string>()
                {
                    {-1, "[GalleryName] has had some setbacks, but it's the start of a brand new month!" },
                    {0, "Still seen as a [RenownDescription] space, [GalleryName] is hoping for better performance at this month's show." },
                    {1, "[GalleryName] is now a [RenownDescription] gallery in the community.  You expect a larger turnout this month and hope to see some familiar faces!" }
                }
            },
            { 2, new Dictionary<int, string>()
                {
                    {-1, "[GalleryName] underperformed last month, but [MonthName] begins with renewed hope!" },
                    {0, "[GalleryName] continues as a [RenownDescription] in the community." },
                    {1, "[GalleryName] is building its reputation and is now known as a [RenownDescription] in [TownName].  With [SubscriberCount] subscribers, you expect to see familiar faces and a larger crowd than last month." }
                }
            },
            { 3, new Dictionary<int, string>()
                {
                    {-1, "The first half of the sales season had some setbacks for [GalleryName], but this month brings another opportunity to excel!  " },
                    {0, "Last months's show did well enough, but hasn't changed the community's view of [GalleryName] as a [RenownDescription] gallery.  The gallery has sold [PrintsSold] so far, but closing on an original is important for improving the community's view of the gallery." },
                    {1, "The second half of the sales season is looking good for [GalleryName], with [OriginalsSold] original and [PrintsSold] print sales this year." }
                }
            },
            { 4, new Dictionary<int, string>()
                {
                    {-1, "Going into one of the last shows of the year, [GalleryName] is losing momentum." },
                    {0, "[GalleryName] continues as a [RenownDescription] in the community." },
                    {1, "[GalleryName] is gaining momentum as a [ReputationDescription] in [TownName] and you have grown your mailing list to [MailingListSubscribers] members!  The crowd should be large with plenty of familiar faces.  With a [ArtistExperience] artist and a good reputation, conditions are good for connecting originals with patrons." }
                }
            },
            { 5, new Dictionary<int, string>()
                {
                    {-1, "The final show of the year, and last month's show was a dud.  You are hopeful that [GalleryName]'s year will end on a high note!" },
                    {0, "[GalleryName] continues to make sales of reproductions, selling [PrintsSold] prints so far this year, but has been behind expectations in moving originals." },
                    {1, "Having momentum going into the last show of the year is a favorable start!  [GalleryName] has shown itself to be a [ReputationDescription] to its community." }
                }
            }
        };
    }
}