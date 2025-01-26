using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationConstants
{
    public static int GetJersonSpriteIndexByState(PlayerGhostState state)
    {
        switch (state)
        {
            case PlayerGhostState.IDLE:
                return 0;
            case PlayerGhostState.IDLE_WALKING:
                return 0;
            case PlayerGhostState.RECEIVING_CLIENT:
                return 0;
            case PlayerGhostState.GOING_TO_WASH:
                return 2;
            case PlayerGhostState.GETTING_CLOTHS:
                return 0;
            case PlayerGhostState.WAITING_WASH_MACHINE:
                return 0;
            case PlayerGhostState.DELIVERYING_CLOTHS:
                return 2;
            default:
                return 0;
        }
    }

    public static int GetClientGhostSpriteIndexByState(ClientGhostState state)
    {
        switch (state)
        {
            case ClientGhostState.ENTERING:
                return 0;
            case ClientGhostState.WAITING_DRESSED:
                return 0;
            case ClientGhostState.WAITING_UNDRESSED:
                return 1;
            case ClientGhostState.LEAVING_SUCCESS:
                return 2;
            case ClientGhostState.LEAVING_FAILED:
                return 3;
            default:
                return 0;
        }
    }

    public static List<Sprite> GetSpritesListByClientGhostType(ClientGhostType ghostType)
    {
        switch (ghostType)
        {
            case ClientGhostType.JONAS:
                return GameDataManager.instance.gameDataSo.clientJonasSprites;
            case ClientGhostType.FOFINHA:
                return GameDataManager.instance.gameDataSo.clientFofinhaSprites;
            case ClientGhostType.GAROTO:
                return GameDataManager.instance.gameDataSo.clientGarotoSprites;
            case ClientGhostType.BIGODE:
                return GameDataManager.instance.gameDataSo.clientBigodeSprites;
            default:
                Debug.Log("Unknown ghost type");
                return new List<Sprite>();
        }
    }
    
    public static float GetXScaleByClientGhostType(ClientGhostType ghostType)
    {
        switch (ghostType)
        {
            case ClientGhostType.JONAS:
                return -1;
            case ClientGhostType.FOFINHA:
                return 1;
            case ClientGhostType.GAROTO:
                return -1;
            case ClientGhostType.BIGODE:
                return -1;
            default:
                return 1;
        }
    }

}