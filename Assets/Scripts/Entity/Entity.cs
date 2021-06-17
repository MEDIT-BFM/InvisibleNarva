using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour {
    public UnityEvent OnBeginEvent;
    public UnityEvent OnEndEvent;
    public event Action<object> OnBegin;
    public event Action<object> OnEnd;

    [SerializeField] private float initialDelay;

    public bool IsPlaying{ get; private set; }
    public float InitialDelay { get => initialDelay; }

    public abstract void Begin();
    public abstract void End();

    protected void TriggerBegin(object entity) {
        IsPlaying = true;
        OnBeginEvent?.Invoke();
        OnBegin?.Invoke(entity);
    }

    protected void TriggerEnd(object entity) {
        IsPlaying = false;
        OnEndEvent?.Invoke();
        OnEnd?.Invoke(entity);
    }
}