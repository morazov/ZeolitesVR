using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Handles population of scale options and selection of scale in the Frameworks UI.
/// </summary>
public class DropdownSize : MonoBehaviour
{
    public TMP_Dropdown dropdown;           // Dropdown component for size selection
    public GameObject frameworkGenerator;   // Container for Framework objects
    public GameObject moleculeGenerator;    // Container for Molecule objects

    // public string selectedString;   // Name of the selected scale - used by methods from other scripts such as AtomGenerator
    public float scale = 1.0f;      // Value of selected scale - used by methods from other scripts such as AtomGenerator
    private List<string> sizes = new List<string> { "1 x", "1/2 x", "1/4 x", "1/8 x", "1/16 x" }; // List of strings corresponding to all available scales that the user may choose
    private List<float> multiplier = new List<float> { 1.0f, 0.5f, 0.25f, 0.125f, 0.0625f };     // List of values corresponding to all available scales that the user may choose

    /// <summary>
    /// Adds the available scales to the dropdown menu
    /// </summary>
    void Start()
    {
        dropdown.AddOptions(sizes);
    }


    /// <summary>
    /// Updates the selected size when the dropdown is changed and scales the framework and molecule atoms accordingly
    /// </summary>
    /// <param name="index">index of the selected size in the sizes array</param>
    public void DropdownIndexChanged(int index)
    {
        // selectedString = sizes[index];
        scale = multiplier[index];
    }

}
