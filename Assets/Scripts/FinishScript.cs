using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
	public TileMovement playerS;
	public TileMovement playerW;
	public Text text;
	
    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerS.GetIntPos() == new Vector2Int(-6, 98))
		{
			text.enabled = true;
			playerW.SetRooted(true);
			playerS.SetRooted(true);
		}
    }
}
