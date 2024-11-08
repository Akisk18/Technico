﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technico.Models;

public class PropertyItem
{
    public int Id { get; set; }
    public string PublicIdentificationNumber { get; set; } = string.Empty;
    public string PropertyAddress { get; set; } = string.Empty;
    public int ConstructionYear { get; set; }
    public PropertyType PropertyType { get; set; }
    public string OwnerVAT { get; set; } = string.Empty;
    //Foreign Key
    public List<int>? PropertyOwnerIds { get; set; }
    public List<PropertyRepair> Repairs { get; set; } = [];
    public List<PropertyOwner> Owners { get; set; } = [];
}
