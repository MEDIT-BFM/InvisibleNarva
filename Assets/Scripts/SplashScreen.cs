﻿using DG.Tweening;
using UnityEngine;

public class SplashScreen : MonoBehaviour {
    [SerializeField] private float displayTime;

    private Sequence _sequence;

    public void Skip() {
        _sequence.Kill();
        ChangeScene();
    }

    private void Start() {
        _sequence = DOTween.Sequence().SetDelay(displayTime).OnComplete(() => ChangeScene());
    }

    private void ChangeScene() {
        SceneController.Instance.ChangeScene("Entrance", 0.5f);
    }
}
