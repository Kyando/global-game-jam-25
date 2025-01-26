using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI hitSuccessText;
    
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
}