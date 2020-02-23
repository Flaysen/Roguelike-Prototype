using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    List<Observer> _observers = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Notify(object value, NotificationType notificationType)
    {
        foreach(var observer in _observers)
        {
            observer.OnNotify(value, notificationType);
        }
    }
}
