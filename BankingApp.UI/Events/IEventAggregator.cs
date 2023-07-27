using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.UI.Events
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent eventToPublish);
        void Subscribe<TEvent>(Action<TEvent> action);
    }
}
