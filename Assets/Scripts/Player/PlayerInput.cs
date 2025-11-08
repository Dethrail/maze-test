using UnityEngine;


public class PlayerInput
{
    public Vector2 ReadMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}