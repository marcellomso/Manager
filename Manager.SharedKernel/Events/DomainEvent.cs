using Manager.SharedKernel.Events.Contracts;
using System;

namespace Manager.SharedKernel.Events
{
    public static class DomainEvent
    {
        public static IServiceProvider Container { get; set; }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            try
            {
                if (Container != null)
                {
                    var obj = Container.GetService(typeof(IHandler<T>));
                    ((IHandler<T>)obj).Handle(args);
                }
            }
            catch
            {
                //throw;
            }
        }

    }
}
