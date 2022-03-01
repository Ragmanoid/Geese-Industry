using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    private Rigidbody2D RigidBody { get; set; }

    private const float Speed = 5f;
    private const float FlySpeed = 9f;
    private const float JumpSpeed = 10f;
    private float MoveDirection { get; set; }

    private bool IsGrounded { get; set; }
    [SerializeField] private Transform groundChecker;
    [SerializeField] private Vector2 checkRadius;

    private SpriteRenderer SpriteRenderer { get; set; }
    private Animator AnimatorComponent { get; set; }

    private GameManager Gm { get; set; }

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        AnimatorComponent = GetComponent<Animator>();
        Gm = FindObjectOfType<GameManager>();
    }


    private void Update()
    {
        if (IsGrounded)
        {
            if (Input.GetAxis("Jump") > 0 || Input.GetAxis("Vertical") > 0)
            {
                
                AnimatorComponent.SetInteger("state", 2);
                RigidBody.velocity = Vector2.up * JumpSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            AnimatorComponent.SetInteger("state", 3);
            RigidBody.velocity = Vector2.down * 1.6f;
        }
    }

    public void Die()
    {
        Gm.EndGame();
    }

    public void Win()
    {
        Gm.LevelComplete();
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.Raycast(
            groundChecker.position,
            checkRadius,
            0f,
            ground
        );

        MoveDirection = Input.GetAxis("Horizontal");

        if (MoveDirection > 0)
        {
            SpriteRenderer.flipX = true;
            AnimatorComponent.SetInteger("state", 1);
        }
        else if (MoveDirection < 0)
        {
            SpriteRenderer.flipX = false;
            AnimatorComponent.SetInteger("state", 1);
        }
        else
        {
            AnimatorComponent.SetInteger("state", 0);
        }

        if (RigidBody.position.y < 190)
            Gm.EndGame();

        RigidBody.velocity = IsGrounded
            ? new Vector2(MoveDirection * Speed, RigidBody.velocity.y)
            : new Vector2(MoveDirection * FlySpeed, RigidBody.velocity.y);
    }
}