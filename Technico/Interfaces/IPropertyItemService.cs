using Technico.Models;
using Technico.Responses;

namespace Technico.Interfaces
{
    public interface IPropertyItemService
    {
         bool ValidateItem(PropertyItem propertyItem);
         void ViewPropertyItem(int id);
         ResponseApi<PropertyItem> CreatePropertyItem(PropertyItem propertyItem, List<int> ownerIds);
         ResponseApi<PropertyItem> UpdatePropertyItem(PropertyItem propertyItem, int id);
        public bool DeletePropertyItem(int id);
    }
}