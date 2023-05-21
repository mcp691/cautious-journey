using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell/Healing")]
public class HealSpellData : ScriptableObject
{
    public GameObject prefab;

    [System.NonSerialized]
    public string magClass = "Healing";
    public int id;

    [TextArea(7,10)]
    public string description;
    
    public int castExp;
    public int minLevel = 1;
    public int castCost = 5;
    
    [Header("Base Stats")]
    public int baseHealPower = 1;

    [Header("Leveled Stats")]
    public int healPower;

    [Header("Known (Default is false)")]
    public bool known = false;


}
