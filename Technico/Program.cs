
using System.Net;
using Technico.Interfaces;
using Technico.Models;
using Technico.Repositories;
using Technico.Services;

//Test

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
    Address = "New York 121",
    VAT = "4274355831",
    PhoneNumber = "122457896",
    UserType = UserType.Owner,
};


PropertyDbContext db = new PropertyDbContext();
IPropertyOwnerService ownerService = new PropertyOwnerService(db);

//Creating Users.
var userFirst = ownerService.Register(ownerFirst);

var userSecond = ownerService.Register(ownerSecond);

var userThird = ownerService.Register(ownerThird);

var userFourth = ownerService.Register(ownerFourth);

var userFifth = ownerService.Register(ownerFifth);

var userSixth = ownerService.Register(ownerSixth);


//Updating a User

var updatedUser = new PropertyOwner
{
    Name = "Phoebe",
    Surname = "Buffay",
    Password = "715254578",
    Email = "phoebe@gmail.com",
    Address = "Los Angeles 14",
    VAT = "4274355831",
    PhoneNumber = "122457896",
    UserType = UserType.Owner,
};

ownerService.UpdatePropertyOwner(updatedUser, 6);

//Deleting a user
//A user cant be deleted if he has properties registered.
ownerService.DeletePropertyOwner(1);

//Displaying all the information of an owner.
var ownerResponse = ownerService.DisplayDetails(4);

if (ownerResponse.Status == 0)
{
    var ownerToDisplay = ownerResponse.Value;

    Console.WriteLine("---------------------------Owner----------------------");
    Console.WriteLine($"Owner Details: ID: {ownerToDisplay.Id}, Name: {ownerToDisplay.Name}, Surname: {ownerToDisplay.Surname}, VAT: {ownerToDisplay.VAT}, Address: {ownerToDisplay.Address}, Email: {ownerToDisplay.Email}");

    if (ownerToDisplay.Properties != null && ownerToDisplay.Properties.Any())
    {
        foreach (var property1 in ownerToDisplay.Properties)
        {
            Console.WriteLine("---------------------------Property----------------------");
            Console.WriteLine($"Property ID: {property1.Id}, Address: {property1.PropertyAddress}, Year: {property1.ConstructionYear}, Type: {property1.PropertyType}");

            if (property1.Repairs != null && property1.Repairs.Any())
            {
                foreach (var repair in property1.Repairs)
                {
                    Console.WriteLine("---------------------------Repair-------------------------");
                    Console.WriteLine($"Repair ID: {repair.Id}, Description: {repair.RepairDescription}, Date: {repair.ScheduledRepair}, Cost: {repair.RepairPrice}");
                }
            }
        }
    }
    else
    {
        Console.WriteLine("No properties found for this owner.");
    }
}
else
{
    Console.WriteLine($"Error: {ownerResponse.Message}");
}


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

//Creation of the Properties.
var item = propertyItemService.CreatePropertyItem(property, [ownerFirst.Id]);
//A property can have multiple owners.
var itemFirst = propertyItemService.CreatePropertyItem(propertyFirst, [ownerFirst.Id, ownerSecond.Id]);

var itemSecond = propertyItemService.CreatePropertyItem(propertySecond, [ownerThird.Id]);

var itemThird = propertyItemService.CreatePropertyItem(propertyThird, [ownerFourth.Id, ownerFifth.Id]);

var itemFourth = propertyItemService.CreatePropertyItem(propertyFourth, [ownerSixth.Id]);



//Deleting a property
//A property cant be deleted if the status is on Pending or InProggress
propertyItemService.DeletePropertyItem(5);

//Updating property details
var updatedProperty = new PropertyItem
{
    PublicIdentificationNumber = "2659782452",
    PropertyAddress = "New York 452",
    PropertyType = PropertyType.ApartmentBuilding,
    ConstructionYear = 2005,
    OwnerVAT = "5234614452",
};

propertyItemService.UpdatePropertyItem(updatedProperty, 5);

//Displaying Property details with the owners if a property have multiple owners.
var propertyResponse = propertyItemService.ViewPropertyItem(2);

if (propertyResponse.Status == 0)
{
    var propertyToDisplay = propertyResponse.Value;

    if (propertyToDisplay != null)
    {
        if (propertyToDisplay.Owners != null && propertyToDisplay.Owners.Count > 0)
        {
            foreach (var owner in propertyToDisplay.Owners)
            {
                if (owner != null)
                {
                    Console.WriteLine($"Owner Details: ID: {owner.Id}, Name: {owner.Name}, Surname: {owner.Surname}, VAT: {owner.VAT}, Address: {owner.Address}, Email: {owner.Email}");
                }
            }
        }
        else
        {
            Console.WriteLine("No owners found for this property.");
        }

        // Display Property Details
        Console.WriteLine($"Property ID: {propertyToDisplay.Id}, Address: {propertyToDisplay.PropertyAddress}, Year: {propertyToDisplay.ConstructionYear}, Type: {propertyToDisplay.PropertyType}");

        if (propertyToDisplay.Repairs != null && propertyToDisplay.Repairs.Count > 0)
        {
            foreach (var repair in propertyToDisplay.Repairs)
            {
                if (repair != null)
                {
                    Console.WriteLine($"Repair ID: {repair.Id}, Description: {repair.RepairDescription}, Date: {repair.ScheduledRepair}, Cost: {repair.RepairPrice}");
                }
            }
        }
        else
        {
            Console.WriteLine("No repairs found for this property.");
        }
    }
    else
    {
        Console.WriteLine("Property details not found.");
    }
}
else
{

    Console.WriteLine($"Error retrieving property: Status code {propertyResponse.Status}");
}

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

//Creating Repair Details
var repairFirst = propertyRepairService.CreatePropertyRepair(repairDetails, 1);

var repairSecond = propertyRepairService.CreatePropertyRepair(repairDetailsSecond, 2);

var repairThird = propertyRepairService.CreatePropertyRepair(repairDetailsThird, 3);

var repairFourth = propertyRepairService.CreatePropertyRepair(repairDetailsFourth, 4);

var repairFifth = propertyRepairService.CreatePropertyRepair(repairDetailsFifth, 5);

//Searching a repair detail
var repairs = propertyRepairService.SearchPropertyRepair(repairDetails.ScheduledRepair);

   if (repairs.Count == 0)
   {
    Console.WriteLine("No repairs found for this date.");
   }
   Console.Write($"{repairs.Count} found in {repairDetails.ScheduledRepair}\n");
   foreach (var repair in repairs)
   {
    Console.WriteLine($"Repair ID: {repair.Id}, Description: {repair.RepairDescription}, Date: {repair.ScheduledRepair}, Cost: {repair.RepairPrice}");

   }

//Updating a repair detail
var updatedRepairDetails = new PropertyRepair
{
    RepairDescription = "Broken Frames",
    RepairAddress = "New York 90",
    ScheduledRepair = DateTime.Now,
    RepairType = RepairType.Frames,
    RepairPrice = 6500m,
    RepairStatus = RepairStatus.Complete,
};

propertyRepairService.UpdatePropertyRepair(updatedRepairDetails, 4);

//Deleting Repair Details if its status is Complete
propertyRepairService.DeletePropertyRepair(4);
