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
	public String deathMessage;
	public Counter counter;
	String[] deadlyTerrain = {"fire", "thorns"};
	bool isDeadS = false;
	bool isDeadW = false;
	
    // Start is called before the first frame update
    void Start()
    {
		message.enabled = false;
        message.text = "You died. Press Space to return to last checkpoint.";
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
			playerS.SetRooted(true);
			playerW.SetRooted(true);
			message.enabled = true;
		}
	}
    // Update is called once per frame
    void Update()
    {
		
		isDeadS = Death(playerS.GetIntPos());
		isDeadW = Death(playerW.GetIntPos());
		if(counter.GetCounter() <= 0)
		{
			isDeadS = true;
		}
		
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
}