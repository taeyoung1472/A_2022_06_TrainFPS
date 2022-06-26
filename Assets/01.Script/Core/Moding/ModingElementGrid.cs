using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModingElementGrid : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform grid;
    [SerializeField] private Transform modingPoint;
    [SerializeField] private SpriteRenderer profile;
    [SerializeField] private GameObject[] parts;
    [SerializeField] private GameObject[] partUIDisplayers;
    [SerializeField] private Transform uiDisplayContent;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    [ContextMenu("√ ±‚»≠")]
    public void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, grid.position);
        lineRenderer.SetPosition(1, grid.position);
    }
    void Update()
    {
        lineRenderer.SetPosition(0, grid.position);
        lineRenderer.SetPosition(1, modingPoint.position);
    }
    public void Use(GameObject _targetObj, Sprite _sprite)
    {
        foreach (var part in parts)
        {
            part.SetActive(false);
        }
        _targetObj.SetActive(true);
        profile.sprite = _sprite;
    }
    private void OnMouseDown()
    {
        for (int i = 0; i < uiDisplayContent.childCount; i++)
        {
            GameObject obj = uiDisplayContent.GetChild(i).gameObject;
            if (obj.name == "Start" || obj.name == "End")
                continue;
            uiDisplayContent.GetChild(i).gameObject.SetActive(false);
        }
        foreach (var partUI in partUIDisplayers)
        {
            partUI.SetActive(true);
        }
    }
}
