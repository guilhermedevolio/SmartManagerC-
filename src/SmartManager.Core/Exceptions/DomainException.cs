using System;

namespace SmartManager.Core.Exceptions {
    public class DomainException : Exception {
        internal List<string> _errors;

        public IReadOnlyCollection<string> Errors => _errors;

        public DomainException()
        {}

        public DomainException(string message) : base(message)
        { }
    }
}