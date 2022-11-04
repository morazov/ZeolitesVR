using UnityEngine;
/// <summary>
/// Handles the drawing of molecules from XYZ type data files, using a spacefilling style.
/// </summary>
public class MoleculeGenerator : MonoBehaviour
{
    public DropdownMolecule moleculeDropdown;   // Dropdown menu where user chooses molecule to draw;
    public DropdownSize sizeDropdown;           // Dropdown menu where user chooses the scale at which to draw the atoms (set in the Framework Selection menu)
    public GameObject[] atomPrefabs;            // Array to contain atom prefabs that will be used to draw framework
    public GameObject atomParentC;              // Empty game object to store all C atoms
    public GameObject atomParentH;              // Empty game object to store all H atoms
    public GameObject atomParentO;              // Empty game object to store all O atoms
    public BondGenerator bondScript;            // Script that joins atoms with bonds
    public GameObject player;                   // The Main Camera object that will be used as a reference position and orientation for the rendering of molecules
    private string molecule;                    // Selected molecule to be drawn
    private float scale;                        // Scalar that determines molecule size
    public int moleculeCount = 0;               // Counter for the number of existing molecules - can implement a limit if performance issues are found
    
    /// <summary>
    /// Draws and connects atoms of the selected molecule according to spacefilling representation.
    /// Gets called from Molecule Selection menu
    /// </summary>
    public void DrawMolecule()
    {
        // Reposition spawn origin to be in front of player
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        float spawnDistance = 5;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        moleculeCount += 1;
        molecule = moleculeDropdown.selectedString; // Get the selected molecule from the moleculeDropdown
        scale = sizeDropdown.scale; // Get selected scale value from the sizeDropdown
        TextAsset file = Resources.Load($"Molecule_Data/{molecule}_atoms") as TextAsset;  //Path of xyz type data
        // split file into array of lines
        string[] filelines = file.text.Split('\n');
        int fileLength = filelines.Length - 2;  //Throw away the first and last lines
        string atomType;
        Vector3 location;

        // Loop through each line to determine atom locations, types, and names
        for (int i = 0; i < fileLength; i++)
        {
            string[] parts = filelines[i + 1].Split(',');
            float coord0 = float.Parse(parts[2]);
            float coord1 = float.Parse(parts[3]);
            float coord2 = float.Parse(parts[4]);
            location = new Vector3(coord0, coord1, coord2) * scale + spawnPos; // Scale the atom location, but not the offset from the player
            atomType = parts[1].Trim();
            if (atomType == "C")
            {
                var newAtom = Instantiate(atomPrefabs[0], location, atomPrefabs[0].transform.rotation);
                newAtom.transform.localScale *= scale;  // Scale the prefab radius
                newAtom.transform.SetParent(atomParentC.transform);
                newAtom.name = parts[0] + $" - Molecule {moleculeCount}";
            }
            else if (atomType == "H")
            {
                var newAtom = Instantiate(atomPrefabs[1], location, atomPrefabs[1].transform.rotation);
                newAtom.transform.localScale *= scale;  // Scale the prefab radius
                newAtom.transform.SetParent(atomParentH.transform);
                newAtom.name = parts[0] + $" - Molecule {moleculeCount}";
            }
            else
            {
                var newAtom = Instantiate(atomPrefabs[2], location, atomPrefabs[2].transform.rotation);
                newAtom.transform.localScale *= scale;  // Scale the prefab radius
                newAtom.transform.SetParent(atomParentO.transform);
                newAtom.name = parts[0] + $" - Molecule {moleculeCount}";
            }
        }
        bondScript.BondPopulate(molecule, moleculeCount);   // Call BondPopulate() method in bondScript to fix the relative positions of the atoms in the generated molecule, so that the player can move and rotate the molecule as a whole.
    }


}
