using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalPlacement : MonoBehaviour
{

    private string[] crystalPlacements = { "Blue", "Yellow", "Purple", "Red", "Green" };
    private bool[] crystalsCorrect = new bool[5]; //initialized to false
    private bool finished = false;
    private List<GameObject> crystals = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //save references to crystals
        foreach (string color in crystalPlacements)
        {
            GameObject crystal = this.transform.Find("Crystal_" + color).gameObject;
            crystals.Add(crystal);
        }

        toggleCrystalGlows(false); //turn off crystal glows
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void registerCrystalEntry(string spotName, Collider crystalCollider)
    {
        int spotIndex = int.Parse(spotName[4].ToString());
        if (crystalPlacements[spotIndex] == crystalCollider.gameObject.name.Substring(8)) //if the crystal is in the correct place
        {
            crystalsCorrect[spotIndex] = true;
        }

        //if all the crystals are correct, we're done
        if (crystalsCorrect.All(x => x)) //TODO: wait until the last crystal is released from the user's hand?
        {
            onFinish();
        }
    }

    public void registerCrystalExit(string spotName, Collider crystalCollider)
    {
        int spotIndex = int.Parse(spotName[4].ToString());
        if (crystalPlacements[spotIndex] == crystalCollider.gameObject.name.Substring(8)) //if we're taking out the crystal that should have been there
        {
            crystalsCorrect[spotIndex] = false;
        }
    }

    private void toggleCrystalGlows(bool on)
    {
        foreach (GameObject crystal in crystals)
        {
            Light light = crystal.transform.GetComponentInChildren<Light>();
            light.intensity = on ? 2 : 0;
        }
    }

    private void onFinish()
    {
        finished = true;
        toggleCrystalGlows(true); //turn on crystal glows

        //TODO: either disable the ability to grab the crystals, or make them stop glowing if they move them again. I think the first option would be better

    }
}
