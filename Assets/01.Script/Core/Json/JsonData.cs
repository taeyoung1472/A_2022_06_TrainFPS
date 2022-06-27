using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class JsonData
{
    public int bestScore;
    public int bestTrain;
    public int money;
    public List<ModingData> modingDatas = new List<ModingData>();
}