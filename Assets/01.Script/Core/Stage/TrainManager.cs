using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation.Samples;
public class TrainManager : MonoBehaviour, IObserver
{
    [SerializeField] private float length;
    [SerializeField] private int initTrainCount = 5;
    [SerializeField] private GameObject backGround;
    LocalNavMeshBuilder navBuilder;
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
        navBuilder.transform.position = tr.transform.position + Vector3.up * 3.25f + Vector3.forward * length;
        navBuilder.UpdateNavMesh();
        tr.transform.SetParent(backGround.transform);
        ScoreUiManager.Instance.Add("Next Stage", 200, ScoreSoundType.NextStage);
        Destroy(tr.gameObject, 10f);
    }

    private void Start()
    {
        navBuilder = LocalNavMeshBuilder.Instance;
        for (int i = 0; i < initTrainCount; i++)
        {
            SpawnTrain();
        }
        navBuilder.UpdateNavMesh();
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
