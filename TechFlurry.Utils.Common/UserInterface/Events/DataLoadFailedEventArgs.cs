using System;

namespace TechFlurry.Utils.Common.UserInterface.Events
{
    public class DataLoadFailedEventArgs : ApplicationEventArgs
    {
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public string ErrorCode { get; set; }
        public int FormCode { get; set; }
    }
}
