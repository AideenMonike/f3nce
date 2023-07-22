using UnityEngine;
using Rand = UnityEngine.Random;

public class PlayFoilAudio : MonoBehaviour
{
    public AudioClip[] foilSounds;
    public AudioSource foil;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit");
        if (!other.gameObject.CompareTag("Foil"))
        {
            int swap = Rand.Range(0, 2);
            foil.PlayOneShot(foilSounds[swap]);
            Debug.Log("hit");
        }
    }
}
