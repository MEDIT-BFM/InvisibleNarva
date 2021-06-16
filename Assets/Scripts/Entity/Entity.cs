using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour {
    public event EventHandler<OnBeginEventArgs> OnBegin;
    public event EventHandler<OnEndEventArgs> OnEnd;

    [SerializeField] private float initialDelay;

    public UnityEvent OnBeginEvent;
    public UnityEvent OnEndEvent;

    public class OnBeginEventArgs : EventArgs { public Entity Entity; }
    public class OnEndEventArgs : EventArgs { public Entity Entity; }

    protected void TriggerBegin(Entity entity) {
        OnBeginEvent?.Invoke();
        OnBegin?.Invoke(this, new OnBeginEventArgs { Entity = entity });
    }

    protected void TriggerEnd(Entity entity) {
        OnEndEvent?.Invoke();
        OnEnd?.Invoke(this, new OnEndEventArgs { Entity = entity });
    }

    public float InitialDelay { get; private set; }

    protected virtual void Awake() {
        InitialDelay = initialDelay;
    }

    public abstract void Begin();
    public abstract void End();
}