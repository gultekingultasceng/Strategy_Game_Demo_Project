using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ObjectPoolingSystem
{
    public interface IPool<T, TParameter1, TParameter2>
    {
        T GetObject(TParameter1 parameter1, TParameter2 parameter2);
        void ReturnObject(T obj);
    }

    public abstract class Pool<T, TParameter1, TParameter2> : MonoBehaviour, IPool<T, TParameter1, TParameter2>
        where T : IEnableDisable
    {
        public abstract Factory<T , TParameter1 , TParameter2> Factory { get; set; }
        private Stack<T> pool = new Stack<T>();
        public T GetObject(TParameter1 parameter1, TParameter2 parameter2)
        {
            T obj =  pool.Count > 0 ? pool.Pop() : Factory.Create(parameter1, parameter2);
            obj.PerformOnEnable();
            return obj;
        }

        public void ReturnObject(T obj)
        {
            pool.Push(obj);
            obj.PerformOnDisable();
        }
    }
    
    
    public interface IPool<T,TUniqueID ,TParameter1, TParameter2, TParameter3>
    {
        T GetObject(TUniqueID uniqueID ,TParameter1 param1,TParameter2 param2,TParameter3 param3);
        void ReturnObject(T obj , TUniqueID id);
    }

    public abstract class Pool<T, TUniqueID, TParameter1, TParameter2, TParameter3> : MonoBehaviour,IPool<T,TUniqueID ,TParameter1, TParameter2, TParameter3> where T : IEnableDisable
    {
        public abstract Factory<T , TParameter1 , TParameter2, TParameter3> Factory { get; set; }
        //private Stack<T> pool = new Stack<T>();
        //private List<T> pool = new List<T>();
        private Dictionary<TUniqueID, List<T>> MyUniqueTypeForPool = new Dictionary<TUniqueID, List<T>>();
        public T GetObject(TUniqueID uniqueID,TParameter1 param1, TParameter2 param2, TParameter3 param3)
        {
            T obj = GetTObjectWithUniqueID(uniqueID, param1, param2, param3);
            return obj;
        }

        public T GetTObjectWithUniqueID(TUniqueID id , TParameter1 param1, TParameter2 param2, TParameter3 param3)
        {
            if (MyUniqueTypeForPool.TryGetValue(id , out List<T> objList) && objList.Count > 0)
            {
                T getFromList = objList[^1]; // Get Last Added
                objList.Remove(getFromList); // remove from list
                return getFromList;
            }
            else
            {
                T created = Factory.Create(param1,param2,param3);
                return created;
            }
        }
        public void ReturnObject(T obj , TUniqueID id)
        {
           if (MyUniqueTypeForPool.TryGetValue(id , out List<T> objList))
           {
               objList.Add(obj);
           }
           else
           {
               List<T> newlist = new List<T>(); // REGISTER AS LIST
               newlist.Add(obj);
               MyUniqueTypeForPool.Add(id,newlist);
           }
           obj.PerformOnDisable();
        }
    }


}



