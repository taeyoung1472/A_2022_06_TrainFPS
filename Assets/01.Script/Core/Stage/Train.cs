using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : PoolAbleObject, ISubject
{
    [SerializeField] private GameObject[] stages;
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

    public void NotifyObserver()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterObserver(IObserver observer)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveObserver(IObserver observer)
    {
        throw new System.NotImplementedException();
    }
}
