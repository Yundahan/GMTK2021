using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{
	//resettable objects
	public TileMovement playerS;
	public TileMovement playerW;
	public InventoryHandler invh;
	public MovementController mc;
	public Counter counter;
	public CameraBehaviour camerab;
	public TilemapInteraction ti;
	Item[] items;
	
	public Text cptext;
	float cpMessageTime = 0.7f;
	float cpTime;
	
	bool cpIfConnected = false;
	
	Vector2Int[] cpPositionsS = {new Vector2Int(56, 52), new Vector2Int(-29, 33), new Vector2Int(-2, 20), new Vector2Int(-1, 20), new Vector2Int(0, 20), new Vector2Int(-3, 49), new Vector2Int(-2, 49), new Vector2Int(-1, 49), new Vector2Int(0, 49), new Vector2Int(1, 49), new Vector2Int(2, 49), new Vector2Int(3, 49), new Vector2Int(35, 36), new Vector2Int(36, 35), new Vector2Int(37, 34), new Vector2Int(-21, 25), new Vector2Int(-21, 26)};
	Vector2Int[] cpPositionsW = {new Vector2Int(187, 85), new Vector2Int(232, 83)};
	
    // Start is called before the first frame update
    void Start()
    {
		items = (Item[])FindObjectsOfType(typeof(Item));
		cptext.enabled = false;
		cpTime = -cpMessageTime;
    }

    // Update is called once per frame
    void Update()
    {
		if(cptext.enabled && Time.time - cpMessageTime > cpTime)
		{
			cptext.enabled = false;
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
		invh.UpdateData();
		counter.UpdateData();
		ti.UpdateData();
		camerab.UpdateData();
		
		foreach(Item item in items)
		{
			item.UpdateData();
		}
		
		cptext.enabled = true;
		cpTime = Time.time;
	}
	
	public void Reset()
	{
		playerS.Reset();
		playerW.Reset();
		invh.Reset();
		counter.Reset();
		ti.Reset();
		camerab.Reset();
		counter.EndDCCounter();
		mc.Reset();
		
		foreach(Item item in items)
		{
			item.Reset();
		}
	}
}
