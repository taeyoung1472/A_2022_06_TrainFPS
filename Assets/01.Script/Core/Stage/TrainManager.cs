using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour, IObserver
{
    [SerializeField] private float length;
    [SerializeField] private int initTrainCount = 5;
    [SerializeField] private GameObject backGround;
    bool isFirst = true;
    Queue<Train> trainQueue = new Queue<Train>();
    int curTrainIndex = 0;
    float spawnPos;

    public void ObserverUpdate()
    {
        if (isFirst)
        {
            isFirst = false;
            return;
        }
        SpawnTrain();
        Train tr = trainQueue.Dequeue();
        tr.transform.SetParent(backGround.transform);
        Destroy(tr.gameObject, 10f);
    }

    private void Start()
    {
        for (int i = 0; i < initTrainCount; i++)
        {
            SpawnTrain();
        }
    }
    void SpawnTrain()
    {
        Train tr = PoolManager.instance.Pop(PoolType.Train).GetComponent<Train>();
        tr.RegisterObserver(this);
        tr.transform.position = new Vector3(0, 0, spawnPos);
        trainQueue.Enqueue(tr);
        spawnPos += length;
        curTrainIndex++;
    }
}
