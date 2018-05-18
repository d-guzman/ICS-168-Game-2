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

    public GameObject bullet;

    public GameObject shootPoint;

    private Transform gunSpawnTrans;

    private Transform camTrans;

    public Vector3 gunOffset;

    public Camera fpsCamera;

    //public Transform spawnPoint;

    public ParticleSystem MuzzleFlash;

    public GameObject impactEffect;

    public void shoot()
    {
        MuzzleFlash.Play();

        if (raycasting)
        {
            if(fpsCamera == null)
            {
                fpsCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            //Debug.Log("Oh yes raycasting time");
            RaycastHit hit;
            

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                enemyScript enemy = hit.transform.GetComponent<enemyScript>();

                if(enemy != null)
                {
                    enemy.HurtEnemy(damage);
                }

                GameObject impac =  Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impac, 1f);

            }

        }
        else
        {

            if (camTrans == null) { camTrans = GameObject.FindGameObjectWithTag("camera").GetComponent<Transform>(); }
            gunSpawnTrans = GameObject.FindGameObjectWithTag("gunspawn").GetComponent<Transform>();
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
        FindObjectOfType<audioManager>().Play("pistol");
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
