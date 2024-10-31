using Technico.Models;
using Technico.Responses;

namespace Technico.Interfaces
{
    public interface IPropertyOwnerService
    {
        ResponseApi<PropertyOwner> Register(PropertyOwner propertyOwner);
        bool ValidateOwner(PropertyOwner propertyOwner);
        void DisplayDetails(int id);
        ResponseApi<PropertyOwner> UpdatePropertyOwner(PropertyOwner propertyOwner , int id);
        public bool DeletePropertyOwner(int id);
    }
}