using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PathEnd : MonoBehaviour {

    private static PathEnd instance;

    [SerializeField]
    private GameObject explosionEffect;

    [SerializeField]
    private AudioClip explosionSound;
    private static AudioClip explosionSoundG;
    private static AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        explosionSoundG = explosionSound;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayExplosion()
    {
        GameObject effect = (GameObject) Instantiate(explosionEffect, transform.position, transform.rotation);  // effect
        Destroy(effect, 4f);
        audioSource.PlayOneShot(explosionSoundG);                                                               // sound
    }

    public static PathEnd GetInstance()
    {
        return instance;
    }
}
