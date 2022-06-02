using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolAbleObject : MonoBehaviour
{
    protected PoolType poolType;
    public PoolType PoolType { get { return poolType; } set { poolType = value; } }
    /// <summary>
    /// �����ö� �ʱ�ȣ
    /// </summary>
    public abstract void Init_Pop();
    /// <summary>
    /// ������ �ʱ�ȭ
    /// </summary>
    public abstract void Init_Push();
}