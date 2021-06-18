using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Task : MonoBehaviour {
    public static event Action<Task> OnInitiated = delegate { };
    public static event Action<Task> OnCompleted = delegate { };

    [SerializeField] private MinimapIconUI minimapIcon;
    [SerializeField] private List<Entity> entities;

    public Vector2 GetMinimapLocation { get => minimapIcon.transform.position; }

    private Entity _current;
    private Queue<Entity> _entityQueue;

    public void Skip() {
        if (!_current.IsSkippable) {
            return;
        }

        Begin(GetNext());

        if (_current == null) {
            Complete();
        }
    }

    private void Awake() {
        _entityQueue = new Queue<Entity>(entities);
    }

    private void EntityEndHandler(object sender) {
        if (_entityQueue.Count > 0) {
            Begin(GetNext());
        }
        else {
            Complete();
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            return;
        }

        Begin(GetNext());
        OnInitiated?.Invoke(this);
    }

    private void Begin(Entity entity) {
        if (entity != null) {
            DOTween.Sequence().AppendCallback(() => entity.Begin()).SetDelay(entity.InitialDelay);
        }
    }

    private void Complete() {
        minimapIcon.Complete();
        gameObject.SetActive(false);
        OnCompleted?.Invoke(this);
        _current.OnEnd -= EntityEndHandler;
    }

    private Entity GetNext() {
        if (_current != null) {
            _current.OnEnd -= EntityEndHandler;
        }

        if (_entityQueue.Count > 0) {
            _current = _entityQueue.Dequeue();
            _current.OnEnd += EntityEndHandler;
            return _current;
        }

        return null;
    }
}