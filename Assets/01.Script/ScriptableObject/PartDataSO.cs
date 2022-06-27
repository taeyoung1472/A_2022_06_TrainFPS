using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/SO/Gun/Part")]
public class PartDataSO : ScriptableObject
{
    public Sprite partSprite;
    public ModingData modingData;
    //public string partName = "Part Name";
    public string desc = "Part Desc";
}