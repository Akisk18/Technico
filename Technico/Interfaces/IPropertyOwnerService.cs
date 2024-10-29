using Technico.Models;

namespace Technico.Interfaces
{
    public interface IPropertyOwnerService
    {
        public PropertyOwner Register(PropertyOwner propertyOwner);
        public bool ValidateOwner(PropertyOwner propertyOwner);
        public void DisplayDetails(int id);
        public PropertyOwner? UpdatePropertyOwner(PropertyOwner propertyOwner, int id);
        public bool DeletePropertyOwner(int id);
    }
}