using Manager.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Manager.SharedKernel.Validations
{
    public static class AssertionConcern
    {
        public static object DataHelper { get; private set; }

        public static bool IsSatisfiedBy(params DomainNotification[] validations)
        {
            var notificationsNotNull = validations.Where(validation => validation != null);
            NotifyAll(notificationsNotNull);

            return notificationsNotNull.Count().Equals(0);
        }

        private static void NotifyAll(IEnumerable<DomainNotification> notifications)
        {
            notifications.ToList().ForEach(validation =>
            {
                DomainEvent.Raise<DomainNotification>(validation);
            });
        }

        public static DomainNotification AssertLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = string.IsNullOrEmpty(stringValue)
                ? 0
                : stringValue.Trim().Length;

            return (length < minimum || length > maximum) ?
                new DomainNotification("AssertArgumentLength", message) : null;
        }

        public static DomainNotification AssertMatches(string pattern, string stringValue, string message)
        {
            Regex regex = new Regex(pattern);

            return (!regex.IsMatch(stringValue)) ?
                new DomainNotification("AssertArgumentLength", message) : null;
        }

        public static DomainNotification AssertNotEmpty(string stringValue, string message)
        {
            return (string.IsNullOrEmpty(stringValue)) ?
                new DomainNotification("AssertArgumentNotEmpty", message) : null;
        }

        public static DomainNotification AssertIsNull(object object1, string message)
        {
            return (object1 != null)
                ? new DomainNotification("AssertIsNull", message)
                : null;
        }

        public static DomainNotification AssertNotNull(object object1, string message)
        {
            return (object1 == null) ?
                new DomainNotification("AssertArgumentNotNull", message) : null;
        }

        public static DomainNotification AssertTrue(bool boolValue, string message)
        {
            return (!boolValue) ?
                new DomainNotification("AssertArgumentTrue", message) : null;
        }

        public static DomainNotification AssertAreEquals<T>(T value, T match, string message)
        {
            return !Equals(value, match) ?
                new DomainNotification("AssertAreEquals", message) : null;
        }

        public static DomainNotification AssertAreDiferents<T>(T value, T match, string message)
        {
            return Equals(value, match) ?
                new DomainNotification("AssertAreDiferents", message) : null;
        }

        public static DomainNotification AssertIsGreaterThan(int value1, int value2, string message)
        {
            return (!(value1 > value2)) ?
                new DomainNotification("AssertIsGreaterThan", message) : null;
        }

        public static DomainNotification AssertIsGreaterThan(decimal value1, decimal value2, string message)
        {
            return (!(value1 > value2)) ?
                new DomainNotification("AssertIsGreaterThan", message) : null;
        }

        public static DomainNotification AssertIsGreaterOrEqualThan(int value1, int value2, string message)
        {
            return (!(value1 >= value2)) ?
                new DomainNotification("AssertIsGreaterOrEqualThan", message) : null;
        }

        public static DomainNotification AssertIsGreaterOrEqualThan(DateTime value1, DateTime value2, string message)
        {
            return (!(value1 >= value2)) ?
                new DomainNotification("AssertIsGreaterOrEqualThan", message) : null;
        }

        public static DomainNotification AssertIsGreaterOrEqualThan(decimal value1, decimal value2, string message)
        {
            return (!(value1 >= value2)) ?
                new DomainNotification("AssertIsGreaterOrEqualThan", message) : null;
        }

        public static DomainNotification AssertIsRange(double value, double minimum, double maximum, string message)
        {
            return (value < minimum || value > maximum) ?
                new DomainNotification("AssertIsRange", message) : null;
        }

        public static DomainNotification AssertIsRange(float value, float minimum, float maximum, string message)
        {
            return (value < minimum || value > maximum) ?
                new DomainNotification("AssertIsRange", message) : null;
        }

        public static DomainNotification AssertIsRange(int value, int minimum, int maximum, string message)
        {
            return (value < minimum || value > maximum) ?
                new DomainNotification("AssertIsRange", message) : null;
        }
    }
}
