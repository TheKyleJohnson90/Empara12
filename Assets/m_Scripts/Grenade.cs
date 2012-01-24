using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {
	
public GameObject explosion;
private double timeOut = 3.0;
	private short upgradeTimeoutStat = 0;
// start a timer for explosion

void Start () {
	Invoke("Kill", (float) timeOut);
	//explosion=(GameObject)Resources.Load("GrenadeExplosion");
}
void upgradeTimeout(short upgrade)
{
	if(upgrade > upgradeTimeoutStat)
	{
		// might need to invert this
		timeOut = timeOut - (double)(upgrade - upgradeTimeoutStat) * 0.5;
	}
}
void OnCollisionEnter( Collision collision)
{
// On collision, we need to have better bounce physics here?
// Also play a sound?
// If sticky type gernade then dont bounce?

//var contact : ContactPoint = collision.contacts[0];
//var rotation = Quaternion.FromToRotation( Vector3.up, contact.normal );
//do stuff here based upon what it hits?
///
}
	void Kill (){
		if(explosion!=null){
			// Stop emitting particles in any children
			GameObject instantiatedExplosion =(GameObject) Instantiate((GameObject) explosion,transform.position,transform.rotation);
		}
		Destroy( gameObject );
	}
//@script RequireComponent (Rigidbody)
}