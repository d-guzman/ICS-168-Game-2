using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public int maxHealth = 100;
    public int health;

    public gunScript gun;

    public float shootTime = 0.0f;

    //This script handles player health and attacking. PlayerController can focus on movement

	// Use this for initialization
	void Start () {

        health = maxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {
        checkAttack();
	}

    public void checkAttack()
    {
        //Debug.Log("Checking attack");
        if(Input.GetButton("A"))
        {
            if(shootTime <= Time.time)
            {
                shootTime = Time.time + 0.4f; //Bullets will be shot every 0.4 seconds. Change this number to change the fire rate.
                gun.shoot();
            }
            
        }
        
    }

    public void checkDeath()
    {
        if(health<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
