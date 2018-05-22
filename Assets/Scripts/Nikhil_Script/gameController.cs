using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

    // This script can handle stuff like respawning and whatnot 
    //Also can keep track of score 

    public static gameController instance;

    public GameObject[] pickups;

    public int overallKillCount;

    [Tooltip("How many deaths needed to win")]
    public int winCondition = 5;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        overallKillCount = 0;
    }

    public void playerKilled(string playerID,int killCount) //called when the player kills someone   
    {
        //When the player shoots someone, maybe HurtPlayer can return true or false so the player knows if they killed someone
        Debug.Log("oH mY lOrDDDy it seems that "+playerID+" has KIIILED");

        if (killCount >= 5)
        {
            Debug.Log(playerID + " HAS WON THE GAAME");
        }

        
    }


    public void playerDied(string playerID) //called when the player dies 
    {
        Debug.Log("It seems like "+playerID+"has died. Very sad =(");
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
