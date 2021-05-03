using System;

namespace TechFlurry.Utils.BlazorComponents.EventArgs
{
    public class DataLoadFailedEventArgs : ApplicationEventArgs
    {
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public string ErrorCode { get; set; }
        public int FormCode { get; set; }
    }
}
