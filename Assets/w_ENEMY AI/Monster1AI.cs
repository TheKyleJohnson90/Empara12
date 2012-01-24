using UnityEngine;
using System.Collections;

public class Monster1AI : MonoBehaviour 
{
	// player
	private Vector3 playerPosition;
	private Vector3 enemyPosition;
	private float distance;

	// speed
	private float speed = 0.0f;
	private float walkingSpeed = 25.0f;
	private float runningSpeed = 50.0f;
	private float rotateSpeed = 1.0f; // 1
	
	// movement over the level
	private Vector3 gravityCenter ;
	private Vector3 target;
	private Vector3 lookingAtTarget;
	private Vector3 goingAroundTheMoon;
	
	// mineral system
	private int maxDistanceBeforeEating = 20;
	private float EatingTimer = 0.0f;

	// waypoint stystem
	private Vector3 lastWayPoint;
	private int closest;
	private const int MaxNoOfWPs = 72;
	private GameObject[] everyWayPointInLevel = new GameObject[MaxNoOfWPs];

	private float[] distanceToWayPoints = new float[MaxNoOfWPs];

	void Start () 
	{
		animation.wrapMode = WrapMode.Loop;
		GameObject moon = GameObject.Find("MOON");
		gravityCenter=moon.transform.position;		
		everyWayPointInLevel = GameObject.FindGameObjectsWithTag("Waypoint");		
	}
	
	void Update () 
	{
		// finds the player and the distance
		GameObject player = GameObject.Find("Character");
		playerPosition = player.transform.position;
		distance = Vector3.Distance (playerPosition, transform.position);
		
		
		WayPoint();
		AttackPlayer();
		
		// this is the vector3.up so the monster is going around the moon while facing target
		goingAroundTheMoon = transform.position - gravityCenter;
		lookingAtTarget = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookingAtTarget, goingAroundTheMoon);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed); 
		transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
	}

	void WayPoint()
	{
		int totalDistance = 10;
		for (int nStart = 0; nStart < MaxNoOfWPs; nStart++)
		{
			distanceToWayPoints[nStart] = Vector3.Distance(everyWayPointInLevel[nStart].transform.position, transform.position);
			if (distanceToWayPoints[nStart] < distanceToWayPoints[closest])
			{
				closest = nStart;
				target = everyWayPointInLevel[closest].transform.position;
				lastWayPoint = everyWayPointInLevel[closest].transform.position;
			}
			if (distanceToWayPoints[closest] < totalDistance)
			{
				// has to be changed 
				// so that instead of placing the wp at 0,0,0
				// it makes it not calculate the wp in the search
				everyWayPointInLevel[closest].transform.position = new Vector3(0,0,0);

			}
		}
		animation.CrossFade("walk");
		speed = walkingSpeed;
	}	
	
	void OnTriggerStay(Collider col)
	{   
		float TimerMaxBeforeEating = 4.5f; // Radius  of collider = 60
		float TimerAfterEating = 7.5f;

		if (col.tag == "Element A" || col.tag == "Element B"  || col.tag == "Element C" || col.tag == "Element D"  ||col.tag == "Element E")   
		{
			if (col.rigidbody.isKinematic == false)
			{
				target = col.transform.position;
				EatingTimer += Time.deltaTime;
			
				if (EatingTimer >= TimerMaxBeforeEating) 
				{
					animation.CrossFade("eating");
					speed = 0.0f;
				
					if (EatingTimer >= TimerAfterEating)
					{
						if(col.rigidbody.isKinematic == false) 
						{
							col.rigidbody.isKinematic = true;
						}
						col.transform.position = transform.position;
						col.transform.parent = transform;
						EatingTimer = 0.0f;
						animation.CrossFade("walk");
						speed = walkingSpeed;
						target = lastWayPoint;
					}
				}
			}
		}
	}
	
	void AttackPlayer()
	{
		int maxDistanceToPlayer = 150;
		int maxDistBeforeAttack = 25;
		if (distance < maxDistanceToPlayer)
		{
			target = playerPosition;
			animation.CrossFade("run");
			speed = runningSpeed;
		if (distance < maxDistBeforeAttack)
		{ 
			animation.CrossFade("attack1");
			speed = 0.0f;
		}
		}
	}
	
	
	// make a void function so when it picks up mineral it disables the gravity on it
	// VOID DISABLE Sphere Gravity On Minerals ();
	
	
	// Still has to be tested
	void ThrowObject() 
	{
//		for (int i = 0; i < MaxNoOfMinerals; i++)
//		{
//			if (this.everyMineralInLevel[i].transform.parent == transform)
//			{
//				this.everyMineralInLevel[i].transform.parent = null;
//				this.everyMineralInLevel[i].rigidbody.isKinematic = false;
//			}
//		}
//		Destroy(gameObject);
		//delete thiscreature object;
//		BroadcastMessage ("Detonate");
	}
}
