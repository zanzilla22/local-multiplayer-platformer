using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public FightingPlatformer mover;
    private GameObject ScrollInput;
    void Awake()
    {
        //mover = this.GetComponent<FightingPlatformer>();
        playerInput = this.GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<FightingPlatformer>();
        while (movers.Length == 0)
        {
            movers = FindObjectsOfType<FightingPlatformer>();
        }
        Debug.Log(movers);
        int index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);

        ScrollInput = GameObject.FindGameObjectWithTag("ScrollInput");

        ScrollInput.GetComponent<PlayerCreator>().On();
    }
    public void OnMove(CallbackContext context)
    {
        if (mover != null)
        {
            mover.SetInputVector(new Vector2(context.ReadValue<float>(), 0));
        }
    }
    public void Jump(CallbackContext context)
    {
        if(context.ReadValue<float>()!= 0)
        {
            if (mover != null)
            {
                mover.Jump();
            }
        }
    }
    public void Attack1(CallbackContext context)
    {
        if(context.ReadValue<float>() != 0)
        {
            if (mover != null)
            {
                mover.basicAttack();
            }
        }
    }
    public void Attack2(CallbackContext context)
    {
        if(context.ReadValue<float>() != 0)
        {
            if (mover != null)
            {
                mover.bigAttack();
            }
        }
    }
    public void DisableScroll()
    {
        ScrollInput.GetComponent<PlayerCreator>().Off();
    }
}
