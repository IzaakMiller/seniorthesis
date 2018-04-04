using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class MoveDemo : MonoBehaviour {

	//initalizing all necessary variables
	private Transform startTransform, endTransform;
	public Transform[] waypoints;
	public float speed;
	Vector3 startPos;
	private Vector3 finalPos;
	private float startTime;
	private float distance;
	private int currentIndex = 0;


	// Use this for initialization
	void Start () {
		startTime = Time.time; //save the current time

		startTransform = waypoints [0]; //accessing first waypoint in waypoint array
		endTransform = waypoints [1]; //accessing second waypoint in waypoint array
		//getting the final way point
		finalPos = waypoints [waypoints.Length - 1].position;
		//calculating distance between two points
		distance = Vector3.Distance (startTransform.position, endTransform.position);
	}

	// Update is called once per frame
	void Update () {
		//check to see if reached end of waypoint array
		if (currentIndex < waypoints.Length) {
			//calculate distance covered and distance left
			float distanceCovered = (Time.time - startTime) * speed;
			float fracJourney = distanceCovered / distance;
			//move the rigidbody slowly along the line between the two points
			transform.position = Vector3.Lerp (startTransform.position, endTransform.position, fracJourney);

			if (fracJourney > 0.95f) { //check if travelled 95 % to finaldestination
				currentIndex++; //update index

				//check if 2 or more waypoints left in array
				if (currentIndex < waypoints.Length - 1) {
					startTime = Time.time; //get time

					startTransform = waypoints [currentIndex]; //get new start waypoint
					endTransform = waypoints [currentIndex + 1]; //get new end waypoint
					//calculate distance between waypoints
					distance = Vector3.Distance (startTransform.position, endTransform.position);
				}
			}
		} else {
			//move rigidbody to waypoint
			transform.position = finalPos;
		}
	}
}
