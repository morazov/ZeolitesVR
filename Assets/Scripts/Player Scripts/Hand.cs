using UnityEngine;
/// <summary>
/// Handles the animation of the hand models.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;             // Speed with which the animations are played
    Animator animator;              // Animator component that controls the Grip and Trigger animations
    private float gripTarget;       // Value of the target state for the grip animation (ranges 0 to 1, based on the input from the side trigger on the VR controller) 
    private float triggerTarget;    // Value of the target state for the trigger animation (ranges 0 to 1, based on the input from the front trigger on the VR controller)
    private float gripCurrent;      // Value of the current state for the grip animation
    private float triggerCurrent;   // Value of the current state for the trigger animation
    private string animatorGripParam = "Grip";          // Name of the grip animation parameter in the animator
    private string animatorTriggerParam = "Trigger";    // Name of the trigger animation parameter in the animator

    /// <summary>
    /// Get the reference to the Animator component
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Every frame call AnimateHand to bring the current animated states of the Grip and Trigger animations towards the target states
    /// </summary>
    void Update()
    {
        AnimateHand();
    }

    /// <summary>
    /// Target value for the Grip animation (passed by the HandController script)
    /// </summary>
    /// <param name="v"> 0 to 1 value obatined from the controller</param>
    internal void SetGrip(float v)
    {
        gripTarget = v;
    }
    /// <summary>
    /// Target value for the Trigger animation (passed by the HandController script)
    /// </summary>
    /// <param name="v">0 to 1 value obatined from the controller</param>
    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    /// <summary>
    /// Updates the Grip and Trigger animation states to approach the corresponding target states
    /// </summary>
    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }
}
