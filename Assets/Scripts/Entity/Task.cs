using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace InvisibleNarva {
    public class Task : MonoBehaviour {
        public static event Action<Task> OnInitiated = delegate { };
        public static event Action<Task> OnCompleted = delegate { };
        public static event Action<Entity> OnSkip = delegate { };

        [SerializeField] private MinimapIconUI minimapIcon;
        [SerializeField] private List<Entity> entities;

        public Vector2 GetMinimapLocation { get => minimapIcon.transform.position; }

        private Entity _current;
        private Queue<Entity> _entityQueue;

        public void Enable() {
            if (gameObject) {
                gameObject.SetActive(true);
            }
        }

        public void Disable() {
            if (gameObject) {
                gameObject.SetActive(false);
            }
        }

        public void Skip() {
            if (_current == null) {
                return;
            }

            if (!_current.IsSkippable) {
                return;
            }

            OnSkip?.Invoke(_current);
            _current.End();
        }

        private void Awake() {
            _entityQueue = new Queue<Entity>(entities);
        }

        private void EntityEndHandler(object sender) {
            Begin(GetNext());

            if (_current == null) {
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
            if (minimapIcon) {
                minimapIcon.Complete();
            }

            OnCompleted?.Invoke(this);
            enabled = false;
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

            return _current = null;
        }
    }
}