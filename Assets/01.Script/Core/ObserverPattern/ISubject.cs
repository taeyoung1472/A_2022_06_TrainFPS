using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    /// <summary>
    /// ������ ���
    /// </summary>
    /// <param name="_observer"></param>
    public void RegisterObserver(IObserver _observer);
    /// <summary>
    /// ������ ��� ����
    /// </summary>
    /// <param name="_observer"></param>
    public void RemoveObserver(IObserver _observer);
    /// <summary>
    /// ������������ ���� ����
    /// </summary>
    public void NotifyObserver();
}
