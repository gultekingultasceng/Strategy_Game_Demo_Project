using System;
namespace EventHandler
{
    public static class Subscriber<T>
    {
        public static void Subscribe(Publisher<T> p, Action<Publisher<T>, T> e)
        {
            p.OnPublish += e; 
        }
        public static void Unsubscribe(Publisher<T> p, Action<Publisher<T>, T> e)
        {
            p.OnPublish -= e;
        }
    }

    public static class Subscriber<TParam1, TParam2>
    {
        public static void Subscribe(Publisher<TParam1, TParam2> p, Action<Publisher<TParam1, TParam2>, TParam1, TParam2> e)
        {
            p.OnPublish += e;

        }
        public static void Unsubscribe(Publisher<TParam1, TParam2> p, Action<Publisher<TParam1, TParam2>, TParam1, TParam2> e)
        {
            p.OnPublish -= e;
        }
    }
}
