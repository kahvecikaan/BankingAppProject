using System;
using System.Collections.Generic;

namespace BankingApp.Common.Events
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<Action<object>>> _eventSubscribers = new Dictionary<Type, List<Action<object>>>();

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            var eventType = typeof(TEvent);
            if (_eventSubscribers.ContainsKey(eventType))
            {
                var eventActions = _eventSubscribers[eventType];
                foreach(var eventAction in eventActions)
                {
                    eventAction(eventToPublish);
                }
            }
        }
        public void Subscribe<TEvent>(Action<TEvent> action)
        {
            var eventType = typeof(TEvent);
            if (_eventSubscribers.ContainsKey(eventType))
            {
                _eventSubscribers[eventType].Add(obj => action((TEvent)obj));
            }
            else
            {
                _eventSubscribers[eventType] = new List<Action<object>> { obj => action((TEvent)obj) };
            }
        }
    }
}
