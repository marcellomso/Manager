﻿using Manager.SharedKernel.Events.Contracts;
using System;

namespace Manager.SharedKernel.Events
{
    public class DomainNotification : IDomainEvent
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime DateOcurred { get; private set; }

        public DomainNotification(string key, string value)
        {
            this.Key = key;
            this.Value = value;
            this.DateOcurred = DateTime.Now;
        }
    }
}
