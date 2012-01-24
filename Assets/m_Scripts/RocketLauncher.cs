using UnityEngine;
using System.Collections;

public class RocketLauncher: MonoBehaviour {
public Rigidbody projectile;
public AudioClip rocket;
	
private double initialSpeed= 20.0f;
private double reloadTime= 0.7f;
private int ammoCount= 20;
private short upgradeInitialSpeedStat = 0;
private short upgradeReloadTimeStat = 0;
private short upgradeAmmoCountStat = 0;
	
private double lastShot= -10.0f;
void  Fire (){
	// Did the time exceed the reload time?
	if (Time.time > reloadTime + lastShot && ammoCount > 0){
		switch(this.transform.tag)
		{
			case "Rocket Launcher":
				audio.PlayOneShot(rocket);
				break;
			case "RPG":
				audio.PlayOneShot(rocket);
				break;		
			default:
				break;
		}
		// create a new projectile, use the same position and rotation as the Launcher.
		if (projectile!=null){
			Rigidbody instantiatedProjectile = (Rigidbody) Instantiate (projectile,transform.position+transform.TransformDirection(0,0,5), transform.rotation);
			// Give it an initial forward velocity. The direction is along the z-axis of
			// the missile launcher's transform.
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0.0f, 0.0f,(float) initialSpeed  ));
			// Ignore collisions between the missile and the character controller
			Physics.IgnoreCollision(instantiatedProjectile.collider, transform.root.collider);
		}
		lastShot = Time.time;
		ammoCount--;
	}
}
void upgradeInitialSpeed(short upgrade)
{
	if(upgrade > upgradeInitialSpeedStat)
	{
		
		initialSpeed = initialSpeed + (double)(upgrade - upgradeInitialSpeedStat) * 2;
	}
}
void upgradeReloadTime(short upgrade)
{
	if(upgrade > upgradeReloadTimeStat)
	{
		
		reloadTime = reloadTime - (double)(upgrade - upgradeReloadTimeStat) * 0.1;
	}
}
void upgradeAmmoCount(short upgrade)
{
	if(upgrade > upgradeAmmoCountStat)
	{
		
		ammoCount = ammoCount + (int)(upgrade - upgradeAmmoCountStat) * 5;
	}
}
}