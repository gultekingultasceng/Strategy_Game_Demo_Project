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

    public class EventThrower<T, Tparameter1, Tparameter2>
    {
        public Action<T,Tparameter1 , Tparameter2> MyAction;

        public void Throw(T t,Tparameter1 parameter1, Tparameter2 parameter2)
        {
            MyAction?.Invoke(t,parameter1 , parameter2);
        }
    }

    public class EventThrower
    {
        public Action MyAction;

        public void Throw()
        {
            MyAction?.Invoke();
        }
    }
    
}

