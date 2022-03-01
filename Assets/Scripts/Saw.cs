public class Saw : KillerObject
{
    public float RotationSpeed = 1;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, RotationSpeed);
    }
}