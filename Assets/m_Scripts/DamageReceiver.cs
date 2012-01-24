using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour {
private Transform explosion;
private Rigidbody deadReplacement ;
	
private double hitPoints = 100.0;
private double detonationDelay = 0.0;
	void Start()
	{
		//
			switch(this.gameObject.tag)
		{
			case "Player":
				detonationDelay=0.0f;
				//SendMessage("UpdateHealth", hitPoints);
				break;
			case "Enemy1":
				detonationDelay=0.0;
				break;
			case "Enemy":
				detonationDelay=0.0;
				break;
			case " ":
				detonationDelay=0.0;
				break;
			default:
				detonationDelay=0.0;
				break;
		}
	}
	void ApplyDamage ( float damage)
	{
	// We already have less than 0 hitpoints, maybe we got killed already?
		if (hitPoints <= 0.0)
			return;
		
		hitPoints -= damage;
		SendMessage("UpdateHealth", hitPoints);
		if (hitPoints <= 0.0){
			// Start emitting particles
			ParticleEmitter emitter  = GetComponentInChildren<ParticleEmitter>();
			if (emitter)
				emitter.emit = true;
			Invoke("DelayedDetonate", (float)detonationDelay);
		}
	}
	void SetDelay(float delayTime)
	{
		detonationDelay=delayTime;
	}

	void DelayedDetonate ()
	{
		//BroadcastMessage ("Detonate");
		Detonate();
	}
	void Detonate ()
	{
// Destroy ourselves
Destroy(gameObject);
// Create the explosion
if (explosion)
Instantiate (explosion, transform.position, transform.rotation);

// If we have a dead barrel then replace ourselves with it!
if (deadReplacement)
{
Rigidbody dead =(Rigidbody) Instantiate(deadReplacement, transform.position,
transform.rotation);
// For better effect we assign the same velocity to the exploded barrel
dead.rigidbody.velocity = rigidbody.velocity;
dead.angularVelocity = rigidbody.angularVelocity;
}

// If there is a particle emitter stop emitting and detach so it doesnt get destroyed
// right away
ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();
if (emitter)
{
emitter.emit = false;
emitter.transform.parent = null;
}
}
// We require the barrel to be a rigidbody, so that it can do nice physics
//@script RequireComponent (Rigidbody)
}