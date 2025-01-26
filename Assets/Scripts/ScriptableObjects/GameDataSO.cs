using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameDataSO : ScriptableObject
{
    public Color leftArrowColor = new Color(1f, 1f, 1f, 1f);
    public Color rightArrowColor = new Color(1f, 1f, 1f, 1f);
    public Color upArrowColor = new Color(1f, 1f, 1f, 1f);
    public Color downArrowColor = new Color(1f, 1f, 1f, 1f);
    public GameObject notePrefab;

    public int maxGameScore = 520;

    public List<Sprite> playerJersonSprites = new List<Sprite>();
    
    public List<Sprite> clientFofinhaSprites = new List<Sprite>();
    public List<Sprite> clientJonasSprites = new List<Sprite>();
    public List<Sprite> clientGarotoSprites = new List<Sprite>();
    public List<Sprite> clientBigodeSprites = new List<Sprite>();
}