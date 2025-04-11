Description: This prefab represents a standard passage or doorway to change the player’s location  in the game. It includes key components required for detecting player interaction and triggering teleportation.

Components:
1.	NormalPassage Script
Handles the logic for transition the player from one area to another using a tag-based system
Attach the script to allow transitions between sections of the map or movements to different areas within a level.
Customize the final location through tags on GameObjects in the scene and changes to the destination setting function in the script.
2.	Sprite Renderer
Displays the visual sprite of the door or passage.
Customize the Sprite Renderer component to change what the door looks like
Adjust sorting layer and order to control render priority in the game environment
3.	Box Collider2D
Provides collision box around the door or passage.
Set the collider size and shape to match how much of the sprite should be part of the door or passage.
4.	CanvasGroup
Controls the screen fade effect during teleportation to provide smooth transitions
Attach a desired CanvasGroup from the hierarchy to this serialized field in the NormalPassage script to trigger smoother game transitions for the camera.
Adjust the fade amount and speed through the script.
Setup Instructions:
1.	Add to Scene
a.	Drag the NormalPassage prefab into your Unity scene.
2.	Set Tag
a.	Assign the proper tag to define where the passage sends the player
b.	Make sure the tag is set with a destination in the getDestination() function
3.	Assign CanvasGroup
a.	In the prefab or scene instance, assign the fadePanel reference in the Inspector to a UI panel with a black image and a CanvasGroup component
4.	Test
a.	Enter Play mode and walk into the passage to verify fade, sound, teleportation, and music all function as expected
Requirements:
Unity 6000.0.38f1 or later
