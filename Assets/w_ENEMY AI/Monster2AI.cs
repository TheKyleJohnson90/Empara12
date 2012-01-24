using UnityEngine;
using System.Collections;

public class Monster2AI : MonoBehaviour 
{
	// player
	private Vector3 playerPosition;
	private Vector3 enemyPosition;
	private float distance;

	// speed
	private float speed = 0.0f;
	private float walkingSpeed = 15.0f;
	private float runningSpeed = 30.0f;
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
		

		
		AttackPlayer();
		
		// this is the vector3.up so the monster is going around the moon while facing target
		goingAroundTheMoon = transform.position - gravityCenter;
		lookingAtTarget = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookingAtTarget, goingAroundTheMoon);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed); 
		transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
	}

	void AttackPlayer()
	{
		int maxDistanceToPlayer = 180;
		int maxDistBeforeAttack = 15;
		
		target = playerPosition;
		speed = 0.0f;
		animation.CrossFade("idle");
		
		if (distance < maxDistanceToPlayer)
		{
			//target = playerPosition;
			animation.CrossFade("run");
			speed = runningSpeed;
		if (distance < maxDistBeforeAttack)
		{ 
			animation.CrossFade("attack");
			speed = 0.0f;
		}
		}
	}
	

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
