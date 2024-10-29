using Technico.Models;

namespace Technico.Interfaces
{
    public interface IPropertyItemService
    {
        public bool ValidateItem(PropertyItem propertyItem);
        public PropertyItem? ViewPropertyItem(int id);
        public PropertyItem CreatePropertyItem(PropertyItem propertyItem, List<int> ownerIds);
        public PropertyItem? UpdatePropertyItem(PropertyItem propertyItem, int id);
        public bool DeletePropertyItem(int id);
    }
}