using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabRight : MonoBehaviour
{
    public GameObject hand;
    private bool canGrab;
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
        }

    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("grab");
        if (other.gameObject.tag == "grab")
        {
            if (canGrab)
            {
                other.gameObject.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z);
                other.gameObject.transform.eulerAngles = new Vector3(hand.transform.rotation.x, hand.transform.rotation.y, hand.transform.rotation.z);
            }

        }
    }
}
