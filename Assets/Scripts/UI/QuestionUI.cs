using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AnswerUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;


}

public class QuestionUI : MonoBehaviour {
    [SerializeField] private Button submit;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI[] answers;

    private Transform _transform;
    private Question _current;
    private AnswerData _selected;

    public void Select(int id) {
        var a = _current.Data.Answers;
        for (int i = 0; i < a.Length; i++) {
            if (a[i].Id == id) {
                _selected = a[i];
                submit.interactable = true;
            }
        }
    }

    public void Submit() {
        _selected.Feedback.OnEnd += (sender, e) => {
            if (!_selected.IsCorrect) {
                gameObject.SetActive(true);
                submit.interactable = true;
            }
            else {
                _current.End();
                _current.OnEnd -= EntityEndHandler;
            }
        };

        DOTween.Sequence()
            .Append(_transform.DOScale(1.1f, 0.25f))
            .Append(_transform.DOScale(0, 0.75f))
            .AppendCallback(() => {
                gameObject.SetActive(false);
                _selected.Feedback.Begin();
            });
    }

    private void Start() {
        _transform = transform;
        Question.OnQuestionBegin += QuestionBeginHandler;
    }

    private void OnEnable() {
        submit.interactable = false;
    }

    private void QuestionBeginHandler(Question q) {
        gameObject.SetActive(true);
        _current = q;
        _current.OnEnd += EntityEndHandler;

        var a = _current.Data.Answers;
        question.text = _current.Data.Question;

        for (int i = 0; i < a.Length; i++) {
            answers[i].text = a[i].Answer;
        }
    }

    private void EntityEndHandler(object sender, Entity.OnEndEventArgs e) {
        if (!(sender is Question)) {
            return;
        }

        for (int i = 0; i < answers.Length; i++) {
            answers[i].text = "";
        }
    }

    private void OnDestroy() {
        Question.OnQuestionBegin -= QuestionBeginHandler;
    }
}