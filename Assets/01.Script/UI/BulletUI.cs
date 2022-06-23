using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class BulletUI : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private TextMeshProUGUI tmp;
    [Header("»ç¿îµå")]
    [SerializeField] private AudioClip[] bulletDropClips;
    Queue<RectTransform> bulletQueue = new Queue<RectTransform>();
    public void Init(int maxBulletCount)
    {
        for (int i = 0; i < maxBulletCount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(true);
            bulletQueue.Enqueue(obj.GetComponent<RectTransform>());
        }
    }
    public void Reload()
    {
        tmp.color = Color.white;
        foreach (var bullet in bulletQueue)
        {
            bullet.gameObject.SetActive(true);
        }
    }
    public void ThrowBullet()
    {
        RectTransform rect = bulletQueue.Dequeue();
        rect.SetParent(canvas);
        rect.DOJump(new Vector2(rect.position.x + Random.Range(150f, 250f), rect.position.y - Random.Range(100, 150)), Random.Range(200f, 300f), 1, 0.5f);
        rect.DORotate(new Vector3(0, 0, Random.Range(200f, 360f) * (Random.Range(0, 2) == 1 ? -1 : 1)), 0.25f);
        transform.parent.DOShakePosition(0.1f, 10, 1000);
        StartCoroutine(DQ(rect));
    }
    public void DryBullet()
    {
        tmp.color = Color.red;
    }
    IEnumerator DQ(RectTransform rect)
    {
        yield return new WaitForSeconds(0.5f);
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(bulletDropClips[Random.Range(0, bulletDropClips.Length)], 0.5f, Random.Range(0.9f, 1.1f));
        rect.gameObject.SetActive(false);
        rect.rotation = Quaternion.identity;
        bulletQueue.Enqueue(rect);
        rect.SetParent(transform);
    }
}
