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

    private Vector2 touchPosition;

    private bool buttonHeld;

    private bool isJump;

    private bool canJump;

    private enum Direction
    {
        Up, Right, Left
    }

    private Direction dir;


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

            // 执行跳跃
            buttonHeld = false;
        }

    }
    public void GetTouchPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            var offset = ((Vector3)touchPosition - transform.position).normalized;
            if (Mathf.Abs(offset.x) <= 0.7f)
            {
                dir = Direction.Up;
            }
            else if (offset.x < 0)
            {
                dir = Direction.Left;
            }
            else
            {
                dir = Direction.Right;
            }
        }
    }
    #endregion

    /// <summary>
    /// 触发执行跳跃动画
    /// </summary>
    private void TriggerJump()
    {
        canJump = false;
        // 获取移动方向并播放动画
        switch (dir)
        {
            case Direction.Up:
                destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
                break;
            case Direction.Right:
                destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
                break;
            case Direction.Left:
                destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
                break;
        }
    
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
