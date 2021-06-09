using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick lookStick;
    [SerializeField] private Transform minimapIcon;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    public Transform Transform { get => _transform; }
    public float VelocityMagnitude { get; private set; }

    private float _xLook;
    private float _yLook;
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
        minimapIcon.position = new Vector3(_transform.position.x, minimapIcon.position.y, _transform.position.z);
        minimapIcon.eulerAngles = new Vector3(minimapIcon.eulerAngles.x, _transform.eulerAngles.y, minimapIcon.eulerAngles.z);
    }

    private void TaskInitiatedHandler(Task task) {
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
            minimapIcon.position = new Vector3(_transform.position.x, minimapIcon.position.y, _transform.position.z);
        }

        if (lookStick.Direction.sqrMagnitude != 0) {
            _xLook += lookStick.Direction.x;
            _yLook += lookStick.Direction.y;

            var rotate = new Vector2(-_yLook, _xLook);
            _transform.eulerAngles = rotationSpeed * rotate;
            minimapIcon.eulerAngles = new Vector3(minimapIcon.eulerAngles.x, _transform.eulerAngles.y, minimapIcon.eulerAngles.z - 90);
        }
    }

    private void OnDisable() {
        Task.OnInitiated -= TaskInitiatedHandler;
        Task.OnCompleted -= TaskCompletedHandler;
    }
}