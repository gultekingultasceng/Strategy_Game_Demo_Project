using System;

namespace EventHandler
{
    public class Publisher<T>
    {
        public Action<Publisher<T>, T> OnPublish;
        public void Publish(T obj)
        {
            OnPublish?.Invoke(this, obj);
        }
    }
    public class Publisher<TParam1, TParam2>
    {
        public Action<Publisher<TParam1, TParam2>, TParam1, TParam2> OnPublish;
        public void Publish(TParam1 param1, TParam2 param2)
        {
            OnPublish?.Invoke(this, param1, param2);
        }
    }
}
  

