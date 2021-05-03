using Microsoft.AspNetCore.Components;
using TechFlurry.Utils.BlazorComponents.EventArgs;

namespace TechFlurry.Utils.BlazorComponents.Infrastructure
{
    public class CustomComponentBase : ComponentBase
    {
        protected void OnValueUpdated(object sender, OnUpdateEventArgs e)
        {
            UpdateUI();
        }
        protected void UpdateUI()
        {
            StateHasChanged();
        }
    }
}
