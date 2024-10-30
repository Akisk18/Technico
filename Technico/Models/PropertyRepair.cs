using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Technico.Models;

public class PropertyRepair
{
    public int Id { get; set; }
    public DateTime ScheduledRepair { get; set; }
    public RepairType RepairType { get; set; }
    public string RepairDescription { get; set; } = string.Empty;
    public string RepairAddress { get; set; } = string.Empty;
    //Foreign Key
    public int PropertyItemId { get; set; }
    public RepairStatus RepairStatus { get; set; } = RepairStatus.Pending;
    [Precision(8, 2)]
    public decimal RepairPrice { get; set; }
    public PropertyItem? Property {  get; set; }
}
