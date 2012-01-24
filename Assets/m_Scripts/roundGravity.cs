using UnityEngine;
using System.Collections;

public class roundGravity : MonoBehaviour {

	public float mainGravity = -3.0f;
	public float mainFriction = 1;
	public float terminalVelocity = 0.5f;
	public float timer = 0.0f;
	public bool fallCalc = true;
	public Vector3 angleToCenter = new Vector3(0.0f,0.0f,0.0f);
	public Vector3 displacement = new Vector3(0.0f,0.0f,0.0f);
	public Vector3 disTraveled = new Vector3(0.0f,0.0f,0.0f);
	public Vector3 gravityCenter= Vector3.zero;
	// Use this for initialization
	void Start () {
		//GameObject moon= GameObject.Find("TestSphere");
		gravityCenter=GameObject.Find("MOON").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(fallCalc == true)
		{
			displacement.x = transform.position.x - gravityCenter.x;
		displacement.y = transform.position.y - gravityCenter.y;
		displacement.z = transform.position.z - gravityCenter.z;
		
			//find angle to center
			float displacementXY = Mathf.Sqrt((displacement.x * displacement.x) + (displacement.y * displacement.y));
			float displacementYZ = Mathf.Sqrt((displacement.z * displacement.z) + (displacement.y * displacement.y));
			float displacementXZ = Mathf.Sqrt((displacement.x * displacement.x) + (displacement.z * displacement.z));
		
			angleToCenter.z = Mathf.Atan(displacement.z / displacementXY)* 57.2957795f;
			angleToCenter.y = Mathf.Atan(displacement.y / displacementXZ)* 57.2957795f;
			angleToCenter.x = Mathf.Atan(displacement.x / displacementYZ)* 57.2957795f;
		
			//move down
			//make it use force and if force does not excede friction then keep the same position 
			//displacement.Normalize ();
		
			timer += Time.smoothDeltaTime;
			disTraveled = new Vector3(
			0.5f  * mainGravity * timer * timer, 
			0.5f  * mainGravity * timer * timer,
			0.5f  * mainGravity * timer * timer);
			if(Mathf.Abs(disTraveled.x) > terminalVelocity)
			{
				disTraveled.x = disTraveled.normalized.x * terminalVelocity; 
			}
			if(Mathf.Abs(disTraveled.y) > terminalVelocity)
			{
				disTraveled.y = disTraveled.normalized.y * terminalVelocity; 
			}
			if(Mathf.Abs(disTraveled.z) > terminalVelocity)
			{
				disTraveled.z = disTraveled.normalized.z * terminalVelocity; 
			}
			transform.position += new Vector3(
			disTraveled.x * (displacement.x/90) ,
			disTraveled.y * (displacement.y/90) ,
			disTraveled.z * (displacement.z/90) );
		}
	}
	void OnCollisionStay() 
	{
		fallCalc = false;
		timer = 0.0f;
		mainFriction = 0.1f;
	}
	void OnCollisionExit() 
	{  
		fallCalc = true;		
		timer = 0.0f;
		mainFriction = 1.0f;
    }
	void OnCollisionEnter() 
	{
		fallCalc = false;
		timer = 0.0f;
		mainFriction = 0.1f;
    }
	  
}
