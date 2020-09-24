using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodDeleter : MonoBehaviour
{
    public float runTime = 4;

    // Update is called once per frame
    void Update()
    {
        runTime -= Time.deltaTime;
        if(runTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
