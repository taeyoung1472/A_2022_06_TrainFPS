using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opthics : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    [SerializeField] private Transform sight;
    [Range(1f, 0f)] [SerializeField] private float fixSenservityValue = 1f;
    void Start()
    {
        gunController.Sight = sight;
        gunController.FixSenservityValue = fixSenservityValue;
    }
}
