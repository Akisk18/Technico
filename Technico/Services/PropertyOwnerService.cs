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

public class PropertyOwnerService : IPropertyOwnerService
{
    private readonly PropertyDbContext db;

    public PropertyOwnerService(PropertyDbContext db)
    {
        this.db = db;
    }

    //Register an owner
    public ResponseApi<PropertyOwner> Register(PropertyOwner propertyOwner)
    {
        var existingUser = db.PropertyOwners.FirstOrDefault(x => x.VAT == propertyOwner.VAT);

        if (!ValidateOwner(propertyOwner))
        {
            Console.WriteLine("Validation failed. Register canceled.");
            return new ResponseApi<PropertyOwner>
            {
                Message = "Validation failed. Register canceled",
                Status = 1
            };
        }
        if (existingUser != null)
        {
            Console.WriteLine("User already exists.");
            return new ResponseApi<PropertyOwner>
            {
                Message = "User already exists.",
                Status = 2
            };
        }
        try
        {
            db.PropertyOwners.Add(propertyOwner);
            db.SaveChanges();
            Console.WriteLine("User registered succesfully!");
            return new ResponseApi<PropertyOwner>
            {
                Message = "User registered succesfully!",
                Status = 0,
                Value = propertyOwner
            };
        }
        catch (Exception)
        {
            Console.WriteLine("An error occured");
            return new ResponseApi<PropertyOwner>
            {
                Message = "An error occured.",
                Status = -1
            };
        }
          
        
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
        if (propertyOwner.Password == null || propertyOwner.Password.Length <8)
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
        Console.WriteLine("---------------------------Owner----------------------");
        Console.WriteLine($"Owner Details : ID: { propertyOwner.Id}, Name: { propertyOwner.Name} Surname: { propertyOwner.Surname}, VAT: { propertyOwner.VAT}, Address: { propertyOwner.Address}, Email: { propertyOwner.Email}");

        if (propertyOwner.Properties != null) 
        {
            
            foreach (var properties in propertyOwner.Properties)
            {
                Console.WriteLine("---------------------------Property----------------------");
                Console.WriteLine($"Property ID: {properties.Id}, Address: {properties.PropertyAddress}, Year: {properties.ConstructionYear}, Type: {properties.PropertyType}");
                
                if (properties.Repairs != null)
                {
                   
                    foreach (var repairs in properties.Repairs)
                    {
                        Console.WriteLine("---------------------------Repair-------------------------");
                        Console.WriteLine($"Repair ID: {repairs.Id}, Description: {repairs.RepairDescription}, Date: {repairs.ScheduledRepair}, Cost: {repairs.RepairPrice}");
                    }
                }
            }

        }
    }

    //Update Owner Details
    public ResponseApi<PropertyOwner> UpdatePropertyOwner(PropertyOwner propertyOwner , int id)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.FirstOrDefault(p => p.Id == id);

        if (!ValidateOwner(propertyOwner))
        {
            Console.WriteLine("Validation failed. Update canceled.");
            return new ResponseApi<PropertyOwner>
            {
                Message="Validation failed. Update canceled",
                Status=1
            };
        }
        if (propertyOwnerdb == null)
        {
            Console.WriteLine("No owner found. Owner Update failed.");
            return new ResponseApi<PropertyOwner>
            {
                Message = "No owner found. Owner update failed",
                Status = 1
            };
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
                return new ResponseApi<PropertyOwner>
                {
                    Message = "Owner Details updated succesfully",
                    Status = 0,
                    Value = propertyOwnerdb
                };
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured while saving in the database.");
                return new ResponseApi<PropertyOwner>
                {
                    Message="An error occured while saving in the database.",
                    Status=-1
                };
            }
            
           
    }
    //Delete Owner
    public bool DeletePropertyOwner(int id)
    {
        PropertyOwner? propertyOwnerdb = db.PropertyOwners.Where(p => p.Id == id)
           .Include(p => p.Properties)
           .FirstOrDefault();
        if (propertyOwnerdb == null)
        {
            Console.WriteLine("Owner could not be found. Deletion failed.");
            return false;
        }
        if(propertyOwnerdb.Properties.Count>0)
        {
            Console.WriteLine("Owner cannot be deleted because registered properties exist.");
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
                return false;
            }       
        
    }
}
