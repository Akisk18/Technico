using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.Interfaces;

public interface IPropertyRepairService
{
    bool ValidateRepair(PropertyRepair propertyRepair);
    List<PropertyRepair> SearchPropertyRepair(DateTime searchDate);
    PropertyRepair CreatePropertyRepair(PropertyRepair propertyRepair , int itemId);
    PropertyRepair? UpdatePropertyRepair(PropertyRepair propertyRepair, int id);
    bool DeletePropertyRepair(int id);

}
