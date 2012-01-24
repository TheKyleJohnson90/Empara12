using UnityEngine;
using System.Collections;

public class EnemyGravity : MonoBehaviour {
	private Vector3 gravityCenter;
	private float mainGravity = 4.5f;
	private float  movementFriction = 1.0f;
	
	// Use this for initialization
	void Start () 
	{
		gravityCenter = GameObject.Find("MOON").transform.position;

	}
	
	// Update is called once per frame
	void Update () 
	{
		

			//vector from position to center of asteriod
			//interpolate position in a vector towords the orign at the rate of gravity by time?
			Vector3 targetPosition = new Vector3(gravityCenter.x-transform.position.x,gravityCenter.y-transform.position.y,gravityCenter.z-transform.position.z).normalized;
			//shall we keep its orentation?
			//keep it unit based
			//and apply interpolation to new position
			transform.position += targetPosition*(Time.smoothDeltaTime*mainGravity)*movementFriction;
			//TODO: have a max fall rate? this would help avoid collision errors i think and make it smoother
			

	
		if(GetComponent<BoxCollider>())
		{
			BoxCollider collider;
			collider = GetComponent<BoxCollider>();
		
			//sends a ray down
			RaycastHit hit = new RaycastHit();
			//need to change this so it still looks even if it hit an ignore 
			
			if(Physics.Raycast(transform.position, -transform.up,out hit, 1000))
			{
				
				//sees if the object is bellow the ground
				if(hit.distance   < collider.size.y/2)
				{
					//makes it so the object cant go bellow the ground
					transform.position +=  transform.up*((collider.size.y/2) - hit.distance);
				}
			}
		}
	}

	void OnCollisionExit(Collision collision)
	{
	//	movementFriction=1.0f;
		movementFriction = 1.0f;
		
	}
	void OnCollisionEnter(Collision collision)
	{
	//	movementFriction=0.0f;
		movementFriction = 0.1f;
		
	}
}