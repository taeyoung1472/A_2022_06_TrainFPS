using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    bool isCheck;
    [SerializeField] private Train train;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isCheck)
            {
                isCheck = true;
                train.NotifyObserver();
            }
        }
    }
}
