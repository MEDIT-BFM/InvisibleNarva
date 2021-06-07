using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick lookStick;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    public float VelocityMagnitude { get; private set; }
    public Transform Transform { get => _transform; }

    private Transform _transform;
    private CharacterController _characterController;

    private void Awake() {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        VelocityMagnitude = _characterController.velocity.magnitude;
    }

    private void OnEnable() {
        Task.OnInitiated += TaskInitiatedHandler;
        Task.OnCompleted += TaskCompletedHandler;

        var startPosition = QuestManager.Instance.SelectedQuest.InitialPlayerTransform;

        _transform.position = startPosition.position;
        _transform.rotation = startPosition.rotation;
    }

    private void TaskInitiatedHandler() {
        moveStick.OnPointerUp(null);
        lookStick.OnPointerUp(null);
        moveStick.gameObject.SetActive(false);
        lookStick.gameObject.SetActive(false);
    }

    private void TaskCompletedHandler(Task task) {
        moveStick.gameObject.SetActive(true);
        lookStick.gameObject.SetActive(true);
    }

    private void FixedUpdate() {
        if (moveStick.Vertical != 0) {
            _characterController.SimpleMove(moveSpeed * moveStick.Vertical * _transform.forward.normalized);
        }

        if (lookStick.Direction.sqrMagnitude != 0) {
            _transform.Rotate(rotationSpeed * lookStick.Direction);
        }
    }

    private void OnDisable() {
        Task.OnInitiated -= TaskInitiatedHandler;
        Task.OnCompleted -= TaskCompletedHandler;
    }
}