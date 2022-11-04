using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Displays the Molecule Selection Menu when user requests. Renders as a stationary (in worldspace)
/// menu at a position offset from the user's position at the time of the request. This method of menu display
/// reduces motion sickness relative to an overlay on user's camera.
/// </summary>
public class ShowMoleculeMenu : MonoBehaviour
{
    public GameObject moleculeMenu;   // GameObject that contains the Molecule Selection Menu. This object will be shown by this script
    public GameObject frameworkMenu;  // GameObject that contains the Framework Selection Menu. This object will be hidden by this script
    public InputActionReference primaryButtonReferenceLeft = null;  // The button that brings up or hides the moleculeMenu
    public GameObject player;       // The Main Camera object that will be used as a reference position and orientation for the rendering of the moleculeMenu

    /// <summary>
    /// Subscribes the Toggle() method to the left primary button (X button on a Quest 2)
    /// </summary>
    private void Awake()
    {
        primaryButtonReferenceLeft.action.started += Toggle;
    }

    /// <summary>
    /// Unsubscribes the Toggle() method from the left primary button (X button on a Quest 2)
    /// </summary>
    private void OnDestroy()
    {
        primaryButtonReferenceLeft.action.started -= Toggle;
    }

    /// <summary>
    /// Toggles whether the moleculeMenu is displayed. 
    /// </summary>
    /// <param name="context">struct that contains callback context from the primary button started action event.
    /// Though it is not used by the Toggle() method, it is a required argument to be able to subscribe to an input action event</param>
    public void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = moleculeMenu.activeSelf;    // Check if the moleculeMenu is already being displayed
        if (!isActive)   // If the menu was hidden perform the following
        {
            frameworkMenu.SetActive(false);    // Hide the frameworkMenu, as it may get in the way
            Vector3 playerPos = player.transform.position;  // Get the user's position
            Vector3 playerDirection = player.transform.forward; // Get the user's forward facing direction
            Quaternion playerRotation = player.transform.rotation;  // Get the user's rotation
            float spawnDistance = 10f;   // Set offset distance, at which to draw the moleculeMenu

            // Calculate the position where to draw the moleculeMenu, and update its position and rotation
            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
            moleculeMenu.transform.SetPositionAndRotation(spawnPos, playerRotation);
        }
        moleculeMenu.SetActive(!isActive); // Flip active state -this will show the moleculeMenu at the new position if it was previously hidden, and hide it otherwise
    }
}