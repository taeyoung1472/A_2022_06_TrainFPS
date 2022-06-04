using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : PoolAbleObject, ISubject
{
    [SerializeField] private GameObject[] stages;
    IObserver observer;
    public override void Init_Pop()
    {
        stages[Random.Range(0, stages.Length)].SetActive(true);
    }

    public override void Init_Push()
    {
        foreach (GameObject obj in stages)
        {
            obj.SetActive(false);
        }
    }
    [ContextMenu("ÀÌÈ÷È÷")]
    public void NOU()
    {
        NotifyObserver();
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
}
