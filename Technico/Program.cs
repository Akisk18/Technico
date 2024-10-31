//Test
using System.Net;
using Technico.Interfaces;
using Technico.Models;
using Technico.Repositories;
using Technico.Services;


var ownerFirst = new PropertyOwner
{
    Name = "Joey",
    Surname = "Tribbiani",
    Password = "123456789",
    Email = "joey@gmail.com",
    Address = "New York 14",
    VAT = "1234567895",
    PhoneNumber = "4565545",
    UserType = UserType.Owner,
};

var ownerSecond = new PropertyOwner
{
    Name = "Chandler",
    Surname = "Bing",
    Password = "123456789",
    Email = "chandler@gmail.com",
    Address = "New York 13",
    VAT = "9876543210",
    PhoneNumber = "12349524",
    UserType = UserType.Owner,
};

var ownerThird = new PropertyOwner
{
    Name = "Ross",
    Surname = "Geller",
    Password = "741852963",
    Email = "ross@gmail.com",
    Address = "New York 14",
    VAT = "4567891230",
    PhoneNumber = "542159518",
    UserType = UserType.Owner,
};

var ownerFourth = new PropertyOwner
{
    Name = "Monica",
    Surname = "Geller",
    Password = "721252963",
    Email = "monica@gmail.com",
    Address = "New York 15",
    VAT = "4564397220",
    PhoneNumber = "542172628",
    UserType = UserType.Owner,
};

var ownerFifth = new PropertyOwner
{
    Name = "Rachel",
    Surname = "Green",
    Password = "768254563",
    Email = "rachel@gmail.com",
    Address = "New York 16",
    VAT = "4324356221",
    PhoneNumber = "122198628",
    UserType = UserType.Owner,
};

var ownerSixth = new PropertyOwner
{
    Name = "Phoebe",
    Surname = "Buffay",
    Password = "715254578",
    Email = "phoebe@gmail.com",
    Address = "New York 119",
    VAT = "4274355831",
    PhoneNumber = "122457896",
    UserType = UserType.Owner,
};


PropertyDbContext db = new PropertyDbContext();
IPropertyOwnerService ownerService = new PropertyOwnerService(db);

//var userFirst = ownerService.Register(ownerSixth);
//ownerService.UpdatePropertyOwner(ownerSixth,3);

//var userSecond = ownerService.Register(ownerSecond);

//var userThird = ownerService.Register(ownerThird);

//var userFourth = ownerService.Register(ownerFourth);

//var userFifth = ownerService.Register(ownerFifth);

//var userSixth = ownerService.Register(ownerSixth);



ownerService.DisplayDetails(3);




//ownerService.DeletePropertyOwner(1);



var property = new PropertyItem
{
    PublicIdentificationNumber = "326771256",
    PropertyAddress = "New York 23",
    PropertyType = PropertyType.Maisonet,
    ConstructionYear = 2010,
    OwnerVAT = "5632635534",
    PropertyOwnerIds = [ownerFirst.Id],
    Owners = [ownerFirst],
};

var propertyFirst = new PropertyItem
{
    PublicIdentificationNumber = "898978456",
    PropertyAddress = "New York 25",
    PropertyType = PropertyType.ApartmentBuilding,
    ConstructionYear = 1995,
    OwnerVAT = "5654615698",
    PropertyOwnerIds = [ownerFirst.Id , ownerSecond.Id],
    Owners = [ownerFirst,ownerSecond],
};

var propertySecond = new PropertyItem
{
    PublicIdentificationNumber = "89897843579",
    PropertyAddress = "New York 64",
    PropertyType = PropertyType.Maisonet,
    ConstructionYear = 2000,
    OwnerVAT = "5654611278",
    PropertyOwnerIds = [ownerThird.Id],
    Owners = [ownerThird],
};

var propertyThird = new PropertyItem
{
    PublicIdentificationNumber = "8989782528",
    PropertyAddress = "New York 78",
    PropertyType = PropertyType.ApartmentBuilding,
    ConstructionYear = 1998,
    OwnerVAT = "5654614896",
    PropertyOwnerIds = [ownerFourth.Id, ownerFifth.Id],
    Owners = [ownerFourth, ownerFifth],
};

