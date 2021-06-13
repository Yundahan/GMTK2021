using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
	public AudioSource[] sources = new AudioSource[2];//summer is 0, winter is 1
	
	public float switchDuration = 0.5f;
	public float baseVolume = 0.5f;
	float lastSwitch;
	int current = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        lastSwitch = -switchDuration;
    }

    // Update is called once per frame
    void Update()
    {
		float timeSinceSwitch = Time.time - lastSwitch;
		
        if(timeSinceSwitch <= switchDuration)
		{
			sources[current].volume = timeSinceSwitch;
			sources[1 - current].volume = 1 - timeSinceSwitch;
		}
    }
	
	public void SwitchTracks()
	{
		current = 1 - current;
		lastSwitch = Time.time;
	}
}
