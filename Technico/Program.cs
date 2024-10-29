//Test
using Technico.Interfaces;
using Technico.Models;
using Technico.Repositories;
using Technico.Services;


var owner3 = new PropertyOwner
{
 
    Name = "Kostas",
    Surname = "Kiagias",
    Password = "54654",
    Email = "kostas@gmail.com",
    Address = "Phgh",
    VAT = "434343",
    PhoneNumber = "4565545",
    UserType = Technico.Enums.UserType.Owner,
};

var owner4 = new PropertyOwner
{

    Name = "Akis",
    Surname = "Kiagias",
    Password = "54654",
    Email = "akis@gmail.com",
    Address = "Phgh",
    VAT = "121212",
    PhoneNumber = "5464",
    UserType = Technico.Enums.UserType.Owner,
};

PropertyDbContext db = new PropertyDbContext();
IPropertyOwnerService ownerService = new PropertyOwnerService(db);

//var existingUser = ownerService.Register(owner3);
//var user2 = ownerService.Register(owner4);
ownerService.DisplayDetails(3);




//ownerService.DeletePropertyOwner(3);

//ownerService.UpdatePropertyOwner(owner,1);

var property = new PropertyItem
{
    PublicIdentificationNumber = "898978",
    PropertyAddress = "Stavromenos 1",
    PropertyType = Technico.Enums.PropertyType.ApartmentBuilding,
    ConstructionYear = 1995,
    OwnerVAT = "565461",
    PropertyOwnerIds = [owner3.Id , owner4.Id],
    Owners = [owner3,owner4],
};

IPropertyItemService propertyItemService = new PropertyItemService(db);

//propertyItemService.ViewPropertyItem(2);

//propertyItemService.CreatePropertyItem(property, [3,4]);

//propertyItemService.DeletePropertyItem(owner.Id);

//propertyItemService.UpdatePropertyItem(property, 1);

var repairDetails = new PropertyRepair
{
    RepairDescription = "Broken pipes",
    RepairAddress = "Heraklion 2",
    ScheduledRepair = DateTime.Now,
    RepairType = Technico.Enums.RepairType.Plumbing,
    RepairPrice = 1500m,
    RepairStatus = Technico.Enums.RepairStatus.InProgress,
    PropertyItemId = property.Id,
    Property = property
};

IPropertyRepairService propertyRepairService = new PropertyRepairService(db);

//propertyRepairService.SearchPropertyRepair(repairDetails.ScheduledRepair);
//propertyRepairService.CreatePropertyRepair(repairDetails, 2);

//propertyRepairService.UpdatePropertyRepair(repairDetails, 5);

//propertyRepairService.DeletePropertyRepair(3);
