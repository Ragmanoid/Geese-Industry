using System;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    public bool isMove = false;
    private const float Speed = 1f;
    private float MoveDirection { get; set; }
    private Rigidbody2D RigidBody { get; set; }
    private const float LookRadius = 7f;
    private Vector2 StartPosition { get; set; }
    private GameObject Target { get; set; }
    private const float BulletTTl = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private const float FireCooldown = 1f;
    private float fireCooldown = FireCooldown;
    private const float AttackRange = 7f;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        StartPosition = transform.position;
        RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        WeaponCooldown();
    }

    private void WeaponCooldown()
    {
        var distance = GetDistanceToTarget();

        fireCooldown -= Time.deltaTime;

        if (distance > AttackRange)
            return;

        if (!(fireCooldown < 0)) return;

        fireCooldown = FireCooldown;
        Shot();
    }

    private float GetDistanceToTarget()
    {
        var targetPosition = Target.transform.position;
        var transformPosition = transform.position;

        return Vector2.Distance(
            targetPosition,
            transformPosition
        );
    }

    private void FixedUpdate()
    {
        if (isMove)
            Move();
    }

    private void Move()
    {
        if (GetDistanceToTarget() <= LookRadius)
        {
            var targetPosition = Target.transform.position;
            var transformPosition = transform.position;
            var playerPosition = new Vector2(targetPosition.x - transformPosition.x, 0);
            RigidBody.velocity = playerPosition.normalized * Speed;
        }
        else
            RigidBody.velocity = Vector2.down;
    }

    private void OnDrawGizmosSelected()
    {
        var position = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, LookRadius);
        Gizmos.DrawWireSphere(position, AttackRange);
    }

    private void Shot()
    {
        var bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Destroy(bullet, BulletTTl);
    }
}