using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    public GameDataSO gameDataSo;
    public int gameScore = 0;
    public int baseNoteScore = 40;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public Color GetColorByIndex(int index)
    {
        switch (index)
        {
            case 0: return gameDataSo.upArrowColor;
            case 1: return gameDataSo.leftArrowColor;
            case 2: return gameDataSo.downArrowColor;
            case 3: return gameDataSo.rightArrowColor;
            default: return Color.white;
        }
    }

    public void UpdateScore(PlayerType playerType, bool hit)
    {
        int scoreToAdd = baseNoteScore;
        if (playerType == PlayerType.PLAYER_TWO)
            scoreToAdd *= -1;
        if (!hit)
            scoreToAdd *= -1;
        gameScore += scoreToAdd;

        if (gameScore > gameDataSo.maxGameScore)
        {
            gameScore = gameDataSo.maxGameScore;
        }

        if (gameScore < -gameDataSo.maxGameScore)
        {
            gameScore = -gameDataSo.maxGameScore;
        }

        float markerPosition = gameScore + gameDataSo.maxGameScore;
        UIManager.Instance.UpdateGameScoreMarker(markerPosition);
    }
}