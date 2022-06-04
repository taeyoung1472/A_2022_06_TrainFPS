using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    /// <summary>
    /// 정보 업데이트시 실행될 함수
    /// </summary>
    public void ObserverUpdate();
}
