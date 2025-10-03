using UnityEngine;

public class TrueFalseSoundManager : MonoBehaviour
{
    // coba buat instance biar bisa diakses dari mana aja
    public AudioSource correctAudio;
    public AudioSource wrongAudio;
    public void PlayCorrectSound()
    {
        correctAudio.Play();
    }
    public void PlayWrongSound()
    {
        wrongAudio.Play();
    }
}
