using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update(); //needed to get Oculus controller input

        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Two))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    private void FixedUpdate()
    {
        OVRInput.FixedUpdate(); //needed to get Oculus controller input
    }
}
