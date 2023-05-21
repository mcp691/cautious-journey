using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public EnemyData data;

    public string unitName;
    public int unitLevel;

    public int attack;
    public int maxHP;
    public int currentHP;
    public float speed;

    void Start()
    {
        setEnemyUnitStats();
    }

    void setEnemyUnitStats() {
        attack = data.enemyAttack;
        maxHP = data.enemyMaxHP;
        currentHP = data.enemyCurrentHP;
        speed = data.enemySpeed;
    }
    
    public bool TakeDamage(int dmg) {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
