using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventHandler
{
    public static class EventCatcher<T>
    {
        public static void Catch(EventThrower<T> thrower, Action<T> connectedFunc)
        {
            thrower.MyAction += connectedFunc;
        }
        public static void ReleaseEvent(EventThrower<T> thrower, Action<T> connectedFunc)
        {
            thrower.MyAction -= connectedFunc;
        }
    }
    public static class EventCatcher
    {
        public static void Catch(EventThrower thrower, Action connectedFunc)
        {
            thrower.MyAction += connectedFunc;
        }
        public static void ReleaseEvent(EventThrower thrower, Action connectedFunc)
        {
            thrower.MyAction -= connectedFunc;
        }
    }
}

