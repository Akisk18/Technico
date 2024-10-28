﻿using Microsoft.EntityFrameworkCore;
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
        var existingUser = db.PropertyOwners.FirstOrDefault(x => x.VAT == propertyOwner.VAT);
        if (existingUser != null)
        {
            Console.WriteLine("User already exists.");
            return existingUser;
        }
        try
        {
            db.PropertyOwners.Add(propertyOwner);
            db.SaveChanges();
            Console.WriteLine("User registered succesfully!");
        }
        catch (Exception)
        {
            Console.WriteLine("An error occured");
        }
            return propertyOwner;
        
    }

    public bool ValidateOwner(PropertyOwner propertyOwner)
    {
        bool isValid = true;
        if (propertyOwner.Name == null)
        {
            isValid = false;
            Console.WriteLine("Name is required.");
        }
        if (propertyOwner.Surname == null)
        {
            isValid = false;
            Console.WriteLine("Surname is required.");
        }
        if (propertyOwner.Address == null)
        {
            isValid = false;
            Console.WriteLine("Address is required.");
        }
        if (propertyOwner.VAT == null || propertyOwner.VAT.Length != 10)
        {
            isValid = false;
            Console.Write("VAT must be 10 characters");
        }
        if (propertyOwner.Password == null || propertyOwner.Password.Length !=8)
        {
            isValid = false;
            Console.WriteLine("Password must have at least 8 numbers/characters.");
        }
        if (!propertyOwner.Email.Contains("@"))
        {
            isValid = false;
            Console.WriteLine("Please enter a valid email.");
        }
        return isValid;
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
    public PropertyOwner? UpdatePropertyOwner(PropertyOwner propertyOwner , int id)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == id);
        if (propertyOwnerdb == null)
        {
            Console.WriteLine("No owner found. Owner Update failed.");
            return null;
        }
            propertyOwnerdb.Name = propertyOwner.Name;
            propertyOwnerdb.Surname = propertyOwner.Surname;
            propertyOwnerdb.VAT = propertyOwner.VAT;
            propertyOwnerdb.Address = propertyOwner.Address;
            propertyOwnerdb.Email = propertyOwner.Email;
            propertyOwnerdb.PhoneNumber = propertyOwner.PhoneNumber;
            propertyOwnerdb.Password = propertyOwner.Password;
            propertyOwnerdb.UserType = propertyOwner.UserType;
            try
            {
                db.SaveChanges();
                Console.WriteLine("Owner Details updated succesfully!");
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured saving in the database.");
            }
            
            return propertyOwnerdb;
    }
    //Delete Owner
    public bool DeletePropertyOwner(int id)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == id);
        if (propertyOwnerdb == null)
        {
            Console.WriteLine("Owner could not be found. Deletion failed.");
            return false;
        }
            try
            {
                db.PropertyOwners.Remove(propertyOwnerdb);
                db.SaveChanges();
                Console.WriteLine("Owner deleted succesfully!");
                return true;
            }
            catch (Exception) 
            {
                Console.WriteLine("An error occured.");
            }       
        return false;
    }
}
