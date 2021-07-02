using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InvisibleNarva {
    public class TaskManager : Singleton<TaskManager> {
        public static event Action OnGameOver = delegate { };
        public static event Action<Task> OnNextPointed = delegate { };

        [SerializeField] private PlayerController player;
        [SerializeField] private List<Task> availableTasks;

        public PlayerController Player { get => player; }
        public Dictionary<Task, bool> CheckList = new Dictionary<Task, bool>();

        private Task _current;

        public void Skip() {
            _current.Skip();
        }

        private void Start() {
            for (int i = 0; i < availableTasks.Count; i++) {
                if (!CheckList.ContainsKey(availableTasks[i])) {
                    CheckList.Add(availableTasks[i], false);
                }
            }
        }

        private void OnEnable() {
            Task.OnInitiated += TaskInitiatedHandler;
            Task.OnCompleted += TaskCompletedHandler;
        }

        private void TaskInitiatedHandler(Task task) {
            _current = task;
            DisableAll();
        }

        private void TaskCompletedHandler(Task task) {
            CheckList[task] = true;
            availableTasks.Remove(task);

            if (CheckList.Values.All(v => v == true)) {
                OnGameOver?.Invoke();
                return;
            }

            EnableAll();
            var next = CheckList.FindFirstKeyByValue(false);
            OnNextPointed?.Invoke(next);
        }

        private void EnableAll() {
            for (int i = 0; i < availableTasks.Count; i++) {
                availableTasks[i].Enable();
            }
        }

        private void DisableAll() {
            for (int i = 0; i < availableTasks.Count; i++) {
                availableTasks[i].Disable();
            }
        }

        private void OnDisable() {
            Task.OnInitiated -= TaskInitiatedHandler;
            Task.OnCompleted -= TaskCompletedHandler;
        }
    }
}