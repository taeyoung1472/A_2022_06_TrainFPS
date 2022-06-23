using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Train : PoolAbleObject, ISubject
{
    static int prevStageIndex = -1;
    [SerializeField] private GameObject[] stages;
    IObserver observer;
    public override void Init_Pop()
    {
        int rand = Random.Range(0, stages.Length);
        while (rand != prevStageIndex)
        {
            rand = Random.Range(0, stages.Length);
        }
        stages[rand].SetActive(true);
        prevStageIndex = rand;
    }
    public override void Init_Push()
    {
        foreach (GameObject obj in stages)
        {
            obj.SetActive(false);
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
