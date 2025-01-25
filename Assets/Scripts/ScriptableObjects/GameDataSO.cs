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
}