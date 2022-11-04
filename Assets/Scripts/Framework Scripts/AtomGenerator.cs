using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Handles the drawing of the framework atoms from XYZ type data files, using a spacefilling style. 
/// If the use of bonds or rings is selected, calls corresponding scripts to render these representations.
/// </summary>
public class AtomGenerator : MonoBehaviour
{
    public DropdownFramework frameworkDropdown; // Dropdown menu where user chooses a framework to draw
    public DropdownSize sizeDropdown;           // Dropdown menu where user chooses the scale at which to draw the framework
    private string framework;                   // String containting the selected framework name
    private float scale;                        // Scalar that determines framework size
    public bool isDrawn = false;                // Drawn state. If a framework is already drawn, another framework will not be drawn
    public bool useBonds = false;               // Option to draw framework with cylinders representing bonds instead of spacefilling atoms
    public Toggle bonds;                        // Reference to a Toggle controlling the Framework display mode (space-filling atoms or bonds)
    public bool useRings = true;                // Option to shade in all of the 3, 4, 5, or 6 membered rings found in the framework
    public Toggle rings;                        // Reference to a Toggle controlling the use of shaded small rings
    public bool staticFramework = true;         // Option to lock framework atoms in place - prevents user from moving the framework by grabbing
    public Toggle staticFrameworkToggle;        // Reference to a Toggle controlling the mobility of the framework
    public GameObject[] atomPrefabs;            // Array that contains atom prefabs for drawing the framework
    public GameObject atomParentSi;             // Empty game object to store all Si atoms
    public GameObject atomParentO;              // Empty game object to store all O atoms
    public GameObject bondParent;               // Empty game object to store all bonds
    public GameObject ringParent;               // Empty game object to store all rings
    public GameObject frameworkParent;          // Game object that contains all other components
    public DrawBonds frameworkBondsScript;      // Script to draw a cylinder representing a bond
    public DrawRings frameworkRingsScript;      // Script to draw rings of size 3, 4, 5, or 6

