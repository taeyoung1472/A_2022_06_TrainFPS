using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour, IObserver
{
    [SerializeField] private float length;
    [SerializeField] private int initTrainCount = 5;
    [SerializeField] private GameObject backGround;
    Queue<Train> trainQueue;
    float spawnPos;

    public void ObserverUpdate()
    {

    }

    private void Start()
    {
        for (int i = 0; i < initTrainCount; i++)
        {
            Train tr = PoolManager.instance.Pop(PoolType.Train).GetComponent<Train>();
            tr.transform.position = new Vector3(0, 0, spawnPos);
            spawnPos += length;
        }
    }
}
