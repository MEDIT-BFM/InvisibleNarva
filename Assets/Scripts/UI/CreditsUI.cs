using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    private void OnEnable() {
        TaskManager.OnGameOver += GameOverHandler;
    }

    private void GameOverHandler() {

    }

    private void OnDisable() {
        TaskManager.OnGameOver -= GameOverHandler;
    }
}