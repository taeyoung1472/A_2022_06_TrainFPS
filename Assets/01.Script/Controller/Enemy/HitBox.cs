using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour, IHitAble
{
    [SerializeField] private float dmgFixValue;
    EnemyController controller;
    public void Awake()
    {
        controller = GetComponentInParent<EnemyController>();
    }
    public void Hit(float power, Vector3 shootPos)
    {
        controller.GetDamage(power * dmgFixValue, shootPos);
    }
}
