using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Handles the controller input to initiate animation of the hand models
/// </summary>
[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;   // Left or right controller
    public Hand hand;                   // Reference to left or right hand object
    // Get the reference to the controller
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Once per frame get the state of front or side trigger and call corresponding animations with the values.
    void Update()
    {
        hand.SetGrip(controller.selectAction.action.ReadValue<float>());        // Grip animation initiated by pressing the side trigger
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());   // Selection animation initiated by pressing the front trigger
    }
}
