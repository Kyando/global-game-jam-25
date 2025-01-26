using UnityEngine;

public class JersonBehaviour : MonoBehaviour
{
    public PlayerGhostState ghostState = PlayerGhostState.IDLE;
    public float idleWaitTime = 1.5f;
    public float washingWaitTime = 6f;
    public float moveSpeed = 1f;
    public Vector3 clientPosition = new Vector3();
    public Vector3 washingMachinePosition = new Vector3();
    public Vector3 endOfRoomPosition = new Vector3();

    private Vector3 targetPosition = new Vector3();

    public SpriteRenderer ghostSpriteRenderer;
    private float timeCounter = 0;

    void Start()
    {
        StartStateIdleWalking();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJersonStates();
    }

    private void HandleJersonStates()
    {
        switch (ghostState)
        {
            case PlayerGhostState.IDLE:
                ProcessStateIdle();
                return;
            case PlayerGhostState.IDLE_WALKING:
                ProcessWalkingStates();
                return;
            case PlayerGhostState.RECEIVING_CLIENT:
                ProcessWalkingStates();
                return;
            case PlayerGhostState.GOING_TO_WASH:
                ProcessWalkingStates();
                return;
            case PlayerGhostState.GETTING_CLOTHS:
                ProcessWalkingStates();
                return;
            case PlayerGhostState.WAITING_WASH_MACHINE:
                ProcessWaitingWashMachine();
                return;
            case PlayerGhostState.DELIVERYING_CLOTHS:
                ProcessWalkingStates();
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

    private void ProcessWaitingWashMachine()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= washingWaitTime)
        {
            StartStateDeliveryingCloths();
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

    private void OnTargetPositionReached()
    {
        switch (ghostState)
        {
            case PlayerGhostState.IDLE:
                StartStateIdle();
                return;
            case PlayerGhostState.IDLE_WALKING:
                StartStateIdle();
                return;
            case PlayerGhostState.RECEIVING_CLIENT:
                GhostsManager.instance.cientGhost.StartStateWaitingUndressed();
                StartStateGoingToWash();
                return;
            case PlayerGhostState.GOING_TO_WASH:
                StartStateWaitingWashMachine();
                return;
            case PlayerGhostState.WAITING_WASH_MACHINE:
                StartStateGettingCloths();
                return;
            case PlayerGhostState.GETTING_CLOTHS:
                StartStateGettingCloths();
                return;
            case PlayerGhostState.DELIVERYING_CLOTHS:
                bool success = GameDataManager.instance.gameScore > 0;
                GhostsManager.instance.cientGhost.StartStateLeaving(success);
                StartStateIdleWalking();
                return;
            default:
                return;
        }
    }


    public void StartStateIdle()
    {
        timeCounter = 0;
        this.ghostState = PlayerGhostState.IDLE;
        UpdateGhostSprites();
    }

    public void StartStateIdleWalking()
    {
        this.ghostState = PlayerGhostState.IDLE_WALKING;
        targetPosition = this.transform.position;
        targetPosition.x = Random.Range(clientPosition.x, endOfRoomPosition.x);
        ghostSpriteRenderer.flipX = targetPosition.x >= transform.position.x;

        UpdateGhostSprites();
    }

    public void StartStateReceivingClient()
    {
        this.ghostState = PlayerGhostState.RECEIVING_CLIENT;
        targetPosition = clientPosition;
        ghostSpriteRenderer.flipX = false;
        UpdateGhostSprites();
    }

    public void StartStateGoingToWash()
    {
        this.ghostState = PlayerGhostState.GOING_TO_WASH;
        targetPosition = washingMachinePosition;
        ghostSpriteRenderer.flipX = targetPosition.x >= transform.position.x;
        UpdateGhostSprites();
    }

    public void StartStateWaitingWashMachine()
    {
        this.timeCounter = 0;
        this.ghostState = PlayerGhostState.WAITING_WASH_MACHINE;
        targetPosition = this.transform.position;
        targetPosition.x = Random.Range(clientPosition.x, endOfRoomPosition.x);
        ghostSpriteRenderer.flipX = targetPosition.x >= transform.position.x;

        UpdateGhostSprites();
    }

    public void StartStateGettingCloths()
    {
        this.ghostState = PlayerGhostState.GETTING_CLOTHS;
        targetPosition = washingMachinePosition;
        ghostSpriteRenderer.flipX = targetPosition.x >= transform.position.x;
        UpdateGhostSprites();
    }

    public void StartStateDeliveryingCloths()
    {
        this.ghostState = PlayerGhostState.DELIVERYING_CLOTHS;
        targetPosition = clientPosition;
        ghostSpriteRenderer.flipX = false;
        UpdateGhostSprites();
    }

    private void UpdateGhostSprites()
    {
        int spriteId = AnimationConstants.GetJersonSpriteIndexByState(this.ghostState);
        var spritesList = GameDataManager.instance.gameDataSo.playerJersonSprites;
        if (spritesList == null || spritesList.Count < spriteId - 1)
            return;

        var sprite = spritesList[spriteId];
        ghostSpriteRenderer.sprite = sprite;
    }
}