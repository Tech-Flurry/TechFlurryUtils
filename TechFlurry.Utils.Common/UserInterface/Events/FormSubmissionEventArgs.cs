namespace TechFlurry.Utils.Common.UserInterface.Events
{
    public class FormSubmissionEventArgs : ApplicationEventArgs
    {
        public string Message { get; set; }
        public int FormCode { get; set; }
    }
}
