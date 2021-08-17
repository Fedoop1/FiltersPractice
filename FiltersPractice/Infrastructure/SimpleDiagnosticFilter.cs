using System.Collections.Generic;

namespace FiltersPractice.Infrastructure
{
    public class SimpleDiagnosticFilter : IDiagnosticFilter
    {
        private readonly List<string> messages = new List<string>();

        public IEnumerable<string> Messages => this.messages;
        public void AddMessage(string message) => this.messages.Add(message);
    }
}
