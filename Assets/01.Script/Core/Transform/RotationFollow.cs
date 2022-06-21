using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 fixRot;
    void Update()
    {
        transform.eulerAngles = target.eulerAngles + fixRot;
    }
}
