using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public partial class ResistancesModel : IRealmObject
{
    [PrimaryKey] [Indexed]
    public string _id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public ResistancesModel() {}

    public ResistancesModel(string id, string name, string desc, int price) {
        _id = id;
        Name = name;
        Description = desc;
        Price = price;
    }
}
