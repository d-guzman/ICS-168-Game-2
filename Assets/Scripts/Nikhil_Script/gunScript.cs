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
        //Debug.Log(shootPoint.transform.position);

        //Vector3 worldPosition = transform.TransformPoint(transform.position);

        //Debug.Log("And the world position is " + worldPosition);

        gunSpawnTrans = GameObject.FindGameObjectWithTag("gunspawn").GetComponent<Transform>();
        
        GameObject bully;

     


        if(gunSpawnTrans!= null)
        {
            Vector3 desiredPosition = gunSpawnTrans.position + shootPoint.transform.position;
            bully = Instantiate(bullet, desiredPosition, camTrans.rotation);
        }
        else
        {
            Debug.Log("Oh man it looks like we can't find the desired position");
        }
       

        //GameObject bully = Instantiate(bullet, gunSpawnTrans.position, gunSpawnTrans.localRotation);
        //bully.transform.position = transform.position;
        FindObjectOfType<audioManager>().Play("pistol");
    }

	// Use this for initialization
	void Awake () {

        

        //childTrans = GetComponentInParent<Transform>();

        //  shootPoint = this.gameObject.transform.GetChild(0);

    }
	
	// Update is called once per frame
	void Update () {
       // camTrans = GameObject.FindGameObjectWithTag("camera").GetComponent<Transform>();
        adjustRotation();

    }

    public void adjustRotation()
    {

        
    }
}
