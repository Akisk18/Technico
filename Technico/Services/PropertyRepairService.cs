using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Repositories;

namespace Technico.Services;

public class PropertyRepairService
{
    private PropertyDbContext db;

    public PropertyRepairService(PropertyDbContext db)
    {
        this.db = db;
    }
    //Create repair details
    public PropertyRepair CreatePropertyRepair(PropertyRepair propertyRepair , int itemId)
    {
        var item = db.PropertyItems.FirstOrDefault(p => p.Id == itemId);
        propertyRepair.PropertyItemId = itemId;
        propertyRepair.Property = item;
        db.PropertyRepairs.Add(propertyRepair);
        db.SaveChanges();
        Console.WriteLine("Repair Details added succesfully!");
        return propertyRepair;
    }
    //Update the Repair Details
    public PropertyRepair? UpdatePropertyRepair(PropertyRepair propertyRepair)
    {
        PropertyRepair? propertyRepairdb = db.PropertyRepairs.FirstOrDefault(p => p.Id == propertyRepair.Id);
        if (propertyRepairdb != null)
        {
            propertyRepairdb.ScheduledRepair = propertyRepair.ScheduledRepair;
            propertyRepairdb.RepairType = propertyRepair.RepairType;
            propertyRepairdb.RepairDescription = propertyRepair.RepairDescription;
            propertyRepairdb.RepairAddress = propertyRepair.RepairAddress;
            propertyRepairdb.RepairStatus = propertyRepair.RepairStatus;
            propertyRepairdb.RepairPrice = propertyRepair.RepairPrice;
            propertyRepairdb.Property = propertyRepairdb.Property;
            Console.WriteLine("Property Repair Details Updated succesfully!");
            return propertyRepairdb;
        }
        Console.WriteLine("Repair details could not found.");
        return null;
    }
    //Delete a propertyRepair
    public bool DeletePropertyRepair(int id)
    {
        PropertyRepair? propertyRepairdb = db.PropertyRepairs.FirstOrDefault(p => p.Id == id);
        if (propertyRepairdb != null)
        {
            db.PropertyRepairs.Remove(propertyRepairdb);
            db.SaveChanges();
            Console.WriteLine("Repair details deleted succesfully!");
            return true;
        }
        Console.WriteLine("Repair details could not be found.");
        return false;
    }
}
