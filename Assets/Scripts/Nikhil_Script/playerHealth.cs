using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //This script handles player health and attacking. PlayerController can focus on movement

    //These variables will be the positions of some guns 
    private Vector3 riflePosition = new Vector3(0.299f, -0.144f, 0.543f);

    //  private Vector3 riflePosition = new Vector3(0.299f, -0.144f, 0.543f);

    // Use this for initialization
    void Start () {
        currentPlayerController = GetComponent<PlayerControllerXboxV1>();
	}
	
	// Update is called once per frame
	void Update () {
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
        string player = currentPlayerController.GetPlayerPrefix();
        float thing = 0.0f;
        if (player != null && player != "not yet assigned") {
            thing = Input.GetAxis(player + "RightTrigger");
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
            Destroy(this.gameObject);
        }
    }
}
