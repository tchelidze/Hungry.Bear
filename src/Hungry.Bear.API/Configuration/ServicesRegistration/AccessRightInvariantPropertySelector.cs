using System.Reflection;
using Autofac.Core;

namespace Hungry.Bear.API.Configuration.ServicesRegistration
{
    public class AccessRightInvariantPropertySelector : DefaultPropertySelector
    {
        public AccessRightInvariantPropertySelector(bool preserveSetValues) : base(preserveSetValues)
        { }

        public override bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            if (!propertyInfo.CanWrite)
            {
                return false;
            }

            if (!PreserveSetValues || !propertyInfo.CanRead)
            {
                return true;
            }

            try
            {
                return propertyInfo.GetValue(instance, null) == null;
            }
            catch
            {
                return false;
            }
        }
    }
}