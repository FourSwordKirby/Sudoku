using UnityEngine;
using System.Collections;

public class selectSound : MonoBehaviour {
    public AudioClip selectSoundClip;
    private AudioSource source;
    private float lowRange = .5f;
    private float highRange = 1.5f;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            source.PlayOneShot(selectSoundClip, 1f);
        }
	
	}
}
