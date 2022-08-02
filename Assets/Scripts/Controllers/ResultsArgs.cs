using System;

namespace Gallerist
{
    public class ResultsArgs : EventArgs
    {
        public string Description;
        public string Summary;

        public ResultsArgs(string description, string summary)
        {
            Description = description;
            Summary = summary;
        }
    }
}