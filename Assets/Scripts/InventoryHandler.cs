using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
	public GameObject canvas;
	
	List<String> inventoryS = new List<String>();
	List<String> inventoryW = new List<String>();
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void GiveItem(String playerName, String itemName, Sprite itemSprite)
	{
		Debug.Log("give " + itemName + " to " + playerName);
		
		if(itemName == "endItem")
		{
			Debug.Log("finish");
			//finish the game here
		}
		
		if(playerName == "PlayerS")
		{
			inventoryS.Add(itemName);
			VisualizeItem(itemSprite);
		}
		else if(playerName == "PlayerW")
		{
			inventoryW.Add(itemName);
			VisualizeItem(itemSprite);
		}
	}
	
	void VisualizeItem(Sprite itemSprite)
	{
		Debug.Log("jo");
		GameObject obj = new GameObject();
		Image img = obj.AddComponent<Image>();
		img.sprite = itemSprite;
		obj.GetComponent<RectTransform>().SetParent(canvas.transform);
		obj.SetActive(true);
		Debug.Log(obj.GetComponent<RectTransform>().transform.position);
		obj.GetComponent<RectTransform>().transform.position = new Vector3(-200f, 100f, 0f);
		obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.6f);
		obj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.6f);
	}
}
