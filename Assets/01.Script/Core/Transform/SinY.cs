using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinY : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float size;
    Vector3 orgin;
    private void Start()
    {
        orgin = transform.localPosition;
    }
    void Update()
    {
        transform.localPosition = orgin + Vector3.up * (size * Mathf.Sin(Time.time * speed));
    }
}
