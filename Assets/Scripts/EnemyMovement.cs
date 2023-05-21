// Controls enemy movement in the overworld.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    Vector2 newPos;
    Task t;

    float time;
    float newX;
    float newY;
    float dX;
    float dY;
    float totalDist;

    public float moveSpeed;
    public GameObject player;
    public float aggroRange;
    public SceneChanger sceneChanger;
    public GameObject sc;



    void OnEnable()
    {
        sc = GameObject.FindWithTag("SceneChanger");
        sceneChanger = sc.GetComponent<SceneChanger>();
        t = new Task(GetTargetLocation());
        StartCoroutine(GetTargetLocation());
    }

    IEnumerator GetTargetLocation()
    {
        for (int i = 0; i < 2; i++)
        {
            if (!sceneChanger.inBattle)
            {
                time = Random.Range(5f, 10f);
                newX = Random.Range(-1f, 1f) + transform.position.x;
                newY = Random.Range(-1f, 1f) + transform.position.y;
                newPos = new Vector2(newX, newY);
                yield return new WaitForSeconds(time);
                i = 0; // infinite loop until GO is destroyed
            }
        }
    }

    bool DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position), 3f);
        
        // draw raycast for debugging
        Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.green);
        
        if (hit)
            return true;
        else
            return false;
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (moveSpeed * 1.5f) * Time.fixedDeltaTime);
    }

    float CalcDist()
    {
        dX = Mathf.Abs(player.transform.position.x - transform.position.x);
        dY = Mathf.Abs(player.transform.position.y - transform.position.y);
        totalDist = Mathf.Sqrt(Mathf.Pow(dX, 2) + Mathf.Pow(dY, 2));
        return totalDist;
    }

    void FixedUpdate()
    {
        if (!t.Running)
        {
            // reenable the script to start coroutine over
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }

        if (!sceneChanger.inBattle)
        {
            totalDist = CalcDist();
            if (totalDist <= aggroRange)
            {
                DetectPlayer();
                ChasePlayer();
            }
            else
                transform.position = Vector2.MoveTowards(transform.position, newPos, moveSpeed * Time.fixedDeltaTime);
        }
    }
}
