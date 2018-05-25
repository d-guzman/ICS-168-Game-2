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

    public int killCount; //this is tied to the win condition

    private GameObject instanceRef;

    private GameObject weapon;

    // [HideInInspector]
    public GameObject weaponPickup;

    private PlayerControllerXboxV1 currentPlayerController;

    public Vector3 spawnPosition;


    public gameController gameCont;
    public string playerID;

    private bool canShoot = true;

    void Start () {
        currentPlayerController = gameObject.GetComponent<PlayerControllerXboxV1>();
        // gameCont = FindObjectOfType<gameController>();
        gunSpawnPoint.localPosition = new Vector3(0, 0, 0);
        spawnPosition = transform.position;
        killCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameCont == null) 
            gameCont = FindObjectOfType<gameController>();
        playerID = currentPlayerController.GetPlayerPrefix(); //I (Nikhil) moved this here so it would be a class variable 
        checkAttack();
        checkDeath();
	}

    public bool hurtPlayer(int damage)
    {
        if(health <= damage )
        {
            Debug.Log("Oh no dead " + playerID);
            health = health - damage;
            return true;
        }
        else
        {
            health = health - damage;
            return false;
        }
        
    }

    public void killedSomeone()
    {
        killCount++;
        gameCont.playerKilled(playerID,killCount); //provide the playerID of the killer
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

        if (weaponPickup != null) {
            weaponPickup.SetActive(true);
        }

        weapon = instanceRef;
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
            //Debug.Log(playerID + "RightTrigger");
        }

        if (thing != 0.0)
        {
            //Debug.Log("sshooting HAPPENS from "+player);
            if(weaponEquipped)
            {
                //if (shootTime <= Time.time)
                //{
                //    shootTime = Time.time + shootInterval; //Bullets will be shot every 0.4 seconds. Change this number to change the fire rate.
                //    bool killShot = gun.shoot();

                //    if(killShot) //this means that a player died
                //    {
                //        killedSomeone();
                //    }
                //}
                if (canShoot) {
                    StartCoroutine(waitForFire());
                    bool killShot = gun.shoot();

                    if (killShot) //this means that a player died
                    {
                        killedSomeone();
                    }
                }
            }
        }
    }
    
    public void respawn()
    {
        transform.position = spawnPosition;
        health = 100;
        Debug.Log("Reborn again! " + playerID);
    }

    public void checkDeath()
    {
        if(health<=0)
        {
            gameCont.playerDied(playerID);
            Destroy(weapon);
            gun = null;
            gunGameObject = null;
            weaponEquipped = false;

            if (weaponPickup != null) {
                weaponPickup.SetActive(true);
            }

            weaponPickup = null;
            
            respawn();
            //Destroy(this.gameObject);
        }
    }

    IEnumerator waitForFire() {
        canShoot = false;
        yield return new WaitForSeconds(shootInterval);
        canShoot = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "KillPlane") {
            health = 0;
            killCount--;
        }
    }
}
