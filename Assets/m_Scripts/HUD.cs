using UnityEngine;
using System.Collections;
//NOTICE all textures are currently public, should be changed to private and loaded upon start fucntion
public class HUD : MonoBehaviour {
	//screen size so we scale it all correctly
	private float sw;
	private float sh;
	//Inventory 
	private Rect inventoryPosition;
	public Texture2D inventoryBGTexture;
	public int items;
	private Texture2D[] inventoryTextures = new Texture2D[9]; 
	public Texture2D[] itemTextures = new Texture2D[13]; //5 minerals +8 guns+??
	//Ammo Amount
	private float ammoPosX=10;
	private float ammoPosY=10;
	private GUIText ammoText ;
	//Shield
	private Rect shieldPosition;
	public Texture2D shieldTexture;
	//Health
	private Rect healthPosition;
	public Texture2D healthTexture ;
	//Crosshair
	public Texture2D crosshairTexture;
	public Texture2D[] crosshairTextures = new Texture2D[8]; 
	private Rect crosshairPosition;
	
	//Effects(White screen/ fade/ blood effects.. )(win /lose/quit screen) 
	private Texture2D overlayTexture ;
	
	// These we need to also scale to screen size !!! aghhg~!
	private float health; //px of image bg
	private float shield; //px og imagebg
	//
	public const int maxItemCount=9;
	private int SelectedItem=-1;
	private GameObject virtualHand;
	//
	
	void Start()
	{
		if( !virtualHand ) {
			/// If there is no virtual hand assigned, have a look if you find it yourself...
			virtualHand = GameObject.Find("rightHand");
		
			/// Nothing found I'm afraid...
			if( !virtualHand ) {
				Debug.Log("Without a hand you're unable to pick up something I'm afraid...");
			}	
			SelectItem(0);
			
			
		}
		
		//save them so we can update it, if changed in a pause menu?
		sw=Screen.width;
		sh=Screen.height;
		//allways middle of screen(almost)
		crosshairPosition =new Rect( (sw / 2)-((sw/20)/2),  (sh / 2)-((sh/20)/2), sw/20, sh/20);
		//intial values full on screen
		inventoryPosition =new Rect( 0, (sh/10)*7, sw/3, sh/3);
		
		//only shortcuts on screen
		//align to bg
		healthPosition =new Rect((sw/3)/10,inventoryPosition.y+((sh/3)*0.075f),100,((sh/3)/8f));
		//insure they both use same Y position/ and Height
		shieldPosition =new Rect((sw/3)/2,healthPosition.y, 25,healthPosition.height) ;
		//overlayPosition =new Rect( ( sw - crosshairTexture.width ) / 2, ( sh - crosshairTexture.height ) / 2, crosshairTexture.width, crosshairTexture.height );
		ammoPosX=(sw/3)-0;
		ammoPosY=inventoryPosition.y;

		//
		items=virtualHand.transform.childCount;
		
	}
	void OnGUI(){
		//draw bg
		GUI.DrawTexture(inventoryPosition,inventoryBGTexture);
		//draw stuff inside
		GUI.DrawTexture(healthPosition, healthTexture);
		GUI.DrawTexture(shieldPosition,shieldTexture);
		GUI.DrawTexture(crosshairPosition, crosshairTexture);
		//GUI.Draw();

		//ammo text only if gun is selected
		if (false)
			//show = !show;
		if (true)
		{
			if (Time.timeScale != 0.0)
			{
				double fps = 1.0 / Time.deltaTime;
				ammoText.guiText.text = fps.ToString();
			}
			else
			{
				ammoText.text = "0";
			}
		}
		else
		{
			ammoText.text = "";	
		}
		
		//draw overlay
		//if (child is gun type != this crosshair type)
			//crosshair type = child gun type
		
		//GUI.DrawTexture(new Rect(0f,0f,sw,sh),overlayTexture);
	}
		
	void Update(){
		if(true){
			/* Should check if its a gun type? Causes error when just a mineral,
			if (Input.GetButton ("Fire1") && CheckItem(SelectedItem))
				virtualHand.BroadcastMessage("Fire");
			if(Input.GetButton("Fire2")){
				virtualHand.BroadcastMessage("Toss");
			}
			/*Inventory ShortCuts
			if (Input.GetKeyDown("1"))
			{
			CheckItem(0);
			}
			else if (Input.GetKeyDown("2"))
			{
			CheckItem(1);
			}
			else if (Input.GetKeyDown("3"))
			{
			CheckItem(2);
			}
			//*/
			if( Input.GetKey( "g" )) {
				Debug.Log("You Tried to grab it");
				BroadcastMessage("GrabObject");	
				CheckItem(0);
			}
			if( Input.GetKey( "t" )) {
				Debug.Log("You tried to toss it");
				//BroadcastMessage("IdentifyYourself", virtualHand.transform.GetChild(SelectedItem).gameObject);
				BroadcastMessage("ThrowObject", SelectedItem);	
				if(SelectedItem-1 > 0)
					CheckItem(SelectedItem--);
				else
					CheckItem(0);
				
			}
		
			//*/
			if (Input.GetKey("4"))
			{
				if(SelectedItem<9){
					//Debug.Log("test1");
					if(!CheckItem(SelectedItem++)){
						//Debug.Log("test2");
						
						if(!CheckItem(0))
						{
							//Debug.Log("test3");
							Debug.Log("Nothing to Select in hands");
						}
					}
				}
				else{
					CheckItem(0);
				}
					
			}
		}
	}
	void SelectItem(int index)
	{
		
		items=virtualHand.transform.childCount;
		for (int i=0;i < items ; i++){
			// Activate the selected weapon
			if (i == index){
				//Debug.Log("changed");
				virtualHand.transform.GetChild(i).gameObject.SetActiveRecursively(true);
				BroadcastMessage("IdentifyYourself", virtualHand.transform.GetChild(i).gameObject);
				SelectedItem=index;
				// Deactivate all other weapons
			}else{
				virtualHand.transform.GetChild(i).gameObject.SetActiveRecursively(false);
				}
		}
		
	}
	bool CheckItem(int index)
	{
		items=virtualHand.transform.childCount;
		if(index > items){
			
			CheckItem(0);
			return false;
		}
		SelectItem(index);
		return true;
	}

	void ChangeCrosshair(int selectedItem){
	crosshairTexture=crosshairTextures[selectedItem];
}
void UpdateHealth(float hitPoints){
	if(health>0){
		if(hitPoints!=health+shield){
			shield=hitPoints-health;
			if(shield<=0){
				health=hitPoints;
			}
				
		}
	}	
}
void AddShield(){
	shield++;
}

void AddInventory(int newItem){
	if(inventoryTextures != null && items<9){
		items++;
		inventoryTextures[items]=itemTextures[newItem];		
	}
}
void RemoveInventory(GameObject item){//int should be a tag? or gameobject refrence?
		for (var i=0;i<virtualHand.transform.childCount;i++){
			// Activate the selected weapon

		}
	switch(this.gameObject.tag)//change back to tag
		{
			case "Rail Gun":
				
				break;
			case "Pistol":
				
				break;
			case "Shot Gun":
				
				break;
			case "Plasma Ray":
				
				break;
			case "Laser Gun":
				
				break;
			case "Large Rail Gun":
				
				break;
			case "Rocket Launcher":
				
				break;
			case "RPG":
				
				break;
			case "Grenade Launcher":
				
				break;
			case "Minneral One":
				
				break;
			case "Minneral Two":
				
				break;
			case "Minneral Three":
				
				break;
			case "Minneral Four":
				
				break;
			case "Minneral Five":
			default:
				break;
		}
}	
}
