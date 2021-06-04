using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Character : Entity {
    [SerializeField] private VideoClip clip;
    [SerializeField] private AudioClip voice;
    [SerializeField] private RawImage targetTexture;

    public VideoClip Clip { get => clip; }
    public AudioClip Voice { get => voice; }
    public RawImage TargetTexture { get => targetTexture; }

    public override void Begin() {
        CharacterManager.Instance.PlayCharacter(this);
        OnBegin?.Invoke();
    }

    public override void End() {
        OnEnd?.Invoke();
    }
}