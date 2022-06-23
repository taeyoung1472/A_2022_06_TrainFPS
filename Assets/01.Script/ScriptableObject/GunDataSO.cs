using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/SO/Gun/Gun")]
public class GunDataSO : ScriptableObject
{
    [Header("기본재원")]
    public string gunName = "총 이름";
    public string gunDescription = "총 설명";
    public Vector3 zoomPos;
    [Range(0.05f, 1f)] public float fireDelay = 0.1f;
    [Range(0.5f, 5f)] public float reloadDelay = 2.5f;
    [Range(0.1f, 1f)] public float zoomDelay = 0.2f;
    [Range(1, 100)] public int bulletAmount = 30;
    [Header("성능재원")]
    public Vector2 recoil = Vector2.one;
    [Range(10,100)] public float dmg = 25;

    [Header("사운드")]
    public AudioClip fireClip;
    public AudioClip magOutClip;
    public AudioClip magInClip;
    public AudioClip cokeClip;
    public AudioClip dryFireClip;
}
