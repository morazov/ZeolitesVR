using UnityEngine;
/// <summary>
/// Creates a "bond" between atom pairs within a molecule by joining them with a FixedJoint component.
/// </summary>
public class BondGenerator : MonoBehaviour
{
    /// <summary>
    /// Reads the bonding scheme of a molecule and accordingly joins atom pairs with FixedJoint components.
    /// </summary>
    /// <param name="molecule">the name of the molecule for which the bonds are created</param>
    /// <param name="moleculeCount">a counter to separate molecules of the same type into distinct objects</param>
    public void BondPopulate(string molecule, int moleculeCount)
    {
        // Path of bond data of format: {atom1, atom2, bond order}
        // TODO: Currently, bond order is not used, as an explicit bond is not rendered as an object for molecules. Future versions can include other render formats for molecules, and a DrawBond script can be adapted and used here.
        TextAsset file = Resources.Load($"Molecule_Data/{molecule}_bonds") as TextAsset;
        string[] filelines = file.text.Split('\n'); // Split file into array of lines
        int fileLength = filelines.Length - 2;      // Throw away the first and last lines
        string atom1;   // Name of first atom in a bonded pair
        string atom2;   // Name of second atom in a bonded pair
        Transform atom1Transform = null;    // Transform of first atom in a bonded pair
        Transform atom2Transform = null;    // Transform of second atom in a bonded pair
        FixedJoint joint;                   // FixedJoint object that bonds the atom objects to a fixed relative position

        for (int i = 0; i < fileLength; i++)    // Loop through each line to determine atom locations, types, and names
        {
            string[] parts = filelines[i + 1].Split(',');   // Split each line into {atom1, atom2, bond order} components
            atom1 = parts[0].Trim() + $" - Molecule {moleculeCount}";   // Get and modify the identifying string for atom1
            atom2 = parts[1].Trim() + $" - Molecule {moleculeCount}";   // Get and modify the identifying string for atom2
            foreach (Transform child in transform)  // Get the transforms of the two atoms in the bond. These are children of the molecule transform.
            {
                if (child.Find(atom1) != null)
                {
                    atom1Transform = child.Find(atom1);
                }
                if (child.Find(atom2) != null)
                {
                    atom2Transform = child.Find(atom2);
                }
            };
            joint = atom1Transform.gameObject.AddComponent<FixedJoint>();   // Add a FixedJoint component to the transform of atom1
            joint.connectedBody = atom2Transform.gameObject.GetComponent<Rigidbody>();  // Specify the atom2 Rigidbody component as joined to atom 1
        }
    }
}
