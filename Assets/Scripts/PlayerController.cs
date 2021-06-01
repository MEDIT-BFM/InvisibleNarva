using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public FixedJoystick joystick;
    public FixedJoystick lookstick;

    private float speed = 2f;
    private Vector3 velocity;
    private CharacterController characterController;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        velocity = new Vector3(0, 0, joystick.Vertical);
        characterController.SimpleMove(speed * velocity);
    }
}