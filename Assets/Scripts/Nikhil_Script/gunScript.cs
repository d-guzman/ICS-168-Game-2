using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {


    //This script creates the bullet and tells it where to spawn


    //The bullet will be set to this variable. 
    public int damage = 10;
    public int speed = 100;
    public float range = 100f;
    public float lifeTime = 2f; //how long should the bullet live, this determines range

    public bool raycasting;


    public float fireRate = 0.4f;

    public GameObject bullet;

    //public GameObject shootPoint;

    private Transform gunSpawnTrans;

    public Transform camTrans;

   // public Vector3 gunOffset;

    public Camera fpsCamera;

    //public Transform spawnPoint;

    public ParticleSystem MuzzleFlash;

    public GameObject impactEffect;

    public Transform bulletSpawn;

    [Tooltip("The name of the gun. Requipred for sound")]
   public string gunName;

    public void unequip()
    {
        Debug.Log("HRY");
        Destroy(this.gameObject);
    }

    public bool shoot()
    {
        //Debug.Log("shoot() called.");
        MuzzleFlash.Play();

        if (raycasting)
        {
            if(fpsCamera == null)
            {
                // fpsCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            //Debug.Log("Oh yes raycasting time");
            RaycastHit hit;
            

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                //Debug.Log(hit.transform.name);

                enemyScript enemy = hit.transform.GetComponent<enemyScript>();

                if(enemy != null)
                {
                    enemy.HurtEnemy(damage);
                }

                playerHealth enemyPlayer = hit.transform.GetComponent<playerHealth>();

                bool killedEnemy;

                if(enemyPlayer != null)
                {

                    killedEnemy = enemyPlayer.hurtPlayer(damage);
                    if(killedEnemy)
                    {
                        return true;
                    }

                }



                GameObject impac =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impac, 1f);

            }

        }
        else
        {
            //This is bullet shooting
            // if (camTrans == null) { camTrans = GameObject.FindGameObjectWithTag("camera").GetComponent<Transform>(); }
            // gunSpawnTrans = GameObject.FindGameObjectWithTag("gunspawn").GetComponent<Transform>();
            //gunSpawnTrans = bulletSpawn;
            GameObject bully;
            if (gunSpawnTrans != null)
            {
                Vector3 desiredPosition = gunSpawnTrans.position;   //At the moment the bullet spawns in the middle of the gun, oh well  WILL BE CHANGED LATER worry not 
                bully = Instantiate(bullet, desiredPosition, camTrans.rotation); //And the bullet roation should follow the camera 
                bulletScript bulletInstance = bully.GetComponent<bulletScript>();
                bulletInstance.speed = speed;
                bulletInstance.damage = damage;
                bulletInstance.lifeTime = lifeTime;
            }
            else
            {
                Debug.Log("Oh man it looks like we can't find the desired position");
            }
        }
        //Debug.Log(gunName+ "I am asking it to play a sound " + gunName);
        FindObjectOfType<audioManager>().Play(gunName);
        return false;
    }

	// Use this for initialization
	void Awake () {


        camTrans = GameObject.FindGameObjectWithTag("camera").GetComponent<Transform>();

        fpsCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
	
	// Update is called once per frame
	void Update () {
        

    }


}
