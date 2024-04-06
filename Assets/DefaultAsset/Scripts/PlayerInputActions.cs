using UnityEngine.InputSystem;

public class PlayerInputActions
{
    private InputActionMap player;
    public InputAction Move { get; }
    public InputAction Jump { get; private set; }
    public InputAction Crouch { get; private set; }
    public InputAction LedgeGrab { get; private set; }
    public InputAction WallSlide { get; private set; }
    public InputAction WallJump { get; private set; }
    public InputAction Attack { get; private set; }

    public PlayerInputActions()
    {
        player = new InputActionMap("Player");
        Move = player.AddAction("Move");
        Jump = player.AddAction("Jump");
        Crouch = player.AddAction("Crouch");
        LedgeGrab = player.AddAction("LedgeGrab");
        WallSlide = player.AddAction("WallSlide");
        WallJump = player.AddAction("WallJump");
        Attack = player.AddAction("Attack");
    }

    public void Enable()
    {
        player.Enable();
    }

    public void Disable()
    {
        player.Disable();
    }
}