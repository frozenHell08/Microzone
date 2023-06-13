using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomescreenController : MonoBehaviour 
{
    #region
    [SerializeField] private RealmController rController;
    [SerializeField] private CurrentCharacter ch;
    [SerializeField] private CurrentHealing cHeal;
    [SerializeField] private CurrentSolution cSol;
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject giftPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text gendata_cells;
    [SerializeField] private TMP_Text gendata_genes;
    [SerializeField] private long cells;
    [SerializeField] private long genes;
    [SerializeField] private int bandaid;
    [SerializeField] private int milaon_s;
    [SerializeField] private TMP_Text iName;
    [SerializeField] private TMP_Text iStatus;
    [SerializeField] private Image iSprite;
    [SerializeField] private TMP_Text iRes;
    [SerializeField] private TMP_Text iHeals;
    [SerializeField] private TMP_Text iSolutions;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private List<GameObject> itmPanels;
    [SerializeField] private List<HealItem> healItems;
    [SerializeField] private List<Solution> solnItems;
    #endregion

    void OnEnable() {
        if (ch.firstLogin) {
            giftPanel.SetActive(true);

            ch.firstLogin = false;
            ch.cells += cells;
            ch.genes += genes;
            ch.totalStages = 1;
            ch.healCount.Bandaid += bandaid;
            ch.solutionsCount.milaon += milaon_s;

            rController.SyncGiftToRealm();
        }

        Refresh();

        TMP_Text ch_name = characterPanel.GetComponentInChildren<TMP_Text>();
        Image ch_sprite = Array.FindLast<Image>(characterPanel.GetComponentsInChildren<Image>(), 
            img => img.GetType() == typeof(Image));

        ch_sprite.preserveAspect = true;
        ch_sprite.sprite = ch.sprite;
        ch_name.text = $"Welcome, {ch.characterName}.";

        rController.SyncFromRealm();
    }

    public void StatusInventory() {
        StringComparison oic = StringComparison.OrdinalIgnoreCase;
        Func<int, int> res = x => (x * 3);

        // -------------------- STATUS --------------------

        string y1 = $"<b>Gender</b> : \t{ch.gender}\n" +
                    $"<b>Level</b> : \t{ch.level}\n" +
                    $"<b>Exp</b> : \t{ch.experience}/{ch.barExperience}\n" +
                    $"<b>Health</b> : \t{ch.currentHealth} / {ch.maxHealth}\n" +
                    $"<b>Cells</b> : \t{ch.cells:n0}\n" +
                    $"<b>Genes</b> : \t{ch.genes:n0}\n" +
                    $"<b>Stages</b> : \t{ch.totalStages}\n";
        
        iName.text = ch.characterName;
        iSprite.preserveAspect = true;
        iSprite.sprite = ch.sprite;
        iStatus.text = y1;

        string resFor = $"<b>Bacteria</b> Lv.{ch.res_bacteria}: \t{res(ch.res_bacteria)} (+{ch.level})\n" +
                        $"<b>Parasite</b> Lv.{ch.res_parasite}:\t{res(ch.res_parasite)} (+{ch.level})\n" +
                        $"<b>Virus</b> Lv.{ch.res_virus}:\t{res(ch.res_virus)} (+{ch.level})";
        
        iRes.text = resFor;

        // -------------------- INVENTORY --------------------

        itmPanels.ForEach(itm => {
            foreach (TMP_Text txt in itm.GetComponentsInChildren<TMP_Text>(true)) {
                if (itm.name.Contains("heal")) {
                    HealItem getItem = healItems.Find(i => i.ItemID.Equals(txt.transform.parent.name, oic));
                    FieldInfo field = Array.Find<FieldInfo>(cHeal.GetType().GetFields(),
                        f => f.Name.Equals(getItem.ItemName));

                    if (txt.name.Equals("name")) txt.text = getItem.ItemName;
                    if (txt.name.Equals("amt")) txt.text = field.GetValue(cHeal).ToString();

                } else if (itm.name.Contains("soln")) {
                    Solution getSoln = solnItems.Find(j => j.solutionID.Equals(txt.transform.parent.name, oic));
                    FieldInfo field = Array.Find<FieldInfo>(cSol.GetType().GetFields(),
                        f => f.Name.Equals(getSoln.solutionName, oic));

                    if (txt.name.Equals("name")) txt.text = getSoln.solutionName;
                    if (txt.name.Equals("amt")) txt.text = field.GetValue(cSol).ToString();
                }
            }

            foreach (Image img in itm.GetComponentsInChildren<Image>(true)) {
                if (img.name.Equals("img")) {
                    img.preserveAspect = true;

                    HealItem hh = healItems.Find(i => i.ItemID == img.transform.parent.name);
                    Solution ss = solnItems.Find(j => j.solutionID == img.transform.parent.name);

                    img.sprite = (itm.name.Contains("heal")) ? hh.ItemSprite : ss.solutionSprite;
                }
            }

            foreach (Button btn in itm.GetComponentsInChildren<Button>(true)) {
                // Debug.Log(btn.name);

                TMP_Text amount = btn.GetComponentsInChildren<TMP_Text>(true).FirstOrDefault(txt => txt.name.Equals("amt"));

                int itemamt = int.Parse(amount.text);

                if (itemamt == 0) {
                    btn.interactable = false;
                } else {
                    btn.interactable = true;
                }
            }
        });
    }

    public void AcceptGift() {
        giftPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        scrollbar.value = 1f;
        Refresh();
    }

    private void Refresh() {
        gendata_cells.text = $"{ch.cells:n0}";
        gendata_genes.text = $"{ch.genes:n0}";
    }
}