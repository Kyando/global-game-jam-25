using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class CientGhostBehaviour : MonoBehaviour
{
    public ClientGhostState ghostState = ClientGhostState.UNDEFINED;
    public ClientGhostType ghostType = ClientGhostType.UNDEFINED;
    public float moveSpeed = 5.0f;
    public Vector3 insideTargetPosition = new Vector3();
    public Vector3 outsideTargetPosition = new Vector3();
    public SpriteRenderer ghostSpriteRenderer;
    // private Animator animator;

    private void Start()
    {
        StartStateEntering();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleGhostStates();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartStateLeaving(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartStateLeaving(false);
        }
    }

    private void HandleGhostStates()
    {
        switch (ghostState)
        {
            case ClientGhostState.ENTERING:
                ProcessEntering();
                return;
            case ClientGhostState.WAITING_DRESSED:
                return;
            case ClientGhostState.WAITING_UNDRESSED:
                return;
            case ClientGhostState.LEAVING_SUCCESS:
                ProcessLeaving();
                return;
            case ClientGhostState.LEAVING_FAILED:
                ProcessLeaving();
                return;
        }
    }

    private void ProcessEntering()
    {
        if (Vector3.Distance(transform.position, insideTargetPosition) < 0.1f)
        {
            StartStateWaitingDressed();
            return;
        }

        transform.position =
            Vector3.MoveTowards(transform.position, insideTargetPosition, moveSpeed * Time.deltaTime);
    }

    private void ProcessLeaving()
    {
        if (Vector3.Distance(transform.position, outsideTargetPosition) < 0.1f)
        {
            StartStateEntering();
            return;
        }

        transform.position =
            Vector3.MoveTowards(transform.position, outsideTargetPosition, moveSpeed * Time.deltaTime);
    }

    public void StartStateEntering()
    {
        this.ghostState = ClientGhostState.ENTERING;
        this.ghostType = GetNextGhostType();
        UpdateGhostSprites();
    }

    private ClientGhostType GetNextGhostType()
    {
        switch (ghostType)
        {
            case ClientGhostType.JONAS:
                return ClientGhostType.FOFINHA;
            case ClientGhostType.FOFINHA:
                return ClientGhostType.GAROTO;
            case ClientGhostType.GAROTO:
                return ClientGhostType.BIGODE;
            case ClientGhostType.BIGODE:
                return ClientGhostType.JONAS;
            default:
                return ClientGhostType.JONAS;
        }
    }

    public void StartStateWaitingDressed()
    {
        this.ghostState = ClientGhostState.WAITING_DRESSED;
        UpdateGhostSprites();
    }

    public void StartStateLeaving(bool success)
    {
        if (success)
        {
            this.ghostState = ClientGhostState.LEAVING_SUCCESS;
        }
        else
        {
            this.ghostState = ClientGhostState.LEAVING_FAILED;
        }

        UpdateGhostSprites();
    }

    private void UpdateGhostSprites()
    {
        int spriteId = AnimationConstants.GetClientGhostSpriteIndexByState(this.ghostState);
        var spritesList = AnimationConstants.GetSpritesListByClientGhostType(this.ghostType);
        if (spritesList == null || spritesList.Count < spriteId - 1)
            return;

        var sprite = spritesList[spriteId];
        ghostSpriteRenderer.sprite = sprite;

        float xScale = AnimationConstants.GetXScaleByClientGhostType(this.ghostType);
        if (ghostState is ClientGhostState.LEAVING_SUCCESS or ClientGhostState.LEAVING_FAILED)
            xScale *= -1;
        Vector3 newScale = ghostSpriteRenderer.transform.localScale;
        newScale.x = xScale;

        ghostSpriteRenderer.transform.localScale = newScale;
    }
}