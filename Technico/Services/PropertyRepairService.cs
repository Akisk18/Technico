using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Interfaces;
using Technico.Models;
using Technico.Repositories;
using Technico.Responses;

namespace Technico.Services;

public class PropertyRepairService : IPropertyRepairService
{
    private readonly PropertyDbContext db;

    public PropertyRepairService(PropertyDbContext db)
    {
        this.db = db;
    }

    public bool ValidateRepair(PropertyRepair propertyRepair)
    {
        bool isValid = true;
        if (propertyRepair.RepairAddress == null)
        {
            isValid = false;
            Console.WriteLine("Please enter a repair address.");
        }
        if(propertyRepair.RepairDescription == null) 
        {
            isValid = false;
            Console.WriteLine("Please enter a repair description");
        }

        return isValid;
    }
    //Search repair details
    public List<PropertyRepair> SearchPropertyRepair (DateTime searchDate)
    {
        var repairs = db.PropertyRepairs
            .Where(repair => repair.ScheduledRepair.Date == searchDate.Date)
            .ToList();
        
        return repairs;
    }

    //Create repair details
    public ResponseApi<PropertyRepair> CreatePropertyRepair(PropertyRepair propertyRepair , int itemId)
    {
        var item = db.PropertyItems.FirstOrDefault(p => p.Id == itemId);
        propertyRepair.PropertyItemId = itemId;
        propertyRepair.Property = item;
        if (!ValidateRepair(propertyRepair)) 
        {
            Console.WriteLine("Validation failed. Creation of Repair details failed.");
             return new ResponseApi<PropertyRepair>
            {
                Message = "Validation failed. Creation of Repair details failed",
                Status = 1
            }; ;
        }

        if (item == null)
        {
            Console.WriteLine("Property not found. Creation of repair detail failed.");
            return new ResponseApi<PropertyRepair>
            {
                Message = "Property not found. Creation of repair detail failed.",
                Status = 1
            };
        }

        try
        {
            db.PropertyRepairs.Add(propertyRepair);
            db.SaveChanges();
            Console.WriteLine("\nRepair Details added succesfully!");
            return new ResponseApi<PropertyRepair>
            {
                Message = "Repair Details added Succesfully!",
                Status = 0,
                Value = propertyRepair
            };
        }
        catch (Exception) 
        {
            Console.WriteLine("An error occured saving in the database.");
            return new ResponseApi<PropertyRepair>
            {
                Message = "An error occured saving in the database.",
                Status = -1
            };
        }
        
       
    }
    //Update the Repair Details
    public ResponseApi<PropertyRepair> UpdatePropertyRepair(PropertyRepair propertyRepair, int id)
    {
        PropertyRepair? propertyRepairdb = db.PropertyRepairs.FirstOrDefault(p => p.Id == id);

        if (!ValidateRepair(propertyRepair)) 
        {
            Console.WriteLine("Validation failed. Creation of Repair details failed.");
            return new ResponseApi<PropertyRepair>
            {
                Message = "Validation failed. Creation of Repair details failed.",
                Status = 1,
            };
        }
        if (propertyRepairdb == null)
        {
            Console.WriteLine("Repair details could not be found. Update failed.");
            return new ResponseApi<PropertyRepair>
            {
                Message = "The Repair details could not be found. Update failed.",
                Status = 1,
            };
        }
            propertyRepairdb.ScheduledRepair = propertyRepair.ScheduledRepair;
            propertyRepairdb.RepairType = propertyRepair.RepairType;
            propertyRepairdb.RepairDescription = propertyRepair.RepairDescription;
            propertyRepairdb.RepairAddress = propertyRepair.RepairAddress;
            propertyRepairdb.RepairStatus = propertyRepair.RepairStatus;
            propertyRepairdb.RepairPrice = propertyRepair.RepairPrice;
            propertyRepairdb.Property = propertyRepair.Property;
            try
            {
                db.SaveChanges();
                Console.WriteLine("Property Repair Details Updated succesfully!");
            return new ResponseApi<PropertyRepair>
            {
                Message = "Property Updated!",
                Status = 0,
                Value = propertyRepairdb
            };
        }
            catch (Exception) 
            {
                Console.WriteLine("An error occured.");
            return new ResponseApi<PropertyRepair>
            {
                Message = "An error occured saving in the database.",
                Status = -1,
            };
        }
            
            
    }
    //Delete a propertyRepair
    public bool DeletePropertyRepair(int id)
    {
        PropertyRepair? propertyRepairdb = db.PropertyRepairs.FirstOrDefault(p => p.Id == id);
        if (propertyRepairdb == null)
        {
            Console.WriteLine("Repair details could not be found. Deletion canceled.");
            return false;
        }
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
               return false;
            }
           
    }
}
