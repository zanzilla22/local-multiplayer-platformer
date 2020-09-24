﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FightingPlatformer : MonoBehaviour
{
    [SerializeField]
    public int playerIndex = 0;
    public float health = 12;
    public Slider healthbar;
    public GameObject blood;
    public GameObject avatar;

    public float basicAttackCoolDown;
    private float orBasicAttackCoolDown;
    public float bigAttackCoolDown;
    private float orBigAttackCoolDown;

    public LayerMask whatIsPlayer;
    private bool dead = false;

    public feetDetection feet;
    private int jumpCount = 3;
    public string specialAttack;

    public float moveSpeed;
    public float jumpMoveSpeed;
    private Rigidbody2D rb;
    public float jumpForce;

    private Vector2 inputVector;
    private Vector2 moveDir;

    private Animator anim;

    public void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        anim.SetBool("Dead", false);
        anim.enabled = true;
        orBasicAttackCoolDown = basicAttackCoolDown;
        orBigAttackCoolDown = bigAttackCoolDown;
        healthbar.maxValue = health;
        //healthbar is not refrenced
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }
    void Update()
    {
        healthbar.value = health;
        if (health <= 0)
        {
            dead = true;
            StartCoroutine(Die());
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            if (feet.isTouchingGround(0.3f))
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                this.GetComponent<BoxCollider2D>().enabled = false;
            }

            return;
        }

        moveDir = inputVector;

        //attack cooldowns
        //if (basicAttackCoolDown == 0)
        //{
        //    StartCoroutine(smallCoolDown(1));
        //}

        //rb.AddForce(this.transform.right * new Vector2(moveDir.x, 0) * moveSpeed);
        if (!dead)
        {
            if (feet.isTouchingGround(0.3f))
            {
                this.transform.position += new Vector3(moveDir.x * moveSpeed, 0, 0);
                jumpCount = 2;
            }
            else
                this.transform.position += new Vector3(moveDir.x * jumpMoveSpeed, 0, 0);

            //running
            if (moveDir.x != 0 && rb.velocity.y == 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
                anim.SetBool("isRunning", false);
            //flipping
            if (moveDir.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (moveDir.x < 0)
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            //jumping animation
            if (rb.velocity.y > 0)
            {
                anim.SetBool("IsJump", true);
            }
            else
                anim.SetBool("IsJump", false);
            if (rb.velocity.y < 0)
            {
                anim.SetBool("isFall", true);
            }
            else
                anim.SetBool("isFall", false);
        }
    }
    public void Jump()
    {
        if (feet.isTouchingGround(0.3f) || jumpCount != 0)
        {
            rb.AddForce(this.transform.up * jumpForce);
            jumpCount--;
        }
    }
    public void basicAttack()
    {
        if (!dead)
        {
            if (basicAttackCoolDown == orBasicAttackCoolDown)
            {
                basicAttackCoolDown = 0;
                StartCoroutine(smallCoolDown(0.5f));
                anim.SetTrigger("Attack1");
                var other = Physics2D.OverlapCircleAll(new Vector3(this.transform.position.x + moveDir.x, this.transform.position.y, this.transform.position.z), 1, whatIsPlayer);
                for (int i = 0; i < other.Length; i++)
                {
                    //Debug.Log(other[i
                    if (other[i].GetComponent<FightingPlatformer>() != this)
                    {
                        if (!other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player1_Hurt") && !other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player2_Hurt"))
                        {
                            other[i].GetComponent<FightingPlatformer>().Hurt(1);
                        }
                    }
                }
            }
        }
    }
    public void bigAttack()
    {
        if (!dead)
        {
            if (bigAttackCoolDown == orBigAttackCoolDown)
            {
                bigAttackCoolDown = 0;
                StartCoroutine(bigCoolDown(0.5f));
                anim.SetTrigger("Attack2");
                if (specialAttack == "bigSmash")
                {
                    var other = Physics2D.OverlapCircleAll(new Vector3(this.transform.position.x + moveDir.x, this.transform.position.y, this.transform.position.z), 2, whatIsPlayer);
                    for (int i = 0; i < other.Length; i++)
                    {
                        //Debug.Log(other[i]);
                        if (other[i].GetComponent<FightingPlatformer>() != this)
                        {
                            if (!other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player1_Hurt") && !other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player2_Hurt"))
                            {
                                other[i].GetComponent<FightingPlatformer>().Hurt(2.5f);
                            }
                        }
                    }
                }
                if (specialAttack == "dash")
                {
                    StartCoroutine(dash());
                }
                if (specialAttack == "comboHit")
                {
                    StartCoroutine(comboHit(1f / 3, 3));
                }
            }
        }
    }
    public void Hurt(float damage)
    {
        anim.SetTrigger("Hurt");
        Instantiate(blood, this.transform.position, Quaternion.identity);
        health -= damage/2 ;
    }
    public IEnumerator Die()
    {
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(6f);
        anim.enabled = false;
    }
    public IEnumerator dash()
    {
        if (feet.isTouchingGround(0.3f))
        {
            rb.AddForce(this.transform.right * 500);
        } else
        {
            rb.AddForce(this.transform.right * 150);
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        this.GetComponent<BoxCollider2D>().enabled = false;

        var other = Physics2D.OverlapCircleAll(new Vector3(this.transform.position.x + moveDir.x, this.transform.position.y, this.transform.position.z), 2, whatIsPlayer);
        for (int i = 0; i < other.Length; i++)
        {
            //Debug.Log(other[i]);
            if (other[i].GetComponent<FightingPlatformer>() != this)
            {
                if (!other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player1_Hurt") && !other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player2_Hurt"))
                {
                    other[i].GetComponent<FightingPlatformer>().Hurt(1.5f);
                }
            }
        }

        yield return new WaitForSeconds(0.4f);
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator smallCoolDown(float interval)
    {

        for (float i = 0; i < orBasicAttackCoolDown; i += 0.5f)
        {
            yield return new WaitForSeconds(interval);
            basicAttackCoolDown += 0.5f;
        }
    }
    public IEnumerator bigCoolDown(float interval)
    {

        for (float i = 0; i < orBigAttackCoolDown; i += 0.5f)
        {
            yield return new WaitForSeconds(interval);
            bigAttackCoolDown += 0.5f;
        }
    }
    public IEnumerator comboHit(float delay, int hits)
    {
        for (int f = 0; f < hits; f++)
        {
            var other = Physics2D.OverlapCircleAll(new Vector3(this.transform.position.x + moveDir.x, this.transform.position.y, this.transform.position.z), 2, whatIsPlayer);
            for (int i = 0; i < other.Length; i++)
            {
                //Debug.Log(other[i]);
                if (other[i].GetComponent<FightingPlatformer>() != this)
                {
                    //if (!other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player1_Hurt") && !other[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player2_Hurt"))
                    //{
                    other[i].GetComponent<FightingPlatformer>().Hurt(1f);
                    //}
                }
                yield return new WaitForSeconds(delay);
            }
        }
    }
}