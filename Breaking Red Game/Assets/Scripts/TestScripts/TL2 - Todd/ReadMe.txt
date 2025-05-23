README FOR "Todd Carter - NPCBoundaryTests" and "Todd Carter - NPCStressTest" BOUNDARY TEST AND USAGE



This is an attempt at creating two adjunct boundary tests.
These do not use Test Runner in any way.

The first test makes the NPC (TheWolf) move in the direction of the player, and
collision terrain is placed in the way.

After a certain time of not reaching the player, the test will succeed.

The second test makes the NPC (TheWolf) reset and then move in the direction of
the player until it encounters damaging terrain.  The NPC health decreases until
it is at or below zero, and after a set amount of time, if the NPC is inactive (dead),
the test succeeds.

Both tests are in the same coroutine, as they cannot happen concurrently but have to
happen one after the next.

Further, this script is meant to be activated by a clickable button that appears on screen,
which is the solution that was presented by the TL3s.  When the tests have completed, a
message is displayed on the console.

This button can be found on Level 1 of the project, as well as the relevant gameObjects
such as the two collidable walls.


USAGE:

Using the "Level 1" scene in the project, an empty game object must be made in the hierarchy.  

The "Todd Carter - NPCBoundaryTests" script must be attached as a component to the new empty game object.

Then, a button must be made.  "Button - TextMeshPro" under UI in the GameObject menu will work.

Add On Click clickable event to the button, and attach the new game object with the component script.

Set the clickable event to the "NPCTestScript.RunTests" function from the component's function list.



FOR NPC STRESS TESTING:

This is an attempt at creating a stress test for the project.

It very simply activates a box beneath the testing position.

Then it starts spawning NPCs at that location, one per frame.

Once one of the NPCs is detected having left the boundary, the test ends.

An on-screen button is required for usage.

An on-screen display shows how many NPCs were spawned before one was pushed out of the boundary.


STRESS TEST USAGE:

Assuming the buttons and prefabs have already been made, then running the test will be done using 
the on screen button.
