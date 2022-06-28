using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class DieManager : MonoBehaviour
{
    public static DieManager instance;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private TextMeshProUGUI timeTMP;
    [SerializeField] private TextMeshProUGUI killTMP;
    float startTime;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        startTime = Time.time;
    }
    public void Die()
    {
        Cursor.lockState = CursorLockMode.Confined;
        deadPanel.SetActive(true);
        timeTMP.text = $"{Time.time - startTime:0} Sec";
        killTMP.text = $"{EnemyController.dieCount} Kill";
        EnemyController.dieCount = 0;
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
