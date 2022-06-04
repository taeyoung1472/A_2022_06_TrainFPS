using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    /// <summary>
    /// ������ ���
    /// </summary>
    /// <param name="observer"></param>
    public void RegisterObserver(IObserver observer);
    /// <summary>
    /// ������ ��� ����
    /// </summary>
    /// <param name="observer"></param>
    public void RemoveObserver(IObserver observer);
    /// <summary>
    /// ������������ ���� ����
    /// </summary>
    public void NotifyObserver();
}
