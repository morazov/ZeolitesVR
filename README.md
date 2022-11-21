# ZeolitesVR

This Unity project implements a virtual reality environment to visualize zeolite frameworks. The resulting build is sufficiently optimized to run on standalone devices such as Quest 2. However, PCVR should be used for a smoother experience, if available.

The frameworks are rendered procedurally from pre-computed atomic coordinates (see [the Zeolite_CIF_Parser repo](https://github.com/morazov/Zeolite_CIF_Parser) for details). All frameworks listed on the [IZA Database of Zeolite Structures](https://america.iza-structure.org/IZA-SC/ftc_table.php) are included in this project, along with a summary of their crystallographic information.

Several display styles are included, allowing for different interaction modes, as [shown below](#render-and-interaction-modes).

A selection of small organic molecules may be rendered to gauge the scale and connectivity of pore channels.

## Render and Interaction Modes

The default settings for rendering and interacting with the zeolite framework result in a [spacefilling representation](#default-settings) of a large scale static framework. For visual clarity 6-membered rings and smaller (i.e., rings too small for significant molecular diffusion) are shaded. The user may navigate the framework structure to get a unique perspective of the morphology of the pores and cages. This experience in VR is difficult to appreciate through a 2D video, which makes the interaction seem similar to viewing a molecular model on a computer screen. The 360° video below is more representative and should be viewed in VR. Alternatively, you may pan around using your mouse.

<table align="center" id="default-settings">
  <tr>
  <th>
    <a href="https://www.youtube.com/watch?v=N912hPK8uY8"><img src="https://user-images.githubusercontent.com/92121568/203129122-520d2166-b90a-4bcd-83ce-6ba57793a566.png" alt="Click to Play" height="300"></a>
    
  </th> 
  <th>
   <a href="https://www.youtube.com/watch?v=lQDemClLt44"><img src="https://user-images.githubusercontent.com/92121568/203154322-2d344e0b-8af4-430a-828d-7c4fa69dc75c.png" alt="Click to Play"  height="300"></a>
  </th>
  </tr>

  <tr>
  <td>
      2D Video of the FAU framework - Click to Play
  </td> 
  <td>
      360° VR Video - Click to Play
  </td> 
  </tr>
  
</table>



Alternatively, the user may render the framework using the Si-O bond representation that may be familiar to researchers that have tediously constructed physical bonded-tetrahedra models in order to develop intuition about the symmetry and structure of a framework. The model may be rendered at different scales, with the smaller sizes giving a sense of a hand-held model, especially when the static rendering mode is disabled and the small rings are not shaded.

Another intuitive way to gain insights about the channels, cages, and side-pockets of a framework is to probe them with small organic molecules. The molecules are rendered in a space-filling style that, in conjunction with the space-filling representation of the framework, provides a first-order approximation of molecular fit. ***Note:** to maintain acceptable performance on stand-alone devices, the current version of the project does not support intramolecular rotations, so separate molecular models are needed in order to consider conformers. Furthermore, no attempts are made at molecular dynamics or other energy minimization schemes, so molecular collisions are treated as rigid body collisions by the Unity physics engine.*

## Installation Instructions
### Standalone Devices

To directly use a prebuilt version of the project, download and install the included [.apk file](Builds/v1.0b1.apk) onto your device. This route has been successfully tested for an Oculus Quest 2, using the [SideQuest app](https://sidequestvr.com/setup-howto) to load an install the .apk file. Once installed, run ZeoliteVR (likely found under the Unknown Sources tab) and follow the in-app instructions. ***Note:** though not tested, in principle, this approach (with minor modifications) should also work for other Android-based standalone headsets. If you succeed in installing and running the app on a unique device, please let me know how, so that I may add the instructions to this repo.*

### PCVR

To run the app on your PC and use the PCVR capabilities of your headset download the [PCVR folder](Builds/PCVR) and run the [ZeolitesVR](Builds/PCVR/ZeolitesVR.exe) executable.

### Build with Unity

If you wish to modify or customize this project, you may clone or fork this repository and open it with Unity (you will need to install all of the required packages). After making the desired changes, build and install the app on your device directly through Unity, or through one of the methods above. Please cite this repository if you wish to publish or share your modified version. Additionally, please cite the original crystallographic data source if you wish to use the data in your own projects:\
Ch. Baerlocher and L.B. McCusker
Database of Zeolite Structures: http://www.iza-structure.org/databases/

## Limitations and Future Development

Suggestions to improve the app performance and/or the user experience are most welcome. This is a hobby project, and thus is made available with no guarantees. I will do my best to monitor and respond to any straightforward issues in this repo. Please let me know if you wish to contribute to further development of the app.
