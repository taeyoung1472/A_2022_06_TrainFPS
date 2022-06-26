using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("카메라 관련")]
    [SerializeField] private CinemachineVirtualCamera[] vcams;

    [Header("Enemy 관련")]
    [SerializeField] private Transform enemyCam;
    [SerializeField] private EnemyDataSO[] enemyDatas;
    [SerializeField] private EnemyInfoUI enemyInfoUI;

    [Header("UI 관련")]
    [SerializeField] private RectTransform[] uiPanels;

    Vector3 orgin;
    int curEnemyIdx = 0;
    public void Start()
    {
        orgin = enemyCam.position;
        enemyInfoUI.SetInfo(enemyDatas[curEnemyIdx]);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ActiveCam(CinemachineVirtualCamera tgtCam)
    {
        foreach (var cam in vcams)
        {
            cam.gameObject.SetActive(false);
        }
        tgtCam.gameObject.SetActive(true);
    }
    public void ActiveRect(RectTransform tgtRect)
    {
        foreach (RectTransform rect in uiPanels)
        {
            if (rect != tgtRect)
            {
                rect.DOMoveX(-2000, 2f);
            }
            else
            {
                rect.DOMoveX(-50, 2f);
            }
        }
    }
    public void PrevEnemy()
    {
        if(curEnemyIdx > 0)
        {
            curEnemyIdx--;
        }
        MoveCam();
    }
    public void NextEnemy()
    {
        if (curEnemyIdx < enemyDatas.Length - 1)
        {
            curEnemyIdx++;
        }
        MoveCam();
    }
    void MoveCam()
    {
        enemyInfoUI.SetInfo(enemyDatas[curEnemyIdx]);
        enemyCam.DOMove(orgin + Vector3.forward * (curEnemyIdx * 3), 0.5f);//position = orgin + Vector3.forward * (curEnemyIdx * 3);
    }
}
