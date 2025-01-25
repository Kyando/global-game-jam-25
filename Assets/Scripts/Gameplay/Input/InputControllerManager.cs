using UnityEngine;
using UnityEngine.InputSystem;

public class InputControllerManager : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputActionMap player1Actions;
    private InputActionMap player2Actions;

    private InputDevice player1Device;
    private InputDevice player2Device;

    private void Awake()
    {
        // Load action maps for each player
        player1Actions = inputActions.FindActionMap("Player1");
        player2Actions = inputActions.FindActionMap("Player2");

        // Enable action maps
        player1Actions.Enable();
        player2Actions.Enable();
        

        var devices = Gamepad.all;
        if (devices.Count >= 2)
        {
            player1Device = devices[0];
            player2Device = devices[1];

            // Bind each player's actions to their specific device
            player1Actions.devices = new InputDevice[] { player1Device };
            player2Actions.devices = new InputDevice[] { player2Device };
        }
    }

    private void Update()
    {
        // Player 1 Inputs
        if (player1Actions["note_right"].WasPressedThisFrame())
        {
            Debug.Log("Player 1: Button Pressed");
        }

        // if (player1Actions["ButtonPress"].IsPressed())
        // {
        //     Debug.Log("Player 1: Button Held");
        // }

        if (player1Actions["note_right"].WasReleasedThisFrame())
        {
            Debug.Log("Player 1: Button Released");
        }

        // Player 2 Inputs
        if (player2Actions["note_right"].WasPressedThisFrame())
        {
            Debug.Log("Player 2: Button Pressed");
        }

        // if (player2Actions["ButtonPress"].IsPressed())
        // {
        //     Debug.Log("Player 2: Button Held");
        // }

        if (player2Actions["note_right"].WasReleasedThisFrame())
        {
            Debug.Log("Player 2: Button Released");
        }
    }
}