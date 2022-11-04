using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Handles the deletion of the Framework objects.
/// </summary>
public class ClearAtoms : MonoBehaviour
{
    public AtomGenerator drawScript;    // Reference to AtomGenerator script, to check if a framework has been drawn already
    public GameObject frameworkParent;  // GameObject that stores all framework components
    /// <summary>
    /// Deletes all GameObjects that were generated through the AtomGenerator script and resets the frameworkParent transform
    /// Gets called from the Framework Selection Menu
    /// </summary>
    public void ClearAtomChildren()
    {
        if (drawScript.isDrawn)
        {
            foreach (Transform child in transform)      // Loops through the different types of objects (e.g., Si atoms, O atoms, Si-O bonds, Rings)
            {
                foreach (Transform grandChild in child) // Loops through each instances of a given object type
                {
                    Destroy(grandChild.gameObject);     // Deletes the object
                }
            }
            drawScript.isDrawn = false; // Updates isDrawn state to false, to allow another framework to be drawn.
            // Remove frameworkParent's collider and interactable components and reset its transform
            Rigidbody rb = frameworkParent.GetComponent<Rigidbody>();       // Get frameworkParent rigidbody to reset velocities
            Destroy(frameworkParent.GetComponent<BoxCollider>());           // Remove the collider that was sized for the previous framework
            Destroy(frameworkParent.GetComponent<XRGrabInteractable>());    // Remove the attached grab interactable
            frameworkParent.transform.rotation = Quaternion.identity;       // Set rotation quaternion for frameworkParent to identity
            rb.angularVelocity = Vector3.zero;                              // Set angular velocity for frameworkParent to zero
            frameworkParent.transform.position = Vector3.zero;              // Zero out the position of frameworkParent
            rb.velocity = Vector3.zero;                                     // Set linear velocity for frameworkParent to zero
        }
    }
}
