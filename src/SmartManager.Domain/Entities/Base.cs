using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartManager.Domain.Entities
{
    public abstract class Base {
        public long Id { get; set; }

        public DateTime CreatedAt = DateTime.UtcNow;

        [JsonIgnore]
        public List<String> _errors;

        // public bool IsValid => _errors.Count > 0;
        [JsonIgnore]
        public IReadOnlyCollection<String> Errors => _errors;
        public abstract bool Validate();
    }
}