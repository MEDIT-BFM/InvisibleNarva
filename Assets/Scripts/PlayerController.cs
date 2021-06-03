using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public FixedJoystick joystick;
    public FixedJoystick lookstick;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    private Transform _transform;
    private CharacterController _characterController;

    private void Awake() {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable() {
        var startPosition = QuestManager.Instance.SelectedQuest.InitialPlayerTransform;

        _transform.position = startPosition.position;
        _transform.rotation = startPosition.rotation;
    }

    private void FixedUpdate() {
        _characterController.SimpleMove(moveSpeed * joystick.Vertical * _transform.forward);
        _transform.Rotate(rotationSpeed * lookstick.Direction);
    }
}