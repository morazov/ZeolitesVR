using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Displays the Framework Selection Menu when user requests. Renders as a stationary (in worldspace)
/// menu at a position offset from the user's position at the time of the request. This method of menu display
/// reduces motion sickness relative to an overlay on user's camera.
/// </summary>
public class ShowFrameworkMenu : MonoBehaviour
{
    public GameObject frameworkMenu;  // GameObject that contains the Framework Selection menu. This object will be shown by this script
    public GameObject moleculeMenu;   // GameObject that contains the Molecule Selection menu. This object will be hidden by this script
    public InputActionReference primaryButtonReferenceRight = null; // The button that brings up or hides the frameworkMenu
    public GameObject player;         // The Main Camera object that will be used as a reference position and orientation for the rendering of the frameworkMenu

    /// <summary>
    /// Subscribes the Toggle() method to the right primary button (A button on a Quest 2)
    /// </summary>
    private void Awake()
    {
        primaryButtonReferenceRight.action.started += Toggle;
    }

    /// <summary>
    /// Unsubscribes the Toggle() method from the right primary button (A button on a Quest 2)
    /// </summary>
    private void OnDestroy()
    {
        primaryButtonReferenceRight.action.started -= Toggle;
    }

    /// <summary>
    /// Toggles whether the frameworkMenu is displayed. 
    /// </summary>
    /// <param name="context">struct that contains callback context from the primary button started action event.
    /// Though it is not used by the Toggle() method, it is a required argument to be able to subscribe to an input action event</param>
    public void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = frameworkMenu.activeSelf;   // Check if the frameworkMenu is already being displayed
        if (!isActive)  // If the menu was hidden perform the following
        {
            moleculeMenu.SetActive(false); // Hide the moleculeMenu, as it may get in the way
            Vector3 playerPos = player.transform.position;  // Get the user's position
            Vector3 playerDirection = player.transform.forward; // Get the user's forward facing direction
            Quaternion playerRotation = player.transform.rotation;  // Get the user's rotation
            float spawnDistance = 10f;   // Set offset distance, at which to draw the frameworkMenu

            // Calculate the position where to draw the framewrokMenu, and update its position and rotation
            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
            frameworkMenu.transform.SetPositionAndRotation(spawnPos, playerRotation);
        }
        frameworkMenu.SetActive(!isActive); ; // Flip active state - this will show the frameworkMenu at the new position if it was previously hidden, and hide it otherwise
    }
}