using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Handles population of options and option selection in the Frameworks UI.
/// </summary>
public class DropdownFramework : MonoBehaviour
{
    public TMP_Dropdown dropdown;   // Dropdown component for Framework code selection
    public TMP_Text frameworkPropertyNames; // Text in the Framework Properties canvas that displays the property names of the selected framework
    public TMP_Text frameworkPropertyValues;// Text in the Framework Properties canvas that displays the property values of the selected framework
    public string selectedString;   // Name of the selected framework - used by methods from other scripts such as AtomGenerator
    public List<string> frameworks; // List of all available frameworks that the user may choose to draw
    /// <summary>
    /// Reads all framework files available, and adds their name to list of framework options that the user can select to draw
    /// </summary>
    void Start()
    {
        frameworks.Add("Select");
        var files = Resources.LoadAll("Crystallographic_Data", typeof(TextAsset));
        for (int i = 0; i < files.Length; i++)
        {
            frameworks.Add(files[i].name);// Load all frameworks
        }
        PopulateList();
    }
    /// <summary>
    /// Updates the identity of the selected framework file, and the property name and values in the Framework Properties canvas, when the value of the dropdown is changed
    /// </summary>
    /// <param name="index">index of the selected framework in the frameworks array</param>
    public void DropdownIndexChanged(int index)
    {
        selectedString = frameworks[index];
        TextAsset file = Resources.Load($"Framework_Info/{frameworks[index]}_data") as TextAsset;
        string[] filelines = file.text.Split('\n'); // Split file into array of lines
        int fileLength = filelines.Length - 2;  //Throw away the first and last lines
        string name = "";
        string value = "";
        for (int i = 0; i < fileLength; i++)
        {
            string[] parts = filelines[i + 1].Split(';');
            name += "\n" + parts[0];
            value += "\n" + parts[1];
        }
        frameworkPropertyNames.text = name;
        frameworkPropertyValues.text = value;
    }

    /// <summary>
    /// Populates the list of selectable frameworks in the dropdown component
    /// </summary>
    void PopulateList()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(frameworks);
    }
}
