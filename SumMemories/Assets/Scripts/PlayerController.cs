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

    private Camera mainCamera;
    private Vector2 minBounds, maxBounds;

    [Header("Sprite")]
    public Transform spriteTransform;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        UpdateCameraBounds();

        if (spriteTransform == null)
            spriteTransform = transform;

        originalScale = spriteTransform.localScale;
    }

    void Update()
    {
        // Считываем ввод только в Update
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

        // Флип спрайта по X для горизонтали
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
            // Мгновенное движение через velocity
            rb.linearVelocity = movement * moveSpeed;

            // Ограничиваем границы камеры
            Vector2 clampedPos = rb.position;
            clampedPos.x = Mathf.Clamp(clampedPos.x, minBounds.x, maxBounds.x);
            clampedPos.y = Mathf.Clamp(clampedPos.y, minBounds.y, maxBounds.y);
            rb.position = clampedPos;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // останавливаем движение во время даша
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction.normalized * dashDistance;

        // Ограничиваем границами камеры
        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);

        // Анимация даша
        animator.SetBool("IsDashing", true);

        while ((Vector2)rb.position != targetPos)
        {
            rb.position = Vector2.MoveTowards(rb.position, targetPos, dashDistance / dashDuration * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
        animator.SetBool("IsDashing", false);
    }

    void UpdateCameraBounds()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        minBounds = (Vector2)mainCamera.transform.position - new Vector2(camWidth, camHeight);
        maxBounds = (Vector2)mainCamera.transform.position + new Vector2(camWidth, camHeight);
    }

    void UpdateAnimation()
    {
        if (movement.sqrMagnitude > 0 && !isDashing)
        {
            // Определяем главное направление: горизонталь или вертикаль
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                // Горизонтальное движение
                animator.SetFloat("MoveX", 1); // всегда вправо, флип спрайта делает левую анимацию
                animator.SetFloat("MoveY", 0);
            }
            else
            {
                // Вертикальное движение
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
