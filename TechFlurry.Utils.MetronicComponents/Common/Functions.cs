using System;

namespace TechFlurry.Utils.MetronicComponents.Common
{
    internal static class Functions
    {
        public static string GenerateElementId()
        {
            return "e_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
