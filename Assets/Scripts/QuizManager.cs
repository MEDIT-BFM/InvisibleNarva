using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : Singleton<QuizManager> {
    [SerializeField] private List<Quiz> availableQuizzes;


    [Tooltip("An entity (images, characters etc.) that will be displayed after the current one. Leave empty if there is not any.")]
    [SerializeField] private Transform nextEntity;
    [SerializeField] private Transform quizPanel;
    [SerializeField] private Transform buttonPanel;

    [SerializeField] private Toggle[] Answers;
    [SerializeField] private Transform[] FeedbackPanels;

   // private bool isSelected = false;

    public Dictionary<Quiz, bool> CheckList = new Dictionary<Quiz, bool>();

    protected override void Awake() {
        for (int i = 0; i < availableQuizzes.Count; i++) {
            if (!CheckList.ContainsKey(availableQuizzes[i])) {
                CheckList.Add(availableQuizzes[i], false);
            }
        }
    }
    private void OnEnable() {
        Quiz.OnQuizCompleted += QuizDoneHander;
    }

    private void QuizDoneHander(Quiz quiz) {
        CheckList[quiz] = true;
        Debug.Log("Quiz Done: " + quiz.gameObject.name);
    }

    //private void Start() {
    //    foreach (Toggle item in Answers) {
    //        item.interactable = false;
    //    }
    //}

    //private void Update() {
    //    OptionController();
    //    SelectionCheck(isSelected);
    //}

    private void OptionController() {
        foreach (Toggle item in Answers) {
            if (areAllTrue()) {
                if (item.isOn) {
                   // isSelected = true;
                }
                if (FeedbackEntityController.CorrectAnswer) {
                    //item.interactable = false;
                    //buttonPanel.gameObject.SetActive(false);
                    quizPanel.gameObject.SetActive(false);
                }
                else {
                    item.interactable = true;
                }
            }
        }
    }

    public void CheckResponse() {
        for (int k = 0; k < Answers.Length; k++) {
            if (Answers[k].isOn) {
                FeedbackPanels[k].gameObject.SetActive(true);
            }
        }
    }

    private void SelectionCheck(bool value) {
        if (value) {
            buttonPanel.gameObject.SetActive(true);
        }
        else {
            buttonPanel.gameObject.SetActive(false);
        }
    }

    private bool areAllTrue() {
        foreach (Toggle a in Answers) {
            if (!a.gameObject.activeInHierarchy) {
                return false;
            }
        }
        return true;
    }
    private void OnDisable() {
        Quiz.OnQuizCompleted -= QuizDoneHander;
    }
    protected override void OnDestroy() {
        if (nextEntity != null) {
            nextEntity.gameObject.SetActive(true);
        }
    }
}