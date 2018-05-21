using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles player health and attacking. PlayerController can focus on movement
public class playerHealth : MonoBehaviour {

    //public int maxHealth = 100;
    public int health = 100;

    public gunScript gun;
    public GameObject gunGameObject;

    public float shootInterval;

    public float shootTime = 0.0f;

    public bool weaponEquipped = false;

    public Transform gunSpawnPoint;


    private GameObject instanceRef;

    private PlayerControllerXboxV1 currentPlayerController;


    public gameController gameCont;
    public string playerID;

    void Start () {
        currentPlayerController = GetComponent<PlayerControllerXboxV1>();
        gameCont = FindObjectOfType<gameController>();
	}
	
	// Update is called once per frame
	void Update () {
        playerID = currentPlayerController.GetPlayerPrefix(); //I (Nikhil) moved this here so it would be a class variable 
        checkAttack();
        checkDeath();
	}

    public void hurtPlayer(int damage)
    {
        health = health - damage;
    }


    public void equipGun(GameObject item)
    {
        if(weaponEquipped == true)
        {
            Destroy(instanceRef);
        }
        gunGameObject = item;
        gun = item.GetComponent<gunScript>();
        shootInterval = gun.fireRate;
        weaponEquipped = true;
        instanceRef =Instantiate(gunGameObject,gunSpawnPoint);
        gunScript gunInfo = instanceRef.GetComponent<gunScript>();
        gun.bulletSpawn = currentPlayerController.pivotTransform.GetChild(currentPlayerController.mainCam.transform.GetSiblingIndex());
        gun.camTrans = currentPlayerController.pivotTransform;
        gun.fpsCamera = currentPlayerController.mainCam;
    }


    public void checkAttack()
    {
        //string player = currentPlayerController.GetPlayerPrefix();
        float thing = 0.0f;
        if (playerID != null && playerID != "not yet assigned") {
            thing = Input.GetAxis(playerID + "RightTrigger");
        }

        if (thing != 0.0)
        {
            //Debug.Log("sshooting HAPPENS from "+player);
            if(weaponEquipped)
            {
                
                if (shootTime <= Time.time)
                {
                    shootTime = Time.time + shootInterval; //Bullets will be shot every 0.4 seconds. Change this number to change the fire rate.
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
            gameCont.playerDied(playerID);
            Destroy(this.gameObject);
        }
    }
}
