using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
	public GameObject canvas;
	public RectTransform Sinv;
	public RectTransform Winv;
	
	MovementController mc;
	
	List<String> inventoryS = new List<String>();
	List<String> inventoryW = new List<String>();
	
    // Start is called before the first frame update
    void Start()
    {
		mc = (MovementController)FindObjectOfType(typeof(MovementController));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
		{
			HideInventory("PlayerS");
		}
    }
	
	public void GiveItem(String playerName, String itemName, Sprite itemSprite)
	{
		if(itemName == "endItem")
		{
			Debug.Log("finish");
			//finish the game here
		}
		
		if(playerName == "PlayerS")
		{
			VisualizeItem(itemSprite, Sinv, inventoryS.Count);
			inventoryS.Add(itemName);
		}
		else if(playerName == "PlayerW")
		{
			VisualizeItem(itemSprite, Winv, inventoryW.Count);
			inventoryW.Add(itemName);
		}
	}
	
	void VisualizeItem(Sprite itemSprite, RectTransform rt, int offset)
	{
		GameObject obj = new GameObject();
		Image img = obj.AddComponent<Image>();
		img.sprite = itemSprite;
		obj.GetComponent<RectTransform>().SetParent(rt);
		obj.SetActive(true);
		obj.GetComponent<RectTransform>().transform.position = rt.transform.position + new Vector3(0f, 1.5f - offset, 0f);
		obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.6f);
		obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.6f);
	}
	
	public bool IsItemInInventory(String playerName, String itemName)//item is in inventory of player p if it is in inventoryp or the players are connected and it is in the other inventory
	{
		if(playerName == "PlayerS")
		{
			if(inventoryS.Contains(itemName))
			{
				return true;
			}
			
			if(mc.GetConnected() && inventoryW.Contains(itemName))
			{
				return true;
			}
			
			return false;
		}
		else if(playerName == "PlayerW")
		{
			if(inventoryW.Contains(itemName))
			{
				return true;
			}
			
			if(mc.GetConnected() && inventoryS.Contains(itemName))
			{
				return true;
			}
			
			return false;
		}
		
		return false;//this return statement should be unreachable
	}
	
	public void HideInventory(String playerName)
	{
		RectTransform inv;
		
		if(playerName == "PlayerS")
		{
			inv = Sinv;
		}
		else if(playerName == "PlayerW")
		{
			inv = Winv;
		}
		else
		{
			return;
		}
		
		//SpriteRenderer[] srs = (SpriteRenderer[])GetComponentsInChildren(typeof(SpriteRenderer));
		
		//foreach(SpriteRenderer sr in srs)
		//{
		//	sr.enabled = false;
		//}
	}
}
