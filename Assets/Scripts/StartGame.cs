using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    bool flag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.S) && !flag)
        {
            flag = true;
            Begin();
        }
    }
    public void Begin()
    {
        GetComponent<AudioSource>().Play();
        StockInventory inventory = GameObject.Find("StockDatabase").GetComponent<StockInventory>();
        while(inventory.numComp < 5)
        {
            Debug.Log("HI");
        }
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
    }
}
