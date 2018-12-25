using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PathEnd : MonoBehaviour {

    [SerializeField]
    private AudioClip explosionSound;
    private static AudioClip explosionSoundG;
    private static AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        explosionSoundG = explosionSound;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void PlayExplosion()
    {
        audioSource.PlayOneShot(explosionSoundG);
    }
}
