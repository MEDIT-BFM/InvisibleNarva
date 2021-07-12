using UnityEngine;

namespace InvisibleNarva {
    public class Speech : Entity {
        [SerializeField] private bool isSubtitleOn = true;
        [SerializeField] private AudioClip voice;
        [SerializeField, TextArea(2, 4)] private string subtitle;

        public AudioClip Voice { get => voice; }
        public string Subtitle { get => subtitle; }
        public float GetDuration { get => voice.length; }

        public override void Begin() {
            TriggerBegin(this);
            NarrationManager.Instance.Play(this, isSubtitleOn);
        }

        public override void End() {
            TriggerEnd(this);
        }
    }
}