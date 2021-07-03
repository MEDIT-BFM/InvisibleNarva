using UnityEngine;
using System;

namespace InvisibleNarva {
    public class Question : Entity {
        [SerializeField] private QuestionData questionData;

        public QuestionData Data { get => questionData; }

        public override void Begin() {
            TriggerBegin(this);
            QuestionManager.Instance.Play(this);
        }

        public override void End() {
            TriggerEnd(this);
        }
    }
}