using UnityEngine;
using UnityEngine.Audio;
using Rand = UnityEngine.Random;

public class PlayFoilAudio : MonoBehaviour
{
    public AudioClip[] foilSounds;
    public AudioSource foil;

    public void PlaySound()
    {
        int swap = Rand.Range(0, 2);
        foil.PlayOneShot(foilSounds[swap]);
    }
}
