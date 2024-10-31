using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Responses;

namespace Technico.Interfaces;

public interface IPropertyRepairService
{
    bool ValidateRepair(PropertyRepair propertyRepair);
    List<PropertyRepair> SearchPropertyRepair(DateTime searchDate);
    ResponseApi<PropertyRepair> CreatePropertyRepair(PropertyRepair propertyRepair , int itemId);
    ResponseApi<PropertyRepair> UpdatePropertyRepair(PropertyRepair propertyRepair, int id);
    bool DeletePropertyRepair(int id);

}
