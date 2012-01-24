using UnityEngine;
using System.Collections;

public class RocketExplosion : MonoBehaviour {
// The reference to the explosion prefab
public GameObject explosion;
public float timeOut= 3.0f;
// Kill the rocket after a while automatically
void  Start (){
	Invoke("Kill", timeOut);
}
void  OnCollisionEnter ( Collision collision  ){
// Instantiate explosion at the impact point and rotate the explosion
// so that the y-axis faces along the surface normal
ContactPoint contact = collision.contacts[0];
Quaternion rotation= Quaternion.FromToRotation(Vector3.up, contact.normal);
Instantiate (explosion, contact.point, rotation);
// And kill our selves
Kill ();
}
void  Kill (){
// Stop emitting particles in any children
ParticleEmitter emitter= GetComponentInChildren<ParticleEmitter>();
if (emitter)
emitter.emit = false;
// Detach children - We do this to detach the trail rendererer which
// should be set up to auto destruct
transform.DetachChildren();

// Destroy the projectile
Destroy(gameObject);
}

}