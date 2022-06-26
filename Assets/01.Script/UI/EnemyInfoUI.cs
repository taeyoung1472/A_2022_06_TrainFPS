using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI hpTMP;
    [SerializeField] private TextMeshProUGUI damageTMP;
    [SerializeField] private TextMeshProUGUI rangeTMP;
    [SerializeField] private TextMeshProUGUI actuallyTMP;
    [SerializeField] private TextMeshProUGUI attackDelayTMP;
    [SerializeField] private TextMeshProUGUI attackPerShootTMP;

    public void SetInfo(EnemyDataSO data)
    {
        nameTMP.text = data.enemyName;
        scoreTMP.text = data.score.ToString();
        hpTMP.text = data.hp.ToString();
        damageTMP.text = data.damage.ToString();
        rangeTMP.text = data.attackRange.ToString();
        actuallyTMP.text = $"{data.actually * 100}%";
        attackDelayTMP.text = $"{data.attackDelay} Sec";
        attackPerShootTMP.text = data.attackPerShoot.ToString();
    }
}
