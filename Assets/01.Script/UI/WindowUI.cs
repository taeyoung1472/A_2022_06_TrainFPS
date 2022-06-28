using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WindowUI : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshPro title;
       
    public void ActiveWindow(GameObject targetWindow)
    {
        window.SetActive(true);
        targetWindow?.SetActive(true);
    }
    public void SetTitle(string tgtString)
    {
        title.text = tgtString.ToUpper();
    }
    public void CloseWindow()
    {
        window.SetActive(false);
    }
}