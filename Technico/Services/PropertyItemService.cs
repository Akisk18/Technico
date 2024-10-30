using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Interfaces;
using Technico.Models;
using Technico.Repositories;

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
    public PropertyItem? ViewPropertyItem(int id)
    {
       var propertyItem = db.PropertyItems.Where(p => p.Id == id)
            .Include(p => p.Repairs)
            .Include(p => p.Owners)
            .FirstOrDefault();

        if (propertyItem == null)
        {
            Console.WriteLine("Property not found.");
            return null;
        }
        if (propertyItem.Owners.Count > 1)
        {
            foreach (var owner in propertyItem.Owners)
            {
                Console.WriteLine($"Owner Details : ID: {owner.Id}, Name: {owner.Name} Surname: {owner.Surname}, VAT: {owner.VAT}, Address: {owner.Address}, Email: {owner.Email}");
                
            }
        }
        Console.WriteLine($"Property ID: {propertyItem.Id}, Address: {propertyItem.PropertyAddress}, Year: {propertyItem.ConstructionYear}, Type: {propertyItem.PropertyType}");
        foreach (var repairs in propertyItem.Repairs)
        {
            Console.WriteLine($"Repair ID: {repairs.Id}, Description: {repairs.RepairDescription}, Date: {repairs.ScheduledRepair}, Cost: {repairs.RepairPrice}");
        }
        return propertyItem;
    }
    //Creates a new propertyItem
    public PropertyItem CreatePropertyItem(PropertyItem propertyItem , List<int> ownerIds)
    {
        var owners = db.PropertyOwners.Where(p => ownerIds.Contains(p.Id)).ToList();

        if (!ValidateItem(propertyItem))
        {
            Console.WriteLine("Validation failed. Creation of Property canceled");
            return null;
        }

        if (owners == null) 
        {
            Console.WriteLine("Owner not found. Property creation failed.");
            return propertyItem;
        }
        propertyItem.Owners = owners;
        propertyItem.PropertyOwnerIds = ownerIds;
        try
        {
            db.PropertyItems.Add(propertyItem);
            db.SaveChanges();
            Console.Write("Property Created Succesfully!");
            return propertyItem;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            return null;
        }
       
    }
    //Update a propertyItem
    public PropertyItem? UpdatePropertyItem(PropertyItem propertyItem ,int id) 
    {
        PropertyItem? propertyItemdb = db.PropertyItems
            .Include(p => p.Owners)
            .FirstOrDefault(p => p.Id == id);

        if (!ValidateItem(propertyItem))
        {
            Console.WriteLine("Validation failed. Creation of Property canceled");
            return null;
        }
        if (propertyItemdb == null)
        {
            Console.WriteLine("The Property could not be found. Update failed.");
            return null;
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
                return propertyItemdb;
        }
            catch (Exception) 
            {
                Console.WriteLine("An error occured saving in the database.");
                return null;
            }
            
     
    }
    //Delete a propertyItem
    public bool DeletePropertyItem(int id)
    {
        PropertyItem? propertyItemdb = db.PropertyItems.FirstOrDefault(p => p.Id==id);
        if (propertyItemdb == null)
        {
            Console.WriteLine("Property Could not be found. Deletion failed.");
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
