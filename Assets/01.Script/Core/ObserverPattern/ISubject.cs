using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    /// <summary>
    /// 옵저버 등록
    /// </summary>
    /// <param name="observer"></param>
    public void RegisterObserver(IObserver observer);
    /// <summary>
    /// 옵저버 등록 해지
    /// </summary>
    /// <param name="observer"></param>
    public void RemoveObserver(IObserver observer);
    /// <summary>
    /// 옵저버들한테 정보 발행
    /// </summary>
    public void NotifyObserver();
}
