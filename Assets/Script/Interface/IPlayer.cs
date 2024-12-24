using UnityEngine.InputSystem;

public interface IPlayer
{
    public void OnAttack(InputAction.CallbackContext context);
    public void OnBlock(InputAction.CallbackContext context);
    
}
