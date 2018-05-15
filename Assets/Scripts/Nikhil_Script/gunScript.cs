using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {


    //This script creates the bullet and tells it where to spawn

    public int damage = 10;

    public GameObject bullet;

    public GameObject shootPoint;

    private Transform childTrans;

    //public Transform spawnPoint;
    
    public void shoot()
    {
        //Debug.Log(shootPoint.transform.position);

        Vector3 worldPosition = transform.TransformPoint(transform.position);

        childTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.Log(childTrans.position);
    
        GameObject bully = Instantiate(bullet,childTrans.position,childTrans.localRotation);
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
		
	}
}
