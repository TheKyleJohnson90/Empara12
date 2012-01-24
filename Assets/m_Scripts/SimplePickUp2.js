// Set weapon pickup model and the weapon to add into inventory in inspector
var weaponAdd = GameObject;
var weaponholder = transform.parent; 

// Add weapon to inventory and destroy the weapon pickup object
function OnTriggerEnter () {
Destroy(gameObject);

if(weaponholder) { 
//weaponAdd.transform.parent = weaponholder.transform.parent; 
}
}

function PositionObject() { 
var xpos = transform.position.x; 
var ypos = transform.position.y; 
var zpos = transform.position.z; 
 
var objectPosition = new Vector3(xpos, ypos, zpos + 1); 
 
//weaponAdd.transform.position = objectPosition; 
}