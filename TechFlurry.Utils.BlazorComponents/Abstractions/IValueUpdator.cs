using System;
using TechFlurry.Utils.BlazorComponents.EventArgs;

namespace TechFlurry.Utils.BlazorComponents.Abstractions
{
    public interface IValueUpdator
    {
        event EventHandler<OnUpdateEventArgs> OnValueUpdate;
    }
}
