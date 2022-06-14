using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Train : PoolAbleObject, ISubject
{
    [SerializeField] private GameObject[] stages;
    IObserver observer;
    public override void Init_Pop()
    {
        stages[Random.Range(0, stages.Length)].SetActive(true);
        NavigationBaker.Instance.Build(GetComponent<NavMeshSurface>());
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
