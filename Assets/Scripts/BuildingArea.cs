using System;
using UnityEngine;

namespace InvisibleNarva {
    public class BuildingArea : MonoBehaviour {
        public static event Action<string> OnEnter = delegate { };

        [SerializeField, TextArea(2, 4)] private string buildingName;

        private void OnTriggerEnter(Collider other) {
            if (other.tag != "Player") {
                return;
            }

            OnEnter?.Invoke(buildingName);
        }
    }
}