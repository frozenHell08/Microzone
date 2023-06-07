using System.Collections;
using System.Collections.Generic;
using Realms;
using Realms.Schema;
using Realms.Weaving;

namespace ProfileCreation {
    #region
    public class Heals : EmbeddedObject {
        [MapTo("bandaid")]
        public int Bandaid { get; set; } = 0;
        
        [MapTo("injector")]
        public int Injector { get; set; } = 0;

        [MapTo("medicine")]
        public int Medicine { get; set; } = 0;
    }

    public class Resistances : EmbeddedObject {
        [MapTo("bacteriaRes")]
        public int BacteriaResist { get; set; } = 0;
        
        [MapTo("parasiteRes")]
        public int ParasiteResist { get; set; } = 0;

        [MapTo("virusRes")]
        public int VirusResist { get; set; } = 0;
    }

    public class Pills : EmbeddedObject {
        [MapTo("milaon_P")]
        public int Milaon_P { get; set; } = 0;
        
        [MapTo("cinaon_P")]
        public int Cinaon_P { get; set; } = 0;

        [MapTo("viraon_P")]
        public int Viraon_P { get; set; } = 0;

        [MapTo("mildha_P")]
        public int Mildha_P { get; set; } = 0;
        
        [MapTo("cinadha_P")]
        public int Cinadha_P { get; set; } = 0;

        [MapTo("virdha_P")]
        public int Virdha_P { get; set; } = 0;

        [MapTo("miltri_P")]
        public int Miltri_P { get; set; } = 0;
        
        [MapTo("cinatri_P")]
        public int Cinatri_P { get; set; } = 0;

        [MapTo("virtri_P")]
        public int Virtri_P { get; set; } = 0;
    }

    public class Solution : EmbeddedObject {
        [MapTo("milaon_S")]
        public int Milaon_S { get; set; } = 0;
        
        [MapTo("cinaon_S")]
        public int Cinaon_S { get; set; } = 0;

        [MapTo("viraon_S")]
        public int Viraon_S { get; set; } = 0;

        [MapTo("mildha_S")]
        public int Mildha_S { get; set; } = 0;
        
        [MapTo("cinadha_S")]
        public int Cinadha_S { get; set; } = 0;

        [MapTo("virdha_S")]
        public int Virdha_S { get; set; } = 0;

        [MapTo("miltri_S")]
        public int Miltri_S { get; set; } = 0;
        
        [MapTo("cinatri_S")]
        public int Cinatri_S { get; set; } = 0;

        [MapTo("virtri_S")]
        public int Virtri_S { get; set; } = 0;
    }

    public class StageList : EmbeddedObject {
        public bool stage1 { get; set;} = true;
        public bool stage2 { get; set;} = false;
        public bool stage3 { get; set;} = false;
        public bool stage4 { get; set;} = false;
        public bool stage5 { get; set;} = false;
        public bool stage6 { get; set;} = false;

        public bool stage7 { get; set;} = false;
        public bool stage8 { get; set;} = false;
        public bool stage9 { get; set;} = false;
        public bool stage10 { get; set;} = false;
        public bool stage11 { get; set;} = false;
        public bool stage12 { get; set;} = false;

        public bool stage13 { get; set;} = false;
        public bool stage14 { get; set;} = false;
        public bool stage15 { get; set;} = false;
        public bool stage16 { get; set;} = false;
        public bool stage17 { get; set;} = false;
        public bool stage18 { get; set;} = false;
    }

    public class ScoreList : EmbeddedObject {
        public string date { get; set; }
        public string category { get; set; }
        public int score { get; set; }
        public int totalScore { get; set; }
        public float rating { get; set; }
    }
    #endregion
    public class ProfileModel : RealmObject {
        [PrimaryKey]
        [MapTo("_id")]
        public string Username_id { get; set;}

        [MapTo("username")]
        public string Username { get; set; }

        [MapTo("gender")]
        public string CharGender { get; set; }

        [MapTo("firstLogin")]
        public bool firstLogin { get; set; } = true;

        [MapTo("level")]
        public int Level { get; set; } = 0;

        [MapTo("exp")]
        public int Experience { get; set; } = 0;

        [MapTo("maxExp")]
        public int CeilingExperience { get; set; } = 100;

        [MapTo("totalExp")]
        public int TotalExperience { get; set; } = 0;        

        [MapTo("cells")]
        public long CellCount { get; set; } = 0;

        [MapTo("genes")]
        public long GeneCount { get; set; } = 0;

        public int CurrentHealth { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;

        [MapTo("healingItems")]
        public Heals HealItems{ get; set; }

        [MapTo("resistances")]
        public Resistances ImmuneSystem { get; set; }

        // [MapTo("pills")]
        // public Pills PillBombs { get; set; }

        [MapTo("solution")]
        public Solution liquidMeds { get; set; }

        [MapTo("stages")]
        public StageList Stages { get; set; }

        [MapTo("scoreList")]
        public IList<ScoreList> Scores { get; }

        public ProfileModel() {}

        public ProfileModel(string id, string name, string gender) {
            Heals h = new Heals();
            Resistances r = new Resistances();
            Solution soln = new Solution();
            StageList sl = new StageList();

            Username_id = id;
            Username = name;
            CharGender = gender;
            firstLogin = this.firstLogin;
            Level = this.Level;
            Experience = this.Experience;
            CeilingExperience = this.CeilingExperience;
            TotalExperience = this.TotalExperience;
            CellCount = this.CellCount;
            GeneCount = this.GeneCount;

            CurrentHealth = this.CurrentHealth;
            MaxHealth = this.MaxHealth;

            HealItems = h;
            ImmuneSystem = r;
            liquidMeds = soln;
            Stages = sl;
            Scores = new List<ScoreList>();;
        }
    }
}

