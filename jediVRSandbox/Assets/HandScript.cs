using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public GameObject LeftHandAnchor;
    public GameObject RightHandAnchor;
    public Animator rightAnimator;
    public Animator leftAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!RightHandAnchor.GetComponent<grabRight>().isGrab)
        {
            rightAnimator.SetBool("isGrab", false);
        }
        if (RightHandAnchor.GetComponent<grabRight>().isGrab)
        {
            rightAnimator.SetBool("isGrab", true);
            rightAnimator.SetBool("isFist", false);
            rightAnimator.SetBool("isIdle", false);
        }
        if (!OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            rightAnimator.SetBool("isIdle", true);
            rightAnimator.SetBool("isFist", false);
        }
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            rightAnimator.SetBool("isFist", true);
            rightAnimator.SetBool("isGrab", false);
            rightAnimator.SetBool("isIdle", false);
        }

        if (!LeftHandAnchor.GetComponent<grabLeft>().isGrab)
        {
            leftAnimator.SetBool("isLGrab", false);
        }
        if (LeftHandAnchor.GetComponent<grabLeft>().isGrab)
        {
            leftAnimator.SetBool("isLGrab", true);
            leftAnimator.SetBool("isLFist", false);
            leftAnimator.SetBool("isLIdle", false);
        }
        if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            leftAnimator.SetBool("isLIdle", true);
            leftAnimator.SetBool("isLFist", false);
        }
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            leftAnimator.SetBool("isLFist", true);
            leftAnimator.SetBool("isLGrab", false);
            leftAnimator.SetBool("isLIdle", false);
        }
    }
}