var propertyFourth = new PropertyItem
{
    PublicIdentificationNumber = "2659782452",
    PropertyAddress = "New York 178",
    PropertyType = PropertyType.ApartmentBuilding,
    ConstructionYear = 1998,
    OwnerVAT = "5234614452",
    PropertyOwnerIds = [ownerSixth.Id],
    Owners = [ownerSixth],
};


IPropertyItemService propertyItemService = new PropertyItemService(db);

//propertyItemService.ViewPropertyItem(2);

//var item = propertyItemService.CreatePropertyItem(property, [ownerFirst.Id]);

//var itemFirst = propertyItemService.CreatePropertyItem(propertyFirst, [ownerFirst.Id, ownerSecond.Id]);

//var itemSecond = propertyItemService.CreatePropertyItem(propertySecond, [ownerThird.Id]);

//var itemThird = propertyItemService.CreatePropertyItem(propertyThird, [ownerFourth.Id, ownerFifth.Id]);

//var itemFourth = propertyItemService.CreatePropertyItem(propertyFourth, [ownerSixth.Id]);

//propertyItemService.DeletePropertyItem(5);

//propertyItemService.UpdatePropertyItem(property, 1);

var repairDetails = new PropertyRepair
{
    RepairDescription = "Broken pipes",
    RepairAddress = "New York 45",
    ScheduledRepair = DateTime.Now,
    RepairType = RepairType.Plumbing,
    RepairPrice = 4500m,
    RepairStatus = RepairStatus.Complete,
    PropertyItemId = propertyFirst.Id,
    Property = propertyFirst
};

var repairDetailsSecond = new PropertyRepair
{
    RepairDescription = "No Power",
    RepairAddress = "New York 12",
    ScheduledRepair = DateTime.Now,
    RepairType = RepairType.ElectricalWork,
    RepairPrice = 1000m,
    RepairStatus = RepairStatus.InProgress,
    PropertyItemId = propertySecond.Id,
    Property = propertySecond
};

var repairDetailsThird = new PropertyRepair
{
    RepairDescription = "Wall Painting",
    RepairAddress = "New York 89",
    ScheduledRepair = DateTime.Now,
    RepairType = RepairType.Painting,
    RepairPrice = 2500m,
    RepairStatus = RepairStatus.Complete,
    PropertyItemId = propertyThird.Id,
    Property = propertyThird
};

var repairDetailsFourth = new PropertyRepair
{
    RepairDescription = "Broken Frames",
    RepairAddress = "New York 78",
    ScheduledRepair = DateTime.Now,
    RepairType = RepairType.Frames,
    RepairPrice = 4500m,
    RepairStatus = RepairStatus.Complete,
    PropertyItemId = propertyFourth.Id,
    Property = propertyFourth
};

var repairDetailsFifth = new PropertyRepair
{
    RepairDescription = "Insulation",
    RepairAddress = "New York 78",
    ScheduledRepair = DateTime.Now,
    RepairType = RepairType.Insulation,
    RepairPrice = 4500m,
    RepairStatus = RepairStatus.InProgress,
    PropertyItemId = propertyFourth.Id,
    Property = propertyFourth
};

IPropertyRepairService propertyRepairService = new PropertyRepairService(db);

//propertyRepairService.SearchPropertyRepair(repairDetails.ScheduledRepair);
//var repairFirst = propertyRepairService.CreatePropertyRepair(repairDetails, propertyFirst.Id);

//var repairSecond = propertyRepairService.CreatePropertyRepair(repairDetailsSecond, propertySecond.Id);

//var repairThird = propertyRepairService.CreatePropertyRepair(repairDetailsThird, propertyThird.Id);

//var repairFourth = propertyRepairService.CreatePropertyRepair(repairDetailsFourth, propertyFourth.Id);

//var repairFifth = propertyRepairService.CreatePropertyRepair(repairDetailsFifth, propertyFourth.Id);

//propertyRepairService.UpdatePropertyRepair(repairDetailsFourth, 4);

//propertyRepairService.UpdatePropertyRepair(repairDetailsFifth, 5);

//propertyRepairService.DeletePropertyRepair(3);
