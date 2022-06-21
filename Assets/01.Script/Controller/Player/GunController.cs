using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
    #region ����
    [Header("����")]
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform fireTrans;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BulletUI bulletUI;
    Vector3 gunOriginPos;
    Camera cam;
    #endregion

    #region �ѱ� ��ƼŬ
    [Header("��ƼŬ")]
    [SerializeField] private ParticleSystem[] flashEffects;
    #endregion

    #region ���̾�
    [Header("���̾�")]
    [SerializeField] private LayerMask layerMask;
    #endregion

    #region �ѱ� ���� ����
    int curBullet;
    #endregion

    #region ����Ʈ ���μ���
    [SerializeField] private Volume volume;
    DepthOfField dof;
    #endregion

    #region �ѱ� ���� Bool ������
    bool isCanReload = true;
    bool isCanFire = true;
    bool isCanZoom = true;
    bool isZoom = false;
    #endregion

    #region �ִϸ����� Hash
    private readonly int FireHash = Animator.StringToHash("Fire");
    private readonly int ReloadHash = Animator.StringToHash("Reload");
    private readonly int ZoomHash = Animator.StringToHash("Zoom");
    private readonly int ZoomInHash = Animator.StringToHash("ZoomIn");
    private readonly int ZoomOutHash = Animator.StringToHash("ZoomOut");
    #endregion

    void Start()
    {
        Init();
    }
    private void Update()
    {
        CheckInput();
    }
    /// <summary>
    /// ��ũ��Ʈ ���� ���� �Լ�
    /// </summary>
    private void Init()
    {
        volume.profile.TryGet<DepthOfField>(out dof);
        cam = Camera.main;
        curBullet = gunData.bulletAmount;
        bulletUI.Init(gunData.bulletAmount);
        gunOriginPos = gunTransform.localPosition;
        StartCoroutine(GunSystem());
    }
    /// <summary>
    /// �������� ���� �ý����� ����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator GunSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0) && curBullet > 0);
            Fire();
            yield return new WaitForSeconds(gunData.fireDelay);
        }
    }
    /// <summary>
    /// Ű�Է� üũ
    /// </summary>
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Zoom();
        }
    }
    /// <summary>
    /// �ݵ�
    /// </summary>
    void Recoil()
    {
        Vector2 recoil = new Vector2(gunData.recoil.x * Random.Range(-0.25f, 1f), gunData.recoil.y * Random.Range(0.75f, 1.25f));
        playerController.Recoil(-recoil, gunData.fireDelay);
    }

    #region �ѱ� ����
    /// <summary>
    /// �� �߻� �Լ�
    /// </summary>
    private void Fire()
    {
        animator.SetTrigger(FireHash);
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(gunData.fireClip, 1, Random.Range(0.9f, 1.1f));
        RaycastHit hit;
        bulletUI.ThrowBullet();
        Debug.DrawRay(fireTrans.position, fireTrans.forward * 1000, Color.blue, 25f);
        if (Physics.Raycast(fireTrans.position, fireTrans.forward, out hit, 1000f, layerMask))
        {
            hit.transform.GetComponent<HitBox>()?.GetDamage(gunData.dmg, fireTrans.position);
            GameObject obj = null;

            obj?.SetActive(true);
        }
        foreach (var effect in flashEffects)
        {
            effect.Play();
        }
        curBullet--;
        Recoil();
    }
    /// <summary>
    /// ����
    /// </summary>
    private void Reload()
    {
        animator.SetTrigger(ReloadHash);
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(gunData.reloadDelay);
        seq.AppendCallback(() =>
        {
            curBullet = gunData.bulletAmount;
            bulletUI.Reload();
        });
    }
    #endregion

    #region ���� �ڵ�
    /// <summary>
    /// ����
    /// </summary>
    void Zoom()
    {
        if (!isCanZoom) return;
        isCanZoom = false;
        isZoom = !isZoom;
        if (isZoom)
        {
            gunTransform.DOLocalMove(gunData.zoomPos, 1f);
            animator.SetTrigger(ZoomInHash);
            animator.SetFloat(ZoomHash, 1);
        }
        else
        {
            gunTransform.DOLocalMove(gunOriginPos, 1f);
            animator.SetTrigger(ZoomOutHash);
            animator.SetFloat(ZoomHash, 0);
        }
        Invoke("ZoomInOutEnd", gunData.zoomDelay);
    }
    /// <summary>
    /// ���� ������ ����Ǵ� �Լ�
    /// </summary>
    private void ZoomInOutEnd()
    {
        isCanZoom = true;
        dof.active = !isZoom;
    }
    #endregion
}
