using UnityEngine;

namespace InvisibleNarva {
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Joystick moveStick;
        [SerializeField] private Joystick lookStick;
        [SerializeField] private Transform minimapIcon;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;

        public bool IsMoving { get; private set; }
        public Transform Transform { get => _transform; }

        private float _xLook;
        private float _yLook;
        private Transform _transform;
        private CharacterController _characterController;

        public void Locate(Transform target) {
            _transform.position = target.position;
            _transform.rotation = target.rotation;
        }

        private void Awake() {
            _transform = transform;
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable() {
            Task.OnInitiated += TaskInitiatedHandler;
            Task.OnCompleted += TaskCompletedHandler;
            TaskManager.OnGameOver += GameOverHandler;
        }

        private void GameOverHandler() {
            TaskInitiatedHandler(null);
        }

        private void Start() {
            var startPosition = QuestManager.Instance.SelectedQuest.InitialPlayerTransform;
            _transform.position = startPosition.position;
            _transform.rotation = startPosition.rotation;
            minimapIcon.position = _transform.position;
            minimapIcon.rotation = Quaternion.Euler(0, _transform.rotation.eulerAngles.y, 0);
        }

        private void TaskInitiatedHandler(Task task) {
            moveStick.Disable();
            lookStick.Disable();
        }

        private void TaskCompletedHandler(Task task) {
            moveStick.Enable();
            lookStick.Enable();
        }

        private void FixedUpdate() {
            IsMoving = moveStick.Vertical != 0;

            if (IsMoving) {
                _characterController.SimpleMove(moveSpeed * moveStick.Vertical * _transform.forward.normalized);
                minimapIcon.position = _transform.position;
            }

            if (lookStick.Direction.sqrMagnitude != 0) {
                _xLook += lookStick.Direction.x;
                _yLook += lookStick.Direction.y;

                var rotate = new Vector2(-_yLook, _xLook);
                _transform.eulerAngles = rotationSpeed * rotate;
                minimapIcon.rotation = Quaternion.Euler(0, _transform.rotation.eulerAngles.y, 0);
            }
        }

        private void OnDisable() {
            Task.OnInitiated -= TaskInitiatedHandler;
            Task.OnCompleted -= TaskCompletedHandler;
            TaskManager.OnGameOver -= GameOverHandler;
        }
    }
}