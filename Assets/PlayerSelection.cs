using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerSelection : MonoBehaviour
{
    public GameObject[] players;
    public TextMeshProUGUI[] healthbarNames;
    private GameObject currentPlayer;
    private GameObject currentAvatar;
    public TextMeshProUGUI charecterName;
    public TextMeshProUGUI specialName;
    public TextMeshProUGUI specialDescription;
    public TextMeshProUGUI PlayerNameField;
    private int playerNo = 0;

    private int activePlayers = 0;
    public Slider[] healthbars;
    void Awake()
    {
        activePlayers = 0;
        currentPlayer = players[0];
        currentAvatar = Instantiate(currentPlayer.GetComponent<FightingPlatformer>().avatar, this.transform.position + currentPlayer.GetComponent<FightingPlatformer>().avatar.transform.position, Quaternion.identity);
        charecterName.SetText(currentAvatar.GetComponent<avatarInfo>().Name);
        specialName.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackName);
        specialDescription.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackDescription);
    }
    public void nextPlayer()
    {
        //Debug.Log("nextPlayer");
        if(playerNo == players.Length - 1)
        {
            playerNo = 0;
        } else
        {
            playerNo++;
        }
        Destroy(currentAvatar);
        currentPlayer = players[playerNo];
        currentAvatar = Instantiate(currentPlayer.GetComponent<FightingPlatformer>().avatar, this.transform.position + currentPlayer.GetComponent<FightingPlatformer>().avatar.transform.position, Quaternion.identity);
        currentAvatar.transform.position = new Vector3(currentAvatar.transform.position.x, currentAvatar.transform.position.y + 0.15f, currentAvatar.transform.position.z);
        charecterName.SetText(currentAvatar.GetComponent<avatarInfo>().Name);
        specialName.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackName);
        specialDescription.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackDescription);
    }
    public void previousPlayer()
    {
        //Debug.Log("nextPlayer");
        if(playerNo == 0)
        {
            playerNo = players.Length -1;
        } else
        {
            playerNo--;
        }
        Destroy(currentAvatar);
        currentPlayer = players[playerNo];
        currentAvatar = Instantiate(currentPlayer.GetComponent<FightingPlatformer>().avatar, this.transform.position + currentPlayer.GetComponent<FightingPlatformer>().avatar.transform.position, Quaternion.identity);
        currentAvatar.transform.position = new Vector3(currentAvatar.transform.position.x, currentAvatar.transform.position.y + 0.15f, currentAvatar.transform.position.z);
        charecterName.SetText(currentAvatar.GetComponent<avatarInfo>().Name);
        specialName.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackName);
        specialDescription.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackDescription);
    }
    public void Chosen()
    {
        Destroy(currentAvatar);
        Instantiate(currentPlayer, currentPlayer.transform.position, Quaternion.identity);
        

        healthbarNames[activePlayers].SetText(PlayerNameField.text);
        currentPlayer.GetComponent<FightingPlatformer>().playerIndex = activePlayers;
        Debug.Log(activePlayers);
        currentPlayer.GetComponent<FightingPlatformer>().healthbar = healthbars[activePlayers];
        activePlayers += 1;
    }
}
