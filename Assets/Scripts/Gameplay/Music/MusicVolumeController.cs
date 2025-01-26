using UnityEngine;

public class MusicVolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    public PlayerType playerType = PlayerType.UNDEFINED;
    public float targetVolume = 1;
    public float volumeTransitionSpeed = 1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            targetVolume = 0;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            targetVolume = 1;
        }
        
        if (audioSource is null)
        {
            return;
        }

        if (audioSource.isPlaying && !Mathf.Approximately(audioSource.volume, targetVolume))
        {
            if (audioSource.volume < targetVolume)
            {
                audioSource.volume = Mathf.Clamp01(audioSource.volume + (volumeTransitionSpeed * Time.deltaTime));
            }
            else
            {
                audioSource.volume = Mathf.Clamp01(audioSource.volume - (volumeTransitionSpeed * Time.deltaTime));
            }
        }
    }
}