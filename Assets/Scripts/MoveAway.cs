using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAway : MonoBehaviour {

    public float Speed;
    public float SpeedTime = 0.3f;
    public AnimationCurve Curve;

    float counter = 0;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {

        counter += Time.deltaTime;

        float eval = Curve.Evaluate(counter * SpeedTime);
        transform.Translate(Time.deltaTime * Vector2.up * eval * Speed);
		
	}
}
