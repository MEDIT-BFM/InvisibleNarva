using UnityEngine;

namespace InvisibleNarva {
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Transform minimapIcon;
        [SerializeField] private Joystick moveStick;
        [SerializeField] private Joystick lookStick;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;

        public bool IsMoving { get; private set; }
        public Transform Transform { get => _transform; }

        private float _xLook;
        private float _yLook;
        private Transform _transform;
        private CharacterController _characterController;

        public void Locate(Transform target) {
            minimapIcon.position =_transform.position = target.position;
            Rotate(target.eulerAngles);

            _xLook = target.eulerAngles.y;
            _yLook = -target.eulerAngles.x;
        }

        public void Rotate(Vector3 eulerRotation) {
            minimapIcon.rotation = _transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
            playerCamera.rotation = Quaternion.Euler(eulerRotation);
        }

        private void Awake() {
            _transform = transform;
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable() {
            QuestionManager.OnPlay += QuestionPlayHandler;
            QuestionManager.OnStop += QuestionStopHandler;
            TaskManager.OnGameOver += GameOverHandler;
        }

        private void QuestionStopHandler() {
            moveStick.Enable();
            lookStick.Enable();
        }

        private void Start() {
            var startPosition = QuestManager.Instance.SelectedQuest.InitialPlayerTransform;

            Locate(startPosition);
        }

        private void QuestionPlayHandler(Question q) {
            moveStick.Disable();
            lookStick.Disable();

            //q.OnEnd += (q) => {
            //    moveStick.Enable();
            //    lookStick.Enable();
            //};
        }

        private void GameOverHandler() {
            moveStick.Disable();
            lookStick.Disable();
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
                _yLook = Mathf.Clamp(_yLook, -60, 60);
                var rotate = new Vector2(-_yLook, _xLook);
                Rotate(rotate);
            }
        }

        private void OnDisable() {
            QuestionManager.OnPlay -= QuestionPlayHandler;
            QuestionManager.OnStop -= QuestionStopHandler;
            TaskManager.OnGameOver -= GameOverHandler;
        }
    }
}