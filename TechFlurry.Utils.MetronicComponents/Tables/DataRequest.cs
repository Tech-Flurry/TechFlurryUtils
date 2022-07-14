﻿using System.Collections.Generic;

namespace TechFlurry.Utils.MetronicComponents.Tables
{
    public class DataRequest
    {
        public int StartRow { get; set; }
        public int Count { get; set; }
        public Filter Filter { get; set; }
        public List<SortOrder> SortOrder { get; set; } = new List<SortOrder>();
    }
}
