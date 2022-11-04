using UnityEngine;
/// <summary>
/// Handles the shading in of small rings
/// </summary>
public class DrawRings : MonoBehaviour
{
    public Material material;   // Material to be used by the mesh renderer
    /// <summary>
    /// Shades the 3,4,5, or 6 Membered Rings - i.e., rings that are too small for molecular diffusion, when called from the AtomGenerator script
    /// </summary>
    /// <param name="vertices">the positions of the atoms that are part of the ring</param>
    /// <param name="ringParent">GameObject that will store all generated rings, for organizational purposes</param>
    public void CreateRing(Vector3[] vertices, GameObject ringParent)
    {
        Mesh mesh = new Mesh();         // Mesh that will be used to render the ring surface
        int ringSize = vertices.Length; // Determine the number of vertices in the ring
        int[] triangles = new int[(ringSize - 2) * 3];  // The ring is a convex hull, so triangulate the polygon using (ringSize - 2) triangles
        for (int i = 0; i < (ringSize - 2); i++)
        {
            int j = 3 * i;
            triangles[j] = 0;           // Set the first vertex as a common vertex of all triangles
            triangles[j + 1] = i + 1;   // Iterate over the rest of the pairs of points, such that the 2nd vertex of the ith triangle is the 3rd vertex of the (i-1)th triangle
            triangles[j + 2] = i + 2;   // The third vertex of the ith triangle is the next point on the hull that has not been used
        }
        // Update the mesh with the vertices and triangles
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();  // Adjust normals to make shading more realistic
        // Generate the ring object and update its mesh to the one just calculated
        GameObject ring = new GameObject($"{ringSize}-Membered Ring", typeof(MeshFilter), typeof(MeshRenderer));
        ring.GetComponent<MeshFilter>().mesh = mesh;            // Set mesh to the triangulated surface
        ring.GetComponent<MeshRenderer>().material = material;  // Set the material to control visualization
        ring.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;  // Turn off shadow casting
        ring.isStatic = true;                           // Set static fo static batching
        ring.transform.SetParent(ringParent.transform); // Assign to ringParent to organize object structure
    }
}


