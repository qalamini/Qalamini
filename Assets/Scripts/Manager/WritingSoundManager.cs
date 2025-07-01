using UnityEngine;

public class WritingSoundManager : MonoBehaviour
{
    public AudioSource writingAudio;

    public void PlayWritingSound()
    {
        if (!writingAudio.isPlaying)
        {
            writingAudio.Play();
        }
    }

    public void StopWritingSound()
    {
        if (writingAudio.isPlaying)
        {
            writingAudio.Stop();
        }
    }
}
