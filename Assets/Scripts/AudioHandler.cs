using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
	public AudioSource[] sources = new AudioSource[2];//summer is 0, winter is 1
	public AudioSource[] sfx = new AudioSource[8];//0 is death, 1 is disconnect, 2 is reconnect, 3 is cutting, 4 is pickaxe, 5 is melting, 7 and 8 are steps
	
	public float switchDuration = 0.5f;
	public float baseVolume = 0.3f;
	float lastSwitch;
	int current = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        lastSwitch = -switchDuration;
		
		foreach(AudioSource s in sfx)
		{
			s.loop = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
		float timeSinceSwitch = Time.time - lastSwitch;
		
        if(timeSinceSwitch <= switchDuration)
		{
			sources[current].volume = timeSinceSwitch;
			sources[1 - current].volume = switchDuration - timeSinceSwitch;
		}
    }
	
	public void SwitchTracks()
	{
		current = 1 - current;
		lastSwitch = Time.time;
	}
	
	public void Reset(int value)
	{
		if(value != current)
		{
			SwitchTracks();
		}
	}
	
	public void PlayClip(int clipNumber)
	{
		sfx[clipNumber].Play();
	}
}
