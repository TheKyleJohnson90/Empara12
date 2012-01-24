using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
// The reference to the explosion prefab
public GameObject explosion ;
private double timeOut = 2.0;
private double damage = 10.0;
	private short upgradeTimeoutStat = 0;
	private short upgradeDamageStat = 0;
// Kill the rocket after a while automatically
void Start () {
	Invoke("Kill", (float) timeOut);
	//explosion=GameObject.Find("RocketExplosion");
}
void OnCollisionEnter (Collision collision) {
	// Instantiate explosion at the impact point and rotate the explosion
	// so that the y-axis faces along the surface normal
	ContactPoint contact = collision.contacts[0];
	Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
		if (explosion!=null)
			Instantiate (explosion, contact.point, rotation);
	//deal damage( rocket deals damage, not the explosion) should explosion also deal damage?
	collision.collider.SendMessageUpwards("ApplyDamage", damage,SendMessageOptions.DontRequireReceiver);
	// And kill our selves
	Kill ();
}
void upgradeTimeout(short upgrade)
{
	if(upgrade > upgradeTimeoutStat)
	{
		// might need to invert this
		timeOut = timeOut - (double)(upgrade - upgradeTimeoutStat) * 0.5;
	}
}
void upgradeDamage(short upgrade)
{
	if(upgrade > upgradeDamageStat)
	{
		// might need to invert this
		damage = damage + (double)(upgrade - upgradeDamageStat) * 5;
	}
}
void Kill ()
{
// Stop emitting particles in any children
ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
if (emitter)
emitter.emit = false;
// Detach children - We do this to detach the trail rendererer which
// should be set up to auto destruct
transform.DetachChildren();
// Destroy the projectile
Destroy(gameObject);
}
//@script RequireComponent (Rigidbody)
}
