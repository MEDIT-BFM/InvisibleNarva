using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class AnswerData {
    public int Id;
    public bool IsCorrect;
    public Entity Feedback;
    [TextArea(2, 4)] public string Answer;
}
[System.Serializable]
public class QuestionData {
    public Character Character;
    [TextArea(2, 4)] public string Question;
    public AnswerData[] Answers;
}
public class Question : Entity {
    public static event Action<Question> OnQuestionBegin = delegate { };

    [SerializeField] private QuestionData questionData;

    public QuestionData Data { get; private set; }

    protected override void Awake() {
        base.Awake();
        Data = questionData;
    }

    public override void Begin() {
        questionData.Character.Begin();
        TriggerBegin(this);
        OnQuestionBegin?.Invoke(this);
    }

    public override void End() {
        TriggerEnd(this);
    }
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
        Task.OnCompleted += TaskCompletedHandler;
    }

    private void TaskCompletedHandler(Task task) {
        CheckList[task] = true;
    }

    private void OnDisable() {
        Task.OnCompleted -= TaskCompletedHandler;
    }
}