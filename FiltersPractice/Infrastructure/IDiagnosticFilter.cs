using System.Collections.Generic;

namespace FiltersPractice.Infrastructure
{
    public interface IDiagnosticFilter
    {
        IEnumerable<string> Messages { get; }

        public void AddMessage(string message);
    }
}
