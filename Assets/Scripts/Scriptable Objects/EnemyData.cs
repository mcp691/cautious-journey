using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Slimes")]
public class EnemyData : ScriptableObject
{
    public Sprite sprite;

    public int id;
    public string enemyName;
    public string description;
    public GameObject enemyObject;

    public int enemyMinLevel;
    public int enemyMaxLevel;
    public int enemyLevel;

    public int baseEnemyExpGain;
    public int enemyExpGain;
    
    [Space(10)]
    [Header("Base Stats")]
    public int enemyBaseAttack;
    public int enemyBaseMaxHP;
    public int enemyBaseSpeed;

    [Space(10)]
    [Header("Total Stats")]
    public int enemyAttack;
    public int enemyMaxHP;
    public int enemyCurrentHP;
    public float enemySpeed;

    public void Initialize()
    {
        enemyLevel = Random.Range(enemyMinLevel, enemyMaxLevel);
        enemyExpGain = baseEnemyExpGain * enemyLevel;
        enemyAttack = Mathf.FloorToInt(enemyBaseAttack * enemyLevel);
        enemyMaxHP = Mathf.FloorToInt(enemyBaseMaxHP * enemyLevel);
        enemyCurrentHP = Mathf.FloorToInt(enemyBaseMaxHP * enemyLevel);
        enemySpeed = Mathf.Floor(enemyBaseSpeed * enemyLevel);
    }

    public bool TakeDamage(int dmg)
    {
        enemyCurrentHP -= dmg;

        if (enemyCurrentHP <= 0)
            return true;
        else
            return false;
    }

}
