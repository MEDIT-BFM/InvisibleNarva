using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        Task.OnCompleted += TaskCompletedHandler;
        Task.OnInitiated += TaskInitiatedHandler;
    }

    private void TaskInitiatedHandler(Task task) {
        _current = task;
    }

    private void TaskCompletedHandler(Task task) {
        CheckList[task] = true;

        if (CheckList.Values.All(v => v == true)) {
            OnGameOver?.Invoke();
            return;
        }

        var next = CheckList.FindFirstKeyByValue(false);
        OnNextPointed?.Invoke(next);
    }

    private void OnDisable() {
        Task.OnCompleted -= TaskCompletedHandler;
    }
}