using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    #region ī�޶� ���� ����
    [Header("ī�޶� ���� ����")]
    [Range(0.01f, 0.1f)] [SerializeField] private float shakeForce = 0.05f;
    [SerializeField] private float shakeForceLimit;
    Vector3 originPos;
    #endregion

    #region �߰� ī�޶� ���� ����
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
    /// �������� ī�޶� ��鸲
    /// </summary>
    void Shake()
    {
        float totalForce = shakeForce + addictiveShakeForce;
        totalForce = Mathf.Clamp(totalForce, 0, shakeForceLimit);
        transform.localPosition = Random.insideUnitSphere * totalForce + originPos;
    }
    /// <summary>
    /// ī�޶� ��鸲 �Լ�
    /// </summary>
    /// <param name="force"></param>
    /// <param name="time"></param>
    public void Shake(float force, float time)
    {
        addictiveShakeForce += force;
        StartCoroutine(ShakeEndCor(force, time));
    }
    /// <summary>
    /// ī�޶� ��鸲 ���߱�� �ڷ�ƾ
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
