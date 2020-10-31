using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpot : MonoBehaviour
{
    private ConnectTheDots connectTheDots;


    // Start is called before the first frame update
    void Start()
    {
        connectTheDots = this.transform.parent.GetComponent<ConnectTheDots>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        connectTheDots.RegisterTouch(this.name);
    }
}
