using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {


    public int speed = 20;

    public int damage = 10;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 5f); //the bullet will kill itself in like 5 seconds 
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}



    public void move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag=="Enemy")
        {
            other.GetComponent<enemyScript>().HurtEnemy(damage); //damage the enemy then die
            Destroy(this.gameObject);
        }
    }

}
