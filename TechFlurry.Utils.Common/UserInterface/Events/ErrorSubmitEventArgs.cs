using System;

namespace TechFlurry.Utils.Common.UserInterface.Events
{
    public class ErrorSubmitEventArgs : FormSubmissionEventArgs
    {
        public string ErrorCode { get; set; }
        public Exception Exception { get; set; }
        public Type ExceptionType { get; set; }
    }
}
