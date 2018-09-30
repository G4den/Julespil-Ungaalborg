using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicbox : MonoBehaviour {


    public AudioClip[] CLIPS;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);



        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
    public void POPONE () 
    {
		
        int i = Random.Range(0,CLIPS.Length);
        source.PlayOneShot(CLIPS[i]);

	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
