using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapInteraction : MonoBehaviour
{
	public TileMovement[] players = new TileMovement[2];
	String[] destructible = {"Winter Uuugly"};
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
		{
			
		}
    }
}
