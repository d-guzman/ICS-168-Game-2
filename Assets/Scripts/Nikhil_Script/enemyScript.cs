
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    //Script for the enemy. So far all it can do is die 

    public int health = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        checkDeath();
	}

    public void checkDeath()
    {
        if(health<=0)
        {
            Destroy(this.gameObject);
        }
    }

    public void HurtEnemy(int damage)
    {
        health = health - damage;
    }
}
