using UnityEngine;

public class Minimap : MonoBehaviour {
    [SerializeField] private PlayerController player;

    private Transform _camera;
    private Vector3 _cameraPosition;
    private Transform _transform;

    public Transform Camera { get => _camera; }

    private void Awake() {
        _transform = transform;
        _camera = GetComponent<Camera>().transform;
        _cameraPosition = new Vector3(player.transform.position.x, _transform.position.y, player.transform.position.z);
    }

    private void LateUpdate() {
        if (player.VelocityMagnitude == 0) {
            return;
        }

        _transform.position = _cameraPosition;
    }
}
