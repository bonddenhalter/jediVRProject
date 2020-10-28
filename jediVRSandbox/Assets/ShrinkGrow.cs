using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkGrow : MonoBehaviour
{
    public GameObject player;
    private GameObject emptyGameObject;
    public GameObject map;
    public float maxSize = 1f;
    public float minSize = 0.5f;
    public float timeToChange = 1f;
    public bool quickChange = false;
    public bool shrinkPlayer = false;
    public float coolDownTimer = 1f;
    private bool coolDownReady = true;
    private bool quickChangeCoolDownReady = false;
    public Camera centerEyeAnchor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            quickChange = !quickChange;
        }

        //Uses Button A on the Rift
        if (OVRInput.Get(OVRInput.RawButton.A) && coolDownReady)
        {
            emptyGameObject = new GameObject("GameObject used to scale world");
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            emptyGameObject.transform.position = new Vector3 (player.transform.position.x, 0f, player.transform.position.z);
            emptyGameObject.transform.localScale = map.transform.localScale;
            map.transform.SetParent(emptyGameObject.transform);
            if (quickChange)
            {
                StartCoroutine(Blindfold());
                if (shrinkPlayer)
                {
                    this.gameObject.GetComponent<CharacterController>().height = 1f;
                    this.gameObject.GetComponent<CharacterController>().radius = 0.25f;
                    emptyGameObject.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                }
                else
                {
                    this.gameObject.GetComponent<CharacterController>().height = 2f;
                    this.gameObject.GetComponent<CharacterController>().radius = 0.5f;
                    emptyGameObject.transform.localScale = new Vector3(minSize, minSize, minSize);
                }
                map.transform.SetParent(null);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                shrinkPlayer = !shrinkPlayer;
            }
            else 
            {
                if (shrinkPlayer)
                {
                    this.gameObject.GetComponent<CharacterController>().height = 1f;
                    this.gameObject.GetComponent<CharacterController>().radius = 0.25f;
                }
                else
                {
                    this.gameObject.GetComponent<CharacterController>().height = 2f;
                    this.gameObject.GetComponent<CharacterController>().radius = 0.5f;
                }
                StartCoroutine(ScaleOverTime(timeToChange));
                shrinkPlayer = !shrinkPlayer;
            }
            StartCoroutine(CoolDownCoroutine());

        }

    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = emptyGameObject.transform.localScale;
        Vector3 destinationScale;
        if (shrinkPlayer)
        {
             destinationScale = new Vector3(maxSize, maxSize, maxSize);
        }
        else
        {
             destinationScale = new Vector3(minSize, minSize, minSize);
        }

        float currentTime = 0.0f;

        do
        {
            emptyGameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
        map.transform.SetParent(null);
        Destroy(emptyGameObject);
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    IEnumerator CoolDownCoroutine()
    {
        coolDownReady = !coolDownReady;
        this.GetComponent<OVRPlayerController>().enabled = false;
        yield return new WaitForSeconds(coolDownTimer);
        coolDownReady = !coolDownReady;
        this.GetComponent<OVRPlayerController>().enabled = true;
    }

    IEnumerator Blindfold()
    {
        centerEyeAnchor.cullingMask = 0;
        yield return new WaitForSeconds(coolDownTimer);
        centerEyeAnchor.cullingMask = -1;
    }


}

