using UnityEngine;
using System.Collections;

public class Movingmatron : MonoBehaviour {

	public GameObject destinationPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.GetComponent<NavMeshAgent>().destination = destinationPoint.transform.position;
	}
}
