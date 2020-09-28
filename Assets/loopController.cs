using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopController : MonoBehaviour
{
    public List<FightingPlatformer> players;
    void Update()
    {
        if(players.Count != 0)
        {
            bool reset = true;
            for (int i = 0; i < players.Count; i++)
            {
                if(!players[i].wantsToReset)
                {
                    reset = false;
                }
            }
            if(reset)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].StartCoroutine(players[i].Restart());
                }
            }
        }
    }
}
