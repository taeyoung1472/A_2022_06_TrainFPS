using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class MenuButtonUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI tmp;
    public void OnPointerEnter(PointerEventData eventData)
    {
        tmp.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmp.color = Color.gray;
    }
}
