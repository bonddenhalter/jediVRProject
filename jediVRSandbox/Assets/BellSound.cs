using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.rotation.eulerAngles.x > 10f || this.transform.rotation.eulerAngles.x < -10f || this.transform.rotation.eulerAngles.z < -10f || this.transform.rotation.eulerAngles.z > 10f)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
