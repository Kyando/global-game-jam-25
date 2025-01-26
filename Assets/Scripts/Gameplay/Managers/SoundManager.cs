using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip lightAudioClip;

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


    public void PlayLightAudio()
    {
        AudioSource.PlayClipAtPoint(lightAudioClip, transform.position);
    }
}