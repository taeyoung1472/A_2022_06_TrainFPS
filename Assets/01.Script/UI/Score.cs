using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Image image;
    Action callback;
    public void Set(string str, int score, Action _callback)
    {
        SetTween();
        tmp.text = $"{str}(+{score})";
        callback = _callback;
    }

    private void SetTween()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(rect.DOScale(Vector3.zero, 1f).SetEase(Ease.InQuart));
        seq.Join(tmp.DOFade(0, 1f).SetEase(Ease.InQuart));
        seq.Join(image.DOFade(0, 1f).SetEase(Ease.InQuart));
        seq.AppendCallback(() => callback?.Invoke());
    }

    public void OnDisable()
    {
        
    }
}
