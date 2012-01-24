using UnityEngine;
using System.Collections;

public class MakeNode : MonoBehaviour {

	private GameObject NewGun;
	private int UpgradeKind;
	private int QualityStat;
	private float upgraded;
	
	
	void Start () 
	{
		NewGun = new GameObject();
		UpgradeKind = 0;
		QualityStat = 0;
		upgraded = 0.0f;
	
	}
	void Update () 
	{
	
	}
	void GetGun(GameObject OldGun)
	{
		this.NewGun = OldGun;
	}
	void GetUpgradeKind(int WhatToUpgrade)
	{
		this.UpgradeKind = WhatToUpgrade;
	}
	void Quality(int NewValue)
	{
		this.QualityStat = NewValue;
	}
	void MakeNewGun()
	{
		//based on the tag for the gun type upgrade gun
		switch(this.NewGun.tag)
		{
			case "Rail Gun":
				RayCastGun();
				break;
			case "Pistol":
				RayCastGun();
				break;
			case "Shot Gun":
				RayCastGun();
				break;
			case "Plasma Ray":
				RayCastGun();
				break;
			case "Laser Gun":
				RayCastGun();
				break;
			case "Large Rail Gun":
				RayCastGun();
				break;
			case "Rocket Launcher":
				RocketLauncher();
				break;
			case "RPG":
				RocketLauncher();
				break;
			case "Grenade Launcher":
				GrenadeLauncher();
				break;
			default:
				break;
		}
		//dislodge the new gun from the machine
		this.NewGun.transform.parent = null;
		this.NewGun.transform.position = transform.position;
		if(this.NewGun.rigidbody) 
		{
			this.NewGun.rigidbody.isKinematic = false;
		}
	}
	void RayCastGun()
	{
		if(UpgradeKind != 0)
		{
			//make a function for each gun that sends its stats back to this so they can be added to
			this.NewGun.SendMessage("upgrade", QualityStat);
			this.NewGun.SendMessage("setStats", UpgradeKind);
		}
	}
	void RocketLauncher()
	{
		if(UpgradeKind != 0)
		{
			switch(UpgradeKind)
			{
				case 1:				
					//upgrade ammo count
					this.NewGun.SendMessage("upgradeAmmoCount",QualityStat);
					break;
				case 2:				
					//upgrade reload time 
					this.NewGun.SendMessage("upgradeReloadTime",QualityStat);
					break;
				case 3:				
					//upgrade time out  
					this.NewGun.SendMessage("upgradeTimeout",QualityStat);
					break;
				case 4:				
					//upgrade initial speed  
					this.NewGun.SendMessage("upgradeInitialSpeed",QualityStat);
					break;
				case 5:				
					//upgrade damage							
					this.NewGun.SendMessage("upgradeDamage",QualityStat);
					break;
				default:
					break;
			}
		}
	}
	void GrenadeLauncher()
	{
		if(UpgradeKind != 0)
		{
			switch(UpgradeKind)
			{
				case 1:				
					//upgrade ammo 
					this.NewGun.SendMessage("upgradeAmmo",QualityStat);
				//need ammo variables 
					break;
				case 2:				
					//upgrade time out 
					this.NewGun.SendMessage("upgradeTimeout",QualityStat);
					break;
				case 3:				
					//upgrade speed 
					this.NewGun.SendMessage("upgradeSpeed",QualityStat);
					break;
				case 4:				
					//upgrade radius 
					this.NewGun.SendMessage("upgradeRadius",QualityStat);
					break;
				case 5:				
					//upgrade damage							
				this.NewGun.SendMessage("upgradeDamage",QualityStat);
					break;
				default:
					break;
			}			
		}
	}
}
