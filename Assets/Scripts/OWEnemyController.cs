using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWEnemyController : MonoBehaviour
{
    public EnemyData enemySO;
    public GameObject enemy;

    public void Update()
    {
        if (enemySO.enemyCurrentHP == 0)
        {
            DestroyEnemy(enemySO.enemyObject);
        }
    }

    public void DestroyEnemy(GameObject defeatedEnemy)
    {
        
        Destroy(defeatedEnemy);
    }    
}