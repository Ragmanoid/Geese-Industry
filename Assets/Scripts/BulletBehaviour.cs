using UnityEngine;

public class BulletBehaviour : KillerObject
{
    private float speed = 5f;
    private Rigidbody2D RigidBody { get; set; }
    private float aimTime = 1.5f;
    private Vector2 lastDirection;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        aimTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        var targetPosition = Target.transform.position;
        var transformPosition = transform.position;
        Vector2 resultedVelocity;

        if (aimTime > 0)
        {
            var playerPosition = new Vector2(
                targetPosition.x - transformPosition.x,
                targetPosition.y - transformPosition.y
            );
            resultedVelocity = playerPosition.normalized * speed;
            lastDirection = playerPosition.normalized;
        }
        else
        {
            speed = 10;
            resultedVelocity = lastDirection * speed;
        }

        RigidBody.velocity = resultedVelocity;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            var player = hitInfo.GetComponent<PlayerBehaviour>();
            player.Die();
            Destroy(gameObject);
        }

        if (hitInfo.CompareTag("Untagged")) return;
        Destroy(gameObject);
    }
}