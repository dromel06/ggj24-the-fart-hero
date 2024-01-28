using UnityEngine;

public abstract class BaseState<StateSubClass, ContextType> where StateSubClass : BaseState<StateSubClass, ContextType>
{
    public string Name => GetType().Name;
    public StateSubClass PreviousState { get; set; } = default;

    private const string _LOG_LABEL_MESSAGE_PREFIX_SEPARATOR = " -> ";

    protected bool _enablesLogs = false;
    protected string _contextLabel = null;

    public BaseState(bool enablesLogs = false, string contextLabel = null)
    {
        _enablesLogs = enablesLogs;
        _contextLabel = contextLabel;
    }

    public virtual void HandleEnter(ContextType context)
    {
        log("Entered " + Name);
    }

    public virtual void HandleUpdate(ContextType context)
    {
        //log("Updated " + Name);
    }

    public virtual void HandleExit(ContextType context)
    {
        log("Exited " + Name);
    }

    protected virtual StateSubClass change(StateSubClass newState, ContextType context)
    {
        if (newState.Name != this.Name)
        {
            HandleExit(context);

            if (_enablesLogs)
            {
                log("Changed from " + this.Name + " to " + newState.Name);
            }

            newState.PreviousState = (StateSubClass)this;
            newState.HandleEnter(context);
        }
        else
        {
            log("Attempted to change state: " + this.Name + " to the same state: " + newState.Name);
        }

        return newState;
    }

    protected void log(string message)
    {
        if (_enablesLogs)
        {
            Debug.Log(getLogLabelMessagePrefix() + message);
        }
    }

    protected void logError(string message)
    {
        Debug.LogError(getLogLabelMessagePrefix() + message);
    }

    protected string getLogLabelMessagePrefix()
    {
        return (_contextLabel != null) ? _contextLabel + _LOG_LABEL_MESSAGE_PREFIX_SEPARATOR : "";
    }
}

