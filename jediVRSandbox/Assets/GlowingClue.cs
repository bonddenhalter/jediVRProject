using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: could add a light/halo or something to the images to make them appear like they're glowing

public class GlowingClue : MonoBehaviour
{
    public GameObject glowActivateObject; // TODO: for testing I set this to the player camera - for the game we'll want it to be a candle or something
    public float appearDistanceThreshold; //when the glowActivateObject gets this close, the crystals will start to appear
    public float fullVisibilityDistance; //when the glowActivateObject gets this close, the crystals will be fully visible

    private SpriteRenderer[] sprites;

    public AudioSource discoverClueSound;
    public AudioSource musicSound;
    private bool discovered = false;

    // Start is called before the first frame update
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites) //start out invisible
        {
            if (sprite.name != "newCandle")
            {
                Color color = sprite.color;
                color.a = 0.0f;
                sprite.color = color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distFromCrystals = Vector3.Distance(this.transform.position, glowActivateObject.transform.position);
        float opacity = 0.0f; //0 if distFromCrystals > appearDistanceThreshold
        if (distFromCrystals <= fullVisibilityDistance)
        {
            opacity = 1f;
        }
        else if (distFromCrystals <= appearDistanceThreshold)
        {
            //essentially I'm setting the opacity to the percentage of distance travelled between appearDistanceThreshold and fullVisibilityDistance
            float distFromFull = distFromCrystals - fullVisibilityDistance;
            float range = appearDistanceThreshold - fullVisibilityDistance;
            opacity = 1 - distFromFull / range; 
        }

        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.name != "newCandle")
            {
                Color color = sprite.color;
                color.a = opacity; //the alpha value is the opacity
                sprite.color = color;
            }
        }

        if (!discovered && opacity > 0.3f)
        {
            musicSound.Stop();
            discoverClueSound.Play();
            musicSound.PlayDelayed(discoverClueSound.clip.length);
            discovered = true;
        }

    }
}
