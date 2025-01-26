using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float timeToNextLevel = 160f;
    public float fadeOutTime = 154;
    public float timeCounter = 0f;
    public GameObject fadeOutImage;
    public GameObject canvas;

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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        timeCounter += Time.deltaTime;
        if (timeCounter >= fadeOutTime)
        {
            fadeOutImage.SetActive(true);
            canvas.SetActive(false);
        }

        if (timeCounter >= timeToNextLevel)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            bool victory = GameDataManager.instance.gameScore > 0;
            GoToEndScreen(victory);
        }
    }


    public void GoToEndScreen(bool victory)
    {
        if (victory)
        {
            SceneManager.LoadScene(2);
            return;
        }

        SceneManager.LoadScene(3);
    }
}