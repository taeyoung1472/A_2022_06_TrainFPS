using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/SO/Sound/HitFeedback")]
public class FeedbackSoundDataSO : ScriptableObject
{
    [SerializeField] private AudioClip[] woodFeedback;
    [SerializeField] private AudioClip[] metalFeedbacl;
    [SerializeField] private AudioClip[] concreteFeedback;
    [SerializeField] private AudioClip[] scrapFeedback;

    /// <summary>
    /// 타입에 대응하는 랜덤 사운드 받기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public AudioClip GetAudio(HitFeedbackType type)
    {
        AudioClip returnClip = null;
        switch (type)
        {
            case HitFeedbackType.wood:
                returnClip = woodFeedback[Random.Range(0, woodFeedback.Length)];
                break;
            case HitFeedbackType.metal:
                returnClip = metalFeedbacl[Random.Range(0, metalFeedbacl.Length)];
                break;
            case HitFeedbackType.concrete:
                returnClip = concreteFeedback[Random.Range(0, concreteFeedback.Length)];
                break;
            case HitFeedbackType.scrap:
                returnClip = scrapFeedback[Random.Range(0, scrapFeedback.Length)];
                break;
        }
        return returnClip;
    }
}
public enum HitFeedbackType
{
    wood,
    metal,
    concrete,
    scrap
}