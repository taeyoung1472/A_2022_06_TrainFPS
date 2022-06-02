using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    #region 카메라 떨림 변수
    [Header("카메라 떨림 변수")]
    [Range(0.01f, 0.1f)] [SerializeField] private float shakeForce = 0.05f;
    [SerializeField] private float shakeForceLimit;
    Vector3 originPos;
    #endregion

    #region 추가 카메라 떨림 변수
    [SerializeField] private float addictiveShakeForce;
    #endregion
    private void Start()
    {
        originPos = transform.localPosition;
    }
    void Update()
    {
        Shake();
    }
    /// <summary>
    /// 직접적인 카메라 흔들림
    /// </summary>
    void Shake()
    {
        float totalForce = shakeForce + addictiveShakeForce;
        totalForce = Mathf.Clamp(totalForce, 0, shakeForceLimit);
        transform.localPosition = Random.insideUnitSphere * totalForce + originPos;
    }
    /// <summary>
    /// 카메라 흔들림 함수
    /// </summary>
    /// <param name="force"></param>
    /// <param name="time"></param>
    public void Shake(float force, float time)
    {
        addictiveShakeForce += force;
        StartCoroutine(ShakeEndCor(force, time));
    }
    /// <summary>
    /// 카메라 흔들림 멈추기용 코루틴
    /// </summary>
    /// <param name="force"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ShakeEndCor(float force, float time)
    {
        yield return new WaitForSeconds(time);
        addictiveShakeForce -= force;
    }
}
