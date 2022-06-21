using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float dmgFixValue;
    EnemyController controller;
    public void Awake()
    {
        controller = GetComponentInParent<EnemyController>();
    }
    public void GetDamage(float dmg, Vector3 shootOrgin)
    {
        controller.GetDamage(dmg * dmgFixValue, shootOrgin);
    }
}
