using UnityEngine;
using System.Collections;

public class playerMovementkj : MonoBehaviour {
	//public these need to be fine tuned over time
	public float movementSpeed = 10.0f;
	public float jumpForce=2.0f;
	public float airTime=0.2f;// used to determin max jump should be based upon how long the space is held / not greater then this
	//private Should not be messed with
	private Vector3 gravityCenter;
	private float moveForward = 0.0f;
	private float moveSide = 0.0f;
	private float moveUp=0.0f;
	private float timer=0.0f;
	private bool jumpcalc=false;
	private short sensitivityX = 15;
	
	// Use this for in game initialization
	void Start () 
	{
		//find gravityCenter and store into just a vector3 so we dont use memory with a game object ref
		gravityCenter = GameObject.Find("MOON").transform.position;
		
		// Make the rigid body not change rotation 
		//if (rigidbody)
			//rigidbody.freezeRotation= true;
	}
	// Update is called once per frame
	void Update () 
	{
		//(x2-x1)+(y2-y1)+(z2-z1) correct formula for a vector of moon to person(this will match the up vector correct??)
		//transform.up= new Vector3(transform.position.x-gravityCenter.x,transform.position.y-gravityCenter.y,transform.position.z-gravityCenter.z);
		//dethermin if we jumping
		
		if (jumpcalc == true ){
			//jump
			timer += Time.smoothDeltaTime;
			//whats going on in this?
			moveUp = (jumpForce * timer);
			//should we stop jumping?
			if(timer>airTime)
				jumpcalc=false;
		}
		//movement speeds 
		moveForward = movementSpeed * Time.smoothDeltaTime  * Input.GetAxis("Vertical");
		moveSide = movementSpeed  * Time.smoothDeltaTime  * Input.GetAxis("Horizontal");
		//(should check vs a max here)
		
		//rotate to match the mouse look scrip on the chartacter mesh( this is a work around so the camera does not also get reupdated based upon X mouse movement
		//transform.Rotate(0,transform.Find("playerLight").transform.eulerAngles.y,0);
		transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		//transform.RotateAround (transform.position, transform.up, transform.Find("playerLight").transform.eulerAngles.y);
		//move everything cept vertical
		//if idle?
		//animation.CrossFade("Idle");
		
		//jump up if we are
		if(jumpcalc == false)
		{
			//animation.CrossFade("Walk");
			transform.Translate(moveSide,0,moveForward);
			
		}
		else
		{
			transform.Translate (moveSide,moveUp,moveForward);
		}
	}
	void OnCollisionStay() 
	{       
		//allow for jump
		if( Input.GetKeyDown ("space") == true)
		{
			moveUp = 0.0f;
			timer = 0.0f;
			jumpcalc = true;
			//animation.CrossFade("Jump");
		}			
		
    }
	void OnCollisionEnter()
	{
		//reset 
		timer = 0.0f;
		jumpcalc = false;
	}

}