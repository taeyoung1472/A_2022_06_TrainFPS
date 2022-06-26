using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ModingPartUI : MonoBehaviour
{
    [SerializeField] private PartDataSO data;
    [SerializeField] private TextMeshProUGUI partTitle;
    [SerializeField] private TextMeshProUGUI partDesc;
    [SerializeField] private Image profileImage;
    [SerializeField] private GameObject targetPart;
    [SerializeField] private ModingElementGrid grid;
    [SerializeField] private AudioClip useClip;
    void Start()
    {
        partTitle.text = data.partName;
        partDesc.text = data.desc;
        profileImage.sprite = data.partSprite;
    }
    public void Use()
    {
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(useClip, 1f, Random.Range(0.9f, 1.1f));
        grid.Use(targetPart, data.partSprite);
    }
}
