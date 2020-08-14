namespace HW
{
    public abstract class SingletonMonoBehaviour<T> : HWBehaviour
    where T : SingletonMonoBehaviour<T>
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
