using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabRight : MonoBehaviour
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
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            canGrab = true;
        }
        else
        {
            canGrab = false;
            isGrab = false;
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "grab")
        {
            //Debug.Log("grab");
            if (canGrab)
            {
                isGrab = true;
                other.transform.SetParent(this.gameObject.transform);
                other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                other.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                isGrab = false;
                other.GetComponent<Rigidbody>().useGravity = true;
                if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
                    other.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                other.transform.SetParent(parent.transform);
                //other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
