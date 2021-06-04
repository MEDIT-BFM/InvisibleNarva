using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour {
    public UnityEvent OnBegin;
    public UnityEvent OnEnd;

    public abstract void Begin();
    public abstract void End();
}