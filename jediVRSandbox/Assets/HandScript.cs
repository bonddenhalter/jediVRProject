using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Animator rightAnimator;
    public Animator leftAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && !GetComponent<grabRight>().isGrab)
        {
            rightAnimator.SetBool("isFist", true);
            rightAnimator.SetBool("isGrab", false);
            rightAnimator.SetBool("isIdle", false);
        }
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && GetComponent<grabRight>().isGrab)
        {
            rightAnimator.SetBool("isGrab", true);
            rightAnimator.SetBool("isFist", false);
            rightAnimator.SetBool("isIdle", false);
        }
        if (!OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            rightAnimator.SetBool("isIdle", true);
            rightAnimator.SetBool("isFist", false);
            rightAnimator.SetBool("isGrab", false);
        }
    }
}
