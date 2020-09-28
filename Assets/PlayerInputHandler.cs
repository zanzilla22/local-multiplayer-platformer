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
    public bool hasPlayer = false;
    void Awake()
    {
        StartCoroutine(Setup());
        

    }
    void Update()
    {
        if (mover != null)
        {
            hasPlayer = true;
        }
    }
    public void Reset(CallbackContext context)
    {
        if (context.ReadValue<float>() != 0)
        {
            if (mover != null)
            {
                mover.resetToggle();
            }
        }
    }
    public void OnMove(CallbackContext context)
    {
        if (mover != null && !mover.dead)
        {
            mover.SetInputVector(new Vector2(context.ReadValue<float>(), 0));
        }
    }
    public void Jump(CallbackContext context)
    {
        if(context.ReadValue<float>()!= 0)
        {
            if (mover != null && !mover.dead)
            {
                mover.Jump();
            }
        }
    }
    public void Attack1(CallbackContext context)
    {
        if(context.ReadValue<float>() != 0)
        {
            if (mover != null && !mover.dead)
            {
                mover.basicAttack();
            }
        }
    }
    public void Attack2(CallbackContext context)
    {
        if(context.ReadValue<float>() != 0)
        {
            if (mover != null && !mover.dead)
            {
                mover.bigAttack();
            }
        }
    }
    public void DisableScroll()
    {
        ScrollInput.GetComponent<PlayerCreator>().Off();
    }
    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.1f);
        //mover = this.GetComponent<FightingPlatformer>();
        playerInput = this.GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<FightingPlatformer>();
        //while (movers.Length == 0)
        //{
        // movers = FindObjectsOfType<FightingPlatformer>();
        //}
        Debug.Log(movers);
        int index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);


        ScrollInput = GameObject.FindGameObjectWithTag("ScrollInput");
        ScrollInput.GetComponent<PlayerCreator>().On();
    }
}