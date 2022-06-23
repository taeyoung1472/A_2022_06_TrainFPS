using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAudioFeedBack : MonoBehaviour, IHitAble
{
    [SerializeField] protected AudioClip feedbackClip;
    public virtual void Hit(float power, Vector3 shootPos)
    {
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(feedbackClip, 1, Random.Range(0.75f, 1.25f));
    }
}
