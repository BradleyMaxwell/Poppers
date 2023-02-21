using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement : CharacterMovement
{
    private Player player;
    private CharacterController controller;
    private PlayerInput playerInput; 
    private InputAction moveAction; // gets the "Move" action from the attached player input object
    private InputAction jumpAction;
    private Transform cameraTransform;

    // jump variables
    private Vector3 velocity;
    private float gravity = -15f;
    private bool grounded;

    private void Awake()
    {
        player = (Player)character;
    }

    void Start()
    {
        // player components configuration
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"]; // sets the move actions to the input system move action bindings, which are WASD and Arrow keys
        jumpAction = playerInput.actions["Jump"]; // sets the jump action to the input system's jump binding

        // other configuration
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        grounded = controller.isGrounded; // finds the controller's isGrounded value to determine if the character is grounded or not at each frame

        // Listen for any inputs on the x and z axis, which is considered x and y in Vector2. Action is Vector2 because movement is only concerned with the x and z planes
        Vector3 direction = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);

        if (direction != Vector3.zero) // only try to move if there is a movement input
        {
            MovePlayer(direction);
        }

        // jump input
        if (jumpAction.triggered && grounded) // if the player jumps, check the player is grounded before jumping
        {
            Jump();
        }

        ApplyVelocity();
    }

    protected void MovePlayer (Vector3 direction) // move player in a given direction on the x and z axis using their character controller
    {
        Vector3 relativeDirection = (direction.x * cameraTransform.right.normalized) + (direction.z * cameraTransform.forward.normalized); // 3d movement vector relative to the camera's x and z direction
        relativeDirection.y = 0; // ignore the y axis to only move horizontally in relation to camera angle 
        controller.Move(relativeDirection * player.movementSpeed * Time.deltaTime);
    }

    private void ApplyVelocity () // moves the player in the direction of their velocity
    {
        velocity.y += gravity * Time.deltaTime; // pull the player down with simulated gravity
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump () // add upward velocity in the y axis
    {
        velocity.y = player.jumpHeight;
    }

}
