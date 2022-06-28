using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class SpriteButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onClickEvent;
    public void OnMouseDown()
    {
        onClickEvent?.Invoke();
    }
}
