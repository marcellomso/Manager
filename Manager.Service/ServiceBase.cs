using Manager.Domain.Contracts.Repositories;
using Manager.SharedKernel;
using Manager.SharedKernel.Events;
using Manager.SharedKernel.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Manager.Service
{
    public class ServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IHandler<DomainNotification> _notifications;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _notifications = DomainEvent.Container.GetService<IHandler<DomainNotification>>();
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications())
                return false;

            _unitOfWork.Commit();
            return true;
        }

        protected bool ValidateObject(object obj, string msg)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(obj, msg)
            );
        }
    }
}
