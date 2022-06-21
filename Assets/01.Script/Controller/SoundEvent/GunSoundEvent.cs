using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundEvent : MonoBehaviour
{
    [SerializeField] private GunDataSO gunData;
    #region ���ϸ��̼� �̺�Ʈ �Լ� / ����
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
