using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Toggles the state of a controlled Toggle object if the controlling Toggle undergoes a state change.
/// </summary>
public class CheckBoxToggle : MonoBehaviour
{
    public Toggle thisToggle;   // Reference to controlling Toggle
    public Toggle otherToggle;  // Reference to controlled Toggle

    /// <summary>
    /// Adds listener for when the state of thisToggle changes, and inverts state of the otherToggle
    /// </summary>
    void Start()
    {
        thisToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    /// <summary>
    /// Inverts the value of otherToggle when the state of thisToggle is changed
    /// </summary>
    void ToggleValueChanged()
    {
        otherToggle.isOn = !thisToggle.isOn;
    }
}