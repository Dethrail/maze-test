using VContainer.Unity;

public class PlayerController : ITickable
{
    private readonly PlayerInput _input;
    private readonly PlayerMovement _movement;


    public PlayerController(PlayerInput input, PlayerMovement movement)
    {
        _input = input;
        _movement = movement;
    }


    public void Tick()
    {
        var input = _input.ReadMovement();
        _movement.Move(input);
    }
}