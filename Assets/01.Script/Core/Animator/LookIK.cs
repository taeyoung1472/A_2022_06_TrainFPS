using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookIK : MonoBehaviour
{
    Animator anim;
    [SerializeField] private Transform target;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        target = GameManager.instance.Player;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtPosition(target.position);
        anim.SetLookAtWeight(0.5f);
    }
}
