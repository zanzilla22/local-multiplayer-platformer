using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    public GameObject[] inputStuff;
    public GameObject cam;
    public void On()
    {
        if (!inputStuff[0].activeSelf)
        {
            for (int i = 0; i < inputStuff.Length; i++)
            {
                inputStuff[i].SetActive(true);
                cam.GetComponent<cameraSizer>().enabled = false;
                cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
                cam.GetComponent<Camera>().orthographicSize = 5;
            }
        }
    }
    public void Off()
    {
        for (int i = 0; i < inputStuff.Length; i++)
        {
            inputStuff[i].SetActive(false);
            cam.GetComponent<cameraSizer>().enabled = true;
        }
    }
}