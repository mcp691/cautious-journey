using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, FLEE }

public class BattleSystem : MonoBehaviour
{

    public float prob;

    private GameObject playerPrefab;
    public PlayerMovement playerMove;
    private GameObject enemyPrefab;

    public GameObject init;
    public InitiateBattle initBattle;

    public EnemyData enemyData;
    public PlayerCharData playerData;

    public Transform playerBattlePlatform;
    public Transform enemyBattlePlatform;

    [Header("Battle UI Elements")]
    GameObject magicMenu;
    GameObject healMagicMenu;
    public TMP_Text dialogueText;
    public Button magicButton;
    private Button mbtn;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    public BattleState state;

    public GameObject sc;
    public SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.FindWithTag("SceneChanger");
        sceneChanger = sc.GetComponent<SceneChanger>();
        state = BattleState.START;
        enemyData = sceneChanger.enemy;

        // setup UI elements
        mbtn = magicButton.GetComponent<Button>();
		mbtn.onClick.AddListener(OpenMagicMenu);
        magicMenu = GameObject.Find("Magic Menu");
        magicMenu.GetComponent<Canvas>().enabled = false;
        healMagicMenu = GameObject.Find("Healing Magic Panel");
        healMagicMenu.SetActive(false);

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerData.Initialize();
        GameObject playerGO = Instantiate(playerData.playerObject, playerBattlePlatform);
        
        enemyData.Initialize();
        GameObject enemyGO = Instantiate(enemyData.enemyObject, enemyBattlePlatform);

        dialogueText.text = "A " + enemyData.enemyName + " charges at you!";

        playerHUD.SetPlayerHUD(playerData);
        enemyHUD.SetEnemyHUD(enemyData);

        yield return new WaitForSeconds(2f);

        if (playerData.playerSpeed > enemyData.enemySpeed)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        } 
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
            
        
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action...";
    }

    IEnumerator PlayerAttack()
    {
        state = BattleState.ENEMYTURN;
        dialogueText.text = enemyData.enemyName + " is hit for " + playerData.playerAttack + " damage!";

        yield return new WaitForSeconds(1f);

        bool isDead = enemyData.TakeDamage(playerData.playerAttack);

        enemyHUD.SetHP(enemyData.enemyCurrentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerCastSpell()
    {
        state = BattleState.ENEMYTURN;
        CloseHealMagicMenu();
        CloseMagicMenu();
        Debug.Log("HELLO");

        yield return new WaitForSeconds(1f);

        dialogueText.text = playerData.playerName + " heals for " + playerData.healingSpells[0].healPower + " health!";

        yield return new WaitForSeconds(1f);

    }

    IEnumerator PlayerFlee()
    {
        state = BattleState.ENEMYTURN;
        dialogueText.text = playerData.playerName + " attempts to flee!";

        prob = playerData.playerSpeed / (enemyData.enemySpeed - 1);
        yield return new WaitForSeconds(2f);

        if (Random.Range(0f, 1f) <= prob)
        {
            state = BattleState.FLEE;
            StartCoroutine(EndBattle());
        } else
        {
            dialogueText.text = playerData.playerName + " can't escape!";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyData.enemyName + " attacks " + playerData.playerName + " for " + enemyData.enemyAttack + " damage!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerData.TakeDamage(enemyData.enemyAttack);

        playerHUD.SetHP(playerData.playerCurrentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {

        if (state == BattleState.WON)
        {
            int expGained = 0;
            
            // if player level is 1, then this divides by zero
            // but this is the best exp calculation we have so let's go with it
            if (playerData.playerLevel > 1)
            {
                expGained = enemyData.enemyExpGain / (playerData.playerLevel - 1);
            }
            else
            {
                expGained = enemyData.enemyExpGain / (playerData.playerLevel);
            }
            dialogueText.text = playerData.playerName + " is victorious!";
            yield return new WaitForSeconds(2f);
            dialogueText.text = playerData.playerName + " gains " + expGained + " experience.";
            playerData.playerExp += expGained;
            yield return new WaitForSeconds(2f);
            if (playerData.CanLevelUp(playerData.playerExpNeeded, playerData.playerExp))
            {
                playerData.LevelUp();
                dialogueText.text = playerData.playerName + " is now level " + playerData.playerLevel + "!";
                yield return new WaitForSeconds(2f);
            }

        } else if (state == BattleState.LOST)
        {
            dialogueText.text = playerData.playerName + " falls defeated!";
            yield return new WaitForSeconds(2f);
        } else if (state == BattleState.FLEE)
        {
            dialogueText.text = playerData.playerName + " fled the battle!";
            yield return new WaitForSeconds(2f);
        }
        sceneChanger = sc.GetComponent<SceneChanger>();
        sceneChanger.inBattle = false;
        SceneManager.UnloadSceneAsync("Battle");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerAttack());
    }

    public void OnSpellButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerCastSpell());
    }

    public void OnFleeButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine(PlayerFlee());
    }

    public void OpenMagicMenu()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        magicMenu.GetComponent<Canvas>().enabled = true;
	}

    public void CloseMagicMenu()
    {
        magicMenu.GetComponent<Canvas>().enabled = false;
	}

    public void OpenHealMagicMenu()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        healMagicMenu.SetActive(true);
	}

    public void CloseHealMagicMenu()
    {
        healMagicMenu.SetActive(false);
	}

}
