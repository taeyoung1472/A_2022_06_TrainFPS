using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image background;
    void Start()
    {
        fill.rectTransform.sizeDelta = background.rectTransform.sizeDelta;
    }
}
