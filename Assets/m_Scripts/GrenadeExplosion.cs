using UnityEngine;
using System.Collections;

public class GrenadeExplosion : MonoBehaviour {
	public GameObject explosion;
	private float explosionTime = 1.0f;
	private float explosionRadius = 5.0f;
	private float explosionPower = 2000.0f;
	private float damage = 20;
	private short upgradeRadiusStat = 0;
	private short upgradeDamageStat = 0;
	void Start(){
		//Destroy the explosion in x seconds,
		//this will give the particle system and audio enough time to finish playing
		Destroy( gameObject, explosionTime );
		//Find all nearby colliders
		//Find all nearby colliders
		Collider[] colliders  = Physics.OverlapSphere( transform.position,
		explosionRadius );
		//Apply a force to all surrounding rigid bodies.
		foreach(Collider hit in colliders ){
			hit.collider.SendMessageUpwards("ApplyDamage", damage,SendMessageOptions.DontRequireReceiver);
			if( hit.rigidbody ){
				hit.rigidbody.AddExplosionForce(  explosionPower,transform.position, explosionRadius );
			}
		}
		//If we have a particle emitter attached, emit particles for .5 seconds
		if( particleEmitter ){
			particleEmitter.emit = true;
			new WaitForSeconds( 0.5f);
			particleEmitter.emit = false;
		}
	}
	void upgradeRadius(short upgrade)
	{
		if(upgrade > upgradeRadiusStat)
		{
			explosionRadius = explosionRadius + (float)(upgrade - upgradeRadiusStat);
			upgradeRadiusStat = upgrade;
		}
	}
	void upgradeDamage(short upgrade)
	{
		if(upgrade > upgradeDamageStat)
		{
			damage = damage + (float)(upgrade - upgradeDamageStat) * 5;
			upgradeDamageStat = upgrade;
		}
	}
}