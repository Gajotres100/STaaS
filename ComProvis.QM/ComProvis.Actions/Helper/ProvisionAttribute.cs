using ComProvis.Enums;
using System;

namespace ComProvis.Actions.Helper
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ProvisionAttribute : Attribute
    {
        public ProvisionAttribute(ProvisionType type)
        {
            ProvisionType = type;
        }

        public ProvisionType ProvisionType { get; set; }
    }
}
