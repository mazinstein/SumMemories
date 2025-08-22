using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isDashing = false;
    private float lastDashTime;

    [Header("Level Horizontal Bounds")]
    public float minX; // левая граница
    public float maxX; // правая граница

    [Header("Sprite")]
    public Transform spriteTransform;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (spriteTransform == null)
            spriteTransform = transform;

        originalScale = spriteTransform.localScale;
    }

    void Update()
    {
        if (!isDashing)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && !isDashing)
        {
            Vector2 dashDir = movement;
            if (dashDir == Vector2.zero) dashDir = Vector2.up;
            StartCoroutine(Dash(dashDir));
            lastDashTime = Time.time;
        }

        // Анимация
        UpdateAnimation();

        // Флип спрайта по X
        if (movement.x != 0)
        {
            spriteTransform.localScale = new Vector3(
                Mathf.Sign(movement.x) * Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = movement * moveSpeed;

            // Ограничиваем только горизонтальные границы
            Vector2 clampedPos = rb.position;
            clampedPos.x = Mathf.Clamp(rb.position.x, minX, maxX);
            rb.position = clampedPos;
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);

        float elapsed = 0f;
        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction.normalized * dashDistance;

        // Ограничение только по X
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);

        while (elapsed < dashDuration)
        {
            elapsed += Time.fixedDeltaTime;
            Vector2 newPos = Vector2.Lerp(startPos, targetPos, elapsed / dashDuration);
            rb.MovePosition(newPos); // ✅ корректные коллизии при движении
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
        animator.SetBool("IsDashing", false);
    }

    void UpdateAnimation()
    {
        if (movement.sqrMagnitude > 0 && !isDashing)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                animator.SetFloat("MoveX", 1);
                animator.SetFloat("MoveY", 0);
            }
            else
            {
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", movement.y);
            }

            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            animator.SetBool("IsMoving", false);
        }
    }
}
