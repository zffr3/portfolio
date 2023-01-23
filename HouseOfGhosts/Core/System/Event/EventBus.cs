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
    START_HUNTING,
    STOP_HUNTING,
    VICTIM_SELECTED,
    CURVEDITEM_TAKED,
    NORMALITEM_TAKED,
    GHOSTTARGET_CHANGED,
    HIDE_PLAER,
    RELEASE_PLAYER,
    KILL_PLAYER,
    KILL_DEMON,
    INTERACT_PERFORMED,
    INTERACT_BUTTON_VALUE,
    PLAYER_STATAE_CHANGED,
    CANDLE_ACTIVATED,
    CANDLE_DIACTIVATED,
    GHOSTEVENT_ACTIVATION_CHANGED,
    GHOSTSENS_PLAYERSTATUS_CHANGED,
    LEFTITEM_PRESSED,
    RIGHTITEM_PRESSED,
    SPRINT_PRESSED,
    SPRINT_RELEASED,
    TRAININGSTATE_CHANGED,
    TRAININGCAM_ITEMFINDED,
    EMF_HIGHLITED,
    RADIO_ACTIVATED,
    DEMON_BURNED,
    MUSICVOLUME_CHANGED,
    SOUNDVOLUME_CHANGED,
    SENSVALUE_CHANGED,
    LANGUAGE_CHANGED,
    PAUSESTATE_CHANGED,
    GAMELOGO_CLOSED,
    CURSEDITEMS_COUNT_CHANGED,
    ALTAR_ACTIVATED,
    GENERATOR_ACTIVATED,
    NET_KEY_ACTIVATED
}
