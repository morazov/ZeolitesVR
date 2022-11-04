using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// Handles population of options and option selection in the Molecules UI.
/// </summary>
public class DropdownMolecule : MonoBehaviour
{
    public TMP_Dropdown dropdown;   // Dropdown component for the Molecule selection
    public string selectedString;   // Name of the selected molecule - used by methods from other scripts such as MoleculeGenerator
    public List<string> molecules;  // List of all available molecules that the user may choose to draw
    /// <summary>
    /// Reads all moleule files available, and adds their name to list of molecule options that the user can select to draw
    /// </summary>
    void Start()
    {
        molecules.Add("Select");
        var files = Resources.LoadAll("Molecule_Data", typeof(TextAsset));
        for (int i = 0; i < files.Length; i++)
        {
            string newMolecule = files[i].name.Split('_')[0];
            if (!molecules.Contains(newMolecule))
            {
                molecules.Add(newMolecule); // Load all molecules
            }
        }
        PopulateList();
    }
    /// <summary>
    /// Updates the identity of the selected molecule file, when the value of the dropdown is changed
    /// </summary>
    /// <param name="index">index of the selected molecule in the molecules array</param>
    public void DropdownIndexChanged(int index)
    {
        selectedString = molecules[index];
    }

    /// <summary>
    /// Populates the list of selectable molecules in the dropdown component
    /// </summary>
    void PopulateList()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(molecules);
    }
}
