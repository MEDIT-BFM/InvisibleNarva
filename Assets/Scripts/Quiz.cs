using System;
using UnityEngine;
using UnityEngine.Events;

public class Quiz : MonoBehaviour {
    public static event Action<Quiz> OnQuizCompleted = delegate { };

    public UnityEvent OnPlayerEnter;

    public void CompleteQuiz() {
        OnQuizCompleted?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Player") {
            return;
        }

        OnPlayerEnter?.Invoke();
    }
}