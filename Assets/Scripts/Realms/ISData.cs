using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class ISData
{
    private RealmController rC;

    public List<ResistancesModel> InitializeData() {
        List<ResistancesModel> resList = new List<ResistancesModel>();
    
        resList.AddRange(new List<ResistancesModel> {
            new ResistancesModel("b1", "Bacteria Resistance Lv.1", "Resistance to Bacteria type enemies +1", 3),
            new ResistancesModel("b2", "Bacteria Resistance Lv.2", "Resistance to Bacteria type enemies +2", 6),
            new ResistancesModel("b3", "Bacteria Resistance Lv.3", "Resistance to Bacteria type enemies +3", 9),
            new ResistancesModel("b4", "Bacteria Resistance Lv.4", "Resistance to Bacteria type enemies +4", 12),
            new ResistancesModel("b5", "Bacteria Resistance Lv.5", "Resistance to Bacteria type enemies +5", 15),
            new ResistancesModel("b6", "Bacteria Resistance Lv.6", "Resistance to Bacteria type enemies +6", 18),
            new ResistancesModel("b7", "Bacteria Resistance Lv.7", "Resistance to Bacteria type enemies +7", 21),
            new ResistancesModel("b8", "Bacteria Resistance Lv.8", "Resistance to Bacteria type enemies +8", 24),
            new ResistancesModel("b9", "Bacteria Resistance Lv.9", "Resistance to Bacteria type enemies +9", 27),
            new ResistancesModel("b10", "Bacteria Resistance Lv.10", "Resistance to Bacteria type enemies +10", 30),
        });

        resList.AddRange(new List<ResistancesModel> {
            new ResistancesModel("p1", "Parasite Resistance Lv.1", "Resistance to Parasite type enemies +1", 3),
            new ResistancesModel("p2", "Parasite Resistance Lv.2", "Resistance to Parasite type enemies +2", 6),
            new ResistancesModel("p3", "Parasite Resistance Lv.3", "Resistance to Parasite type enemies +3", 9),
            new ResistancesModel("p4", "Parasite Resistance Lv.4", "Resistance to Parasite type enemies +4", 12),
            new ResistancesModel("p5", "Parasite Resistance Lv.5", "Resistance to Parasite type enemies +5", 15),
            new ResistancesModel("p6", "Parasite Resistance Lv.6", "Resistance to Parasite type enemies +6", 18),
            new ResistancesModel("p7", "Parasite Resistance Lv.7", "Resistance to Parasite type enemies +7", 21),
            new ResistancesModel("p8", "Parasite Resistance Lv.8", "Resistance to Parasite type enemies +8", 24),
            new ResistancesModel("p9", "Parasite Resistance Lv.9", "Resistance to Parasite type enemies +9", 27),
            new ResistancesModel("p10", "Parasite Resistance Lv.10", "Resistance to Parasite type enemies +10", 30),
        });

        resList.AddRange(new List<ResistancesModel> {
            new ResistancesModel("v1", "Virus Resistance Lv.1", "Resistance to Virus type enemies +1", 3),
            new ResistancesModel("v2", "Virus Resistance Lv.2", "Resistance to Virus type enemies +2", 6),
            new ResistancesModel("v3", "Virus Resistance Lv.3", "Resistance to Virus type enemies +3", 9),
            new ResistancesModel("v4", "Virus Resistance Lv.4", "Resistance to Virus type enemies +4", 12),
            new ResistancesModel("v5", "Virus Resistance Lv.5", "Resistance to Virus type enemies +5", 15),
            new ResistancesModel("v6", "Virus Resistance Lv.6", "Resistance to Virus type enemies +6", 18),
            new ResistancesModel("v7", "Virus Resistance Lv.7", "Resistance to Virus type enemies +7", 21),
            new ResistancesModel("v8", "Virus Resistance Lv.8", "Resistance to Virus type enemies +8", 24),
            new ResistancesModel("v9", "Virus Resistance Lv.9", "Resistance to Virus type enemies +9", 27),
            new ResistancesModel("v10", "Virus Resistance Lv.10", "Resistance to Virus type enemies +10", 30),
        });

        return resList;
    }
}
