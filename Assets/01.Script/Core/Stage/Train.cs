using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Train : PoolAbleObject, ISubject
{
    static int prevStageIndex = -1;
    [Header("스테이지")]
    [SerializeField] private GameObject[] stages;
    int stageIdx;
    Transform enemys;
    IObserver observer;
    public override void Init_Pop()
    {
        stageIdx = Random.Range(0, stages.Length);
        while (stageIdx == prevStageIndex)
        {
            stageIdx = Random.Range(0, stages.Length);
        }
        stages[stageIdx].SetActive(true);
        prevStageIndex = stageIdx;
    }
    public override void Init_Push()
    {
        foreach (GameObject obj in stages)
        {
            obj.SetActive(false);
        }
    }
    public void SpawnEnemy()
    {
        enemys = stages[stageIdx].transform.Find("Enemys");
        for (int i = 0; i < enemys.childCount; i++)
        {
            enemys.GetChild(i).GetComponent<EnemySpawner>().SpawnEnemy();
        }
    }
    public void KillEnemy()
    {
        for (int i = 0; i < enemys.childCount; i++)
        {
            enemys.GetChild(i).GetComponent<EnemySpawner>().Kill();
        }
    }
    public void NotifyObserver()
    {
        observer.ObserverUpdate();
    }

    public void RegisterObserver(IObserver _observer)
    {
        observer = _observer;
    }

    public void RemoveObserver(IObserver _observer)
    {
        observer = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
    }
}
