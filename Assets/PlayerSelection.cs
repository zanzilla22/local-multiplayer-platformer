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

    public GameObject cam;

    public loopController loopController;
    void Awake()
    {
        StartIt();
    }
    void StartIt()
    {
        activePlayers = 0;
        currentPlayer = players[0];
        currentAvatar = Instantiate(currentPlayer.GetComponent<FightingPlatformer>().avatar, this.transform.position + currentPlayer.GetComponent<FightingPlatformer>().avatar.transform.position, Quaternion.identity);
        charecterName.SetText(currentAvatar.GetComponent<avatarInfo>().Name);
        specialName.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackName);
        specialDescription.SetText(currentAvatar.GetComponent<avatarInfo>().specialAttackDescription);
        nextPlayer();
        previousPlayer();
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
        //Instantiate(PlayerInputController, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < players.Length; i++)
        {
            nextPlayer();
        }
        Destroy(currentAvatar);
        GameObject thatSpawnedPlayer = Instantiate(currentPlayer, currentPlayer.transform.position, Quaternion.identity);
        

        healthbarNames[activePlayers].SetText(PlayerNameField.text);
        currentPlayer.GetComponent<FightingPlatformer>().playerIndex = activePlayers;


        Slider healthbarToAdd = healthbars[activePlayers];
        Debug.Log(activePlayers + " - " + healthbarToAdd);
        thatSpawnedPlayer.GetComponent<FightingPlatformer>().healthbar = healthbarToAdd;
        Debug.Log("Log2: " + activePlayers + " - " + healthbarToAdd);


        GameObject[] playerInput = GameObject.FindGameObjectsWithTag("PlayerInputHandler");
        for (int i = 0; i < playerInput.Length; i++)
        { 
            if (!playerInput[i].GetComponent<PlayerInputHandler>().hasPlayer)
            {
                playerInput[i].GetComponent<PlayerInputHandler>().mover = thatSpawnedPlayer.GetComponent<FightingPlatformer>();
            }
        }

        cam.GetComponent<multipleTargetCameraBrackeys>().targets.Add(thatSpawnedPlayer.transform);

        loopController.players.Add(thatSpawnedPlayer.GetComponent<FightingPlatformer>());

        activePlayers += 1;
    }
}