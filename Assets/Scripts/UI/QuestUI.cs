using UnityEngine;
using UnityEngine.UI;

namespace InvisibleNarva {
    public class QuestUI : MonoBehaviour {
        [SerializeField] private Quest quest;
        [SerializeField] private Image questImage;

        public Quest Quest { get => quest; }

        private Toggle _toggle;
        private QuestManager _questManager;

        private void Awake() {
            _toggle = GetComponent<Toggle>();
        }

        private void Start() {
            if (QuestManager.TryGetInstance(out QuestManager instance)) {
                _questManager = instance;
            }

            if (quest.IsCompleted) {
                questImage.sprite = quest.CompletedSprite;
            }

            _toggle.onValueChanged.AddListener(UpdateInfoText);
        }

        private void UpdateInfoText(bool isOn) {
            if (isOn) {
                _questManager.HandleUpdatedQuest(Quest);
            }
        }

        private void OnDestroy() {
            _toggle.onValueChanged.RemoveAllListeners();
        }
    }
}