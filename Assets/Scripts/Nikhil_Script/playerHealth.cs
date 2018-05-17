using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    //public int maxHealth = 100;
    public int health = 100;

    public gunScript gun;
    public GameObject gunGameObject;

    public float shootTime = 0.0f;

    public bool weaponEquipped = false;

    public Transform gunSpawnPoint;

    //This script handles player health and attacking. PlayerController can focus on movement

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        checkAttack();
	}


    public void equipGun(GameObject item)
    {
        gunGameObject = item;
        gun = gunGameObject.GetComponent<gunScript>();
        weaponEquipped = true;
        Vector3 desiredPosition = gunSpawnPoint.transform.position + gun.gunOffset;
        GameObject instanceRef =Instantiate(gunGameObject,gunSpawnPoint.transform.position, gunSpawnPoint.transform.rotation, gunSpawnPoint);
        Debug.Log("The offset here us " + gun.gunOffset);
        //instanceRef.transform.position = gun.gunOffset;
        //instanceRef.transform.position = gunGameObject.transform.position;
        //instanceRef.transform.rotation = gunGameObject.transform.rotation;



    }


    public void checkAttack()
    {
        //Debug.Log("Checking attack");
        //RightTrigger_2
        float thing = Input.GetAxis("RightTrigger");
        if (thing != 0.0)
        {
            //Debug.Log("sshooting HAPPENS");
            if(weaponEquipped)
            {
                
                if (shootTime <= Time.time)
                {
                    shootTime = Time.time + 0.4f; //Bullets will be shot every 0.4 seconds. Change this number to change the fire rate.
                    gun.shoot();
                }
            }
            else
            {
                //Debug.Log("You cannot shoot for nothing is equipped yet");
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
