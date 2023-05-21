// Script to display the Character menu which shows the player character's
// stats such as Health, Attack, Speed, etc.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text expNeededText;
    public TMP_Text hpText;
    public TMP_Text attackText;
    public TMP_Text speedText;
    public PlayerCharData data;
    GameObject charCanvas;
    public PauseMenu pause;

    public GameObject sc;
    public SceneChanger sceneChanger;

    public void Start()
    {
        Time.timeScale = 1f;
        charCanvas = GameObject.Find("Character Canvas");
        charCanvas.GetComponent<Canvas>().enabled = false;
        sc = GameObject.FindWithTag("SceneChanger");
        sceneChanger = sc.GetComponent<SceneChanger>();
    }

    public void Update()
    {
        nameText.text = data.playerName;
        levelText.text = "Lvl. " + data.playerLevel;
        expNeededText.text = "Exp Needed: " + (data.playerExpNeeded - data.playerExp);
        hpText.text = "HP: " + data.playerCurrentHP + "/" + data.playerMaxHP;
        attackText.text = "Atk: " + data.playerAttack;
        speedText.text = "Spd: " + data.playerSpeed;

        if (Input.GetKeyDown("c") && charCanvas.GetComponent<Canvas>().enabled == true ||
            Input.GetKeyDown("escape") && charCanvas.GetComponent<Canvas>().enabled == true)
        {
            Unpause();
        } else if (Input.GetKeyDown("c") && charCanvas.GetComponent<Canvas>().enabled == false && pause.isPaused == false && !sceneChanger.inBattle)
        {
            Pause();
        }
    }

    public void Pause()
    {
        pause.isPaused = true;
        Time.timeScale = 0f;
        charCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void Unpause()
    {
        pause.isPaused = false;
        Time.timeScale = 1f;
        charCanvas.GetComponent<Canvas>().enabled = false;
    }

}
