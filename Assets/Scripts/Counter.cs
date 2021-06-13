using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
	public int counterStart = 30;
	
	Text text;
	
	int counter;
	
    // Start is called before the first frame update
    void Start()
    {
		counter = counterStart;
        text = (Text)gameObject.GetComponent<Text>();
		text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ActivateCounter()
	{
		if(!text.enabled)
		{
			counter = counterStart;
			text.enabled = true;
			text.text = counter.ToString();
		}
		else
		{
			DecreaseCounter();
		}
	}
	
	public void EndCounter()
	{
		counter = counterStart;
		text.enabled = false;
	}
	
	public void DecreaseCounter()
	{
		counter--;
		text.text = counter.ToString();
	}
	
	public int GetCounter()
	{
		return counter;
	}
}
