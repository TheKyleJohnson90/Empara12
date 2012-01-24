using UnityEngine;
using System.Collections;

public class AnimatedDamageReceiver : MonoBehaviour {
private Transform explosion;// blood effect?
private Rigidbody deadReplacement ; // this may need to be a list of minerals
	
private double hitPoints = 100.0;
private double detonationDelay = 0.0;
	void Start()
	{
		//need to match detonationDelay to animation sequence
			switch(this.gameObject.tag)
		{
			case "Player":
				detonationDelay=0.0f;
				SendMessage("UpdateHealth", hitPoints);
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
		//SendMessage("UpdateHealth", hitPoints);
		
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
		//throw away ??
		Detonate();
	}
	void Detonate ()
	{
		// Destroy ourselves
		if(true)//
		Destroy(gameObject);
		// Create the explosion
		if (explosion)
			Instantiate (explosion, transform.position, transform.rotation);


		if (deadReplacement)
		{
			Rigidbody dead =(Rigidbody) Instantiate(deadReplacement, transform.position,
			transform.rotation);
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
//@script RequireComponent (Rigidbody)
}