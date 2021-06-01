using UnityEngine;

public class FeedbackEntityController : MonoBehaviour {
    public bool isCorrectFeedback;
    public Transform quizEntity;
    public Transform quizPanel;
    public float displayTime;

    public static bool CorrectAnswer = false;

    private void OnEnable() {
        if (isCorrectFeedback) {
            CorrectAnswer = true;
            Destroy(quizEntity.gameObject, displayTime);
        }
        else {
            Invoke("DisplayFeedback", displayTime);
        }
    }

    private void DisplayFeedback() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        CorrectAnswer = false;
    }
}