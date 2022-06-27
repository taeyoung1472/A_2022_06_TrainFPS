using UnityEngine;
using System;

[Serializable]
public class ModingData
{
    public ModingPart part;
    public string partName;
}
public enum ModingPart
{
    Sight,
    Muzzel
}