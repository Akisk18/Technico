//Test
using Technico.Models;
using Technico.Repositories;
using Technico.Services;


var owner = new PropertyOwner
{
 
    Name = "Akis",
    Surname = "Kiagias",
    Password = "54654",
    Email = "akis@gmail.com",
    Address = "Phgh",
    VAT = "544545",
    PhoneNumber = "484554",
    UserType = Technico.Enums.UserType.Owner,
};

PropertyDbContext db = new PropertyDbContext();
PropertyOwnerService ownerService = new PropertyOwnerService(db);

//var existingUser = ownerService.Register(owner);
//ownerService.DisplayDetails(1);



//ownerService.DeletePropertyOwner(3);

//ownerService.UpdatePropertyOwner(owner);

var property = new PropertyItem
{
    PublicIdentificationNumber = "898978",
    PropertyAddress = "Stavromenos",
    PropertyType = Technico.Enums.PropertyType.ApartmentBuilding,
    ConstructionYear = 1995,
    OwnerVAT = "565461",
    PropertyOwnerId = owner.Id,
    Owner = owner,
};

PropertyItemService propertyItemService = new PropertyItemService(db);

//propertyItemService.ViewPropertyItem(6);

//propertyItemService.CreatePropertyItem(property, 1);

//propertyItemService.DeletePropertyItem(owner.Id);

//propertyItemService.UpdatePropertyItem(property);

var repairDetails = new PropertyRepair
{
    RepairDescription = "Broken pipes",
    RepairAddress = "Heraklion",
    ScheduledRepair = DateTime.Now,
    RepairType = Technico.Enums.RepairType.Plumbing,
    RepairPrice = 1500m,
    RepairStatus = Technico.Enums.RepairStatus.InProgress,
    PropertyItemId = property.Id,
    Property = property
};

PropertyRepairService propertyRepairService = new PropertyRepairService(db);

//propertyRepairService.SearchPropertyRepair(repairDetails.ScheduledRepair);
//propertyRepairService.CreatePropertyRepair(repairDetails, 2);

//propertyRepairService.UpdatePropertyRepair(repairDetails);

//propertyRepairService.DeletePropertyRepair(3);
