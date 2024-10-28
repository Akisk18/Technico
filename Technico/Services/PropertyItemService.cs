using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Repositories;

namespace Technico.Services;

public class PropertyItemService
{
    private PropertyDbContext db;

    public PropertyItemService(PropertyDbContext db)
    {
        this.db = db;
    }

    //Displays all details of the property
    public PropertyItem? ViewPropertyItem(int id)
    {
       var propertyItem = db.PropertyItems.Where(p => p.Id == id)
            .Include(p => p.Repairs)
            .FirstOrDefault();

        if (propertyItem == null)
        {
            Console.WriteLine("Property not found.");
            return null;
        }
            Console.WriteLine(propertyItem);
        if (propertyItem != null)
        {
            foreach (var repairs in propertyItem.Repairs)
            {
                Console.WriteLine(repairs);
            }
        }
        return propertyItem;
    }
    //Creates a new propertyItem
    public PropertyItem CreatePropertyItem(PropertyItem propertyItem , int ownerId)
    {
        var owner = db.PropertyOwners.FirstOrDefault(p => p.Id == ownerId);
        propertyItem.Owner = owner;
        propertyItem.PropertyOwnerId = ownerId;
        try
        {
            db.PropertyItems.Add(propertyItem);
            db.SaveChanges();
            Console.Write("Property Created Succesfully!");
        }
        catch (Exception) 
        {
            Console.WriteLine("An error occured.");
        }
        return propertyItem;
    }
    //Update a propertyItem
    public PropertyItem? UpdatePropertyItem(PropertyItem propertyItem) 
    {
        PropertyItem? propertyItemdb = db.PropertyItems.FirstOrDefault(p => p.Id == propertyItem.Id);
        if (propertyItemdb != null)
        {
            
            propertyItemdb.PublicIdentificationNumber = propertyItem.PublicIdentificationNumber;
            propertyItemdb.PropertyAddress = propertyItem.PropertyAddress;
            propertyItemdb.PropertyType = propertyItem.PropertyType;
            propertyItemdb.ConstructionYear = propertyItem.ConstructionYear;
            propertyItemdb.OwnerVAT = propertyItem.OwnerVAT;
            propertyItemdb.Owner = propertyItem.Owner;
            try 
            {
                db.SaveChanges();
                Console.WriteLine("Property Updated!");
            }
            catch (Exception) 
            {
                Console.WriteLine("An error occured saving in the database.");
            }
            return propertyItemdb;
        }
        Console.WriteLine("The property could not be found.");
        return null;
    }
    //Delete a propertyItem
    public bool DeletePropertyItem(int id)
    {
        PropertyItem? propertyItemdb = db.PropertyItems.FirstOrDefault(p => p.Id==id);
        if(propertyItemdb != null)
        {
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
             }
        }
        Console.WriteLine("Property Could not be found.");
       return false;
    }

}
