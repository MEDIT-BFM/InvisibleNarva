using UnityEngine;

public class Speech : Entity {
    [SerializeField] private AudioClip voice;
    [SerializeField, TextArea(2, 4)] private string subtitle;

    public AudioClip Voice { get => voice; }
    public string Subtitle { get => subtitle; }
    public float GetDuration { get => voice.length; }

    public override void Begin() {
        NarrationManager.Instance.Play(this);
        OnBegin?.Invoke();
    }

    public override void End() {
        OnEnd?.Invoke();
    }
}