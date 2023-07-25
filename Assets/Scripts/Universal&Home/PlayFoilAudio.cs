using UnityEngine;
using Rand = UnityEngine.Random;

/*"PlayFoilAudio" script controls the audio 
playback for the foil game object when it collides with other objects. */
public class PlayFoilAudio : MonoBehaviour
{
    public AudioClip[] foilSounds;
    public AudioSource foil;


/*"OnCollisionEnter" function is called when a collision occurs with the game object that has a Collider and Rigidbody and collides with another object.
Parameters:
    other: The Collision data containing information about the collision.*/
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
