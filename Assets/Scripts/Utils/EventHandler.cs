using System;
using UnityEngine;

public class EventHandler
{
    public static event Action<int> GetPointEvent;

    public static void CallGetPointEvent(int point)
    {
        GetPointEvent?.Invoke(point);
    }
}
