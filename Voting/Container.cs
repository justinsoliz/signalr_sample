using Ninject;

namespace Voting
{
    public class Container
    {
        private static IKernel _kernel;

        public static IKernel Init()
        {
            _kernel = new StandardKernel();
            return _kernel;
        }

        public static IKernel Kernel { get { return _kernel; } }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
