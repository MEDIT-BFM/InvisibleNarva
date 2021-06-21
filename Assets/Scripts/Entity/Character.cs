using UnityEngine;
using UnityEngine.Video;

namespace InvisibleNarva {
    public class Character : Entity {
        [SerializeField] private bool isPlayingOnUI;
        [SerializeField] private bool isLooping;
        [SerializeField] private VideoClip clip;
        [SerializeField] private AudioClip voice;
        [SerializeField] private RectTransform renderTransform;
       // [SerializeField] private RenderTexture renderTexture;

        public bool IsLooping { get => isLooping; }
        public VideoClip Clip { get => clip; }
        public AudioClip Voice { get => voice; }
        public RectTransform RenderTransform { get => renderTransform; }
      //  public RenderTexture RenderTexture { get => renderTexture; }

        public override void Begin() {
            TriggerBegin(this);

            if (isPlayingOnUI) {
                CharacterManager.Instance.Play(this);
            }
        }

        public override void End() {
            TriggerEnd(this);
        }
    }
}