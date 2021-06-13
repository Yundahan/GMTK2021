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
	
	bool connected = true;
	String currentPlayer = "PlayerS";
	
	List<String> inventoryS = new List<String>();
	List<String> inventoryW = new List<String>();
	
	String resetCurrentPlayer;
	List<String> resetInventoryS = new List<String>();
	List<String> resetInventoryW = new List<String>();
	
    // Start is called before the first frame update
    void Start()
    {
		UpdateData();
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
		img.enabled = ((Image)(rt.gameObject.GetComponent<Image>())).enabled;
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
			
			if(connected && inventoryW.Contains(itemName))
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
			
			if(connected && inventoryS.Contains(itemName))
			{
				return true;
			}
			
			return false;
		}
		
		return false;//this return statement should be unreachable
	}
	
	public void UpdateInventoryVisibility()
	{
		if(connected)
		{
			ShowInventory("PlayerS");
			ShowInventory("PlayerW");
		}
		else
		{
			ShowInventory(currentPlayer);
			
			if(currentPlayer == "PlayerS")
			{
				HideInventory("PlayerW");
			}
			else if(currentPlayer == "PlayerW")
			{
				HideInventory("PlayerS");
			}
		}
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
		
		Image img = (Image)inv.gameObject.GetComponent(typeof(Image));
		img.enabled = false;
		
		foreach(Transform child in inv.transform)
		{
			img = (Image)child.gameObject.GetComponent(typeof(Image));
			img.enabled = false;
		}
	}
	
	public void ShowInventory(String playerName)
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
		
		Image img = (Image)inv.gameObject.GetComponent(typeof(Image));
		img.enabled = true;
		
		foreach(Transform child in inv.transform)
		{
			img = (Image)child.gameObject.GetComponent(typeof(Image));
			img.enabled = true;
		}
	}
	
	public void SetConnected(bool value)
	{
		connected = value;
		UpdateInventoryVisibility();
	}
	
	public void SetCurrentPlayer(int value)
	{
		if(value == 0)
		{
			currentPlayer = "PlayerS";
		}
		else
		{
			currentPlayer = "PlayerW";
		}
		
		UpdateInventoryVisibility();
	}
	
	public void CopyStringList(List<String> list, List<String> copy)
	{
		copy.Clear();
		
		foreach(String s in list)
		{
			copy.Add(String.Copy(s));
		}
	}
	
	public void UpdateData()
	{
		CopyStringList(inventoryS, resetInventoryS);
		CopyStringList(inventoryW, resetInventoryW);
		resetCurrentPlayer = currentPlayer;
	}
	
	public void Reset()
	{
		CopyStringList(resetInventoryS, inventoryS);
		CopyStringList(resetInventoryW, inventoryW);
		currentPlayer = resetCurrentPlayer;
		ShowInventory("PlayerS");
		ShowInventory("PlayerW");
		connected = true;
	}
}
