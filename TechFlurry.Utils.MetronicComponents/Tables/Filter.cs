using System.Collections.Generic;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public class Filter
    {
        public Filter()
        {
            SearchableColumns=new List<string>();
        }
        public string Keyword { get; set; }
        public bool IsIgnoreCase { get; set; } = true;
        public bool IsMatchExact { get; set; }
        public bool IsSearchAll { get; set; }
        public List<string> SearchableColumns { get; set; }
    }
}
