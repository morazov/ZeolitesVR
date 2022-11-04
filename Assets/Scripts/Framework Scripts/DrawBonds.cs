using UnityEngine;
/// <summary>
/// Handles the drawing of cylinders that represent Si-O bonds
/// </summary>
public class DrawBonds : MonoBehaviour
{
    public GameObject cylinderPrefab;   // Bond cylinder prefab

    /// <summary>
    /// Draws a cylinder representing an Si-O bond, when called from the AtomGenerator script
    /// </summary>
    /// <param name="start">the location of the Si atom, and starting point of the bond</param>
    /// <param name="end">the location of the O atom, and the ending point of the bond</param>
    /// <param name="width">the width of the cylinder representing the bond</param>
    /// <param name="bondParent">GameObject that will store all generated bonds, for organizational purposes</param>
    public void CreateBond(Vector3 start, Vector3 end, float width, GameObject bondParent)
    {
        float overlap = 1.02f;  // A scaling factor that draws the bond a little beyond the starting point and ending point, to avoid awkward gaps when cylinders meet at angles
        var offset = (end - start) * overlap;   // Vector that points along the direction of the bond cylinder
        var scale = new Vector3(width, offset.magnitude / 2.0f, width); // Scaling factors for x, y, and z directions
        var position = ((start + end) / 2.0f);   // The position of the center point of the bond

        var cylinder = Instantiate(cylinderPrefab, position, Quaternion.identity);  // Generate bond at the center point
        cylinder.transform.up = offset; // Reorient the cylinder axis to point along the direction of the bond
        cylinder.transform.localScale = scale;  // Rescale to selected size
        cylinder.transform.SetParent(bondParent.transform); // Assign to bondParent to organize object structure
    }

}
