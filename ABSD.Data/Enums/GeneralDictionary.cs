using System.Collections.Generic;

namespace ABSD.Data.Enums
{
    public static class GeneralDictionary
    {
        public static readonly Dictionary<int, string> Criterion = new Dictionary<int, string>()
        {
            {1, "Service Benefits Criterion"},
            {2, "Service Barriers Criterion"},
            {3, "Service Ethnicity Criterion"},
            {4, "Service Disability Criterion"},
            {5, "Service Personal Circumstances Criterion"},
            {6, "Other Service Participation Criterion"}
        };

        public static readonly Dictionary<int, string> ClientSupporter = new Dictionary<int, string>()
        {
            {1, "Client Support Process"},
            {2, "Client Outcome"},
            {3, "Target Client"},
            {4, "Client Journey"},
            {5, "Referal Sources"},
            {6, "Support Centres"}
        };

        public static readonly Dictionary<int, string> Content = new Dictionary<int, string>()
        {
            {1, "Contract Outcome"},
            {2, "Contract Obligation"}
        };
    }
}