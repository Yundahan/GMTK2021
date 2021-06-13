using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
	public TileMovement playerS;
	public TileMovement playerW;
	public InventoryHandler invh;
	public MovementController mc;
	public Counter counter;
	public CameraBehaviour camerab;
	
	bool cpIfConnected = false;
	
	Item[] items;
	
	Vector2Int[] cpPositionsS = {new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2), new Vector2Int(2, 3)};
	Vector2Int[] cpPositionsW = {};
	
    // Start is called before the first frame update
    void Start()
    {
		items = (Item[])FindObjectsOfType(typeof(Item));
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.R))
		{
			Reset();
		}
		
		if(mc.GetConnected() && cpIfConnected)
		{
			cpIfConnected = false;
			UpdateCheckpoint();
		}
		
        foreach(Vector2Int cpPosition in cpPositionsS)
		{
			if(cpPosition == playerS.GetIntPos())
			{
				if(!mc.GetConnected())//cant cp if not connected
				{
					cpIfConnected = true;
				}
				else
				{
					UpdateCheckpoint();
				}
				
				return;
			}
		}
		
        foreach(Vector2Int cpPosition in cpPositionsW)
		{
			if(cpPosition == playerW.GetIntPos())
			{
				if(!mc.GetConnected())//cant cp if not connected
				{
					cpIfConnected = true;
				}
				else
				{
					UpdateCheckpoint();
				}
				
				return;
			}
		}
    }
	
	void UpdateCheckpoint()
	{
		playerS.UpdateData();
		playerW.UpdateData();
		
		foreach(Item item in items)
		{
			item.UpdateData();
		}
	}
	
	public void Reset()
	{
		playerS.Reset();
		playerW.Reset();
		counter.EndCounter();
		mc.Reset();
		
		foreach(Item item in items)
		{
			item.Reset();
		}
	}
}
