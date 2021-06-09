using System;
using TechFlurry.Utils.Common.UserInterface.Events;

namespace TechFlurry.Utils.Common.UserInterface.Processes
{
    public interface IFormHandler
    {
        event EventHandler<SubmitSuccessEventArgs> OnSuccess;
        event EventHandler<ErrorSubmitEventArgs> OnError;
        int FormCode { get; }
    }
}
