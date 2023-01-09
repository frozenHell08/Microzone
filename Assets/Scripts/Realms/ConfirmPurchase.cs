using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProfileCreation;

public class ConfirmPurchase : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private RealmController rController;
    [SerializeField] private Color nextColor;
    [SerializeField] private TMP_Text summary;

    [SerializeField] private GameObject ISbact;
    [SerializeField] private GameObject ISpara;
    [SerializeField] private GameObject ISvir;

    [SerializeField] private GameObject warningIS;
    [SerializeField] private GameObject warningShop;

    [SerializeField] private TMP_Text cells;
    [SerializeField] private TMP_Text genes;
    [SerializeField] private TMP_Text upgradePrice;
    [SerializeField] private TMP_Text itemPrice;

    [SerializeField] private TMP_InputField amount;

    [SerializeField] private PurchaseScreen _purchasing;
    [SerializeField] private ItemList _item;
    [SerializeField] private SolnList _soln;
    [SerializeField] private Tab _tab;

    public static ResistancesModel selectedItem;
    private ProfileModel profiledata;

    void OnEnable() {
        // profiledata = rController.FindProfile(GeneralData.player_LoggedIn);
        profiledata = rController.FindProfile("JOSHUA_M");
    }

    public void Purchase() {
        switch(_purchasing) {
            case PurchaseScreen.Shop :
                checkCells(profiledata);
                break;
            case PurchaseScreen.ImmuneSystem :
                checkUpgrade(profiledata);
                break;
        }
    }

    private void checkCells(ProfileModel profile) {
        int amt;

        if (amount.text == "") {
            return;
        } else {
            amt = int.Parse(amount.text);
        }

        long _cells = profile.CellCount;
        int iprice = int.Parse(itemPrice.text);
        int result = (int) _cells - (iprice * amt);

        if (result >= 0) {
            int newval = 0; 

            switch(_tab) {
                case Tab.Heals :
                    ForHeals(profile, amt, newval); 
                    break;
                case Tab.Solutions : 
                    ForSoln(profile, amt, newval);
                    break;
            }            

            rController.realmDB.Write(() => {
                profile.CellCount -= (iprice * amt);
            });

            cells.text = string.Format("{0:n0}", result); 
            amount.text = "";
        } else {
            warningShop.SetActive(true);
        }
    }

    private void ForHeals(ProfileModel profile, int _amt, int val) {
        var itm = profile.HealItems;
        string x = _item.ToString();         
        
        switch(_item) {
            case ItemList.Bandaid : 
                val = GettingValues(val, x, itm, _amt, 1);
                rController.realmDB.Write(() => {
                    profile.HealItems.Bandaid = val;
                });
                break;
            case ItemList.Injector : 
                val = GettingValues(val, x, itm, _amt, 1);
                rController.realmDB.Write(() => {
                    profile.HealItems.Injector = val;
                });
                break;
            case ItemList.Medicine : 
                val = GettingValues(val, x, itm, _amt, 1);
                rController.realmDB.Write(() => {
                    profile.HealItems.Medicine = val;
                });
                break;
        }
    }

    private void checkUpgrade(ProfileModel profile) {
        int lBact = profile.ImmuneSystem.BacteriaResist;
        int lPara = profile.ImmuneSystem.ParasiteResist;
        int lVir = profile.ImmuneSystem.VirusResist;

        long _genes = profile.GeneCount;
        int price = int.Parse(upgradePrice.text);
        
        int change = (int) _genes - price;

        if (change >= 0) {
            string firstchar = selectedItem._id.Substring(0, 1);
            
            rController.realmDB.Write(() => {
                profile.GeneCount -= price;
            });

            genes.text = string.Format("{0:n0}", profiledata.GeneCount); 

            if ((firstchar == "b") && (lBact < 10)) {
                rController.realmDB.Write(() => {
                    profile.ImmuneSystem.BacteriaResist += 1;
                });
            } else if ((firstchar == "p") && (lPara < 10)) {
                rController.realmDB.Write(() => {
                    profile.ImmuneSystem.ParasiteResist += 1;
                });
            } else if ((firstchar == "v") && (lVir < 10)) {
                rController.realmDB.Write(() => {
                    profile.ImmuneSystem.VirusResist += 1;
                });
            }

            ChangeNextButton(ISbact, lBact);
            ChangeNextButton(ISpara, lPara);
            ChangeNextButton(ISvir, lVir);
            
            ISSummary(profile);
        } else {
            warningIS.SetActive(true);
        }
    }

    private void ChangeNextButton (GameObject _panel, int reslvl) {
        ColorBlock current, target;

        if (reslvl < 9) {
            Button btnCurrent = _panel.transform.GetChild(reslvl).gameObject.GetComponent<Button>();
            Button btnNext = _panel.transform.GetChild(reslvl + 1).gameObject.GetComponent<Button>();
            
            current = btnCurrent.colors;
            target = btnNext.colors;

            current.disabledColor = new Color(1, 1, 1, 1);
            btnCurrent.colors = current;
            btnCurrent.interactable = false;

            target.normalColor = nextColor;
            btnNext.colors = target;
            btnNext.interactable = true;
        } else if (reslvl == 9) {
            Button btnCurrent = _panel.transform.GetChild(reslvl).gameObject.GetComponent<Button>();
            current = btnCurrent.colors;

            current.disabledColor = new Color(1, 1, 1, 1);
            btnCurrent.colors = current;
            btnCurrent.interactable = false;
        }
    }

    private void ISSummary(ProfileModel profile) {
        int rBact = profile.ImmuneSystem.BacteriaResist;
        int rPara = profile.ImmuneSystem.ParasiteResist;
        int rVir = profile.ImmuneSystem.VirusResist;

        int cBact = rBact * 3;
        int cPara = rPara * 3;
        int cVir = rVir * 3;

        string resFor = $"<b>Bacteria</b> Lv.{rBact}: \t{cBact}\n" +
                        $"<b>Parasite</b> Lv.{rPara}: \t{cPara}\n" +
                        $"<b>Virus</b> Lv.{rVir}: \t\t{cVir}";

        summary.text = resFor;
    }

    private void ForSoln(ProfileModel profile, int _amt, int val) {
        var itm = profile.liquidMeds;
        string x = _soln.ToString() + "_S";         
        
        switch(_soln) {
            case SolnList.Milaon : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Milaon_S = val;
                });
                break;
            case SolnList.Cinaon : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Cinaon_S = val;
                });
                break;
            case SolnList.Viraon : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Viraon_S = val;
                });
                break;
            case SolnList.Mildha : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Mildha_S = val;
                });
                break;
            case SolnList.Cinadha : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Cinadha_S = val;
                });
                break;
            case SolnList.Virdha : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Virdha_S = val;
                });
                break;
            case SolnList.Miltri : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Miltri_S = val;
                });
                break;
            case SolnList.Cinatri : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Cinatri_S = val;
                });
                break;
            case SolnList.Virtri : 
                val = GettingValues(val, x, itm, _amt, 5);
                rController.realmDB.Write(() => {
                    profile.liquidMeds.Virtri_S = val;
                });
                break;
        }
    }

    private int GettingValues(int _v, string _x, object _i, int _a, int multi) {
        _v = (int) _i.GetType().GetProperty(_x).GetValue(_i);
        _v += (_a * multi) ;
        return _v;
    }
}

public enum PurchaseScreen { Shop, ImmuneSystem };
public enum Tab { Heals, Solutions }
public enum ItemList { Bandaid, Injector, Medicine }
public enum SolnList { Milaon, Cinaon, Viraon, Mildha, Cinadha, Virdha, Miltri, Cinatri, Virtri };