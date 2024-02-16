using System;
namespace SGD.Core.EventHandler
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

    public static class EventCatcher<T, TParameter1, TParameter2>
    {
        public static void Catch(EventThrower<T, TParameter1, TParameter2> thrower,
            Action<T, TParameter1, TParameter2> connectedFunc)
        {
            thrower.MyAction += connectedFunc;
        }
        public static void ReleaseEvent(EventThrower<T, TParameter1, TParameter2> thrower,
            Action<T, TParameter1, TParameter2> connectedFunc)
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

