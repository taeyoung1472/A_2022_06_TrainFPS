using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/SO/Enemy/Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("���� ���")]
    public string enemyName = "New Enemy";
    public int score = 100;
    public float hp = 100;

    [Header("���� ���")]
    [Range(0.5f, 10f)] public float attackDelay = 2.5f;
    [Range(1, 100)] public float attackRange = 25;
    [Range(0.1f, 1f)] public float actually = 0.5f;

    [Header("���� ���")]
    public int attackPerShoot = 3;
    public float shootDelay = 0.2f;
    public float damage = 25;

    [Header("����")]
    public AudioClip fireClip;
    public AudioClip[] hitClips;
}
