using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnswerData {
    public int Id;
    public bool IsCorrect;
    public Entity Character;
    public Entity Feedback;
    [TextArea(2, 4)] public string Answer;
}
[System.Serializable]
public class QuestionData {
    public Entity Character;
    [TextArea(2, 4)] public string Question;
    public AnswerData[] Answers;
}
public class TaskManager : Singleton<TaskManager> {
    [SerializeField] private List<Task> availableTasks;

    public Dictionary<Task, bool> CheckList = new Dictionary<Task, bool>();

    private void Start() {
        for (int i = 0; i < availableTasks.Count; i++) {
            if (!CheckList.ContainsKey(availableTasks[i])) {
                CheckList.Add(availableTasks[i], false);
            }
        }
    }

    private void OnEnable() {
        Task.OnCompleted += (task) => CheckList[task] = true;
    }
}