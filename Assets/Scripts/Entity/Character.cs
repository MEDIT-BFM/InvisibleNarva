using UnityEngine;
using UnityEngine.Video;

public class Character : Entity {
    [SerializeField] private bool isLooping;
    [SerializeField] private VideoClip clip;
    [SerializeField] private AudioClip voice;
    [SerializeField] private RectTransform renderTransform;

    public bool IsLooping { get => isLooping; }
    public VideoClip Clip { get => clip; }
    public AudioClip Voice { get => voice; }
    public RectTransform RenderTransform { get => renderTransform; }

    public override void Begin() {
        CharacterManager.Instance.Play(this);
        TriggerBegin(this);

        Debug.Log("Height: " + clip.height);
        Debug.Log("Width: " + clip.width);
    }

    public override void End() {
        TriggerEnd(this);
    }
}