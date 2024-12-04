using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    [Header("---------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ---------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;

    [Header("---------- Looping Option ---------")]
    public bool loopMusic = true;  // Option to enable/disable looping, default is true

    // Start is called before the first frame update
    private void Start()
    {
        musicSource.clip = background;  // Assign the background music clip
        musicSource.loop = loopMusic;   // Set the loop option based on the Inspector value
        musicSource.Play();             // Start playing the music
    }

    // Update is called once per frame
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);  // Play the sound effect one time
    }
}
