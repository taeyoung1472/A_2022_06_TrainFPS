using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform player;
    public Transform Player { get { return player; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
