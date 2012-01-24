using UnityEngine;
using System.Collections;

public class SphereGravity : MonoBehaviour {
	//so far to adjust the rate of gravity use this float(but we can have this  be set on start based up scale of asteriod?)
	
	public float movementFriction = 1.0f;
	public bool grounded=false;
	
	private Vector3 gravityCenter ;
	private float minVelocity=-0.5f;
	private float maxVelocity=0.5f;
	private float mainGravity = 12.0f;//m/s
	private RaycastHit hit = new RaycastHit();
	private RaycastHit[] hits;
	// Use this for initialization
	void Start () {
		//find asteriod
		//GameObject moon = GameObject.Find("MOON");
		//translate center to be moons center
		gravityCenter=GameObject.Find("MOON").transform.position;
		
		if(rigidbody)
		{
			
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ|
				RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezePositionZ;
			rigidbody.useGravity = false;
			rigidbody.drag = 10;
		}
		//mainGravity=??;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.up= new Vector3(transform.position.x-gravityCenter.x,transform.position.y-gravityCenter.y,transform.position.z-gravityCenter.z);
			
		
		Vector3 goingAroundTheMoon = transform.position - gravityCenter;
		
		if(Physics.Raycast(transform.position, -transform.up,out hit, 400))
		{			
			if(hit.transform.tag == "world")
			{
				HitWorld(goingAroundTheMoon);
			}
			else
			{
				DidntHitWorld(goingAroundTheMoon);				
			}
		}
		
		
		//else{} //TODO: have some sort of slope check?  this should get rid of unwanted sliding?
		
	}
	void HitWorld(Vector3 goingAroundTheMoon)
	{
		goingAroundTheMoon = hit.normal;
		Quaternion rotation = Quaternion.LookRotation(transform.forward , goingAroundTheMoon);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime); 
		if(!grounded)
		{			
			transform.position -= transform.up*(Time.smoothDeltaTime*mainGravity)*movementFriction;			
		}		
		if(GetComponent<CapsuleCollider>())
		{
			CapsuleCollider collider;
			collider = GetComponent<CapsuleCollider>();	
			if(hit.distance   < collider.height/2)
			{
				//makes it so the object cant go bellow the ground
				transform.position +=  transform.up*((collider.height/2) - hit.distance);
			}					
		}
		else if(GetComponent<BoxCollider>())
		{
			BoxCollider collider;
			collider = GetComponent<BoxCollider>();
			if(hit.distance   < collider.size.y/2)
			{
					//makes it so the object cant go bellow the ground
				transform.position +=  transform.up*((collider.size.y/2) - hit.distance);
			}		
		}	
	}
	void DidntHitWorld(Vector3 goingAroundTheMoon)
	{
		hits = Physics.RaycastAll(transform.position, -transform.up,400);
		for(int ii = 0; ii < hits.Length; ii++)
		{
			if(hits[ii].transform.tag == "world")
			{
				goingAroundTheMoon = hits[ii].normal;
				break;
			}
		}			
		//lookingAtTarget = target - transform.position;		
		Quaternion rotation = Quaternion.LookRotation(transform.forward , goingAroundTheMoon);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime); 
		//this should check if we havnt already collied with the moon to save computation
		if(!grounded)
		{			
			transform.position -= transform.up*(Time.smoothDeltaTime*mainGravity)*movementFriction;			
		}		
		if(GetComponent<CapsuleCollider>())
		{
			CapsuleCollider collider;
			collider = GetComponent<CapsuleCollider>();		
			//sends a ray down
			//RaycastHit[] hits = new RaycastHit();
			//need to change this so it still looks even if it hit an ignore 
			
			//if(Physics.RaycastAll(transform.position, -transform.up,out hits, 400))
			//{	
			for(int ii = 0; ii < hits.Length; ii++)
			{
				if(hits[ii].transform.tag == "world")
				{				
					if(hits[ii].distance   < collider.height/2)
					{
						//makes it so the object cant go bellow the ground
						transform.position +=  transform.up*((collider.height/2) - hits[ii].distance);
					}
					break;
				}
			}			
		}
		else if(GetComponent<BoxCollider>())
		{
			BoxCollider collider;
			collider = GetComponent<BoxCollider>();
		
			//sends a ray down
			//RaycastHit[] hits = new RaycastHit();
			//need to change this so it still looks even if it hit an ignore 
			
			//if(Physics.Raycast(transform.position, -transform.up,out hit, 1000))
			//{
				//transform.up = hit.normal;	
				//sees if the object is bellow the ground
			for(int ii = 0; ii < hits.Length; ii++)
			{
				if(hits[ii].transform.tag == "world")
				{		
					if(hits[ii].distance   < collider.size.y/2)
					{
						//makes it so the object cant go bellow the ground
						transform.position +=  transform.up*((collider.size.y/2) - hits[ii].distance);
					}
					break;
				}
			}
		}
	}

	//This is affecting all gameobjects, when actually it should only affect player I belive
	void OnCollisionExit(Collision collision){
	//	movementFriction=1.0f;
		grounded=false;
		
	}
	void OnCollisionEnter(Collision collision){
	//	movementFriction=0.0f;
		grounded=true;
	}
	
	}
 