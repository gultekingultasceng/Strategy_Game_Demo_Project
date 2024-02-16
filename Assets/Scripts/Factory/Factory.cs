using UnityEngine;
namespace SGD.Core.Factory
{
    public abstract class Factory<T> : MonoBehaviour
    {
        public abstract T Create();
    }
    
    public abstract class Factory<T, TParameter1 , TParameter2 , TParameter3> : MonoBehaviour
    {
        public abstract T Create(TParameter1 unitGameObject , TParameter2 parameter2 , TParameter3 parameter3);
    }

    public abstract class Factory<T, TParameter1, TParameter2> : MonoBehaviour
    {
        public abstract T Create(TParameter1 button , TParameter2 parameter2);
    }
}

