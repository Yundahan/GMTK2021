using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : MonoBehaviour
{	
	public SpriteRenderer sr;
	public TileMovement playerS;
	public TileMovement playerW;
	public Sprite deadTree;
	public Occupies ocp;
	
    // Start is called before the first frame update
    void Start()
    {	
		sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void InteractionTree()
	{
		sr.sprite = deadTree;
		
	}
	
	/*bool IsInArray(Vector2Int a, Vector2Int[] b)
	{
		foreach(Vector2Int c in b)
		{
			if(c==a){
				return true;
			}
		}
		return false;
	}*/
	
	bool TargetAccessible()
	{
		foreach(Vector2Int position in ocp.GetOccupiedPositions())
		{
			if(position == new Vector2Int(playerS.GetIntPos().x + playerS.GetDirection().x, playerS.GetIntPos().y + playerS.GetDirection().y))
			{
				return true;
			}
			
			if(position == new Vector2Int(playerW.GetIntPos().x + playerW.GetDirection().x, playerW.GetIntPos().y + playerW.GetDirection().y))
			{
				return true;
			}
		}
		
		return false;
	}
		// Update is called once per frame
    void Update()
    {
		
		if(Input.GetKeyDown(KeyCode.E)) //if E is pressed check whether interaction is possible then do it.	
		{ 
			if(TargetAccessible())
			{
				InteractionTree();
			}
		}
    }
}
