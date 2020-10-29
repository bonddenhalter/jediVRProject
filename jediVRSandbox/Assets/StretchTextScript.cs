using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StretchTextScript : MonoBehaviour
{
    public float revealTime = 2f;

    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();

        //start out with text invisible
        Color color = text.color;
        color.a = 0.0f;
        text.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //make the text appear
    public void RevealText()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float currentTime = 0.0f;
        do
        {
            float alpha = currentTime / revealTime;
            Color c = text.color;
            c.a = alpha;
            text.color = c;
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= revealTime);

        //make sure we get to 100% opacity
        Color color = text.color;
        color.a = 1.0f;
        text.color = color;
    }
}
