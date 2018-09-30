using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement : MonoBehaviour {

    public float Speed = 500;
    public Vector2 Dir = Vector2.zero;

    private Rigidbody2D rb;


    float minY;

    private Gamecontroller gc;

	// Use this for initialization
	void Start () {

        gc = GameObject.FindObjectOfType<Gamecontroller>();

        rb = GetComponent<Rigidbody2D>();
       // rb.AddForce(Vector2.down * Speed*Time.deltaTime);
        rb.velocity = Dir*Speed;

        Vector3 tmp = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        minY = tmp.y;
		
	}
	
    void Update()
    {
        if(Vector3.Distance(new Vector3(transform.position.x,transform.position.y,0),Vector3.zero) >= gc.DistanceCenterToSide*2f)
        {
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void FixedUpdate () 
    {

       // rb.AddForce(Vector2.down * Speed*Time.deltaTime);
		
	}
}
