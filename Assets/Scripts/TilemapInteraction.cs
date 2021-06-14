using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInteraction : MonoBehaviour
{
	public Tilemap tilemap;
	public InventoryHandler invh;
	public TileMovement[] players = new TileMovement[2];
	public AudioHandler ah;
	
	Dictionary<String, String> interactionDict = new Dictionary<String, String>{{"Kleine Dornenhecke", "Schere"}, {"Große Dornenhecke", "Schere"}, {"W Eisspitzen", "Fackel"}, {"W großer Stein", "Spitzhacke"}, {"Großer Stein", "Spitzhacke"}};//this dictionary holds pairs of (tile type, item for tile type)
	
	public List<String> tbrList = new List<String>();//list of tiles to be replaced
	public List<TileBase> rList = new List<TileBase>();//list of their replacements
	
	Dictionary<Vector3Int, TileBase> resetDict = new Dictionary<Vector3Int, TileBase>();
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
		{
			CheckInteractions();
		}
    }
	
	void CheckInteractions()
	{
		foreach(TileMovement player in players)
		{
			Vector2Int lookatPos = player.GetIntPos() + player.GetDirection();
			Vector3Int position = new Vector3Int(lookatPos.x, lookatPos.y, 0);
			TileBase tb = tilemap.GetTile(position);
			
			if(tb == null)//this should actually never trigger if wall detection and level layout are done correctly
			{
				return;
			}
			
			DealWithTile(tb, position, player.gameObject.name);
		}
	}
	
	void DealWithTile(TileBase tb, Vector3Int position, String playerName)
	{
		if(interactionDict.ContainsKey(tb.name))
		{
			String requiredItem = interactionDict[tb.name];
			
			if(invh.IsItemInInventory(playerName, requiredItem))
			{
				int index = FindStringInList(tbrList, tb.name);
				
				if(requiredItem == "Schere")
				{
					ah.PlayClip(3);
				}
				
				if(requiredItem == "Fackel")
				{
					ah.PlayClip(5);
				}
				
				if(requiredItem == "Spitzhacke")
				{
					ah.PlayClip(4);
				}
				
				if(index == -1)
				{
					return;//this is extreme doodoo
				}
				
				resetDict.Add(position, tb);
				tilemap.SetTile(position, rList[index]);
			}
		}
	}
	
	int FindStringInList(List<String> stringList, String str)
	{
		for(int i = 0; i < stringList.Count; i++)
		{
			if(stringList[i] == str)
			{
				return i;
			}
		}
		
		return -1;
	}
	
	public void UpdateData()
	{
		resetDict.Clear();
	}
	
	public void Reset()
	{
		foreach(KeyValuePair<Vector3Int, TileBase> entry in resetDict)
		{
			tilemap.SetTile(entry.Key, entry.Value);
		}
	}
}
