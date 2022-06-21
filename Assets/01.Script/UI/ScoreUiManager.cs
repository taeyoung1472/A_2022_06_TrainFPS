using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUiManager : MonoBehaviour
{
    [SerializeField] private Score socrePrefab;
    public static ScoreUiManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void Add()
    {
        Score score = Instantiate(socrePrefab, transform);
        score.Set();
    }
}
