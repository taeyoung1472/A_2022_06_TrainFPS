using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUiManager : MonoBehaviour
{
    [SerializeField] private Score socrePrefab;

    #region 사운드
    [Header("사운드")]
    [SerializeField] private AudioClip defaultSound;
    [SerializeField] private AudioClip enemyKillSound;
    [SerializeField] private AudioClip HeadShootSound;
    [SerializeField] private AudioClip nextStageSound;
    [SerializeField] private AudioClip otherSound;
    #endregion
    public static ScoreUiManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void Add(string str, int score, ScoreSoundType soundType)
    {
        Score scoreUi = Instantiate(socrePrefab, transform);
        AudioClip callbackClip = null;

        #region 사운드 제어
        switch (soundType)
        {
            case ScoreSoundType.None:
                break;
                callbackClip = null;
            case ScoreSoundType.EnemyKill:
                callbackClip = enemyKillSound;
                break;
            case ScoreSoundType.HeadShoot:
                callbackClip = HeadShootSound;
                break;
            case ScoreSoundType.NextStage:
                callbackClip = nextStageSound;
                break;
            case ScoreSoundType.Other:
                callbackClip = otherSound;
                break;
        }
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(callbackClip, 1f, Random.Range(0.9f, 1.1f));
        #endregion

        scoreUi.gameObject.SetActive(true);
        scoreUi.Set(str, score, () => Destroy(scoreUi.gameObject));
    }
}
public enum ScoreSoundType
{
    None,
    EnemyKill,
    HeadShoot,
    NextStage,
    Other,
}