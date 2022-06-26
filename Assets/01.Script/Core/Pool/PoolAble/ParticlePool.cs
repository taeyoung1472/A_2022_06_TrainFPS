using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : PoolAbleObject
{
    [SerializeField] private float poolTime;
    public override void Init_Pop()
    {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(Wait());
    }

    public override void Init_Push()
    {

    }

    public void Set(RaycastHit info)
    {
        Quaternion rot = Quaternion.LookRotation(info.normal);
        transform.position = info.point + info.normal * 0.15f;
        transform.rotation = rot;
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(poolTime);
        PoolManager.instance.Push(PoolType, gameObject);
    }
}
