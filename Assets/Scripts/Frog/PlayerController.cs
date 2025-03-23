using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public TerrainManager terrainManager;

    private Rigidbody2D rb;

    private Animator anim;

    private SpriteRenderer sr;

    public float jumpDistance;

    private float moveDistance;

    private Vector2 destination;

    private Vector2 touchPosition;

    private bool buttonHeld;

    private bool isJump;

    private bool canJump;

    private bool isDead;

    private enum Direction
    {
        Up, Right, Left
    }

    private Direction dir;

    // 判断碰撞检测返回的物体
    private RaycastHit2D[] result = new RaycastHit2D[3];

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Border") || other.CompareTag("Car"))
        {
            Debug.Log("game over");
            isDead = true;
        }

        if (!isJump && other.CompareTag("Obstacle"))
        {
            Debug.Log("game over");
            isDead = true;
        }

        if (!isJump && other.CompareTag("Water"))
        {
            Physics2D.RaycastNonAlloc(transform.position + Vector3.up * 0.1f, Vector2.zero, result);

            bool inWater = true;

            foreach (var hit in result)
            {
                if (hit.collider == null) continue;

                if (hit.collider.CompareTag("Wood"))
                {
                    // 跟随木板
                    transform.parent = hit.collider.transform;
                    inWater = false;
                }
            }

            if (inWater && !isJump)
            {
                Debug.Log("game over");
                isDead = true;
            }
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
                anim.SetBool("isSide", false);
                destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
                transform.localScale = Vector3.one;
                break;
            case Direction.Right:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case Direction.Left:
                anim.SetBool("isSide", true);
                destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
                transform.localScale = Vector3.one;
                break;
        }

        anim.SetTrigger("Jump");
    }

    #region Animation Event 动画事件
    public void JumpAnimationEvent()
    {
        // 修改跳跃状态
        isJump = true;

        // 修改排序图层
        sr.sortingLayerName = "Front";

        transform.parent = null;

    }

    public void FinishJumpAnimationEvent()
    {
        isJump = false;
        sr.sortingLayerName = "Middle";

        if (dir == Direction.Up && !isDead)
        {
            // 触发地图检测
            terrainManager.CheckPosition();
        }
    }
    #endregion
}
