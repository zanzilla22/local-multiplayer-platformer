using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSizer : MonoBehaviour
{
    private Vector3 lerpPos;
    public float scalingSpeed = 0.8f;
    public float scalingSize;
    public float maxSize;
    public float minSize;
    public GameObject[] players;
    //public Transform p1;
    //public Transform p2;
    void LateUpdate()
    {
        //this.transform.position = p1.position * (Vector3.Distance(p1.position, p2.position) / 2);
        //this.transform.position
        for (int i = 0; i < players.Length-1; i++)
        {
            lerpPos = new Vector3(Vector3.Lerp(players[i].transform.position, players[i+1].transform.position, 0.5f).x, Vector3.Lerp(players[i].transform.position, players[i+1].transform.position, 0.5f).y, this.transform.position.z);//new Vector3(p1.position.x + p2.position.x /2, p1.position.y + p2.position.y / 2, -10);
        }
        this.transform.position = lerpPos;
        this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(this.GetComponent<Camera>().orthographicSize, Vector3.Distance(players[0].transform.position, players[1].transform.position) / scalingSize, scalingSpeed);
        if (this.GetComponent<Camera>().orthographicSize > maxSize)
        {
            this.GetComponent<Camera>().orthographicSize = maxSize;
        } else if(this.GetComponent<Camera>().orthographicSize < minSize)
        {
            this.GetComponent<Camera>().orthographicSize = minSize;
        }
        //if(GameObject.FindGameObjectsWithTag("Player").Length != 0)
        //{
        //    ResetPlayers();
        //}
    }
    public void ResetPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
