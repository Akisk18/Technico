//Test
using Technico.Models;
using Technico.Repositories;
using Technico.Services;


var owner2 = new PropertyOwner
{
 
    Name = "Akis",
    Surname = "Kiagias",
    Password = "123456",
    Email = "akis3@gmail.com",
    Address = "Stavromenos",
    VAT = "5433645",
    PhoneNumber = "1234567890",
    UserType = Technico.Enums.UserType.Owner,
};

PropertyDbContext db = new PropertyDbContext();
PropertyOwnerService ownerService = new PropertyOwnerService(db);

var existingUser  = ownerService.Register(owner2);
ownerService.DisplayDetails(existingUser.Id);



//ownerService.DeletePropertyOwner(4);

//ownerService.UpdatePropertyOwner(owner);

var property = new PropertyItem
{
    PublicIdentificationNumber = "898978",
    PropertyAddress = "Rethumno",
    PropertyType = Technico.Enums.PropertyType.ApartmentBuilding,
    ConstructionYear = 2005,
    OwnerVAT = "4865",
    PropertyOwnerId = owner2.Id,
    Owner = owner2,
};

PropertyItemService propertyItemService = new PropertyItemService(db);

//propertyItemService.ViewPropertyItem(6);

//propertyItemService.CreatePropertyItem(property, owner.Id);

//propertyItemService.DeletePropertyItem(owner.Id);

//propertyItemService.UpdatePropertyItem(property);

var repairDetails = new PropertyRepair
{
    RepairDescription = "Changins frames",
    RepairAddress = "Stavromenos",
    ScheduledRepair = DateTime.Now,
    RepairType = Technico.Enums.RepairType.Painting,
    RepairPrice = 5000m,
    RepairStatus = Technico.Enums.RepairStatus.InProgress,
    PropertyItemId = property.Id,
    Property = property
};

PropertyRepairService propertyRepairService = new PropertyRepairService(db);

//propertyRepairService.CreatePropertyRepair(repairDetails, property.Id);

//propertyRepairService.UpdatePropertyRepair(repairDetails);

//propertyRepairService.DeletePropertyRepair(3);
