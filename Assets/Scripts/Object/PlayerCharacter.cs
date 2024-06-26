using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerStatus
{
    Idle = 0,
    Jump,
    Run,
    Crouch
}
public class PlayerCharacter : MonoBehaviour
{
    //不能直接修改角色的坐标position，因为直接修改坐标是没有经过物理碰撞的计算的
    #region fields
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    Animator anim;
    public float speedX;
    public float speedY;

    float timeY = 0;

    private bool isJumping;
    private bool canJump;
    public PlayerStatus currentStatus = PlayerStatus.Idle;

    private Transform cameraFollowTarget;
    public Vector3 followOffset;
    #endregion

    #region periodic methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cameraFollowTarget = transform.Find("TargetForCamera");
        cameraFollowTarget.position = transform.position + followOffset;
    }

    private void Update()
    {
        CheckPlayerStatus();

        UpdateHorizontalMovement();

        UpdateJump();

        CheckIsJumping();
        UpdateAnimation();
        UpdateTargetPosition();
    }

    private void UpdateHorizontalMovement()
    {
        SetPeedX(PlayerInput.instance.Horizontal.value * speedX);
    }
    #endregion
    #region private methods
    private void UpdateJump()
    {
        if (PlayerInput.instance.Jump.isDown && !isJumping)
        {
            timeY = 0;
            canJump = true;
        }
        if (PlayerInput.instance.Jump.isPressed && canJump)
        {
            timeY += Time.deltaTime;
            if (timeY >= 0.2f)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }
        }
        if (PlayerInput.instance.Jump.isUp)
        {
            canJump = false;
        }
        if (canJump)
        {
            SetPeedY(speedY);
        }
    }
    private void UpdateAnimation()
    {
        anim.SetBool(Constants.Anim_IsJump, isJumping);
        anim.SetBool(Constants.Anim_IsCrouch, PlayerInput.instance.Vertical.value < 0);
    }
    private void SetPeedX(float x)
    {
        // anim.SetBool(Constants.Anim_IsRun, Mathf.Approximately(x, 0));
        anim.SetBool(Constants.Anim_IsRun, x != 0);

        if (x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (currentStatus == PlayerStatus.Crouch) x = 0;
        rb.velocity = new Vector2(x, rb.velocity.y);
    }
    private void SetPeedY(float y)
    {
        if (currentStatus == PlayerStatus.Crouch) y = 0;

        rb.velocity = new Vector2(rb.velocity.x, y);
    }

    float checkDistance = 0.2f;
    private void CheckIsJumping()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down, checkDistance, 1 << 8);
        // RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.down, 1, 8);
        isJumping = !hit2D;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * checkDistance, Color.red);
        if (hit2D)
        {
            // Debug.Log("Detecting game object:" + hit2D.rigidbody.gameObject.name);
            // Debug.Log("Detecting game object:" + hit2D.transform.gameObject.name);
        }
    }

    public void CheckPlayerStatus()
    {
        currentStatus = PlayerStatus.Idle;
        if (rb.velocity.x != 0)
        {
            currentStatus = PlayerStatus.Run;
        }
        if (isJumping)
        {
            currentStatus = PlayerStatus.Jump;
        }
        if (PlayerInput.instance.Vertical.value < 0 && !isJumping)
        {
            currentStatus = PlayerStatus.Crouch;
        }
    }

    //update target position
    private void UpdateTargetPosition()
    {
        if (spriteRenderer.flipX)
        {
            cameraFollowTarget.position = Vector3.MoveTowards(cameraFollowTarget.position, transform.position - followOffset, 0.05f);
        }
        else
        {
            cameraFollowTarget.position = Vector3.MoveTowards(cameraFollowTarget.position, transform.position + followOffset, 0.05f);
        }
    }
    #endregion
}
