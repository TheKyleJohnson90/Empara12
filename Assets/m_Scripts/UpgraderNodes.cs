using UnityEngine;
using System.Collections;

public class UpgraderNodes : MonoBehaviour {

	
	
	public GameObject ore;
	public bool withinRange;
	public bool pickedUp;
	public bool taken;
	public int PreviousElement;
	

	void Start()
	{
		this.taken = false;
		this.withinRange = false;
		this.pickedUp = false;
		this.PreviousElement = 0;
		
	}
	void Update () 
	{
		if( this.withinRange ) 
		{
			SendMessageUpwards("SendType");
			AddPart();	
			if(taken == true)
			{
				this.ore.transform.position = transform.position;
			}
		}
		
	}
	public void FirstElement(int element)
	{
		this.PreviousElement  = element;
	}
	
	public void AreWeInRange( bool withinRange ) 
	{
		this.withinRange = withinRange;
		if( withinRange ) 
				Debug.Log("in range");
				
	}
	
	public void IdentifyYourself( GameObject ore ) 
	{
		this.ore = ore;
	}
	
	void AddPart()
	{
		if(taken == false && (
				this.tag == "Upgrade ore" && 
				(this.ore.tag == "Element A" 
				|| this.ore.tag == "Element B"
				|| this.ore.tag == "Element C"
				|| this.ore.tag == "Element D"
				|| this.ore.tag == "Element E")))
		{
			int element = 0;
			
			if(this.ore.tag == "Element A")
			{
				element = 1;
			}
			else if(this.ore.tag == "Element B")
			{
				element = 2;
			}
			else if(this.ore.tag == "Element C")
			{
				element = 3;
			}
			else if(this.ore.tag == "Element D")
			{
				element = 4;
			}
			else if(this.ore.tag == "Element E")
			{
				element = 5;
			}
			if(this.PreviousElement == element || this.PreviousElement == 0)
			{
				if(this.ore.rigidbody) 
				{
					this.ore.rigidbody.isKinematic = true;
				}
				
				this.PreviousElement = element;
				this.ore.transform.position = transform.position;
				this.ore.transform.parent = transform;
				this.withinRange = false;		
				this.pickedUp = true;
		
			
				//make sure its one of the elements 
				if (element != 0)
				{
					
					SendMessageUpwards("AddElement", element);
					taken = true;
				}
			}
		}//if its the gun node and whats being picked up isnt an element pick it up
		else if(this.tag == "Upgrade gun" && 
				(this.ore.tag != "Element A" 
				|| this.ore.tag != "Element B"
				|| this.ore.tag != "Element C"
				|| this.ore.tag != "Element D"
				|| this.ore.tag != "Element E"))
		{
			if(this.ore.rigidbody) 
			{
				this.ore.rigidbody.isKinematic = true;
			}
			this.ore.transform.position = transform.position;
			this.ore.transform.parent = transform;
			this.withinRange = false;		
			this.pickedUp = true;
			//this might not work since i think it needs to be sent up and i think its only being sent down not sure need to bug test
			SendMessageUpwards("AddWeapon", this.ore);			
		}
	}
	
	void DestroyElements()
	{
		taken = false;
		this.ore.transform.parent = null;
		Destroy(this.ore);
	}
}