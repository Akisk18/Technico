using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;
using Technico.Repositories;

namespace Technico.Services;

public class PropertyOwnerService
{
    private PropertyDbContext db;

    public PropertyOwnerService(PropertyDbContext db)
    {
        this.db = db;
    }
    //Display all details
    public void DisplayDetails(int id)
    {
        var propertyOwner = db.PropertyOwners.Where(p => p.Id == id)
            .Include(p => p.Properties)
            .ThenInclude(p => p.Repairs)
            .FirstOrDefault();
        if (propertyOwner == null)
        {
            Console.WriteLine("Owner not Found");
            return;
        }
        Console.WriteLine(propertyOwner.ToString());
        if (propertyOwner.Properties.Count > 0)
        {
            foreach (var properties in propertyOwner.Properties)
            {
                Console.WriteLine($"{properties}");
                if (properties.Repairs.Count > 0)
                {
                    foreach (var repairs in properties.Repairs)
                    {
                        Console.WriteLine($"{repairs}");
                    }
                }
            }

        }
    }

    //Update Owner Details
    public PropertyOwner UpdatePropertyOwner(PropertyOwner propertyOwner)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == propertyOwner.Id);
        if (propertyOwnerdb == null)
        {
            propertyOwnerdb.Name = propertyOwner.Name;
            propertyOwnerdb.Surname = propertyOwner.Surname;
            propertyOwnerdb.VAT = propertyOwner.VAT;
            propertyOwnerdb.Address = propertyOwner.Address;
            propertyOwnerdb.Email = propertyOwner.Email;
            propertyOwnerdb.PhoneNumber = propertyOwner.PhoneNumber;
            propertyOwnerdb.Password = propertyOwner.Password;
            propertyOwnerdb.UserType = propertyOwner.UserType;
        }
        return propertyOwnerdb;
    }
    //Delete Owner
    public bool DeletePropertyOwner(int id)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == id);
        if (propertyOwnerdb != null)
        {
            db.PropertyOwners.Remove(propertyOwnerdb);
            db.SaveChanges();
            return true;
        }
        return false;
    }
}
