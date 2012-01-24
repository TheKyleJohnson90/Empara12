using UnityEngine;
using System.Collections;

public class GrenadeLauncher : MonoBehaviour{
	public Rigidbody projectile;
	public AudioClip grenade;
	
	private short ammo = 20;
	private short upgradeAmmoStat = 0;
	private float forwardSpeed = 17;
	private short upgradeSpeedStat = 0;
	private float ySpeed = 2;
			
	void Toss(){
		if( Input.GetButtonDown( "Fire2" )&&(projectile!=null) && ammo > 0){
			switch(this.transform.tag)
			{
				case "Grenade Launcher":
					audio.PlayOneShot(grenade);
					break;
				default:
					break;
			}
			Rigidbody instantiatedProjectile = Instantiate( projectile, transform.position, transform.rotation) as Rigidbody;
			if(instantiatedProjectile!=null){
				instantiatedProjectile.velocity = transform.TransformDirection( new Vector3( 0, ySpeed, forwardSpeed) );
				//all colliders?
				for (var i=0;i<transform.childCount;i++){
					Physics.IgnoreCollision( instantiatedProjectile.collider,transform.GetChild(i).gameObject.transform.collider );
				}
			}
			ammo--;
		}
	}
	void upgradeSpeed(short upgrade)
	{
		if(upgrade > upgradeSpeedStat)
		{
			// might need to invert this
			forwardSpeed = forwardSpeed + (float)(upgrade - upgradeSpeedStat) * 2;
			upgradeSpeedStat = upgrade;
		}
	}
	void upgradeAmmo(short upgrade)
	{
		if(upgrade > upgradeAmmoStat)
		{
			// might need to invert this
			ammo = (short)(ammo + (upgrade - upgradeAmmoStat) * 2);
		}
	}
}