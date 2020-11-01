using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DataCollector : MonoBehaviour
{
    public GameObject player;
    private string dataBig;
    private string dataSmall;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<ShrinkGrow>().shrinkPlayer)
        {
            dataBig = player.transform.position.x + player.transform.position.z + Time.deltaTime + "";
        }
        else
        {
            dataSmall = player.transform.position.x + player.transform.position.z + Time.deltaTime + "";
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            WriteString();
        }
    }

    void WriteString()
    {
        string path = "Assets/test.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(dataBig);
        writer.WriteLine(dataSmall);
        writer.Close();
    }
}
