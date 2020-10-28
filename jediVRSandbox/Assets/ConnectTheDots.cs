using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Notes:
 * To set up this puzzle, create several colliders with lights as children. The parent colliders should be named Spot0, Spot1, and so on. Set numSpots
 * to the number of dots we need to hit (i.e. how many SpotX objects there are). Position the spots as desired. The spot gameobjects need to have the
 * DotSpot.cs script attached to them.
 * */

public class ConnectTheDots : MonoBehaviour
{

    public Camera playerCamera; // for determining user position
    public LineRenderer line; // the line we will draw
    public int numSpots; //the number of dots to connect. If we need to have the same dot twice, just create multiple dots in the same position

    private GameObject nextSpot; //the next dot we need to touch
    private int lightCount = 0; //how many lights we've touched
    private bool inProgress = false; // true if the first light has been touched, starting the game, and the last light hasn't been reached yet. TODO: reset puzzle if we leave the game area without finishing, if that's possible
    private bool finished = false; // true if we've connected all the dots and finished the puzzle
    private float heightAboveGround; //how high off the ground the lines should be drawn

    // Start is called before the first frame update
    void Start()
    {
        nextSpot = this.transform.Find("Spot0").gameObject; //initialize nextSpot to the Spot0
        for (int i = 1; i < numSpots; i++) //disable all spots except the first one
        {
            GameObject spot = this.transform.Find("Spot" + i.ToString()).gameObject;
            spot.SetActive(false);
        }

        heightAboveGround = nextSpot.transform.position[1] - 0.5f;
        line.SetPosition(0, new Vector3(nextSpot.transform.position[0], heightAboveGround, nextSpot.transform.position[2])); //initialize the line to be at the center of the first dot
        line.SetPosition(1, new Vector3(nextSpot.transform.position[0], heightAboveGround, nextSpot.transform.position[2]));
    }

    // Update is called once per frame
    void Update()
    {
        if (inProgress) //we're actively drawing the line
        {
            line.SetPosition(lightCount, new Vector3(playerCamera.transform.position[0], heightAboveGround, playerCamera.transform.position[2])); //follow the player with the end of the line
        }
    }

    // this function will be called by the trigger boxes to indicate when the player has touched a light
    public void RegisterTouch(string spotName)
    {
        if (!finished) //ignore touches after we've finished the puzzle
        {
            if (spotName == "Spot0") //start the game when you touch the first dot
            {
                inProgress = true;
            }

            if (spotName == nextSpot.name && inProgress) //reached the next checkpoint
            {
                //remove this line to have the vertex just lock in place instead of snapping to the center of the light
                line.SetPosition(lightCount, new Vector3(nextSpot.transform.position[0], heightAboveGround, nextSpot.transform.position[2]));

                //create a new line vertex
                if (lightCount != 0 && lightCount != numSpots-1)
                {
                    line.positionCount++;
                }
                lightCount++;

                nextSpot.SetActive(false); //disable the current light
            }

            if (lightCount == numSpots) // finished the game by reaching the last light
            {
                inProgress = false;
                finished = true;
                onFinish();
            }
            else //game still going
            {
                //advance nextSpot to the next light and activate it
                nextSpot = this.transform.Find("Spot" + lightCount.ToString()).gameObject;
                nextSpot.SetActive(true);
            }
        }
    }

    private void onFinish()
    {
        line.loop = true;
    }
}
