using UnityEngine;

public class Gun : MonoBehaviour
{
    private GameObject Target { get; set; }
    private SpriteRenderer SpriteRenderer { get; set; }

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private int GetQuadrant(float angle)
    {
        if (angle > 90)
            return 2;
        if (angle > 0)
            return 1;
        if (angle < -90)
            return 3;
        if (angle < 0)
            return 4;

        return 0;
    }

    private void FlipGun(float angle)
    {
        var quadrant = GetQuadrant(angle);

        switch (quadrant)
        {
            case 1:
                SpriteRenderer.flipX = true;
                SpriteRenderer.flipY = false;
                break;
            case 2:
                SpriteRenderer.flipX = true;
                SpriteRenderer.flipY = true;
                break;
            case 3:
                SpriteRenderer.flipX = true;
                SpriteRenderer.flipY = true;
                break;
            case 4:
                SpriteRenderer.flipX = true;
                SpriteRenderer.flipY = false;
                break;
        }
    }

    private void FixedUpdate()
    {
        var dir = Target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        FlipGun(angle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}