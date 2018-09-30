using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public int Point;
    public GameObject ps;
    public Color textColor;

	// Use this for initialization
    public void DestroyMe () 
    {
        Destroy(gameObject);	
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
