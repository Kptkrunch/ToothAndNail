// using UnityEngine.InputSystem;
//
// namespace JAssets.Scripts_SC
// {
//     public class PlayerInputActions
//     {
//         private readonly InputActionMap player;
//
//         public PlayerInputActions()
//         {
//             player = new InputActionMap("Player");
//             Move = player.AddAction("Move");
//             Jump = player.AddAction("Jump");
//             Crouch = player.AddAction("Crouch");
//             LedgeGrab = player.AddAction("LedgeGrab");
//             WallSlide = player.AddAction("WallSlide");
//             WallJump = player.AddAction("WallJump");
//             Attack = player.AddAction("Attack");
//         }
//
//         public InputAction Move { get; }
//         public InputAction Jump { get; private set; }
//         public InputAction Crouch { get; private set; }
//         public InputAction LedgeGrab { get; private set; }
//         public InputAction WallSlide { get; private set; }
//         public InputAction WallJump { get; private set; }
//         public InputAction Attack { get; private set; }
//
//         public void Enable()
//         {
//             player.Enable();
//         }
//
//         public void Disable()
//         {
//             player.Disable();
//         }
//     }
// }