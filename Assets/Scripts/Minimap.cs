using UnityEngine;

public class Minimap : MonoBehaviour {
    [SerializeField] private PlayerController player;

    private Vector3 _cameraPosition;
    private Transform _transform;

    private void Awake() {
        _transform = transform;
        _cameraPosition = new Vector3(player.Transform.position.x, _transform.position.y, player.Transform.position.z);
    }

    private void LateUpdate() {
        if (player.IsMoving) {
            _transform.position = _cameraPosition;
        }
    }
}
