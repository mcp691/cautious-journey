
// Initiates a turn based battle with the enemy that the player collides with
// in the overworld

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitiateBattle : MonoBehaviour
{
    public ScriptableObject enemySO;
    public GameObject sc;
    public SceneChanger sceneChanger;
    public PlayerMovement playerMove;
    GameObject obj;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        sc = GameObject.FindWithTag("SceneChanger");
        sceneChanger = sc.GetComponent<SceneChanger>();
        obj = collision.gameObject;
        sceneChanger.enemy = obj.GetComponent<EnemySO>().enemySO;
        Destroy(obj);

        SceneManager.LoadScene("Battle", LoadSceneMode.Additive);
        sceneChanger.inBattle = true;
    }
}
