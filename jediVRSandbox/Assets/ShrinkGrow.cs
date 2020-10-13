using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkGrow : MonoBehaviour
{
    public GameObject player;
    public GameObject emptyGameObject;
    public GameObject map;
    public float maxSize = 2;
    public float minSize = 1;
    public float timeToChange = 1f;
    public bool quickChange = false;
    public bool smallToBig = true;
    public float coolDownTimer = 1f;
    private bool coolDownReady = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && coolDownReady)
        {
            emptyGameObject.transform.position = new Vector3 (player.transform.position.x, 0f, player.transform.position.z);
            map.transform.SetParent(emptyGameObject.transform);
            if (quickChange)
            {
                if (smallToBig)
                {
                    emptyGameObject.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                }
                else
                {
                    emptyGameObject.transform.localScale = new Vector3(minSize, minSize, minSize);
                }
                map.transform.SetParent(null);
                smallToBig = !smallToBig;
            }
            else 
            {
                StartCoroutine(ScaleOverTime(timeToChange));
                smallToBig = !smallToBig;
            }
            
            StartCoroutine(CoolDownCoroutine());

        }

    }

    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = emptyGameObject.transform.localScale;
        Vector3 destinationScale;
        if (smallToBig)
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
    }

    IEnumerator CoolDownCoroutine()
    {
        coolDownReady = !coolDownReady;
        yield return new WaitForSeconds(coolDownTimer);
        coolDownReady = !coolDownReady;
    }
}

