namespace HW
{
    public abstract class SingletonBehaviour<T> : HWBehaviour
    where T : SingletonBehaviour<T>
    {
        public static T Inst { get; private set; }
        private void Awake()
        {
            if (Inst == null)
            {
                Inst = this as T;
            }
        }
    }
}
