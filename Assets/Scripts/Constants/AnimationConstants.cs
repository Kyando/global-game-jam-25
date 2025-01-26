using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationConstants
{
    public static string GetClientGhostAnimationByState(ClientGhostState state)
    {
        switch (state)
        {
            case ClientGhostState.ENTERING:
                return "entering";
            case ClientGhostState.WAITING_DRESSED:
                return "waiting_dressed";
            case ClientGhostState.WAITING_UNDRESSED:
                return "waiting_undressed";
            case ClientGhostState.LEAVING_SUCCESS:
                return "leaving_success";
            case ClientGhostState.LEAVING_FAILED:
                return "leaving_failed";
            default:
                return null;
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
                return 1;
            default:
                return 1;
        }
    }

}