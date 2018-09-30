using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musictoggle : MonoBehaviour {

    public Sprite On, Off;
    private Image My;

    public AudioSource SourceToToggle;

	// Use this for initialization
	void Start () {

        My = GetComponent<Image>();

    //    SourceToToggle.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleSound()
    {
        SourceToToggle.enabled = !SourceToToggle.enabled;
        if (SourceToToggle.enabled)
        {
            My.sprite = On;
        }
        else
        {
            My.sprite = Off;
        }
    }
}
