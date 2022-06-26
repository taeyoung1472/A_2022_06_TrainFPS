using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModingDisplay : MonoBehaviour
{
    [SerializeField] private GameObject preview;
    [SerializeField] private float senservity = 250;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float x = Input.GetAxis("Mouse X");
            preview.transform.localRotation *= Quaternion.Euler(0, 0, senservity * x * Time.deltaTime);
        }
    }
}
