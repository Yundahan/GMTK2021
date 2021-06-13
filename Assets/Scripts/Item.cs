using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public InventoryHandler inv;
	
	bool active = true;
	bool resetActive;
	bool resetEnabled = true;
	
	TileMovement[] players;
	Vector2Int intPos;
	
    // Start is called before the first frame update
    void Start()
    {
		intPos = new Vector2Int((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y));
		players = (TileMovement[])FindObjectsOfType(typeof(TileMovement));
		UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void CheckPlayerPositions()
	{
		if(!active)
		{
			return;
		}
		
		foreach(TileMovement player in players)
		{
			Vector2Int position = player.GetIntPos();
			
			if(position == intPos)
			{
				SpriteRenderer sr = (SpriteRenderer)gameObject.GetComponent(typeof(SpriteRenderer));
				inv.GiveItem(player.gameObject.name, gameObject.name, sr.sprite);
				sr.enabled = false;
				active = false;
			}
		}
	}
	
	public void UpdateData()
	{
		resetActive = active;
		resetEnabled = ((SpriteRenderer)gameObject.GetComponent(typeof(SpriteRenderer))).enabled;
	}
	
	public void Reset()
	{
		active = resetActive;
		((SpriteRenderer)gameObject.GetComponent(typeof(SpriteRenderer))).enabled = resetEnabled;
	}
}
