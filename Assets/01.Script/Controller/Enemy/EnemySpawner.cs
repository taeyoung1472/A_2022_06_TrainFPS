using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    private GameObject myEnemy;
    public void SpawnEnemy()
    {
        myEnemy = Instantiate(enemy, transform).gameObject;
        myEnemy.transform.position = transform.position;
    }

    public void Kill()
    {
        Destroy(myEnemy);
    }
}
