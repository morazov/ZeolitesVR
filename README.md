# ZeolitesVR

This Unity project implements a virtual reality environment to visualize zeolite frameworks. The resulting build is sufficiently optimized to run on standalone devices such as Quest 2. However, PCVR should be used for a smoother experience, if available.

The frameworks are rendered procedurally from pre-computed atomic coordiantes (see [the Zeolite_CIF_Parser repo](https://github.com/morazov/Zeolite_CIF_Parser) for details). All frameworks listed on the [IZA Database of Zeolite Structures](https://america.iza-structure.org/IZA-SC/ftc_table.php) are included in this project, along with a summary of their crystallographic information.

Several display styles are included, allowing for different interaction modes, as [shown below](#render-and-interaction-modes).

A selection of small organic molecules may be rendered to gauge the scale and connectivity of pore channels.

## Render and Interaction Modes

The default settings for rendering and interacting with the zeolite framework result in a spacefilling representation of a large scale static framework. For visual clarity 6-membered rings and smaller (i.e., rings too small for significant molecular diffusion) are shaded. The user may navigate the framework structure to get a unique perspective of the morphology of the pores and cages. This experience in VR is difficult to appreciate through a 2D video, which makes the interaction seem similar to viewing a molecular model on a computer screen. The 360° video below is more representative and should be viewed in VR.

Alternatively, the user may render the framework using the Si-O bond representation that looks similar to the physical bonded-tetrahedra models some researchers may have tediously constructed to develop intuition about the symmetry and structure of a specific framework. The model may be rendered at different scales, with the smaller sizes giving a sense of a hand-held model, especially when the static rendering mode is disabled and the small rings are not shaded.

Another satisfying way to gain insights about the channels, cages, and side-pockets of a framework is to probe them with small organic molecules. The molecules are rendered in a space-filling style that, in conjunction with the space-filling representation of the framework, provides a first-order approximation of molecular fit.
