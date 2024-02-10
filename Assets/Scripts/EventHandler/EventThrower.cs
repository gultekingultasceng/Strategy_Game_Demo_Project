using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventHandler
{
    public class EventThrower<T>
    {
        public Action<T> MyAction;
        public void Throw(T data)
        {
            MyAction?.Invoke(data);
        }
    
    }
}

