using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolAbleObject : MonoBehaviour
{
    protected PoolType poolType;
    public PoolType PoolType { get { return poolType; } set { poolType = value; } }
    /// <summary>
    /// 꺼내올때 초기호
    /// </summary>
    public abstract void Init_Pop();
    /// <summary>
    /// 넣을때 초기화
    /// </summary>
    public abstract void Init_Push();
}