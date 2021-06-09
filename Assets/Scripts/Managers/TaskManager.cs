using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : Singleton<TaskManager> {
    public static event Action OnGameOver = delegate { };

    [SerializeField] private PlayerController player;
    [SerializeField] private List<Task> availableTasks;

    public PlayerController Player { get; private set; }

    public Dictionary<Task, bool> CheckList = new Dictionary<Task, bool>();

    private void Start() {
        Player = player;
        for (int i = 0; i < availableTasks.Count; i++) {
            if (!CheckList.ContainsKey(availableTasks[i])) {
                CheckList.Add(availableTasks[i], false);
            }
        }
    }

    private void OnEnable() {
        Task.OnCompleted += TaskCompletedHandler;
    }

    private void TaskCompletedHandler(Task task) {
        CheckList[task] = true;

        if (CheckList.Values.All(v => v == true)) {
            OnGameOver?.Invoke();
        }
    }

    private void OnDisable() {
        Task.OnCompleted -= TaskCompletedHandler;
    }
}