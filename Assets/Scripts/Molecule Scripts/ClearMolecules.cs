using UnityEngine;
/// <summary>
/// Handles the deletion of all Molecule objects.
/// </summary>
public class ClearMolecules : MonoBehaviour
{
    /// <summary>
    /// Deletes all GameObjects that were generated through the MoleculeGenerator script.
    /// Gets called from the Molecule Selection menu
    /// </summary>
    public void ClearMoleculeChildren()
    {
        foreach (Transform child in transform)      // Loops through the different types of objects (e.g., C atoms, H atoms, O atoms)
        {
            foreach (Transform grandChild in child) // Loops through each instances of a given object type
            {
                Destroy(grandChild.gameObject);     // Deletes the object
            }
        }
    }
}
