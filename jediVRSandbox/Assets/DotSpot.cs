using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpot : MonoBehaviour
{
    private ConnectTheDots connectTheDots;
    private CrystalPlacement crystalPlacement;


    // Start is called before the first frame update
    void Start()
    {
        connectTheDots = this.transform.parent.GetComponent<ConnectTheDots>();
        crystalPlacement = GameObject.Find("Crystal-Placement Puzzle").GetComponent<CrystalPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        connectTheDots.RegisterTouch(this.name);
        if(connectTheDots.isFinished() && other.gameObject.name.StartsWith("Crystal_")) //NOTE: making assumption that no crystals will be placed before connect the dots is finished.
        {
            crystalPlacement.registerCrystalEntry(this.name, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (connectTheDots.isFinished() && other.gameObject.name.StartsWith("Crystal_"))
        {
            crystalPlacement.registerCrystalExit(this.name, other);
        }
    }

}
