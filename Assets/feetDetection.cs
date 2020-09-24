using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetDetection : MonoBehaviour
{
    public LayerMask whatisGround;
    public bool isTouchingGround(float radius)
    {
        var other = Physics2D.OverlapCircleAll(this.transform.position, radius, whatisGround);
        if (other.Length > 0)
        {
            return true;
        }
        else
            return false;
    }

}
