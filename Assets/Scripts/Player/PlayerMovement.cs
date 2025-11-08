using UnityEngine;


public class PlayerMovement
{
    private readonly Rigidbody2D _rb;
    private Vector2 _lastPos;
    public float DistanceTraveled { get; private set; }


    public PlayerMovement(Rigidbody2D rb)
    {
        _rb = rb;
        _lastPos = rb.position;
    }


    public void Move(Vector2 input)
    {
        Vector2 newPos = _rb.position + input * Time.deltaTime * 5f;
        DistanceTraveled += Vector2.Distance(_lastPos, newPos);
        _rb.MovePosition(newPos);
        _lastPos = newPos;
    }
}