    /// <summary>
    /// Draws framework according to selected display options (spacefilling or bond representation; with or without shaded small rings; static or mobile framework)
    /// Gets called from Framework Selection Menu
    /// </summary>
    public void DrawAtoms()
    {
        if (!isDrawn)   //Check if a framework is already drawn, to avoid instancing too many objects.
        {
            framework = frameworkDropdown.selectedString;   // Update the selected framework based on user input through the frameworkDropdown
            scale = sizeDropdown.scale; // Update the scale for drawing the framework based on user input through the sizeDropdown 
            useBonds = bonds.isOn;      // If useBonds selected, draw cylinder prefabs between Si and O atoms to represent Si-O bonds. Currently, atoms are not draw for this option.

            // Read and parse the crystallographic data
            TextAsset file = Resources.Load($"Crystallographic_Data/{framework}") as TextAsset; //Path of XYZ type 
            string[] filelines = file.text.Split('\n'); // Split file into array of lines
            int fileLength = filelines.Length - 2;      // Throw away the first and last lines
            string atomType;    // Si or O atoms supported currently (have prefabs implemented) - easy to extend to other atoms, if commonly used (i.e., heteroatoms such as Al, Sn, etc.)
            Vector3 location;   // Location to draw the atom prefab

            // Define a Box Collider that encapsulates all of the framework atoms.
            BoxCollider parentCollider = frameworkParent.GetComponent<BoxCollider>();   // If collider already exists, get it from frameworkParent.
            // If staticFramework selected, do not proceed with code that defines parentCollider, as it will not be used.
            staticFramework = staticFrameworkToggle.isOn;
            if (!staticFramework)
            {
                parentCollider = frameworkParent.AddComponent<BoxCollider>();                               // Add a BoxCollider that will be expanded to encapsulate all framework atoms.
                XRGrabInteractable grabInteractor = frameworkParent.AddComponent<XRGrabInteractable>();     // Add an XRGrabInteractable component to allow user to interact with the parentCollider.
                grabInteractor.retainTransformParent = false;   // Disable snapping back to the position and orientation the parentCollider was in prior to user grabbing the object.
                grabInteractor.useDynamicAttach = true;         // Ensure that the attachment point for the interactor is where the user grabs the object, instead of a predefined point.
                grabInteractor.selectMode = (InteractableSelectMode)1;
            }

            if (useBonds)
            {
                Vector3 bondEnd;    // Location of atom on the other end of the bond
                // Loop through each line to parse Si atom locations and their corresponding Si-O bonds
                for (int i = 0; i < fileLength; i++)
                {
                    string[] parts = filelines[i + 1].Split(',');
                    float coord0 = float.Parse(parts[0]);
                    float coord1 = float.Parse(parts[1]);
                    float coord2 = float.Parse(parts[2]);
                    location = new Vector3(coord0, coord1, coord2) * scale; // Scale the location of bond origin
                    atomType = parts[4].Trim();


                    // Find Si atoms only - they will be the start point of the bond
                    if (atomType == "Si")
                    {
                        // Get the list of bonded O atom locations
                        string[] bonds = parts[6].Split('N');
                        // For each O atom, draw a bond using the O location as the endpoint using the CreateBond method in the DrawBonds script
                        foreach (string bond in bonds)
                        {
                            string[] bondParts = bond.Split(';');
                            float oX = float.Parse(bondParts[0]);
                            float oY = float.Parse(bondParts[1]);
                            float oZ = float.Parse(bondParts[2]);
                            bondEnd = new Vector3(oX, oY, oZ) * scale;      // Scale the location of bond end
                            frameworkBondsScript.CreateBond(location, bondEnd, .3f * scale, bondParent); // Generate bond object with the scaled locations and scaled prefab width
                        }
                    }
                }
            }

            // If useBonds not selected, draw space-filling representation of Si and O atoms.
            else
            {
                // Loop through each line to determine atom locations, types, and names
                for (int i = 0; i < fileLength; i++)
                {
                    string[] parts = filelines[i + 1].Split(',');
                    float coord0 = float.Parse(parts[0]);
                    float coord1 = float.Parse(parts[1]);
                    float coord2 = float.Parse(parts[2]);
                    location = new Vector3(coord0, coord1, coord2) * scale;  // Scale the atom location
                    atomType = parts[4].Trim();

                    // Store O and Si separately for easier management elsewhere
                    if (atomType == "O")
                    {
                        var newAtom = Instantiate(atomPrefabs[0], location, atomPrefabs[0].transform.rotation);
                        newAtom.transform.localScale *= scale;  // Scale the prefab radius
                        newAtom.transform.SetParent(atomParentO.transform);
                        newAtom.name = i.ToString();   // Parts[3] contains O-site information - can incorporate into name, if helpful
                    }
                    else
                    {
                        var newAtom = Instantiate(atomPrefabs[1], location, atomPrefabs[1].transform.rotation);
                        newAtom.transform.localScale *= scale;  // Scale the prefab radius
                        newAtom.transform.SetParent(atomParentSi.transform);
                        newAtom.name = i.ToString();   // Parts[3] contains T-site information - can incorporate into name, if helpful
                    }
                }
            }
            isDrawn = true; // Update the Drawn state to true to avoid instantiating additional frameworks and creating too many objects.

            useRings = rings.isOn;
            if (useRings)   // If useRings selected, shade in the 3, 4, 5, and 6 memebered rings. These are generally too small for molecules to diffuse through.
            {
                TextAsset ring_file = Resources.Load($"Ring_Data/{framework}_rings") as TextAsset; //Path of xyz data of ring vertices
                string[] rings = ring_file.text.Split('\n');  // Split file into array of lines
                int ringCount = rings.Length - 2;   //Throw away the first and last lines
                // Loop through each line to find ring vectices and draw the ring using the CreateRing method in the DrawRings script
                for (int i = 0; i < ringCount; i++)
                {
                    string parsed = rings[i + 1].Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
                    string[] verticesFlat = parsed.Split(',');
                    int ringSize = verticesFlat.Length / 3;
                    Vector3[] vertices = new Vector3[ringSize];
                    for (int j = 0; j < ringSize * 3; j += 3)
                    {
                        float coord0 = float.Parse(verticesFlat[j]);
                        float coord1 = float.Parse(verticesFlat[j + 1]);
                        float coord2 = float.Parse(verticesFlat[j + 2]);
                        vertices[j / 3] = new Vector3(coord0, coord1, coord2) * scale;    // Scale the location of vertices
                    }
                    frameworkRingsScript.CreateRing(vertices, ringParent);  // Render rings using a separate script for the mesh triangulation
                }
            }

            // Update the frameworkParent collider if the framework is chosen to be dynamic
            if (!staticFramework)
            {
                Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);                     // Create a new Bounds object, but assign 0 size.
                bool hasBounds = false;                                                     // The parentCollider starts without finite bounds

                Renderer[] renderers = frameworkParent.GetComponentsInChildren<Renderer>(); // Get the renderers of all children in Framework
                foreach (Renderer renderer in renderers)                                    // For each renderer, encapsulate the bounds of parentCollider to encorporate the bounds of the rederer
                {
                    if (hasBounds)
                    {
                        bounds.Encapsulate(renderer.bounds);
                    }
                    else
                    {
                        bounds = renderer.bounds;                                           // If the rendere is the first to be considered, set the bounds of parentCollider to the bounds of this renderer
                        hasBounds = true;
                    }
                }
                if (hasBounds)  // Set the position and size of the parentCollider
                {
                    parentCollider.center = bounds.center - frameworkParent.transform.position; 
                    parentCollider.size = bounds.size;
                }
                else
                {
                    parentCollider.size = parentCollider.center = Vector3.zero;
                    parentCollider.size = Vector3.zero;
                }
            }
        }
    }
}
