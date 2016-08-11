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
	public string consultantName;
	public string seniorReview;
	
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
	
		
	void DiagnosisRNG()
	{
		diagnoses.Add ("MI");
		diagnoses.Add ("LRTI");
		diagnoses.Add ("GORD");
		
		int r = Random.Range (0,diagnoses.Count);
		string diagName = diagnoses [r];
		
		Invoke ("D_" + diagName, 0);
		
		Invoke ("ModifyOrder", 0);
		
		Invoke ("Consultant", 0);
		
		Invoke ("SeniorReview", 0);
	}
	
	void ModifyOrder()
	{
		while (symptoms_start.Count > 0)
		{
			int r = Random.Range (0,symptoms_start.Count);
			symptoms.Add (symptoms_start [r]);
			symptoms_start.RemoveAt (r);
		}
		
		while (examination_start.Count > 0)
		{
			int r = Random.Range (0,examination_start.Count);
			examination.Add (examination_start [r]);
			examination_start.RemoveAt (r);
		}
		
		while (treatment_start.Count > 0)
		{
			int r = Random.Range (0,treatment_start.Count);
			treatment.Add (treatment_start [r]);
			treatment_start.RemoveAt (r);
		}
		
		
		
		//		for (int i = 0; i < symptoms.Count; i++)
		//		{
		//			Debug.Log (blood [i]);
		//		}
	}
	
	void Consultant()
	{
		if(conditionType == "cardiac")
		{
			consultantName = "Payne";
		}
		else if(conditionType == "respiratory")
		{
			consultantName = "Hope";
		}
		else if(conditionType == "gastroenterology")
		{
			consultantName = "Butt";
		}
		else
		{
			consultantName = "Cope";
		}
	}
	
	void SeniorReview()
	{
		int rngDays = Random.Range (2,8);
		List<string> dialogueOTC = new List<string>();
		dialogueOTC.Add("Over the counter remedies have poorly helped here namely because none have been tried yet.");
		dialogueOTC.Add("The lazy sod has not tried any treatment at home and is clearly getting worse.");
		dialogueOTC.Add("No attempt has been made with OTC remedies as they are too cheap to purchase.");
		dialogueOTC.Add("Reports no improvement with homeopathic medicine but is clinically well hydrated.");
		dialogueOTC.Add("The patient has more than 100 allergies including saline. Nurse to kindly use 'hydrolating infusions'.");
		string dialogueOTCText = dialogueOTC [Random.Range (0,dialogueOTC.Count)];
		
		List<string> dialoguePlan = new List<string>();
		dialoguePlan.Add("Admit to ward for further assessment.");
		dialoguePlan.Add("As clinically worsening, admit for treatment.");
		dialoguePlan.Add("Ward based care and follow-up.");
		dialoguePlan.Add("Not for discharge presently. Admit for observation and treatment.");
		dialoguePlan.Add("Requires hospital treatment - for admission.");
		string dialoguePlanText = dialoguePlan [Random.Range (0,dialoguePlan.Count)];
		
		string line1 = patientName + " has been complaining over the last " + rngDays + " days with a history of " + symptoms[0] + " and " + symptoms[1] + ". ";
		string line2 = dialogueOTCText + "\n \n";
		string line3 = "Examination findings: " + examination[0] + "\n \n";
		string line4 = "This patient may benefit from a blood test but they clearly have a " + conditionType + " problem, like " + diagnosisName + ", and " + treatment[0] + " may prove effective." + "\n \n";
		string line5 = "Plan: " + dialoguePlanText;
		
		seniorReview = line1 + line2 + line3 + line4 + line5;
	}
	
	void D_MI()
	{
		diagnosisID = 0;
		diagnosisName = "MI";
		conditionType = "cardiac";
		
		symptoms_start.Add ("nausea");
		symptoms_start.Add ("breathlessness");
		symptoms_start.Add ("palpitations");
		symptoms_start.Add ("chest pain");
		
		examination_start.Add ("murmur");
		examination_start.Add ("raised JVP");
		examination_start.Add ("irregular pulse");
		
		blood_start.Add ("high CK");
		blood_start.Add ("high Trop T");
		
		xray_start.Add ("normal");
		
		treatment_start.Add ("tixabrufen");

	}
	
	void D_LRTI()
	{
		diagnosisID = 1;
		diagnosisName = "LRTI";
		conditionType = "respiratory";
		
		symptoms_start.Add ("cough");
		symptoms_start.Add ("breathlessness");
		symptoms_start.Add ("fever");
		symptoms_start.Add ("chest pain");
		
		examination_start.Add ("fever");
		examination_start.Add ("chest crackles");
		examination_start.Add ("fast breathing");
		
		blood_start.Add ("high WCC");
		blood_start.Add ("high CRP");
		
		xray_start.Add ("consolidation");
		xray_start.Add ("shadowing");
		
		treatment_start.Add ("amoxcitol");
		treatment_start.Add ("placebolin");
	
	}
	
	void D_GORD()
	{
		diagnosisID = 2;
		diagnosisName = "GORD";
		conditionType = "gastroenterology";
		
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
		DiagnosisRNG ();
		
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
		targetTimer = 300;
		
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