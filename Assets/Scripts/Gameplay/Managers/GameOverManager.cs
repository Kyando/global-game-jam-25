using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    
    public float timeToNextLevel = .5f;
    public float fadeOutTime = 0f;
    
    public float timeCounter = 0;
    public bool goToMainMenu = false;
    public GameObject fadeOutImage;

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


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && goToMainMenu == false)
        {
            
            goToMainMenu = true;
            return;
        }

        if (goToMainMenu)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= fadeOutTime)
            {
                fadeOutImage.SetActive(true);
            }
            if (timeCounter >= timeToNextLevel)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}