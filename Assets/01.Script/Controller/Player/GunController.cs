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
    #region 참조
    [Header("참조")]
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform fireTrans;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BulletUI bulletUI;
    Vector3 gunOriginPos;
    Camera cam;
    #endregion

    #region 총구 파티클
    [Header("파티클")]
    [SerializeField] private ParticleSystem[] flashEffects;
    #endregion

    #region 레이어
    [Header("레이어")]
    [SerializeField] private LayerMask layerMask;
    #endregion

    #region 총기 제어 변수
    int curBullet;
    #endregion

    #region 포스트 프로세싱
    [SerializeField] private Volume volume;
    DepthOfField dof;
    #endregion

    #region 총기 제어 Bool 변수들
    bool isCanReload = true;
    bool isCanFire = true;
    bool isCanZoom = true;
    bool isZoom = false;
    #endregion

    #region 애니메이터 Hash
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
    /// 스크립트 최초 실행 함수
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
    /// 전반적은 총의 시스템을 담당하는 코루틴
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
    /// 키입력 체크
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
    /// 반동
    /// </summary>
    void Recoil()
    {
        Vector2 recoil = new Vector2(gunData.recoil.x * Random.Range(-0.25f, 1f), gunData.recoil.y * Random.Range(0.75f, 1.25f));
        playerController.Recoil(-recoil, gunData.fireDelay);
    }

    #region 총기 제어
    /// <summary>
    /// 총 발사 함수
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
    /// 장전
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

    #region 조준 코드
    /// <summary>
    /// 조준
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
    /// 줌이 끝날시 실행되는 함수
    /// </summary>
    private void ZoomInOutEnd()
    {
        isCanZoom = true;
        dof.active = !isZoom;
    }
    #endregion
}
