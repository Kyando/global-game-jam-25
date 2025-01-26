using UnityEngine;

public class PedroBehaviour : MonoBehaviour
{
    public PedroGhostState ghostState = PedroGhostState.IDLE;
    public float idleWaitTime = 1.5f;
    public float interactionTime = 12f;
    public float moveSpeed = 1f;
    public Vector3 lightSwitchPosition = new Vector3();
    public Vector3 washingMachinePosition = new Vector3();
    public Vector3 waterRegistryPosition = new Vector3();
    public Vector3 initialRoomPosition = new Vector3();
    public Vector3 finalRoomPosition = new Vector3();

    public Vector3 targetPosition = new Vector3();

    public SpriteRenderer ghostSpriteRenderer;
    private float timeCounter = 0;

    void Start()
    {
        StartStateIdleWalking();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePedroStates();
    }

    private void HandlePedroStates()
    {
        switch (ghostState)
        {
            case PedroGhostState.IDLE:
                ProcessStateIdle();
                return;
            case PedroGhostState.IDLE_WALKING:
                ProcessWalkingStates();
                return;
            case PedroGhostState.GOING_TO_LIGHT_SWITCH:
                ProcessWalkingStates();
                return;
            case PedroGhostState.LIGHT_SWITCH:
                ProcessStateLightSwitch();
                return;
            default:
                return;
        }
    }

    private void ProcessStateIdle()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= idleWaitTime)
        {
            StartStateIdleWalking();
        }
    }

    private void ProcessWalkingStates()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            OnTargetPositionReached();
            return;
        }

        transform.position =
            Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void ProcessStateLightSwitch()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= interactionTime)
        {
            InterfererManager.instance.TurnLightSwitch(false);
            StartStateIdleWalking();
        }
    }

    private void OnTargetPositionReached()
    {
        switch (ghostState)
        {
            case PedroGhostState.IDLE:
                StartStateIdle();
                return;
            case PedroGhostState.IDLE_WALKING:
                StartStateIdle();
                return;
            case PedroGhostState.GOING_TO_LIGHT_SWITCH:
                InterfererManager.instance.TurnLightSwitch(true);
                StartStateLightSwitch();
                return;

            default:
                return;
        }
    }


    public void StartStateIdle()
    {
        timeCounter = 0;
        this.ghostState = PedroGhostState.IDLE;
        UpdateGhostSprites();
    }

    public void StartStateIdleWalking()
    {
        this.ghostState = PedroGhostState.IDLE_WALKING;
        targetPosition = this.transform.position;
        targetPosition.x = Random.Range(initialRoomPosition.x, finalRoomPosition.x);
        ghostSpriteRenderer.flipX = targetPosition.x >= transform.position.x;

        UpdateGhostSprites();
    }

    public void StartStateLightSwitch()
    {
        timeCounter = 0;
        this.ghostState = PedroGhostState.LIGHT_SWITCH;
        this.ghostSpriteRenderer.flipX = true;
        UpdateGhostSprites();
    }

    public void StartStateGoingToLightSwitch()
    {
        this.ghostState = PedroGhostState.GOING_TO_LIGHT_SWITCH;
        targetPosition = lightSwitchPosition;
        ghostSpriteRenderer.flipX = targetPosition.x >= transform.position.x;
        UpdateGhostSprites();
    }

    private void UpdateGhostSprites()
    {
        int spriteId = AnimationConstants.GetPedroSpriteIndexByState(this.ghostState);
        var spritesList = GameDataManager.instance.gameDataSo.playerPedroSprites;
        if (spritesList == null || spritesList.Count < spriteId - 1)
            return;

        var sprite = spritesList[spriteId];
        ghostSpriteRenderer.sprite = sprite;
    }
}