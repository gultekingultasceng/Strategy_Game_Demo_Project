namespace SGD.Core.ObjectPooling
{
    public interface IEnableDisable
    {
        void PerformOnEnable();
        void PerformOnDisable();
    }

    public interface IEnableDisable<T>
    {
        void PerformOnEnable(T parameter1); // For Object Pulled From The Pool. The Goal is set Position
        void PerformDisable();
    }
}





