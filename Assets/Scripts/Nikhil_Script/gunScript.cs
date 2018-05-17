using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {


    //This script creates the bullet and tells it where to spawn

    public int damage = 10;

    public GameObject bullet;

    public GameObject shootPoint;

    private Transform gunSpawnTrans;

    private Transform camTrans;

    public Vector3 gunOffset;

    //public Transform spawnPoint;
    
    public void shoot()
    {


        gunSpawnTrans = GameObject.FindGameObjectWithTag("gunspawn").GetComponent<Transform>();
        
        GameObject bully;


        

        if (gunSpawnTrans!= null)
        {
            Vector3 desiredPosition = gunSpawnTrans.position;   //At the moment the bullet spawns in the middle of the gun, oh well  WILL BE CHANGED LATER worry not 
            bully = Instantiate(bullet, desiredPosition, camTrans.rotation); //And the bullet roation should follow the camera 
        }
        else
        {
            Debug.Log("Oh man it looks like we can't find the desired position");
        }
       
        FindObjectOfType<audioManager>().Play("pistol");
    }

	// Use this for initialization
	void Awake () {


        camTrans = GameObject.FindGameObjectWithTag("camera").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void Update () {


    }


}
