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
    public bool shrinkPlayer = true;
    public float coolDownTimer = 1f;
    private bool coolDownReady = true;
    private bool quickChangeCoolDownReady = false;
    public Camera centerEyeAnchor;
    public Light light0;
    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;
    public Light light5;

    public GameObject monsterHandLeft;
    public GameObject monsterHandRight;
    public GameObject localAvatar; //human hands

    private bool shrinkGrowEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        SetHands(shrinkPlayer);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            quickChange = !quickChange;
        }

        //Uses Button A on the Rift
        if (OVRInput.Get(OVRInput.RawButton.A) && coolDownReady && shrinkGrowEnabled)
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
            SetHands(shrinkPlayer); //assuming shrinkPlayer has already been toggled at this point
            StartCoroutine(CoolDownCoroutine());

        }

    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = emptyGameObject.transform.localScale;
        float originalLight = light0.range;
        float lightSize = 0.3f;
        Vector3 destinationScale;
        if (shrinkPlayer)
        {
             destinationScale = new Vector3(maxSize, maxSize, maxSize);
             lightSize = 0.3f * 5;
        }
        else
        {
             destinationScale = new Vector3(minSize, minSize, minSize);
            lightSize = 0.3f;
        }

        float currentTime = 0.0f;

        do
        {
            emptyGameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);

            //light0.range = lightSize * currentTime / time;
            light0.range = Mathf.Lerp(originalLight, lightSize, currentTime / time);
            light1.range = Mathf.Lerp(originalLight, lightSize, currentTime / time);
            light2.range = Mathf.Lerp(originalLight, lightSize, currentTime / time);
            light3.range = Mathf.Lerp(originalLight, lightSize, currentTime / time);
            light4.range = Mathf.Lerp(originalLight, lightSize, currentTime / time);
            light5.range = Mathf.Lerp(originalLight, lightSize, currentTime / time);

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

    // human: if true, set to human hands. If false, set to monster hands
    void SetHands(bool human)
    {
        monsterHandLeft.SetActive(!human);
        monsterHandRight.SetActive(!human);
        localAvatar.SetActive(human);
    }

    public void EnableShrinkGrow(bool enable)
    {
        shrinkGrowEnabled = enable;
    }

}

