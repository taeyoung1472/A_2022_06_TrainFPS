using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
    [SerializeField] private GunDataSO gunData;
    [SerializeField] private Transform gunTransform;
    [SerializeField] Animator animator;
    Camera mainCam;
    Vector3 gunOriginPos;
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
    private readonly int ZoomHash = Animator.StringToHash("Zoom");
    private readonly int ZoomInHash = Animator.StringToHash("ZoomIn");
    private readonly int ZoomOutHash = Animator.StringToHash("ZoomOut");
    #endregion
    void Start()
    {
        volume.profile.TryGet<DepthOfField>(out dof);
        mainCam = Camera.main;
        gunOriginPos = gunTransform.localPosition;
        StartCoroutine(GunSystem());
    }
    private void Update()
    {
        CheckInput();
    }
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
    /// 전반적은 총의 시스템을 담당하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator GunSystem()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetButton("Fire1"));
            Fire();
            yield return new WaitForSeconds(gunData.fireDelay);
        }
    }
    public void Fire()
    {
        animator.SetTrigger(FireHash);
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(gunData.fireClip);
    }
    void Reload()
    {

    }
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
    public void ZoomInOutEnd()
    {
        isCanZoom = true;
        dof.active = !isZoom;
    }
}
