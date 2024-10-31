using Microsoft.EntityFrameworkCore;
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

public class PropertyItemService : IPropertyItemService
{
    private readonly PropertyDbContext db;

    public PropertyItemService(PropertyDbContext db)
    {
        this.db = db;
    }


    public bool ValidateItem(PropertyItem propertyItem)
    {
        bool isValid = true;
        if (propertyItem.OwnerVAT == null || propertyItem.OwnerVAT.Length != 10) 
        {
            isValid = false;
            Console.WriteLine("Please enter a valid VAT number");
        }
        if(propertyItem.PublicIdentificationNumber == null) 
        {
            isValid = false;
            Console.WriteLine("Please enter Public Identification Number.");
        }
        if (propertyItem.PropertyAddress == null) 
        {
            isValid = false;
            Console.WriteLine("Please enter the address of the property");
        }
        return isValid;
    }

    //Displays all details of the property
    public ResponseApi<PropertyItem> ViewPropertyItem(int id)
    {
       var propertyItem = db.PropertyItems.Where(p => p.Id == id)
            .Include(p => p.Repairs)
            .Include(p => p.Owners)
            .FirstOrDefault();

        if (propertyItem == null)
        {
            Console.WriteLine("Property not found.");
            return new ResponseApi<PropertyItem>
            {
                Message = "Property not found.",
                Status = 1
            };
        }
        return new ResponseApi<PropertyItem>
        {
            Message = "Success",
            Status = 0,
            Value = propertyItem
        };
    }
    //Creates a new propertyItem
    public ResponseApi<PropertyItem> CreatePropertyItem(PropertyItem propertyItem , List<int> ownerIds)
    {
        var owners = db.PropertyOwners.Where(p => ownerIds.Contains(p.Id)).ToList();

        if (!ValidateItem(propertyItem))
        {
            Console.WriteLine("Validation failed. Creation of Property canceled");
            return new ResponseApi<PropertyItem>
            {
                Message = "Validation failed. Creation of Property canceled",
                Status = 1
            };
        }    
        if (owners == null) 
        {
            Console.WriteLine("Owner not found. Property creation failed.");
            return new ResponseApi<PropertyItem>
            {
                Message = "Owner not found. Property creation failed",
                Status = 1
            };
        }
        propertyItem.Owners = owners;
        propertyItem.PropertyOwnerIds = ownerIds;
        try
        {
            db.PropertyItems.Add(propertyItem);
            db.SaveChanges();
            Console.Write("\nProperty Created Succesfully!");
            return new ResponseApi<PropertyItem>
            {
                Message = "Property Created Succesfully!",
                Status = 0,
                Value = propertyItem       
            };
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            return new ResponseApi<PropertyItem>
            {
                Message = "An error occured.",
                Status = -1
            };
        }
       
    }
    //Update a propertyItem
    public ResponseApi<PropertyItem> UpdatePropertyItem(PropertyItem propertyItem ,int id) 
    {
        PropertyItem? propertyItemdb = db.PropertyItems
            .FirstOrDefault(p => p.Id == id);

        if (!ValidateItem(propertyItem))
        {
            Console.WriteLine("Validation failed. Creation of Property canceled");
            return new ResponseApi<PropertyItem>
            {
                Message = "Validation failed. Creation of Property canceled.",
                Status = 1,
            };
        }
        if (propertyItemdb == null)
        {
            Console.WriteLine("The Property could not be found. Update failed.");
            return new ResponseApi<PropertyItem>
            {
                Message = "The property could not be found. Update failed.",
                Status = 1,
            };
        }
            propertyItemdb.PublicIdentificationNumber = propertyItem.PublicIdentificationNumber;
            propertyItemdb.PropertyAddress = propertyItem.PropertyAddress;
            propertyItemdb.PropertyType = propertyItem.PropertyType;
            propertyItemdb.ConstructionYear = propertyItem.ConstructionYear;
            propertyItemdb.OwnerVAT = propertyItem.OwnerVAT;
            propertyItemdb.Owners = propertyItem.Owners;
            
        try
        {
            db.SaveChanges();
            Console.WriteLine("Property Updated!");
            return new ResponseApi<PropertyItem>
            {
                Message = "Property Updated!",
                Status = 0,
                Value = propertyItemdb
            };
        }
        catch (Exception)
        {
            Console.WriteLine("An error occured saving in the database.");
            return new ResponseApi<PropertyItem>
            {
                Message = "An error occured saving in the database.",
                Status = -1,
            };
        }
            
     
    }
    //Delete a propertyItem
    public bool DeletePropertyItem(int id)
    {
        PropertyItem? propertyItemdb = db.PropertyItems.Where(p => p.Id == id)
            .Include(p => p.Repairs)
            .FirstOrDefault();

        if (propertyItemdb == null)
        {
            Console.WriteLine("Property Could not be found. Deletion failed.");
            return false;
        }
 
        if (propertyItemdb.Repairs.Any(repair => repair.RepairStatus == RepairStatus.InProgress || repair.RepairStatus == RepairStatus.Pending))
        {
            Console.WriteLine("Properties could not be deleted because there is a Pending or InProgress repair.");
            return false;
        }
        
            try
            {
                db.PropertyItems.Remove(propertyItemdb);
                db.SaveChanges();
                Console.WriteLine("Property Deleted Succesfully!");
                return true;
            }
            catch (Exception) 
             {
                Console.WriteLine("An error occured.");
                return false;
             }
       
        
    }

}
