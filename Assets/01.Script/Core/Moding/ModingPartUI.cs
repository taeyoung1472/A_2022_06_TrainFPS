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
        partTitle.text = data.modingData.partName;
        partDesc.text = data.desc;
        profileImage.sprite = data.partSprite;
        foreach (ModingData modingData in JsonManager.instance.Data.modingDatas)
        {
            if (data.modingData.part != modingData.part) { continue; }
            if(data.modingData.partName == modingData.partName)
            {
                grid.Use(targetPart, data.partSprite);
                gameObject.SetActive(false);
                return;
            }
        }
        gameObject.SetActive(false);
    }
    public void Use()
    {
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(useClip, 1f, Random.Range(0.9f, 1.1f));
        grid.Use(targetPart, data.partSprite);
        bool isSucces = false;
        foreach (var _modingData in JsonManager.instance.Data.modingDatas)
        {
            if(_modingData.part == data.modingData.part)
            {
                isSucces = true;
                _modingData.partName = data.modingData.partName;
                break;
            }
        }
        if (!isSucces)
        {
            JsonManager.instance.Data.modingDatas.Add(data.modingData);
        }
        JsonManager.instance.Save();
    }
}
