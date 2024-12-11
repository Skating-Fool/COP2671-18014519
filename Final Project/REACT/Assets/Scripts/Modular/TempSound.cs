using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TempSound : MonoBehaviour
{

    public bool playOnStart;
    private bool playing = false;

    public AudioSource audioSource;

    void Start()
    {

        audioSource = audioSource != null ? audioSource : GetComponent<AudioSource>();

        if (playOnStart)
        {
            Play();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying && playing)
        {
            Destroy(gameObject);
        }
    }

    public void Play()
    {
        audioSource.Play();
        playing = true;
    }

}