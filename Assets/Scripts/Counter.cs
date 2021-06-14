using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
	public CameraBehaviour camerab;
	public TileMovement playerS;
	
	public int dccounterStart = 30;//disconnect counter
	public int counterStart = 4;//normal switch counter
	
	bool connected = true;
	
	Vector2Int startPos;
	
	int counter;
	
	Vector2Int resetStartPos;
	int resetCounter;
	
	Text text;
	
    // Start is called before the first frame update
    void Start()
    {
		startPos = playerS.GetIntPos();
		counter = counterStart;
        text = (Text)gameObject.GetComponent<Text>();
		text.enabled = true;
		UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
		if(counter == 0 && connected)
		{
			camerab.SwitchCamera();
			startPos = playerS.GetIntPos();
			counter = counterStart;
		}
		
        text.text = counter.ToString();
    }
	
	public void ActivateDCCounter()
	{
		if(connected)
		{
			text.color = Color.red;
			counter = dccounterStart;
			connected = false;
		}
		else
		{
			counter--;
		}
	}
	
	public void EndDCCounter()
	{
		text.color = Color.black;
		connected = true;
		counter = counterStart;
		startPos = playerS.GetIntPos();
	}
	
	public void UpdateCounter()
	{
		Vector2Int intPos = playerS.GetIntPos();
		counter = counterStart - Math.Abs(intPos.x - startPos.x) - Math.Abs(intPos.y - startPos.y);
	}
	
	public int GetCounter()
	{
		return counter;
	}
	
	public void UpdateData()
	{
		resetCounter = counter;
		resetStartPos = startPos;
	}
	
	public void Reset()
	{
		startPos = resetStartPos;
		counter = resetCounter;
		connected = true;
	}
}
