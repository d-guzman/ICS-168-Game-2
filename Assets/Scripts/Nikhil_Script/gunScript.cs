using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {


    //This script creates the bullet and tells it where to spawn

    public int damage = 10;

    public GameObject bullet;

    public GameObject shootPoint;
    
    public void shoot()
    {
        Instantiate(bullet,shootPoint.transform.position,shootPoint.transform.rotation);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
