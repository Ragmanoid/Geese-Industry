using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Align align;
    [Header("Extreme positions")] public float max;
    public float min;
    public float smooth;

    private Vector2 StartPosition { get; set; }
    private Vector2 LastPosition { get; set; }
    private Vector2 MoveTo { get; set; }
    private float MoveTime { get; set; }

    private void Start()
    {
        var position = transform.position;
        StartPosition = new Vector2(position.x, position.y);
        MoveTo = StartPosition;
        LastPosition = align switch
        {
            Align.Vertical => new Vector2(position.x, max),
            Align.Horizontal => new Vector2(max, position.y),
            _ => MoveTo
        };
    }

    private void FixedUpdate()
    {
        var position = transform.position;
        
        if (Math.Abs(Vector2.Distance(position, StartPosition)) < 0.1)
        {
            MoveTo = align switch
            {
                Align.Vertical => new Vector2(position.x, max),
                Align.Horizontal => new Vector2(max, position.y),
                _ => MoveTo
            };
            MoveTime = 0;
        }

        if (Math.Abs(Vector2.Distance(position, LastPosition)) < 0.1)
        {
            MoveTo = align switch
            {
                Align.Vertical => new Vector2(position.x, min),
                Align.Horizontal => new Vector2(min, position.y),
                _ => MoveTo
            };
            MoveTime = 0;
        }
        MovePlatform();
    }

    private void MovePlatform()
    {
        MoveTime += Time.deltaTime * smooth;
        transform.position = Vector3.Lerp(transform.position, MoveTo, MoveTime);
    }
}