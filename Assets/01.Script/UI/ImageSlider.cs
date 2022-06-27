using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image background;
    Vector2 fullSize;
    void Start()
    {
        fill.rectTransform.sizeDelta = background.rectTransform.sizeDelta;
        fullSize = background.rectTransform.sizeDelta;
    }
    public void SetValue(float value, float maxValue)
    {
        if(value < 0)
        {
            value = 0;
        }
        fill.rectTransform.sizeDelta = new Vector2(fullSize.x * (value / maxValue), fullSize.y);
    }
}
