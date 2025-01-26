using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InterfererManager : MonoBehaviour
{
    public static InterfererManager instance;
    public Vector2 interactionTimeRange = new Vector2(20f, 30f);
    public GameObject lightObject;
    public GameObject lightSwitchOn;
    public GameObject lightSwitchOff;
    public bool isInteractinActive = false;
    [SerializeField] private float timerCounter = 0f;
    [SerializeField] private float nextInteractionTime = 20f;


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


    private void Update()
    {
        timerCounter += Time.deltaTime;
        if (!isInteractinActive && timerCounter >= nextInteractionTime)
        {
            timerCounter = 0f;
            GhostsManager.instance.playerPedro.StartStateGoingToLightSwitch();
            nextInteractionTime = Random.Range(interactionTimeRange.x, interactionTimeRange.y);
        }
    }

    public void TurnLightSwitch(bool active)
    {
        if (active)
        {
            SoundManager.instance.PlayLightAudio();
        }

        lightSwitchOn.SetActive(active);
        lightSwitchOff.SetActive(!active);
        
        lightObject.SetActive(active);
        isInteractinActive = active;
        timerCounter = 0;
    }
}