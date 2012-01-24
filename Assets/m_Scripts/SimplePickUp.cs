using UnityEngine;
using System.Collections;

public class SimplePickUp : MonoBehaviour {
	/* PRIVATE VARIABLES */
	public GameObject mplayer;
	
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
		//this.Three = GameObject.Find("Machine/Three");
	}
	
	/// How's there ?
	void OnTriggerEnter( Collider who ) 
	{
		/// Only the player is welcome !
		if( who.tag == "Player" ) 
		{
			this.mplayer.gameObject.SendMessage( "AreWeInRange", true );
			this.mplayer.gameObject.SendMessage( "IdentifyYourself", this.gameObject );
		}
		if( who.tag == "Upgrade ore" ) //add if for ore object so gun cant go in ore
		{
			Debug.Log("in there");
			who.gameObject.SendMessage( "AreWeInRange", true );
			who.gameObject.SendMessage( "IdentifyYourself", this.gameObject );
		}
		if( who.tag == "Upgrade gun" ) //add if for gun object so ore cant go in gun
		{
			who.gameObject.SendMessage( "AreWeInRange", true );
			who.gameObject.SendMessage( "IdentifyYourself", this.gameObject );
		}
	}
	
	/// Goodbye !
	void OnTriggerExit( Collider who ) 
	{
		/// He's leaving you !
		if( who.tag == "Player") 
		{
			this.mplayer.gameObject.SendMessage( "AreWeInRange", false );
		}
		if( who.tag == "Upgrade gun") 
		{
			who.gameObject.SendMessage( "AreWeInRange", false );
		}
		if( who.tag == "Upgrade ore") 
		{
			who.gameObject.SendMessage( "AreWeInRange", false );
		}
		
	}
}
