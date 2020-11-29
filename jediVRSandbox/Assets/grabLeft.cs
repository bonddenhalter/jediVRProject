using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabLeft : MonoBehaviour
{
    public GameObject hand;
    public GameObject parent;
    private bool canGrab;
    public bool isGrab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            canGrab = true;
            isGrab = false;
        }
        else
        {
            canGrab = false;
        }

    }
    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "grab")
        {
            isGrab = true;
            //Debug.Log("grab");
            if (canGrab)
            {
                other.transform.SetParent(this.gameObject.transform);
                other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                other.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                other.GetComponent<Rigidbody>().useGravity = true;
                if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
                    other.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                other.transform.SetParent(parent.transform);
                //other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "grab")
        {
            isGrab = false;
        }
    }
}