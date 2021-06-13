using System.Collections.Generic;

namespace TechFlurry.Utils.MetronicComponents.Models
{
    public class SearchResultDataModel
    {
        public List<SearchResultModel> Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}