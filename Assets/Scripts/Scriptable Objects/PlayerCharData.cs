using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Character", menuName = "Player/Character")]
public class PlayerCharData : ScriptableObject
{
    [Header("Basic Player Character Data")]
    public Sprite sprite;
    public string playerName;
    public string description;
    public GameObject playerObject;

    [Header("Magic/Spells Data")]
    public List<HealSpellData> healingSpells = new List<HealSpellData>();
    public ScriptableObject[] warfareSpells;
    public ScriptableObject[] deceptionSpells;
    public ScriptableObject[] transformSpells;

    [Header("Leveling Info")]
    public int playerLevel = 3;

    public int playerExp = 0;
    public int playerExpNeeded;
    
    [Header("Base Stats")]
    public int playerBaseMaxHP;
    public int playerBaseAttack;
    public int playerBaseSpeed;

    [Header("Leveled Stats")]
    public int playerMaxHP;
    public int playerCurrentHP;
    public int playerAttack;
    public float playerSpeed;

    public void Initialize()
    {
        playerAttack = Mathf.FloorToInt(playerBaseAttack * playerLevel);
        playerMaxHP = Mathf.FloorToInt(playerBaseMaxHP * playerLevel);
        playerSpeed = Mathf.Floor(playerBaseSpeed * playerLevel);
        playerExpNeeded = NextLevel(playerLevel);
    }

    public bool CanLevelUp(int expNeeded, int currentExp)
    {
        if (currentExp >= expNeeded) 
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void LevelUp()
    {
        playerLevel++;
        playerExp -= playerExpNeeded;
        Initialize();
        playerCurrentHP = playerMaxHP;
        playerExpNeeded = NextLevel(playerLevel);
    }

    public int NextLevel(int level)
    {
        return Mathf.FloorToInt((4 * (int) (Math.Pow(level, 3)) / 5));
    }

    public bool TakeDamage(int dmg)
    {
        playerCurrentHP -= dmg;

        if (playerCurrentHP <= 0)
            return true;
        else
            return false;
    }

    public int Heal(int healBy)
    {
        playerCurrentHP += healBy;
        if (playerCurrentHP >= playerMaxHP)
        {
            return playerMaxHP;
        } else
        {
            return playerCurrentHP;
        }
    }

}
