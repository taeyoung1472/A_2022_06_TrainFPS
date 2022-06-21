using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundEvent : MonoBehaviour
{
    [SerializeField] private GunDataSO gunData;
    #region 에니매이션 이벤트 함수 / 사운드
    private void PlayAudio(AudioClip clip, float volume = 1, float pitch = 1)
    {
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(clip, pitch, volume);
    }
    public void MagInSound()
    {
        PlayAudio(gunData.magInClip);
    }
    public void MagOutSound()
    {
        PlayAudio(gunData.magOutClip);
    }
    public void CokeSound()
    {
        PlayAudio(gunData.cokeClip);
    }
    #endregion
}
