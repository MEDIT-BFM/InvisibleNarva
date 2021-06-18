using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;

namespace InvisibleNarva {
    public class QuestionUI : MonoBehaviour {
        [SerializeField] private Button skip;
        [SerializeField] private Button submit;
        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private AnswerUI[] answers;

        private Transform _transform;
        private Question _current;
        private AnswerData _selected;

        public void Select(int id) {
            var a = _current.Data.Answers;
            for (int i = 0; i < a.Length; i++) {
                if (a[i].Id == id) {
                    if (_selected != null) {
                        answers[_selected.Id - 1].Deselect();
                    }

                    _selected = a[i];
                    answers[i].Select();
                    submit.interactable = true;
                }
            }
        }

        private void Start() {
            _transform = transform;
            Question.OnQuestionBegin += QuestionBeginHandler;
            submit.onClick.AddListener(Submit);
            skip.onClick.AddListener(() => {
                submit.interactable = false;
                gameObject.SetActive(false);
                TaskManager.Instance.Skip();
            });

            gameObject.SetActive(false);
        }

        private void QuestionBeginHandler(Question q) {
            _current = q;
            var questionCharacter = _current.Data.Character;
            question.text = _current.Data.Question;
            skip.gameObject.SetActive(false);
            submit.gameObject.SetActive(false);
            Show(questionCharacter.Begin);
            questionCharacter.OnEnd += (sender) => StartCoroutine(ReadAnswerCor());

            for (int j = 0; j < answers.Length; j++) {
                answers[j].Hide();
            }
        }

        private void Submit() {
            _selected.Feedback.OnEnd += FeedbackEnd;

            Hide(_selected.Feedback.Begin);
        }

        private void FeedbackEnd(object sender) {
            if (!_selected.IsCorrect) {
                Show(() => submit.interactable = true);
            }
            else {
                _current.End();
            }

            _selected.Feedback.OnEnd -= FeedbackEnd;
        }

        private void Show(TweenCallback callback = null) {
            gameObject.SetActive(true);
            DOTween.Sequence()
               .Append(_transform.DOScale(1.1f, 0.15f))
               .Append(_transform.DOScale(1, 0.15f))
               .AppendCallback(() => {
                   callback();
               });
        }

        private void Hide(TweenCallback callback = null) {
            DOTween.Sequence()
                .Append(_transform.DOShakeRotation(duration: 0.5f, strength: Vector2.one * 10, vibrato: 20, fadeOut: false))
                .AppendCallback(() => {
                    submit.interactable = false;
                    gameObject.SetActive(false);
                    callback();
                });
        }

        private IEnumerator ReadAnswerCor() {
            int count = 0;
            var a = _current.Data.Answers;

            do {
                a[count].Character.Begin();
                answers[count].Display(a[count].Answer);
                yield return new WaitUntil(() => a[count].Character.IsPlaying == false);
                count++;
            } while (count < a.Length);

            skip.gameObject.SetActive(true);
            submit.gameObject.SetActive(true);
        }

        private void OnDestroy() {
            Question.OnQuestionBegin -= QuestionBeginHandler;
            skip.onClick.RemoveAllListeners();
            submit.onClick.RemoveAllListeners();
        }
    }
}