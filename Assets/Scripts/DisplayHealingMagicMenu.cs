using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayHealingMagicMenu : MonoBehaviour
{
    public PlayerCharData knownSpells;
    public BattleSystem bs;

    public int X_START;
    public int Y_START;

    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<HealSpellData, GameObject> spellsDisplayed = new Dictionary<HealSpellData, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //X_START *= playerMagic.healingSpells.Count;
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
         for (int i = 0; i < knownSpells.healingSpells.Count; i++)
         {
            if (spellsDisplayed.ContainsKey(knownSpells.healingSpells[i]))
            {
                spellsDisplayed[knownSpells.healingSpells[i]].transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = knownSpells.healingSpells[i].name.ToString();
                spellsDisplayed[knownSpells.healingSpells[i]].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = knownSpells.healingSpells[i].castCost.ToString() + " MP";
            }
            else
            {
                var obj = Instantiate(knownSpells.healingSpells[i].prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = knownSpells.healingSpells[i].name.ToString();
                obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = knownSpells.healingSpells[i].castCost.ToString() + " MP";
                obj.GetComponent<Button>().onClick.AddListener(bs.OnSpellButton);
                spellsDisplayed.Add(knownSpells.healingSpells[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < knownSpells.healingSpells.Count; i++)
        {
            var obj = Instantiate(knownSpells.healingSpells[i].prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = knownSpells.healingSpells[i].name.ToString();
            obj.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = knownSpells.healingSpells[i].castCost.ToString() + " MP";
            spellsDisplayed.Add(knownSpells.healingSpells[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + ((-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN))), 0f);
    }
}
