using System;

namespace BankingApp.Common.Events
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent eventToPublish);
        void Subscribe<TEvent>(Action<TEvent> action);
    }
}
