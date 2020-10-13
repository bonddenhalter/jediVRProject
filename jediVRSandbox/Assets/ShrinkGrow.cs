using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkGrow : MonoBehaviour
{
    private GameObject player;
    private GameObject emptyGameObject;
    public GameObject map;
    public float maxSize = 1f;
    public float minSize = 0.5f;
    public float timeToChange = 1f;
    public bool quickChange = false;
    public bool shrinkPlayer = false;
    public float coolDownTimer = 1f;
    private bool coolDownReady = true;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && coolDownReady)
        {
            emptyGameObject = new GameObject("GameObject used to scale world");
            emptyGameObject.transform.position = new Vector3 (player.transform.position.x, 0f, player.transform.position.z);
            emptyGameObject.transform.localScale = map.transform.localScale;
            map.transform.SetParent(emptyGameObject.transform);
            if (quickChange)
            {
                if (shrinkPlayer)
                {
                    emptyGameObject.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                }
                else
                {
                    emptyGameObject.transform.localScale = new Vector3(minSize, minSize, minSize);
                }
                map.transform.SetParent(null);
                shrinkPlayer = !shrinkPlayer;
            }
            else 
            {
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
    }

    IEnumerator CoolDownCoroutine()
    {
        coolDownReady = !coolDownReady;
        yield return new WaitForSeconds(coolDownTimer);
        coolDownReady = !coolDownReady;
    }
}

