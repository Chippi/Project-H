using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {
	private NavMeshAgent navMeshAgent;
	private Vector3 targetPosition;
	private bool targetReached;

	const int LEFT_MOUSE_BUTTON = 0; //0 Means left mouse button

	//Awake is called when the script instance is being loaded.
	void Awake () {
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		targetPosition = transform.position; //Position of attached gameObject eg. PlayerModel
		targetReached = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (LEFT_MOUSE_BUTTON)) {
			setTargetPosition();
		}

		if (!targetReached) {
			movePlayer();
		}
	}
		
	void setTargetPosition() {
		Plane plane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float point = 0f;

		if(plane.Raycast(ray, out point)) {
			targetPosition = ray.GetPoint (point);
			targetReached = false;
		}
	}

	void movePlayer() {
		navMeshAgent.SetDestination (targetPosition);

		if (transform.position == targetPosition) {
			targetReached = true;
		}
	}
}
