using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Handles player vertical motion control
/// </summary>
public class VerticalMotion : MonoBehaviour
{
    public InputActionReference thumbstickReferenceRight = null; // The right thumbstick that controls the player's vertical motion.
    public float vertSpeed = 5.0f;  // Scalar to adjust the sensitivity for thumbstick input and resulting motion
    CharacterController character;  // Player's CharacterController object that is used to manipulate position

    /// <summary>
    /// Initializes the reference to the player's CharacterController object
    /// </summary>
    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Moves the player vertically based on input from thumbstickReferenceRight
    /// </summary>
    void Update()
    {
        Vector2 valueRight = thumbstickReferenceRight.action.ReadValue<Vector2>();
        MovePlayer(valueRight);
    }

    /// <summary>
    /// Handles the vertical motion of the player based on the y component of user input on the right thumbstick
    /// </summary>
    /// <param name="valueRight">right thumbstick input value</param>
    private void MovePlayer(Vector2 valueRight)
    {
        Vector3 direction = new Vector3(0, valueRight.y, 0) * vertSpeed * Time.deltaTime;   // Calculates the direction and magnitude of vertical motion, with a deltaTime modifier to keep motion independent of calculation speed
        character.Move(direction);  // Moves player unless impeded by collision with a RigidBody
    }

}
