using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellSound : MonoBehaviour
{
    private Vector3 lastFrame;
    private Vector3 thisFrame;
    // Start is called before the first frame update
    void Start()
    {
        lastFrame = this.transform.position;
        thisFrame = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastFrame = thisFrame;
        thisFrame = this.transform.position;

        if ((thisFrame - lastFrame).magnitude > 0.1f)
        {
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                if (!this.GetComponent<Rigidbody>().useGravity)
                {
                    this.GetComponent<AudioSource>().Play();
                }
            }
        }

    }
}
