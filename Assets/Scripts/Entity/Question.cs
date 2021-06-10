using UnityEngine;
using System;

public class Question : Entity {
    public static event Action<Question> OnQuestionBegin = delegate { };

    [SerializeField] private QuestionData questionData;

    public QuestionData Data { get => questionData; }

    public override void Begin() {
        questionData.Character.Begin();
        TriggerBegin(this);
        OnQuestionBegin?.Invoke(this);
    }

    public override void End() {
        TriggerEnd(this);
    }
}
