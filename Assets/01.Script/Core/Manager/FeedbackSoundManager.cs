using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSoundManager : MonoBehaviour
{
    public static FeedbackSoundManager Instance;
    [SerializeField] private FeedbackSoundDataSO data;
    public FeedbackSoundDataSO Data { get { return data; } }
    public void Awake()
    {
        Instance = this;
    }
}
