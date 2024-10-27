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

    //Register an owner
    public PropertyOwner Register(PropertyOwner propertyOwner)
    {
        var existingUser = db.PropertyOwners.FirstOrDefault(x => x.Email == propertyOwner.Email || x.VAT == propertyOwner.VAT);
        if (existingUser != null)
        {
            Console.WriteLine("User already exists.");
            return existingUser;
        }
            db.PropertyOwners.Add(propertyOwner);
            db.SaveChanges();
            Console.WriteLine("User registered succesfully!");
            return propertyOwner;
        
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
        Console.WriteLine($"Owner Details : {propertyOwner}");
        if (propertyOwner.Properties != null) 
        {
            Console.WriteLine("\nProperties: ");
            foreach (var properties in propertyOwner.Properties)
            {
                Console.WriteLine(properties);
                if (properties.Repairs != null)
                {
                    foreach (var repairs in properties.Repairs)
                    {
                        Console.WriteLine(repairs);
                    }
                }
            }

        }
    }

    //Update Owner Details
    public PropertyOwner? UpdatePropertyOwner(PropertyOwner propertyOwner)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == propertyOwner.Id);
        if (propertyOwnerdb != null)
        {
            propertyOwnerdb.Name = propertyOwner.Name;
            propertyOwnerdb.Surname = propertyOwner.Surname;
            propertyOwnerdb.VAT = propertyOwner.VAT;
            propertyOwnerdb.Address = propertyOwner.Address;
            propertyOwnerdb.Email = propertyOwner.Email;
            propertyOwnerdb.PhoneNumber = propertyOwner.PhoneNumber;
            propertyOwnerdb.Password = propertyOwner.Password;
            propertyOwnerdb.UserType = propertyOwner.UserType;
            db.SaveChanges();
            Console.WriteLine("Owner Details updated succesfully!");
            return propertyOwnerdb;

        }
        
        Console.WriteLine("No owner found.");
        return null;

    }
    //Delete Owner
    public bool DeletePropertyOwner(int id)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == id);
        if (propertyOwnerdb != null)
        {
            db.PropertyOwners.Remove(propertyOwnerdb);
            db.SaveChanges();
            Console.WriteLine("Owner deleted succesfully!");
            return true;
        }
        Console.WriteLine("Owner could not be found");
        return false;
    }
}
