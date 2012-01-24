using UnityEngine;
using System.Collections;

// it is set in a way that it will go to a waypoint first,
// if mineral is close go to that instead,
// else if Player is near go to him instead

public class ThirdAI : MonoBehaviour 
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
	private float EatingTimer = 0.0f;
	private const int MaxNoOfMinerals = 6;	
	private float[] distanceToMineral = new float[MaxNoOfMinerals];
	private GameObject[] everyMineralInLevel = new GameObject[MaxNoOfMinerals]; 

	
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
		//short ii = 0;
		// instantiates the array and finds all the game objects
		//LayerMask minerals = 8;
		
		everyMineralInLevel =GameObject.FindGameObjectsWithTag("Element A"); 
		
		
		// is there any way they can all be tagged as ELEMENT A ?
		
		/*
		everyMineralA = GameObject.FindGameObjectsWithTag("Element A");
		everyMineralB = GameObject.FindGameObjectsWithTag("Element B");
		everyMineralC = GameObject.FindGameObjectsWithTag("Element C");
		everyMineralD = GameObject.FindGameObjectsWithTag("Element D");
		everyMineralE = GameObject.FindGameObjectsWithTag("Element E");
		*/
		
		everyWayPointInLevel = GameObject.FindGameObjectsWithTag("Waypoint");
	}
	
	void Update () 
	{
		// finds the player and the distance
		GameObject player = GameObject.Find("Character");
		playerPosition = player.transform.position;
		distance = Vector3.Distance (playerPosition, transform.position);
		// this is the vector3.up so the monster is going around the moon
		goingAroundTheMoon = transform.position - gravityCenter;
		
		// waypoint system
		WayPoint();
		
		// gets all them nice minerals + walk towards mineral if in range
		MineralFunction();
			
		// walk towards player if in sight
		AttackPlayer();
		
		print (target);

		lookingAtTarget = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookingAtTarget, goingAroundTheMoon);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed); 
		transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
	}

	void WayPoint()
	{
		int totalDistance = 10;
		
		
		for (int j = 0; j < MaxNoOfWPs; j++)
		{
			distanceToWayPoints[j] = Vector3.Distance(everyWayPointInLevel[j].transform.position, transform.position);
			
			if (distanceToWayPoints[j] < distanceToWayPoints[closest])
			{
				closest = j;
				target = everyWayPointInLevel[j].transform.position;
				lastWayPoint = everyWayPointInLevel[j].transform.position;
				animation.CrossFade("walk");
				speed = walkingSpeed;
			}
			if (distanceToWayPoints[j]  < totalDistance)
			{
				if (j >= MaxNoOfMinerals)
				{
					target = everyWayPointInLevel[j - 1].transform.position;
				}
				else
				{
					target = everyWayPointInLevel[j + 1].transform.position;
				}
			}
		}
	}
	
	void MineralFunction()
	{
		int maxDistanceToMineral = 125;
		int maxDistanceBeforeEating = 20;

		for (int i = 0; i < MaxNoOfMinerals; i++)
		{
			distanceToMineral[i] = Vector3.Distance(everyMineralInLevel[i].transform.position, transform.position);
				
			if (distanceToMineral[i] < maxDistanceToMineral && everyMineralInLevel[i].rigidbody.isKinematic == false)
			{
				target = everyMineralInLevel[i].transform.position;
				
				if (distanceToMineral[i] < maxDistanceBeforeEating)
				{
					animation.CrossFade("eating");
					speed = 0.0f;
					EatingTimer+= Time.deltaTime;
					if (EatingTimer >= 2)
					{
						if(everyMineralInLevel[i].rigidbody.isKinematic == false) 
						{
							everyMineralInLevel[i].rigidbody.isKinematic = true;
						}
						everyMineralInLevel[i].transform.position = transform.position;
						everyMineralInLevel[i].transform.parent = transform;
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
		int maxDistBeforeAttack = 50;
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
	

	void ThrowObject() 
	{
		for (int i = 0; i < MaxNoOfMinerals; i++)
		{
			if (this.everyMineralInLevel[i].transform.parent == transform)
			{
				this.everyMineralInLevel[i].transform.parent = null;
				this.everyMineralInLevel[i].rigidbody.isKinematic = false;
			}
		}
		Destroy(gameObject);
		//delete thiscreature object;
		BroadcastMessage ("Detonate");
	}
}
