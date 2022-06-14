using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IK : MonoBehaviour
{
    Animator anim;
    [SerializeField] private Transform target;
    [SerializeField] AvatarIKGoal targetGoal;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(targetGoal, 1f);
        anim.SetIKRotationWeight(targetGoal, 1f);

        anim.SetIKPosition(targetGoal, target.position);
        anim.SetIKRotation(targetGoal, target.rotation);
    }
}
