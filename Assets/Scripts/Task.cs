using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Task : MonoBehaviour {
    public static event Action OnInitiated = delegate { };
    public static event Action<Task> OnCompleted = delegate { };

    [SerializeField] private List<Entity> entities;

    private Entity _current;
    private Queue<Entity> _entityQueue;

    private void Awake() {
        _entityQueue = new Queue<Entity>(entities);
    }

    private void EntityEndHandler(object sender, Entity.OnEndEventArgs e) {
        if (_entityQueue.Count <= 0) {
            OnCompleted?.Invoke(this);
            _current.OnEnd -= EntityEndHandler;
            return;
        }

        _current = GetNext();
        DOTween.Sequence().AppendCallback(() => _current.Begin()).SetDelay(_current.InitialDelay);
    }

    private Entity GetNext() {
        var entity = _entityQueue.Dequeue();
        entity.OnEnd += EntityEndHandler;

        return entity;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            return;
        }

        _current = GetNext();
        _current.Begin();
        OnInitiated?.Invoke();
    }
}