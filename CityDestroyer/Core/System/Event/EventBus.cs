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
    SHOT_BTN_PRESSED,
    FRAGMENT_TOUCHED,
    BULLETS_FRAGMENT_TOUCHED,
    BULLETS_ENDED,
    BUILDING_INITIALIZED,
    STAR_TAKED,
    BULLET_COLISTION_ENTER,
    CAM_ANIMATION_ENDED,
    LEVEL_CLOSED,
    LEVEL_IND_LOADED,
    CAR_ANIM_ENDED,
    DATA_CB_LOADED,
    DATA_SHOP_LOADED,
    STARS_GIVEN_COMPLETE,
    AD_COMPLETED
}
