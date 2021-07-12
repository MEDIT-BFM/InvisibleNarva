using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace InvisibleNarva {
    public class QuestionUI : MonoBehaviour {
        [SerializeField] private Button skip;
        [SerializeField] private Button submit;
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private AnswerUI[] answers;

        private Transform _transform;
        private Question _current;
        private Sequence _negativeFeedbackTween;
        private Sequence _questionPlayTween;
        private AnswerData _selectedAnswer;

        private void OnEnable() {
            Task.OnSkip += TaskSkipHandler;
        }

        private void Start() {
            QuestionManager.OnPlay += PlayHandler;
            QuestionManager.OnSubmit += SubmitHandler;
            QuestionManager.OnSelected += AnswerSelectedHandler;
            QuestionManager.OnAnswerRead += AnswerReadingHandler;
            QuestionManager.OnQuestionReady += QuestionReadyHandler;

            _transform = transform;

            _questionPlayTween = DOTween.Sequence()
                .Append(_transform.DOScale(0, 0))
                .Append(_transform.DOScale(1, 0.5f))
                .SetEase(Ease.InFlash)
                .SetAutoKill(false);

            _negativeFeedbackTween = DOTween.Sequence()
                .Append(_transform.DOShakeRotation(duration: 0.3f, strength: Vector2.one * 10, vibrato: 20, fadeOut: false))
                .SetAutoKill(false);

            _questionPlayTween.Pause();
            _negativeFeedbackTween.Pause();

            gameObject.SetActive(false);
        }

        private void PlayHandler(Question q) {
            _current = q;
            gameObject.SetActive(true);
            skip.gameObject.SetActive(false);
            submit.gameObject.SetActive(false);

            question.text = _current.Data.Question;
            _questionPlayTween.Restart();
        }

        private void SubmitHandler(WaitUntil wait) {
            if (_selectedAnswer.IsCorrect) {
                gameObject.SetActive(false);
            }
            else {
                StartCoroutine(PlayFeedbackCor(wait));
            }
        }

        private IEnumerator PlayFeedbackCor(WaitUntil wait) {
            skip.gameObject.SetActive(false);
            submit.gameObject.SetActive(false);
            _negativeFeedbackTween.Restart();
            yield return wait;
            skip.gameObject.SetActive(true);
            submit.gameObject.SetActive(true);
        }

        private void AnswerSelectedHandler(AnswerData answer) {
            _selectedAnswer = answer;
            var index = Mathf.Clamp(answer.Id - 1, 0, answers.Length);

            for (int i = 0; i < answers.Length; i++) {
                answers[i].Deselect();
            }

            answers[index].Select();
            submit.interactable = true;
        }

        private void QuestionReadyHandler() {
            skip.gameObject.SetActive(true);
            submit.gameObject.SetActive(true);
            submit.interactable = false;
        }

        private void AnswerReadingHandler(AnswerData answer) {
            var index = Mathf.Clamp(answer.Id - 1, 0, answers.Length);
            answers[index].Display(answer.Answer);
        }

        private void TaskSkipHandler(Entity entity) {
            gameObject.SetActive(false);
        }

        private void OnDisable() {
            Task.OnSkip -= TaskSkipHandler;
        }

        private void OnDestroy() {
            QuestionManager.OnPlay -= PlayHandler;
            QuestionManager.OnSubmit -= SubmitHandler;
            QuestionManager.OnSelected -= AnswerSelectedHandler;
            QuestionManager.OnAnswerRead -= AnswerReadingHandler;
            QuestionManager.OnQuestionReady -= QuestionReadyHandler;
        }
    }
}