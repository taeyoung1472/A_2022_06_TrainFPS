using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float length;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (transform.position.z < -length)
        {
            transform.position += new Vector3(0, 0, length);
        }
    }
}
