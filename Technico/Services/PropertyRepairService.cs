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

    //Search repair details
    public List<PropertyRepair> SearchPropertyRepair (DateTime searchDate)
    {
        var repairs = db.PropertyRepairs
            .Where(repair => repair.ScheduledRepair.Date == searchDate.Date)
            .ToList();
        if (repairs.Count == 0)
        {
            Console.WriteLine("No repairs found for this date.");
        }
        Console.Write($"{repairs.Count} found in {searchDate}\n");
        foreach (var repair in repairs)
        {
            Console.WriteLine(repair);
        }
        return repairs;
    }

    //Create repair details
    public PropertyRepair CreatePropertyRepair(PropertyRepair propertyRepair , int itemId)
    {
        var item = db.PropertyItems.FirstOrDefault(p => p.Id == itemId);
        propertyRepair.PropertyItemId = itemId;
        propertyRepair.Property = item;

        try
        {
            db.PropertyRepairs.Add(propertyRepair);
            db.SaveChanges();
            Console.WriteLine("Repair Details added succesfully!");
        }
        catch (Exception) 
        {
            Console.WriteLine("An error occured.");
        }
        
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
            try
            {
                db.SaveChanges();
                Console.WriteLine("Property Repair Details Updated succesfully!");
            }
            catch (Exception) 
            {
                Console.WriteLine("An error occured.");
            }
            
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
            try 
            {
                db.PropertyRepairs.Remove(propertyRepairdb);
                db.SaveChanges();
                Console.WriteLine("Repair details deleted succesfully!");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured.");
            }
            
        }
        Console.WriteLine("Repair details could not be found.");
        return false;
    }
}
