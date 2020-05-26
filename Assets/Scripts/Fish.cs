using UnityEngine;
using System.Collections;

namespace fishtanksoftware
{
public class Fish : MonoBehaviour 
{
    GlobalFlock GlobalFlock = new GlobalFlock();
	public float speed = 0.001f;
	float minspeed = 0.8f;
	float maxspeed = 2.0f; 
	// Vector3 averageHeading;
	// Vector3 averagePosition;
	float neighborDistance = 2.0f; //used to be 3
    public Vector3 newGoalPos;
	bool turning = false; 
	void Start () {
		speed = Random.Range (minspeed, maxspeed);
		this.GetComponent<Animation>()["Motion"].speed = speed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ApplyTankBoundary ();

		if(turning) 
		{
			Vector3 direction = newGoalPos - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction),
				TurnSpeed () * Time.deltaTime);
			speed = Random.Range (minspeed, maxspeed);
			this.GetComponent<Animation>()["Motion"].speed = speed;
		} 
		else 
		{
			if (Random.Range (0, 10) < 1) //used to be 5
				ApplyRules ();
		}

		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void ApplyTankBoundary() 
	{
		if(Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.tankSize) 
		{
			turning = true;
		} else 
		{
			turning = false;
		} 
	}
	// void OnTriggerEnter(Collider other)
	// {
	// 	if(!turning)
	// 	{
	// 		newGoalPos = this.transform.position - other.gameObject.transform.position;
	// 	}
	// 	turning = true;
	// }

	// void OnTriggerExit(Collider other)
	// {
	// 	turning = false;
	// }

	void ApplyRules() {
		GameObject[] gos;
		gos = GlobalFlock.allFish;

		Vector3 vCenter = Vector3.zero;
		Vector3 vAvoid = Vector3.zero;
		float gSpeed = 0.1f;

		Vector3 goalPos = GlobalFlock.goalPos;

		float dist;
		int groupSize = 0;

		foreach (GameObject go in gos) {
			if (go != this.gameObject) 
			{
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighborDistance) 
				{
					vCenter += go.transform.position;
					groupSize++;

					if(dist < 2.0f)  //could change, increase for big fish
					{
						vAvoid = vAvoid + (this.transform.position - go.transform.position);
					}

					Fish anotherFish = go.GetComponent<Fish> ();
					gSpeed += anotherFish.speed;
				}
			}
		}

		if (groupSize > 0) 
		{
			vCenter = vCenter / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;
			this.GetComponent<Animation>()["Motion"].speed = speed;

			Vector3 direction = (vCenter + vAvoid) - transform.position;
			if (direction != Vector3.zero) 
			{
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					TurnSpeed () * Time.deltaTime); 
			}
		}

 	}

	float TurnSpeed() {
		return Random.Range (2.0f, 6.0f);
	}
 }
}
