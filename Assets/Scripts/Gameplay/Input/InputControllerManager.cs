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
        if (player1Actions["note_up"].WasPressedThisFrame() || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            NotesManager.instance.OnNotePressed(0, PlayerType.PLAYER_ONE);
        }

        if (player1Actions["note_left"].WasPressedThisFrame() || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NotesManager.instance.OnNotePressed(1, PlayerType.PLAYER_ONE);
        }

        if (player1Actions["note_down"].WasPressedThisFrame() || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            NotesManager.instance.OnNotePressed(2, PlayerType.PLAYER_ONE);
        }

        if (player1Actions["note_right"].WasPressedThisFrame() || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            NotesManager.instance.OnNotePressed(3, PlayerType.PLAYER_ONE);
        }

        if (player1Actions["note_right"].WasReleasedThisFrame())
        {
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