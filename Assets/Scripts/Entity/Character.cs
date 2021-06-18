using UnityEngine;
using UnityEngine.Video;

namespace InvisibleNarva {
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
            TriggerBegin(this);
            CharacterManager.Instance.Play(this);
        }

        public override void End() {
            TriggerEnd(this);
        }
    }
}