using UnityEngine;
using UnityEngine.Video;

public class Character : Entity {
    [SerializeField] private VideoClip clip;
    [SerializeField] private AudioClip voice;
    [SerializeField] private Texture renderTexture;
    [SerializeField] private RectTransform renderTransform;

    public VideoClip Clip { get => clip; }
    public AudioClip Voice { get => voice; }
    public Texture RenderTexture { get => renderTexture; }
    public RectTransform RenderTransform { get => renderTransform; }

    public override void Begin() {
        CharacterManager.Instance.PlayCharacter(this);
        TriggerBegin(this);
    }

    public override void End() {
        TriggerEnd(this);
    }
}