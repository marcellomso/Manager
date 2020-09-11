using Manager.SharedKernel;
using Manager.SharedKernel.Events;
using System.Collections.Generic;

namespace Manager.CrossCutting
{
    public class DomainNotificationHandler : IHandler<DomainNotification>
    {
        private List<DomainNotification> _notificacoes;

        public DomainNotificationHandler()
        {
            _notificacoes = new List<DomainNotification>();
        }

        public void Handle(DomainNotification args)
        {
            _notificacoes.Add(args);
        }

        public IEnumerable<DomainNotification> Notify()
        {
            return GetValue();
        }

        private List<DomainNotification> GetValue()
        {
            return _notificacoes;
        }

        public bool HasNotifications()
        {
            return GetValue().Count > 0;
        }

        public void Dispose()
        {
            this._notificacoes = new List<DomainNotification>();
        }
    }
}
