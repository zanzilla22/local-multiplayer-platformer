using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<FightingPlatformer>();
        if(player != null)
        {
            player.StartCoroutine(player.lavaHurt());
        }
    }
}
