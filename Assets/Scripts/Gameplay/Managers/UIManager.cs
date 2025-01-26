using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI hitSuccessText;
    public RectTransform scoreMarker;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnNoteHit()
    {
        hitSuccessText.text = "Sucess";
        hitSuccessText.color = Color.green;
    }

    public void OnNoteMiss()
    {
        hitSuccessText.text = "Miss";
        hitSuccessText.color = Color.gray;
    }

    public void UpdateGameScoreMarker(float score)
    {
        if (scoreMarker is null)
        {
            return;
        }

        var basePosition = scoreMarker.transform.position;
        basePosition.y = score;
        scoreMarker.transform.position = basePosition;
    }
}