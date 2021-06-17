using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour {
    public event EventHandler<OnBeginEventArgs> OnBegin;
    public event EventHandler<OnEndEventArgs> OnEnd;

    [SerializeField] private float initialDelay;

    public UnityEvent OnBeginEvent;
    public UnityEvent OnEndEvent;
    public WaitUntil WaitUntilEnd { get => new WaitUntil(() => isPlaying == false); }

    public class OnBeginEventArgs : EventArgs { public Entity Entity; }
    public class OnEndEventArgs : EventArgs { public Entity Entity; }

    private bool isPlaying;

    protected void TriggerBegin(Entity entity) {
        isPlaying = true;
        OnBeginEvent?.Invoke();
        OnBegin?.Invoke(this, new OnBeginEventArgs { Entity = entity });
    }

    protected void TriggerEnd(Entity entity) {
        isPlaying = false;
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