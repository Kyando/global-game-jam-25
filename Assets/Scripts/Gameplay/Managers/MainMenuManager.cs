using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameObject newImage;
    public float timeToNextLevel = 3f;
    public float fadeOutTime = 2.4f;
    
    public float timeCounter = 0;
    public bool goToNextLevel = false;
    [FormerlySerializedAs("FadeOutImage")] public GameObject fadeOutImage;

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
        if (Input.GetKeyDown(KeyCode.Space) && goToNextLevel == false)
        {
            newImage.SetActive(true);
            
            goToNextLevel = true;
            return;
        }

        if (goToNextLevel)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= fadeOutTime)
            {
                fadeOutImage.SetActive(true);
            }
            if (timeCounter >= timeToNextLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}