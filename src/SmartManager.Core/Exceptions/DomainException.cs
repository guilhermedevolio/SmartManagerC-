using System;
using System.Collections;

namespace SmartManager.Core.Exceptions {
    public class DomainException : Exception {
        public List<String> _errors;

        public List<String> GetErrors => _errors;

        public DomainException()
        {
            _errors = new List<String>();
        }

        public DomainException(string message) : base(message)
        { 
            if(_errors == null) {
                 _errors = new List<String>();
            }

            _errors.Add(message);
        }
    }
}