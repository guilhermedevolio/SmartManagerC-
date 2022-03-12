using System.Collections.Generic;

namespace SmartManager.Domain.Entities
{
    public abstract class Base {
        public long Id { get; set; }

        public List<String> _errors;

        public bool IsValid => _errors.Count > 0;

        public IReadOnlyCollection<String> Errors => _errors;

        public abstract bool Validate();
    }
}