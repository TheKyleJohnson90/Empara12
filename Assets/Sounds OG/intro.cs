	#if !STARTED
	#define STARTED
using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {


	public AudioClip speech;
	// Use this for initialization
	void Start () {
	audio.PlayOneShot(speech);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
#endif