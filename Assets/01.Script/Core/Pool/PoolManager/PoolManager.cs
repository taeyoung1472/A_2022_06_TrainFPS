using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;//싱글톤
    [SerializeField] private PoolData[] poolAbleDatas;//풀링 타입 갯수만큼 만들기
    Dictionary<PoolType, LocalPoolManager> localPoolDic = new Dictionary<PoolType, LocalPoolManager>();//PoolType으로 검색하기 위한 딕셔너리
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < poolAbleDatas.Length; i++)
        {
            LocalPoolManager localPool = Instantiate(new GameObject(), transform).AddComponent<LocalPoolManager>();
            PoolData data = poolAbleDatas[i];
            localPool.Init(data.InitCount, data.PoolAbleObject, data.PoolType);
            localPoolDic.Add(data.PoolType, localPool);
            localPool.name = $"{localPool.name} : {data.PoolType}";
        }
    }
    /// <summary>
    /// Type에 맞는 오브젝트 꺼내오기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject Pop(PoolType type)
    {
        return localPoolDic[type].Pop().gameObject;
    }
    /// <summary>
    /// Type에 맞게 오브젝트 넣기
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public void Push(PoolType type, GameObject obj)
    {
        localPoolDic[type].Push(obj.GetComponent<PoolAbleObject>());
    }
}
[Serializable]
public class PoolData/*풀링 데이터*/
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private int initCount;
    [SerializeField] private PoolAbleObject poolAbleObject;
    public PoolType PoolType { get { return poolType; } }
    public int InitCount { get { return initCount; } }
    public PoolAbleObject PoolAbleObject { get { return poolAbleObject; } }
}
public enum PoolType/*풀링 타입(풀링할 에들이 많아지면 계속 추가*/
{
    Sound,
    Train,
    BulletImpact,
}