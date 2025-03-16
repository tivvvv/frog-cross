using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    public float jumpDistance;

    private float moveDistance;

    private Vector2 destination;

    private bool buttonHeld;

    private bool isJump;

    private bool canJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canJump)
        {
            TriggerJump();
        }

    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            rb.position = Vector2.Lerp(transform.position, destination, 0.134f);
        }

    }

    #region INPUT 输入回调函数
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = jumpDistance;
            destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
            // 执行跳跃
            canJump = true;

        }

    }

    public void LongJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJump)
        {
            moveDistance = jumpDistance * 2;
            buttonHeld = true;
            canJump = true;
        }

        if (context.canceled && buttonHeld)
        {
            destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
            // 执行跳跃
            buttonHeld = false;
        }

    }
    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("JUMP!" + context);
        }

    }
    #endregion

    /// <summary>
    /// 触发执行跳跃动画
    /// </summary>
    private void TriggerJump()
    {
        canJump = false;
        // 播放动画
        anim.SetTrigger("Jump");
    }

    #region Animation Event 动画事件
    public void JumpAnimationEvent()
    {
        isJump = true;
    }

    public void FinishJumpAnimationEvent()
    {
        isJump = false;
    }
    #endregion
}
