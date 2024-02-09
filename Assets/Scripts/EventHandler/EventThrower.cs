using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventHandler
{
    public class EventThrower<T>
    {
        public Action<T> myaction;

        public void Throw(T data)
        {
            myaction?.Invoke(data);
        }
        /*
         THROWER
         Action<Vector3> myaction;
         myaction?.Invoke(mousepos);
         */
    
    }
}


/*
   CATCHER
   
   Thrower.myaction += Mouseclicked;
   Thrower.myaction -= MouseClicked;
   
   void MouseClicked (Vector3 _catchedmousepos)
   {
           
   }
 */
