using System;

namespace TechFlurry.Utils.MetronicComponents.Models
{
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string ToString(string format)
        {
            return $"{Start.ToString(format)} to {End.ToString(format)}";
        }

        public override string ToString()
        {
            return $"{Start} to {End}";
        }
    }
}
