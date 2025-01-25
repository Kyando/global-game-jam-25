using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;
    }

    public void OnNoteRightPlayed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Played");
    }
    public void OnNoteLeftPlayed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Played");
    }
    public void OnNoteTopPlayed(InputAction.CallbackContext context)
    {
        Debug.Log("Top Played");
    }
    public void OnNoteDownPlayed(InputAction.CallbackContext context)
    {
        Debug.Log("Down Played");
        var value = context.ReadValue<float>();
        Debug.Log(value);
    }
}
