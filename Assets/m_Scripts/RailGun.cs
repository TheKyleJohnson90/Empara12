using UnityEngine;
using System.Collections;

public class RailGun : MonoBehaviour 
{
	//These are public until I can make a load defaults
	
	public float fireRate = 0.05f;
	
	public float range = 100.0f; //upgrade stats
	public float force = 10.0f; //upgrade stats
	public float damage = 5.0f; //upgrade stats
	public int ammoPerClip = 40; //upgrade stats
	public int clips = 25; //upgrade stats
	
	public short upgradeStat = 0;
	
	public short currentRangeUpgrade = 0;
	public short currentForceUpgrade = 0;
	public short currentDamageUpgrade = 0;
	public short currentAmmoPerClipUpgrade = 0;	
	
	public AudioClip railGun;
	public AudioClip laser;	
	public AudioClip pistol;
	public AudioClip plasma;
	
	public float reloadTime = 0.5f;
	public ParticleEmitter hitParticles ;//=GetComponent<ParticleEmitter>("RailGunExplosion");
	public Renderer muzzleFlash;
	private int ammoLeft  = 0;
	private double nextFireTime = 0.0;
	private double m_LastFrameShot = -1;
	void Start ()
	{
		ParticleEmitter hitParticles = GetComponentInChildren<ParticleEmitter>();
		// 	We don't want to emit particles all the time, only when we hit something.
		if (hitParticles)

		hitParticles.emit = false;
		ammoLeft = ammoPerClip;
	}

	void LateUpdate()
	{
		if (muzzleFlash)
		{
			// We shot this frame, enable the muzzle flash
			if (m_LastFrameShot == Time.frameCount)
			{
				muzzleFlash.transform.localRotation =	Quaternion.AngleAxis(Random.Range(0, 359), Vector3.forward);
				muzzleFlash.enabled = true;
				if (audio)
				{
					if (!audio.isPlaying)
						audio.Play();
					audio.loop = true;
				}
			}
			// We didn't, disable the muzzle flash
			else
			{
				muzzleFlash.enabled = false;
				enabled = false;
				// Play sound
				if (audio)
				{
					audio.loop = false;
				}
			}
		}
	}
	void Fire ()
	{
		if (ammoLeft == 0)
			return;
		// If there is more than one bullet between the last and this frame
		// Reset the nextFireTime
		if (Time.time - fireRate > nextFireTime)
			nextFireTime = Time.time - Time.deltaTime;
		// Keep firing until we used up the fire time
		while( nextFireTime < Time.time && ammoLeft != 0)
		{
			FireOneShot();
			nextFireTime += fireRate;
		}
	}
	void FireOneShot ()
	{
		switch(this.transform.tag)
		{
			case "Rail Gun":
				audio.PlayOneShot(railGun);
				break;
			case "Pistol":
				audio.PlayOneShot(pistol);
				break;
			case "Shot Gun":
				audio.PlayOneShot(pistol);
				break;
			case "Plasma Ray":
				audio.PlayOneShot(plasma);
				break;
			case "Laser Gun":
				audio.PlayOneShot(laser);
				break;
			case "Large Rail Gun":
				audio.PlayOneShot(railGun);
				break;
			default:
				break;
		}
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		// Did we hit anything?
		if (Physics.Raycast (transform.position, direction,out hit,(float) range))
		{
			// Apply a force to the rigidbody we hit
			if (hit.rigidbody)
				hit.rigidbody.AddForceAtPosition((float)force * direction, hit.point);
			// Place the particle system for spawing out of place where we hit the surface!
			// And spawn a couple of particles
			if (hitParticles)
			{
				hitParticles.transform.position = hit.point;
				hitParticles.transform.rotation =
					Quaternion.FromToRotation(Vector3.up, hit.normal);
				hitParticles.Emit();
			}
			// Send a damage message to the hit object
			hit.collider.SendMessageUpwards("ApplyDamage", damage,
				SendMessageOptions.DontRequireReceiver);
		}
		ammoLeft--;
		// Register that we shot this frame,
		// so that the LateUpdate function enabled the muzzleflash renderer for one frame
		m_LastFrameShot = Time.frameCount;
		enabled = true;
		// Reload gun in reload Time
		if (ammoLeft == 0)
			Reload();
	}
	void Reload () 
	{
		// Wait for reload time first - then add more ammo!
		new WaitForSeconds((float)reloadTime);
		// We have a clip left reload
		if (clips > 0)
		{
			clips--;
			ammoLeft = ammoPerClip;
		}
	}
	double GetammoLeft () 
	{
		return ammoLeft;
	}
	
	void upgrade(short Quality)
	{
		this.upgradeStat = Quality;
	}
	void setStats(int whatToGet)
	{
		switch(whatToGet)
		{
			case 1:				
				 this.clips +=  this.upgradeStat;				
				break;
			case 2:
				 this.ammoPerClip += (int)this.upgradeStat - this.currentAmmoPerClipUpgrade * 5;
				this.currentAmmoPerClipUpgrade = this.upgradeStat;
				break;
			case 3:
				 this.range += (float)this.upgradeStat - (float)this.currentRangeUpgrade * 20;
				this.currentRangeUpgrade = this.upgradeStat;
				break;
			case 4:
				 this.force += (float)this.upgradeStat - (float)this.currentForceUpgrade * 2;
				this.currentForceUpgrade = this.upgradeStat;
				break;
			case 5:
				 this.damage += (float)this.upgradeStat - (float)this.currentDamageUpgrade * 2;
				this.currentDamageUpgrade = this.upgradeStat;
				break;
			default:
				break;
		}
	}
}