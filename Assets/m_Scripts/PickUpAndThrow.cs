using UnityEngine;
using System.Collections;

public class PickUpAndThrow : MonoBehaviour {

	public GameObject virtualHand;
	
	/* PRIVATE VARIABLES */
	private GameObject ourObject;
	private bool withinRange;
	private bool pickedUp;

	/// Use this for initialization
	void Start () {		
		/// Do you have a virtual hand ?
		if( !this.virtualHand ) {
			/// If there is no virtual hand assigned, have a look if you find it yourself...
			this.virtualHand = GameObject.Find("rightHand");
		
			/// Nothing found I'm afraid...
			if( !this.virtualHand ) {
				Debug.Log("Without a hand you're unable to pick up something I'm afraid...");
			}	
		}
		this.withinRange = false;
		this.pickedUp = false;
	}
	
	/// Did some object send a message ?
	public void AreWeInRange( bool inRange ) {
		if(withinRange!=inRange){
			withinRange = inRange;
			if( withinRange ) 
				Debug.Log("You can grab it");
			else
				Debug.Log("To far to away pick Up");
		
		}
				
	}
	
	/// Which object can we pick up ?
	public void IdentifyYourself( GameObject ourObject ) {
		this.ourObject = ourObject;
	}
	

	public void GrabObject() {
		if(	withinRange ){
			Debug.Log("You grabbed it");
			if(this.ourObject.rigidbody) {
				this.ourObject.rigidbody.isKinematic = true;
			}
			
			if(this.virtualHand) {
				this.ourObject.transform.position = this.virtualHand.transform.position;
				this.ourObject.transform.parent = this.virtualHand.transform;
				this.withinRange = false;		
				this.pickedUp = true;
				this.ourObject.GetComponent<SphereGravity>().enabled=false;
				//Put it in Inventory, Player can select it later
				//BroadcastMessage("SelectItem",1f);
			}
		}
	}
	
	// is now more of a drop object function
	public void ThrowObject(int index) {
		if(pickedUp){
			Debug.Log("You tossed it");
			ourObject=virtualHand.transform.GetChild(index).gameObject;
			this.ourObject.transform.parent = null;
			this.pickedUp = false;	
			//move away from player first (like just in front of yourself)
			this.ourObject.transform.position += transform.TransformDirection(0, 0, 5);
			//this needs to ingone both collisions
			//Physics.IgnoreCollision(this.ourObject.collider, transform.root.collider);
			this.ourObject.GetComponent<SphereGravity>().enabled=true;
			//make it have collider
			if(this.ourObject.rigidbody) {
				this.ourObject.rigidbody.isKinematic = false;
			}

		}
	}
}
