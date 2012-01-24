using UnityEngine;
using System.Collections;

public class LMM : MonoBehaviour {
	
	private GameObject Gun;
	 
	private int UpgradeComponent1 = 0;
	private int UpgradeComponent2 = 0;
	private int UpgradeComponent3 = 0;
	private int UpgradeComponent4 = 0;
	private int UpgradeComponent5 = 0;
	
	private int NumberOfParts = 0;
	
	void Start()
	{
		//null everthing!?
		//maybe instatiat the collision spheres? (guessing they willl have to be children objects)
		
		
	}
	void update()
	{
		//upgrade the gun
		if( Input.GetKey( "u" ))
		{
			//check what it has in each slot 
			//determine upgrade
			BroadcastMessage("GetGun", this.Gun);
			BroadcastMessage("GetUpgradeKind", this.NumberOfParts);
			BroadcastMessage("Quality", this.UpgradeComponent1);
			
			BroadcastMessage("MakeNewGun");
			 
			//set everything equal to null
			
			DestroyElements();
		}
		//delete elements and output the gun
		if( Input.GetKey( "j" ))
		{
			BroadcastMessage("GetGun", this.Gun);
			BroadcastMessage("MakeNewGun");
			DestroyElements();
		}
		
	}
	void SendType()
	{
		BroadcastMessage("FirstElement",this.UpgradeComponent1);
	}
	void AddWeapon(GameObject weapon)
	{
		this.Gun = weapon;
	}
	void AddElement(int element)
	{
		//
		
		switch(NumberOfParts)
		{
		case 1:
			this.UpgradeComponent1 = element;
			break;
		case 2:
			this.UpgradeComponent2 = element;
			break;
		case 3:
			this.UpgradeComponent3 = element;
			break;
		case 4:
			this.UpgradeComponent4 = element;
			break;
		case 5:
			this.UpgradeComponent5 = element;
			break;
		default:
			break;
		}
		this.NumberOfParts++;
	}
	
	void DestroyElements()
	{
		BroadcastMessage("DestroyElements");
		//all gameobjects that are "pickups" within a radious of each barrel(perferable the bottom to insure the item is there)?
	}
}

