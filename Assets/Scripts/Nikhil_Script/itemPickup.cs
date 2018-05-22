using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour {

    //The player will collide with this script, and this will give the weapon/whatever to the player 

    public GameObject item; //this is what the pickup is carrying

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // other.GetComponent<playerHealth>().equipGun(item);
            other.gameObject.GetComponent<playerHealth>().equipGun(item);
            Destroy(this.gameObject);
        }
    }
}
