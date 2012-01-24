using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	/* PRIVATE VARIABLES */
	private GameObject mplayer;
	
	void Start() {
		if (rigidbody)
			rigidbody.freezeRotation= true;
		
		if( !this.mplayer ) {
			/// If there is no virtual hand assigned, have a look if you find it yourself...
			this.mplayer = GameObject.Find("Character");	
			Debug.Log("Automatically Found Character");
			
			/// Nothing found I'm afraid...
			if( !this.mplayer ) {
				Debug.Log("Without a player you're unable to pick up something I'm afraid...");
			}	
		}
	}
	
	/// How's there ?
	void OnTriggerEnter( Collider who ) {
		/// Only the player is welcome !
		if( who.tag == "Player" ) {
			//this.rigidbody.isKinematic = true;
			who.gameObject.SendMessage( "AreWeInRange", true );
			who.gameObject.SendMessage( "IdentifyYourself", this.gameObject );
		}
	}
	
	/// Goodbye !
	void OnTriggerExit( Collider who ) {
		/// He's leaving you !
		if( who.tag == "Player" ) {
			//this.rigidbody.isKinematic = false;
			who.gameObject.SendMessage( "AreWeInRange", false );
		}
		//
		//this.gameObject.SetActiveRecursively(true);
	}
}