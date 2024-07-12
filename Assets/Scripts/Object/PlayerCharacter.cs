using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public enum PlayerStatus
{
    Idle = 0,
    Jump,
    Run,
    Crouch
}
public enum AttackType
{
    MeleeAttack,
    RangeAttack
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

    private bool isGround;
    private bool canJump;
    public PlayerStatus currentStatus = PlayerStatus.Idle;

    private Transform cameraFollowTarget;
    public Vector3 followOffset;

    PassPlatform currentPlatform;

    Damageable playerDamageable;

    private float gravityScale;
    private string playerSpawnLocation;

    float attackColdDownDuration = 1f;
    bool canAttack = true;
    #endregion

    #region periodic methods
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cameraFollowTarget = transform.Find("TargetForCamera");
        cameraFollowTarget.position = transform.position + followOffset;

        playerDamageable = GetComponent<Damageable>();
        playerDamageable.OnDie += OnPlayerDie;
        playerDamageable.OnGetHurt += OnPlayerGetHurt;
        //if InitHp method is called here, there is a potential problem that if the Start method is called before the one inGamePanel, the variable parent won't be found because its start method isn't called yet. So the solution is to call the method of finding the parent in the Awake method in GamePanel class.
        GamePanel.Instance.InitHP(playerDamageable.health);
    }

    private void OnPlayerGetHurt(HurtType type, string pos)
    {
        playerSpawnLocation = pos;
        switch (type)
        {
            case HurtType.Normal:
                anim.SetTrigger(Constants.Anim_TriggerHurt);
                SetInvulnerability(1.5f);
                break;
            case HurtType.Deadly:
                //play the death animation

                SetPlayerDeadState();
                Invoke("ResetPlayerDeadState", 1);

                //reset the player's position
                break;
        }
        //play the death animation


        GamePanel.Instance.UpdateHp(playerDamageable.health);
        //refresh UI
        // Debug.Log("GetHurt called");
    }
    private void SetPlayerDeadState()
    {
        anim.SetBool(Constants.Anim_IsDead, true);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        PlayerInput.instance.SetEnableInput(false);
        TipMessagePanel.Instance.ShowTip(null, TipStyle.Style2);
    }

    private void ResetPlayerDeadState()
    {
        anim.SetBool(Constants.Anim_IsDead, false);
        rb.gravityScale = gravityScale;
        PlayerInput.instance.SetEnableInput(true);
        SetInvulnerability(1f);
        //set player position
        transform.position = GameObject.Find(playerSpawnLocation).transform.position;
    }

    private void SetInvulnerability(float duration)
    {
        anim.SetBool(Constants.Anim_IsInvulnerable, true);
        playerDamageable.DisableDamage();
        Invoke("ResetDamageAble", duration);
    }
    private void ResetDamageAble()
    {
        // Debug.Log("Invulnerable finished");
        playerDamageable.EnableDamage();
        anim.SetBool(Constants.Anim_IsInvulnerable, false);
    }

    private void OnPlayerDie(HurtType type, string pos)
    {
        SetPlayerDeadState();
        playerDamageable.DisableDamage();
        GamePanel.Instance.UpdateHp(playerDamageable.health);
        Invoke("DelayShowTip", 1);
    }
    private void DelayShowTip()
    {
        TipMessagePanel.Instance.ShowTip(null, TipStyle.Style3);
        playerSpawnLocation = "Spawn1";
        ResetPlayerDeadState();
        playerDamageable.ResetHealth();
        GamePanel.Instance.ResetHP();
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
        SetPeedX(PlayerInput.instance.isInputEnabled ? PlayerInput.instance.Horizontal.value * speedX : 0);
    }
    #endregion

    #region trigger events
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log($"Collision{other.gameObject.name}");

        currentPlatform = other.gameObject.GetComponent<PassPlatform>();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log("trigger");
        currentPlatform = null;
    }

    #endregion
    #region private methods
    private void UpdateJump()
    {
        if (PlayerInput.instance.Jump.isDown && isGround)
        {
            timeY = 0;
            canJump = true;
        }
        if (PlayerInput.instance.Jump.isHolding && canJump)
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
        anim.SetBool(Constants.Anim_IsJump, !isGround);
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
        isGround = hit2D;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * checkDistance, Color.red);
        // if (hit2D)
        // {
        //     // Debug.Log("Detecting game object:" + hit2D.rigidbody.gameObject.name);
        //     // Debug.Log("Detecting game object:" + hit2D.transform.gameObject.name);
        // }
    }

    public void CheckPlayerStatus()
    {
        currentStatus = PlayerStatus.Idle;
        if (rb.velocity.x != 0)
        {
            currentStatus = PlayerStatus.Run;
        }
        if (!isGround)
        {
            currentStatus = PlayerStatus.Jump;
        }
        if (PlayerInput.instance.Vertical.value < 0 && isGround)
        {
            currentStatus = PlayerStatus.Crouch;
        }
        //to check player can jump down from the passthrough platform
        if (PlayerInput.instance.Vertical.value == -1 && isGround
        && PlayerInput.instance.Jump.isDown)
        {
            //fall
            // Debug.Log(PlayerInput.instance.Jump.isDown);

            if (currentPlatform != null)
            {
                currentPlatform.Fall(gameObject);
                anim.SetTrigger(Constants.Anim_TriggerFall);
            }
        }
        if (PlayerInput.instance.Melee.isDown || PlayerInput.instance.Melee.isHolding)
        {
            Attack(AttackType.MeleeAttack);
        }
        if (PlayerInput.instance.Shoot.isDown || PlayerInput.instance.Shoot.isHolding)
        {
            Attack(AttackType.RangeAttack);
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

    public void Attack(AttackType attackType)
    {
        if (!IsHoldWeapon())
        {
            return;
        }
        if (!canAttack) return;
        anim.SetTrigger(Constants.Anim_TriggerAttack);
        anim.SetInteger(Constants.Anim_IntAttackType, (int)attackType);
        canAttack = false;
        Invoke("ResetAttack", attackColdDownDuration);
    }
    public void ResetAttack()
    {
        canAttack = true;
    }

    private bool IsHoldWeapon()
    {
        Data data = DataManager.Instance.GetData(DataConstraints.IsHavingWeapon);
        if (data != null && !((Data<bool>)data).value) return true;
        return false;
    }
    #endregion
}
