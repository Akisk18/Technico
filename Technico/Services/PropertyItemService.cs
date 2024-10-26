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
        return db.PropertyItems.Where(p => p.Id == id).FirstOrDefault();
    }
    //Creates a new propertyItem
    public PropertyItem CreatePropertyItem(PropertyItem propertyItem)
    {
        db.PropertyItems.Add(propertyItem);
        db.SaveChanges();
        return propertyItem;
    }
    //Update a propertyItem
    public PropertyItem UpdatePropertyItem(PropertyItem propertyItem) 
    {
        PropertyItem? propertyItemdb = db.PropertyItems.FirstOrDefault(p => p.Id == propertyItem.Id);
        if (propertyItemdb != null)
        {
            propertyItemdb.PublicIdentificationNumber = propertyItem.PublicIdentificationNumber;
            propertyItemdb.PropertyAdrress = propertyItem.PropertyAdrress;
            propertyItemdb.PropertyType = propertyItem.PropertyType;
            propertyItemdb.ConstructionYear = propertyItem.ConstructionYear;
            propertyItemdb.OwnerVAT = propertyItem.OwnerVAT;
            propertyItemdb.Owner = propertyItem.Owner;
            db.SaveChanges();
        }
        return propertyItemdb;
    }
    //Delete a propertyItem
    public bool DeletePropertyItem(int id)
    {
        PropertyItem? propertyItemdb = db.PropertyItems.FirstOrDefault(p => p.Id==id);
        if(propertyItemdb != null)
        {
            db.PropertyItems.Remove(propertyItemdb);
            db.SaveChanges();
            return true;
        }
       return false;
    }

}
