using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Enums;

namespace Technico.Models;

public class PropertyOwner
{
    public int Id { get; set; }
    public string VAT { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname {  get; set; } = string.Empty;
    public string Address {  get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public List<PropertyItem> Properties { get; set; } = [];

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name} Surname: {Surname}, VAT: {VAT}, Address: {Address}, Email: {Email}";
    }
}
