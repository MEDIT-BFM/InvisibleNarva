using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QuestionUI : MonoBehaviour {
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

    public void Submit() {
        _selected.Feedback.OnEnd += FeedbackEnd;

        Hide(() => _selected.Feedback.Begin());
    }

    private void FeedbackEnd(object sender, Entity.OnEndEventArgs e) {
        if (!_selected.IsCorrect) {
            Show(() => submit.interactable = true);
        }
        else {
            _current.End();
        }

        _selected.Feedback.OnEnd -= FeedbackEnd;
    }

    private void OnEnable() {
        submit.interactable = false;
    }

    private void Start() {
        _transform = transform;
        Question.OnQuestionBegin += QuestionBeginHandler;
        gameObject.SetActive(false);
    }

    private void QuestionBeginHandler(Question q) {
        _current = q;
        Debug.Log("NAME: " + q);
        question.text = _current.Data.Question;
        Show();

        var a = _current.Data.Answers;
        for (int i = 0; i < a.Length; i++) {
            answers[i].SetText(a[i].Answer);
        }
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
            //.Append(_transform.DOScale(1.1f, 0.15f))
            .Append(_transform.DOShakeRotation(duration: 0.5f, strength: Vector2.one * 10, vibrato: 20, fadeOut: false))
            //.Append(_transform.DOScale(0, 0.15f))
            .AppendCallback(() => {
                gameObject.SetActive(false);
                callback();
            });
    }

    private void OnDestroy() {
        Question.OnQuestionBegin -= QuestionBeginHandler;
    }
}