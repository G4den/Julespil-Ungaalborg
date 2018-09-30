using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Klubber : MonoBehaviour {

    public Klub [] klubber ;
    public Dropdown dropdown;

	// Use this for initialization
	void Awake () {
        for (int i = 0; i < klubber.Length; i++)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text ="<color=#" + ColorUtility.ToHtmlStringRGB(klubber[i].LeaderboardFarve) + ">" + klubber[i].Navn + "</color>" });
        }
	}
}

[System.Serializable]
public class Klub
{
    public string Navn;
    public Color LeaderboardFarve = Color.white;
}
