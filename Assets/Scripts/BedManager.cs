using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BedManager : MonoBehaviour {

	public PatientManager pm;
	int numberOfBeds;
	public int allocationSpeed;
	public GameObject bedPrefab;
	public GameObject[] bedSlot;
	public Transform[] bedLocationTransform;
	public GameObject[] beds;
//	public List<GameObject> bedSlots = new List<GameObject> ();
//	public List<BedAllocation> Bed = new List<BedAllocation> ();
//	public Queue<GameObject> bedSlots = new Queue<GameObject>();
	
	
	void CreateBedAreas()
	{
		beds = GameObject.FindGameObjectsWithTag("Bed");
		numberOfBeds = beds.Length;

		int b = 0;

		bedSlot = new GameObject[numberOfBeds];
		bedLocationTransform = new Transform[numberOfBeds];

		foreach(GameObject bed in beds)
		{
			bed.transform.SetParent(this.gameObject.transform, false);
			bed.name = "Bed" + b;
			bedLocationTransform[b] = bed.GetComponent<Transform>();

			b++;
		}

//		bedSlots = new GameObject[bedCount];
//		bedSlotsPos = new Vector3[bedCount];
//		int x = (-bedCount + 1) * 5;
//		for(int k = 0; k < bedCount; k++)
//		{
//			GameObject bed = (GameObject)Instantiate(bedPrefab);
//			
//
//			bed.transform.SetParent(this.gameObject.transform, false);
//			bed.name = "Bed" + k;
//			bed.GetComponent<Transform>().localPosition = new Vector3(x,3,0);
//			bedSlotsPos[k] = new Vector3(x,3,0);
//
//			x = x + 10;
//		}

	}


	void AllocatePatientToBed()
	{
		for(int i = 0; i < numberOfBeds; i++)
		{
			if (pm.patientQueue.Count > 0 && bedSlot[i] == null)
			{
			bedSlot[i] = (pm.patientQueue.Peek());
//			bedLocationTransform[i].localPosition = new Vector3(bedLocationTransform[i].localPosition.x, bedLocationTransform[i].localPosition.y, 0);
			bedSlot[i].GetComponent<Transform>().localPosition = bedLocationTransform[i].localPosition;
			PatientData pd = bedSlot[i].GetComponent<PatientData>();
			pd.destinationTransform = bedLocationTransform[i];
//			Vector3 changeZ = new Vector3(bedLocationTransform[i].localPosition.x, bedLocationTransform[i].localPosition.y, 0);
//			pd.destinationTransform.localPosition = changeZ;

			pd.assignedBedNumber = i;
			pm.patientQueue.Dequeue();
			
//			Debug.Log(bedSlots[i].transform.localPosition);
//			bedSlots.Enqueue(pm.patientQueue.Peek());
			}
		}
		Invoke("AllocatePatientToBed",allocationSpeed);
	}
	
	void DeallocatePatientFromBed(int bedNumber)
	{
			bedSlot[bedNumber] = null;
	}
	
	
	
//	public void addPatient()
//	{
//		for (int i = 0; i < pm.Patients.Count; i++)
//		{
//			if(pm.Patients[i].gameObject.name == "Patient"+i)
//			{
//				BedAllocation bed = database.items[i];
//				addPatientEmptyBed(bed);
//				break;
//			}
//		}
//	}
//	
	
//	void addPatientToEmptyBed()
//	{
//		for (int i = 0; i < Bed.Count; i++)
//		{
//			if(Bed[i] == null)
//			{
//				for (int k = 0; k < pm.Patients.Count; k++)
//				{		
//					for (int l = 0; l < pm.Patients.Count; l++)
//					{		
//						if(pm.Patients[l].gameObject.name != "Patient"+k)
//						{
//							go = true;
//						}else{
//							go = false;
//						}
//						
//						if(pm.Patients[k].gameObject.name == "Patient"+k && go)
//						{
//							Bed[i] = new GameObject(pm.Patients[k]);
//	//						Bed[i] = new BedAllocation(i, pm.Patients[k], "no");
//							Debug.Log(Bed[1]);
//							break;
//						}
//					}
//				}
//			}
//		}	
//	}

	// Use this for initialization
	void Awake () {
		CreateBedAreas();
		AllocatePatientToBed();
//		Invoke("addPatientToEmptyBed",4f);
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if(Input.GetMouseButtonDown(1))
//		{
//			Debug.Log("RMB - patients allocated");
//			AllocatePatientToBed();
//		}
		
//		if(Input.GetKeyDown(KeyCode.Alpha2))
//		{
//			Debug.Log("Num2 - bed element 2 deallocated");
//			DeallocatePatientFromBed(2);
//		}
		
	}
}
