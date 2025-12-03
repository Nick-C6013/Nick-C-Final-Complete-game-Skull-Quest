using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public LayerMask wallLayer;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private PlayerState currentState;

    public GameObject playerobject;
    private bool isfacingright; 
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeState(new IdleState());
    }

    void Update()
    {

        // NEW: Only update if game is not paused
        //Debug.Log(GameManager.Instance);
        //Debug.Log(GameManager.Instance.IsPaused());
        if (GameManager.Instance != null && !GameManager.Instance.IsPaused())
        {
            if (Input.GetAxis("Horizontal") > 0 && transform.localScale.x < 0)
            {
                Vector3 ls = transform.localScale;
                ls.x *= -1;
                transform.localScale = ls;
                isfacingright = true;
            }
            else if (Input.GetAxis("Horizontal") < 0 && transform.localScale.x > 0)
            {
                Vector3 ls = transform.localScale;
                ls.x *= -1;
                transform.localScale = ls;
                isfacingright = false;
            }
            //return; // Skip all input when paused
        }

        if (currentState != null)
        {
            currentState.UpdateState(this);
            animator.SetFloat("walk", Mathf.Abs(rb.linearVelocity.x));
        }
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);

        EventManager.TriggerEvent("OnPlayerStateChanged", currentState.GetStateName());
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public bool isWalled() 
    {
        if (!IsGrounded()) 
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        } return false;
    }

    public void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().direction = Mathf.Sign(transform.localScale.x);
            //AudioManager.Instance.PlayShootSound();
        }
    }

    public void TakeDamage()
    {
        GameManager.Instance.PlayerDied();
        Respawn();
    }

    void Respawn()
    {
        transform.position = GameManager.Instance.spawnPoint;
        ChangeState(new IdleState());
    }

    public string GetCurrentStateName()
    {
        return currentState != null ? currentState.GetStateName() : "None";
    }

    public bool getIsFacingRight()
    {
        return isfacingright;
    }
}