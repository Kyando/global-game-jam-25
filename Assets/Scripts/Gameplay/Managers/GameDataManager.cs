using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    public GameDataSO gameDataSo;

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
}