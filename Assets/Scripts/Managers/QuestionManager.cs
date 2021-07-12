using System;
using System.Collections;
using UnityEngine;

namespace InvisibleNarva {
    public class QuestionManager : Singleton<QuestionManager> {
        public static event Action<WaitUntil> OnSubmit = delegate { };
        public static event Action OnStop = delegate { };
        public static event Action OnQuestionReady = delegate { };
        public static event Action<AnswerData> OnAnswerRead = delegate { };
        public static event Action<Question> OnPlay = delegate { };
        public static event Action<AnswerData> OnSelected = delegate { };

        private Question _current;
        private AnswerData _selectedAnswer;
        private WaitUntil _waitUntilQuestionIsRead;
        private WaitUntil _waitUntilFeedbackIsEnd;
        private WaitUntil _waitUntilAnswerIsRead;

        public void Select(int id) {
            var a = _current.Data.Answers;
            for (int i = 0; i < a.Length; i++) {
                if (a[i].Id == id) {
                    _selectedAnswer = a[i];
                    OnSelected?.Invoke(a[i]);
                }
            }
        }

        public void Play(Question question) {
            _current = question;
            OnPlay?.Invoke(question);
            StartCoroutine(ReadQuestionCor());
        }

        public void Submit() {
            _selectedAnswer.Feedback.Begin();
            OnSubmit?.Invoke(_waitUntilFeedbackIsEnd);

            if (_selectedAnswer.IsCorrect) {
                StartCoroutine(PlayFeedbackCor());
            }
        }

        private void Start() {
            _waitUntilQuestionIsRead = new WaitUntil(() => _current.Data.Character.IsPlaying == false);
            _waitUntilFeedbackIsEnd = new WaitUntil(() => _selectedAnswer.Feedback.IsPlaying == false);
        }

        private IEnumerator ReadQuestionCor() {
            _current.Data.Character.Begin();
            yield return _waitUntilQuestionIsRead;
            StartCoroutine(ReadAnswerCor());
        }

        private IEnumerator ReadAnswerCor() {
            int count = 0;
            var a = _current.Data.Answers;

            _waitUntilAnswerIsRead = new WaitUntil(() => a[count].Character.IsPlaying == false);

            do {
                a[count].Character.Begin();
                OnAnswerRead?.Invoke(a[count]);
                yield return _waitUntilAnswerIsRead;
                count++;
            } while (count < a.Length);

            OnQuestionReady?.Invoke();
        }

        private IEnumerator PlayFeedbackCor() {
            OnStop?.Invoke();
            yield return _waitUntilFeedbackIsEnd;
            _current.End();
            _current = null;
            _selectedAnswer = null;
        }

        private void OnDisable() {
            StopAllCoroutines();
        }
    }
}