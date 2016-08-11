using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class PatientData : MonoBehaviour {
	
	
	RNGManager rng;
	
	
	public bool questPatient;
	public int visitorNumber;
	public int assignedBedNumber;
	public string patientName;
	public string patientAge;
	public string patientHospitalNumber;
	public string patientLabel;
	public string patientDiagnosis;
	public string patientSymptom;
	public string patientExamination;
	public string patientThermometer;
	public string patientBlood;
	public string patientUs;
	public string patientXr;
	public string patientMri;
	public string correctTreatment;
	public string currentTreatment;
	public bool patientDead;
	public bool patientCured;
	public int health;
	public int deathTimer;
	public int targetTimer;
	
	public bool patientInBedZone;
	public Vector3 allocatedBedVector3;
	
	
	//	GameObject medicalRecord;
	//	GameObject biographicsPages;
	//	GameObject diagnosticsPages;
	//	GameObject treatmentPages;
	//	GameObject player;
	
	List<string> diagnoses = new List<string>();
	
	public int diagnosisID;
	public string diagnosisName;
	public string conditionType;
	
	List<string> symptoms_start = new List<string>();
	public List<string> symptoms = new List<string>();
	
	List<string> examination_start = new List<string>();
	public List<string> examination = new List<string>();
	
	List<string> blood_start = new List<string>();
	public List<string> blood = new List<string>();
	
	List<string> xray_start = new List<string>();
	public List<string> xray = new List<string>();
	
	List<string> treatment_start = new List<string>();
	public List<string> treatment = new List<string>();
	
	
	void diagnosisRNG()
	{
		diagnoses.Add ("MI");
		diagnoses.Add ("LRTI");
		diagnoses.Add ("GORD");
		
		int r = Random.Range (0,diagnoses.Count);
		string diagName = diagnoses [r];
		
		Invoke ("d_" + diagName, 0);
		
		Invoke ("modifyOrder", 0);
	}
	
	void modifyOrder()
	{
		while (symptoms_start.Count > 0)
		{
			int r = Random.Range (0,symptoms_start.Count);
			symptoms.Add (symptoms_start [r]);
			symptoms_start.RemoveAt (r);
		}
		
		//		for (int i = 0; i < symptoms.Count; i++)
		//		{
		//			Debug.Log (blood [i]);
		//		}
	}
	
	void d_MI()
	{
		diagnosisID = 0;
		diagnosisName = "MI";
		conditionType = "cardiac";
		
		symptoms_start.Add ("cough");
		symptoms_start.Add ("breathless");
		symptoms_start.Add ("palpitations");
		symptoms_start.Add ("chest pain");
		
		examination_start.Add ("fever");
		examination_start.Add ("chest crackles");
		examination_start.Add ("tachypnoea");
		
		blood_start.Add ("high CK");
		blood_start.Add ("high Trop T");
		
		xray_start.Add ("normal");
		
		treatment_start.Add ("tixabrufen");
		
		
	}
	
	void d_LRTI()
	{
		diagnosisID = 1;
		diagnosisName = "LRTI";
		conditionType = "chest";
		
		symptoms_start.Add ("cough");
		symptoms_start.Add ("breathless");
		symptoms_start.Add ("fever");
		symptoms_start.Add ("chest pain");
		
		examination_start.Add ("fever");
		examination_start.Add ("chest crackles");
		examination_start.Add ("tachypnoea");
		
		blood_start.Add ("high WCC");
		blood_start.Add ("high CRP");
		
		xray_start.Add ("consolidation");
		xray_start.Add ("shadowing");
		
		treatment_start.Add ("amoxcitol");
		treatment_start.Add ("placebolin");
		
		
		
	}
	
	void d_GORD()
	{
		diagnosisID = 3;
		diagnosisName = "GORD";
		conditionType = "gastro";
		
		symptoms_start.Add ("cough");
		symptoms_start.Add ("nausea");
		symptoms_start.Add ("burping");
		symptoms_start.Add ("abdominal pain");
		
		examination_start.Add ("normal");
		
		blood_start.Add ("normal");
		
		xray_start.Add ("normal");
		
		treatment_start.Add ("synapticol");
		
		
	}
	
	
	
	//	void UpdateVisitorNumber ()
	//	{
	//		visitorNumber = gameObject.name;
	//	}
	
	void Awake ()
	{
		rng = GameObject.Find ("RNGManager").GetComponent<RNGManager> ();
		AcquirePatientData ();
		BreachTarget();
		diagnosisRNG ();
		
		//		player = GameObject.Find("Player1");
		
	}
	
	
	
	void AcquirePatientData()
	{
		rng.getRNG ();
		
		//		Invoke("UpdateVisitorNumber",1f);
		patientName = rng.GetArray (0);
		patientAge = rng.GetArray (1);
		patientHospitalNumber = rng.GetArray (2);
		patientDiagnosis = rng.GetArray (3);
		patientSymptom = rng.GetArray (4);
		patientExamination = rng.GetArray (5);
		patientThermometer = rng.GetArray (6);
		patientBlood = rng.GetArray (7);
		patientUs = rng.GetArray (8);
		patientXr = rng.GetArray (9);
		patientMri = rng.GetArray (10);
		correctTreatment = rng.GetArray (11);
		currentTreatment = "No treatment";
		questPatient = false;
		patientDead = false;
		patientCured = false;
		health = 2;
		deathTimer = 20000;
		targetTimer = 200;
		
		patientInBedZone = false;
		//		patientPositionX = gameObject.transform.position.x;
		//		patientPositionY = gameObject.transform.position.y;
		
		
		patientLabel = patientName + ", " + patientAge + "\n" + patientHospitalNumber;
		
	}
	
	
	void BreachTarget()
	{
		if(targetTimer <= 0)
		{
			Debug.Log("gameObject Destroyed");
			Destroy(gameObject);
		}
		else if(targetTimer > 0)
		{
			targetTimer = targetTimer - 5;
			Invoke("BreachTarget",5f);
		}
	}
	
	
	//	public void OpenMedicalRecord()
	//	{
	//		medicalRecord = GameObject.Find ("MedicalRecord");
	//		biographicsPages = GameObject.Find ("BiographicsPages");
	//		diagnosticsPages = GameObject.Find ("DiagnosticsPages");
	//		treatmentPages = GameObject.Find ("TreatmentPages");
	//		
	//		medicalRecord.SetActive (true);	
	//		// Populate pages
	//		biographicsPages.SetActive (true);
	//		diagnosticsPages.SetActive (true);
	//		treatmentPages.SetActive (true);
	//		// Update patientlabels
	//		
	//		GameObject.Find("MainLabel").GetComponent<Text>().text = patientLabel;
	//		GameObject.Find("BiographicsLabel").GetComponent<Text>().text = patientLabel;
	//		GameObject.Find("DiagnosticsLabel").GetComponent<Text>().text = patientLabel;
	//		GameObject.Find("TreatmentLabel").GetComponent<Text>().text = patientLabel;
	//		GameObject.Find("CurrentTreatmentLabel").GetComponent<Text>().text = currentTreatment;
	//		// Close pages
	//		biographicsPages.SetActive (false);
	//		diagnosticsPages.SetActive (false);
	//		treatmentPages.SetActive (false);
	//	}
	
	//			result = rng.GetArray (value);
	
	//	rng = GameObject.Find ("GameManager");
	//	
	//				rng.getRNG ();
	//
	
	//
	//		patientName = rng.MyRNG[0];
	//		patientAge;
	//		patientHospitalNumber;
	//		patientDiagnosis;
	//		patientSymptom;
	//		patientExamination;
	//		patientThermometer;
	//		patientBlood;
	//		patientUs;
	//		patientXr;
	//		patientMri;
	//		patientTreatment;
	
	
	
	
}