using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class HowToDie : MonoBehaviour
{
	public Tilemap tilemap;
	public CheckpointController cpc;
	public TileMovement playerS;
	public TileMovement playerW;
	public Text message;
	public Counter counter;
	public MovementController mc;
	public AudioHandler ah;
	
	String deathMessage;
	String[] deadlyTerrain = {"Große Dornenhecke", "Kleine Dornenhecke", "W Eisspitzen"};
	bool isDeadS = false;
	bool isDeadW = false;
	
    // Start is called before the first frame update
    void Start()
    {
		message.enabled = false;
        message.text = "You died. \n Press Space to return to last checkpoint.";
    }
	
    // Update is called once per frame
    void Update()
    {
        if((isDeadS || isDeadW) && Input.GetKey(KeyCode.Space))
        {
			message.enabled = false;
			isDeadS = false;
			isDeadW = false;
			cpc.Reset();
			playerS.SetRooted(false);
			playerW.SetRooted(false);
        }
    }
	
	public bool Death(Vector2Int intPos)
	{	
		Vector3Int gridPos = new Vector3Int(intPos.x, intPos.y, 0);
		TileBase tb = tilemap.GetTile(gridPos);
		
		if(tb == null)//this should actually never trigger if wall detection and level layout are done correctly
		{
			return true;
		}
		
		foreach(String tile in deadlyTerrain)
		{
			if(tb.name == tile)
			{
				return true;
			}
		}
		return false;
	}
	
	public void DisplayDeathMessage()
	{
		if(isDeadS || isDeadW)
		{
			return;
		}
		
		isDeadS = Death(playerS.GetIntPos());
		isDeadW = Death(playerW.GetIntPos());
		
		if(counter.GetCounter() <= 0 && !mc.GetConnected())
		{
			isDeadS = true;
		}
		
		if(isDeadS || isDeadW)
		{
			ah.PlayClip(0);
			playerS.SetRooted(true);
			playerW.SetRooted(true);
			message.enabled = true;
		}
 
	}
}