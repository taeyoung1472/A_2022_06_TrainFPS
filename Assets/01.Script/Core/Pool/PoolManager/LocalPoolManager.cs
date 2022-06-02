using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPoolManager : MonoBehaviour
{
    PoolAbleObject poolTarget;
    public PoolAbleObject PoolTarget { get { return poolTarget; } set { poolTarget = value; } }
    Queue<PoolAbleObject> poolQueue = new Queue<PoolAbleObject>();
    PoolType type;
    /// <summary>
    /// �ʱ� ���� ������ŭ ������Ʈ �ν��Ͻ�ȭ
    /// </summary>
    /// <param name="����"></param>
    /// <param name="������Ʈ"></param>
    /// <param name="Ǯ Ÿ��"></param>
    public void Init(int count, PoolAbleObject targetObj, PoolType poolType)
    {
        type = poolType;
        poolTarget = targetObj;
        for (int i = 0; i < count; i++)
        {
            PoolAbleObject obj = Instantiate(PoolTarget, transform);
            obj.gameObject.SetActive(false);
            obj.PoolType = type;
            poolQueue.Enqueue(obj);
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <returns></returns>
    public PoolAbleObject Pop()
    {
        PoolAbleObject obj;
        if (poolQueue.Count > 0)
        {
            obj = poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate(poolTarget, transform);
            obj.PoolType = type;
        }
        obj.Init_Pop();
        return obj;
    }
    /// <summary>
    /// �ֱ�
    /// </summary>
    /// <param name="obj"></param>
    public void Push(PoolAbleObject obj)
    {
        obj.Init_Push();
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}