using System.Collections.Generic;
using UnityEngine;

public abstract class GlobalEventBase
{
    protected string _name;
    protected bool _shouldLogErrorWithNoListener = false;
    protected bool _logEnabled = false;

    public GlobalEventBase(string name, bool shouldLogErrorWithNoListener, bool logEnabled)
    {
        _name = name;
        _shouldLogErrorWithNoListener = shouldLogErrorWithNoListener;
        _logEnabled = logEnabled;
    }

    protected void addListener<ConnectionDelegate>(Dictionary<object, ConnectionDelegate> listeners, object listener, ConnectionDelegate connection)
    {
        if (!listeners.ContainsKey(listener))
        {
            if (_logEnabled)
            {
                Debug.Log(nameof(GlobalEvent) + "-> " + listener.ToString() + " is now listening to event: " + _name);
            }

            listeners.Add(listener, connection);
        }
        else
        {
            Debug.LogWarning(nameof(GlobalEvent) + " -> " + listener.ToString() + " is already listening to event: " + _name);
        }
    }

    protected void removeListener<ConnectionDelegate>(Dictionary<object, ConnectionDelegate> connections, object listener)
    {
        if (connections.ContainsKey(listener))
        {
            if (_logEnabled)
            {
                Debug.Log(nameof(GlobalEvent) + "-> " + listener.ToString() + " is no longer listening to event: " + _name);
            }

            connections.Remove(listener);
        }
        else if (_logEnabled)
        {
            Debug.LogWarning(nameof(GlobalEvent) + "-> " + listener.ToString() + " was not listening to event: " + _name);
        }
    }
}

public class GlobalEvent : GlobalEventBase
{
    public delegate void Connection(object dispatcher);
    private Dictionary<object, Connection> _listeners = new Dictionary<object, Connection>();

    public GlobalEvent(string messageName, bool shouldLogErrorWithNoListener = false, bool logMessages = false) : base(messageName, shouldLogErrorWithNoListener, logMessages) { }

    public void Dispatch(object dispatcher)
    {
        if (_logEnabled)
        {
            Debug.Log(nameof(GlobalEvent) + "-> " + dispatcher.ToString() + " has fired event: " + _name);
        }

        if (_listeners.Count == 0)
        {
            if (_shouldLogErrorWithNoListener)
            {
                Debug.LogWarning(nameof(GlobalEvent) + " ->  There were no listeners to event: " + _name + " from dispatcher: " + dispatcher.ToString());
            }
        }
        else
        {
            foreach (object listener in _listeners.Keys)
            {
                if (_logEnabled)
                {
                    Debug.Log(nameof(GlobalEvent) + " -> " + listener.ToString() + " has recieved event: " + _name + " from dispatcher: " + dispatcher.ToString());
                }

                _listeners[listener](dispatcher);
            }
        }
    }

    public void AddListener(object listener, Connection callback)
    {
        addListener<Connection>(_listeners, listener, callback);
    }

    public void RemoveListener(object listener)
    {
        removeListener<Connection>(_listeners, listener);
    }
}

public class GlobalEvent<Parameter> : GlobalEventBase
{
    public delegate void Connection(object dispatcher, Parameter parameter);

    private Dictionary<object, Connection> _listeners = new Dictionary<object, Connection>();

    public GlobalEvent(string messageName, bool shouldLogErrorWithNoListener = false, bool logMessages = false) : base(messageName, shouldLogErrorWithNoListener, logMessages) { }

    public void Dispatch(object dispatcher, Parameter arg)
    {
        if (_logEnabled)
        {
            Debug.Log(nameof(GlobalEvent) + " -> " + dispatcher.ToString() + " has fired event: " + _name + " with argument: " + arg);
        }

        if (_listeners.Count == 0)
        {
            if (_shouldLogErrorWithNoListener)
            {
                Debug.LogError(nameof(GlobalEvent) + " -> There were no listeners to event: " + _name + " from dispatcher: " + dispatcher.ToString());
            }
        }
        else
        {
            foreach (object listener in _listeners.Keys)
            {
                if (_logEnabled)
                {
                    Debug.Log(nameof(GlobalEvent) + "-> " + listener.ToString() + " has received argument: " + arg.ToString() + " from dispatcher: " + dispatcher.ToString() + " through event: " + _name);
                }

                _listeners[listener](dispatcher, arg);
            }
        }
    }

    public void AddListener(object listener, Connection callback)
    {
        addListener<Connection>(_listeners, listener, callback);
    }

    public void RemoveListener(object listener)
    {
        removeListener<Connection>(_listeners, listener);
    }
}

//public class GenericGlobalEvent<EventSubclass, EventParameter> : GlobalEvent<EventParameter> where EventSubclass : GenericGlobalEvent<EventSubclass, EventParameter>
//{
//    public GenericGlobalEvent(string messageName, bool shouldLogErrorWithNoListener = false, bool logMessages = false) : base(messageName, shouldLogErrorWithNoListener, logMessages) { }
//}

