using System.Collections.Generic;
using UnityEngine.Events;

public static class EventBus
{
    private static Dictionary<EventType, EventContainer> _eventList;

    private static void Initialize()
    {
        _eventList = new Dictionary<EventType, EventContainer>();
    }

    public static void SubscribeToEvent(EventType type, UnityAction<object, object> handler)
    {
        if (_eventList == null)
        {
            Initialize();
        }

        EventContainer container = null;
        if (_eventList.TryGetValue(type, out container))
        {
            container.AddHandler(handler);
        }
        else
        {
            container = new EventContainer();
            container.AddHandler(handler);
            _eventList.Add(type, container);
        }

    }

    public static void UnsubscribeFromEvent(EventType type, UnityAction<object, object> handler)
    {
        if (_eventList == null)
        {
            Initialize();
            return;
        }

        EventContainer container = null;
        if (_eventList.TryGetValue(type, out container))
        {
            container.RemoveHandler(handler);
        }
    }

    public static void Dispath(EventType type, object sender, object param)
    {
        if (_eventList == null)
        {
            Initialize();
            return;
        }

        EventContainer container = null;
        if (_eventList.TryGetValue(type, out container))
        {
            container.Invoke(sender,param);
        }
    }

    private class EventContainer
    {
        private List<UnityAction<object,object>> _handlers { get; set; }

        public void AddHandler(UnityAction<object, object> nHandler)
        {
            if (this._handlers == null)
            {
                Init();
            }

            this._handlers.Add(nHandler);
        }

        public void RemoveHandler(UnityAction<object, object> handler)
        {
            if (this._handlers == null)
            {
                Init();
                return;
            }

            this._handlers.Remove(handler);
        }

        public void Invoke(object sender, object param)
        {
            for (int i = 0; i < this._handlers.Count; i++)
            {
                this._handlers[i].Invoke(sender, param);
            }
        }

        private void Init()
        {
            this._handlers = new List<UnityAction<object, object>>();
        }
    }
}

public enum EventType
{
    PLAYER_RANK_UPPED,
    WEAPON_UPGRADED,
    HEALTH_UPGRADED,
    POWER_UPGRADED,
    PAUSE_STATE_CHANGED,
    PLAYERDATA_LOADED,
    UPGRADEDATA_LOADED,
    PLAYER_INITIALIZED,
    LANGUAGE_CHANGED,
    UI_PANEL_LOADED,
    VOLUME_CHANGED,
    MUSIC_CHANGED,
    SENS_CHANGED,
    REACTIVE_ZOMBIE_KILLED
}
