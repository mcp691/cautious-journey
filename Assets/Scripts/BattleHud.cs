using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleHud : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Slider hpSlider;

    public void SetEnemyHUD(EnemyData data)
    {
        hpSlider.interactable = false;
        nameText.text = data.enemyName;
        levelText.text = "Lvl. " + data.enemyLevel;
        hpSlider.maxValue = data.enemyMaxHP;
        hpSlider.value = data.enemyCurrentHP;
    }

    public void SetPlayerHUD(PlayerCharData data)
    {
        hpSlider.interactable = false;
        nameText.text = data.playerName;
        levelText.text = "Lvl. " + data.playerLevel;
        hpSlider.maxValue = data.playerMaxHP;
        hpSlider.value = data.playerCurrentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
