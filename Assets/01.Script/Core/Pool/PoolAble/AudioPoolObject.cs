using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPoolObject : PoolAbleObject
{
    private AudioSource source;
    public override void Init_Pop()
    {
        if(source == null)
        {
            source = GetComponent<AudioSource>();
        }
    }

    public override void Init_Push()
    {
        source.Stop();
    }
    /// <summary>
    /// 오디오 재생
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void Play(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        source.clip = clip;
        source.Play();
        WaitForPush(source.clip.length * 1.25f);
    }
    IEnumerator WaitForPush(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.instance.Push(PoolType, gameObject);
    }
}
