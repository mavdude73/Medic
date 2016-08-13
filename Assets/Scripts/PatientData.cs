using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class PatientData : MonoBehaviour {
	

	UIManager uim;
	
	void Awake ()
	{
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
		GeneratePatientInfo ();
		BreachTarget();
		
		//		player = GameObject.Find("Player1");
		
	}
	
	void Update ()
	{
		
	}

	
	
	public int visitorNumber;
	public int assignedBedNumber;
	public string patientName;
	public string patientAge;
	public string patientHospitalNumber;
	public string patientLabel;

	public bool patientDead;
	public bool patientCured;
	public int health;
	public int deathTimer;
	public int targetTimer;
	
	
	public bool playerInZone = false;
	public bool patientInBedZone;
	public Vector3 allocatedBedVector3;
	
	public void PlayerInZoneBool (bool inZone)
	{
		if(inZone)
		{
			playerInZone = true;
		}
		else if (!inZone)
		{
			playerInZone = false;
		}
	}
	
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
	public List<string> treatments = new List<string>();
	
	public bool treatmentInProgress = false;
	
	public List<string> treatmentLogList = new List<string>();
	public string treatmentLog;
		
	void GeneratePatientInfo()
	{
		// list diagnoses here then add the corresponding function
		PatientBio();

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
			treatments.Add (treatment_start [r]);
			treatment_start.RemoveAt (r);
		}

		while (blood_start.Count > 0)
		{
			int r = Random.Range (0,blood_start.Count);
			blood.Add (blood_start [r]);
			blood_start.RemoveAt (r);
		}


		while (xray_start.Count > 0)
		{
			int r = Random.Range (0,xray_start.Count);
			xray.Add (xray_start [r]);
			xray_start.RemoveAt (r);
		}


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
		dialoguePlan.Add("For ward-based care and treatment.");
		dialoguePlan.Add("Not for discharge presently. Admit for observation and treatment.");
		dialoguePlan.Add("Requires hospital treatment - for admission.");
		string dialoguePlanText = dialoguePlan [Random.Range (0,dialoguePlan.Count)];
		
		string line1 = patientName + " has been complaining over the last " + rngDays + " days with a history of " + symptoms[0] + " and " + symptoms[1] + ". ";
		string line2 = dialogueOTCText + "\n \n";
		string line3 = "Examination findings: " + examination[0] + "\n \n";
		string line4 = "This patient may benefit from a blood test but they clearly have a " + conditionType + " problem, like " + diagnosisName + ", and " + treatments[0] + " may prove effective." + "\n \n";
		string line5 = "Plan: " + dialoguePlanText;
		
		seniorReview = line1 + line2 + line3 + line4 + line5;
	}
	
	public void TreatmentProgress(string treatment, string response)
	{
		string answer = treatment + " " + response + "\n";
		StartCoroutine(TreatmentLog(answer));
	}
	
	public IEnumerator TreatmentLog(string answer)
	{
		yield return new WaitForSeconds(1);
		treatmentLogList.Add(answer);
		
		for(int tl = 0; tl < treatmentLogList.Count; tl++)
		{
			treatmentLog = treatmentLog + treatmentLogList[tl];
		}
		treatmentLogList.Clear();
		
//		yield return new WaitForSeconds(1);
		if(treatments.Count == 0)
		{
			patientCured = true;
			treatmentLog = treatmentLog + "\n\n" + "CURED";
		}
				
		if(playerInZone)
		{
			uim.treatmentHistoryLabel.GetComponent<Text>().text = treatmentLog;
		}
		
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
		
		treatment_start.Add ("Tixabrufen");

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
		
		treatment_start.Add ("Amoxcitol");
		treatment_start.Add ("Placebolin");
	
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
		
		treatment_start.Add ("Synapticol");

	}
	
	
	
	//	void UpdateVisitorNumber ()
	//	{
	//		visitorNumber = gameObject.name;
	//	}
	

	void PatientBio()
	{
		string[] patientFName = new string[] {"Harry","John","Will","Thomas","Charlie","Felix","Dave","Mike","Steven","Ben","Rob","Matthew","Dennis","Brett","Joe","Justin","Lenny","Gavin"};
		string firstName = patientFName[Random.Range (0, patientFName.Length)];

		string[] patientLName = new string[] {"Hobart","Smith","Dickinson","Cope","Gilder","Seddon","Swann","Dixon","Gellar","Green","Wright","Jackson","Moyes","Harris","Petit","Morgan","Biddle","Tank","Lewis","Robinson"};
		string lastName = patientLName[Random.Range (0, patientLName.Length)];

		patientName = firstName + " " + lastName;

		patientAge = Random.Range (26, 80) + "Y";

		patientHospitalNumber = "DIS0" + Random.Range (10000, 99999);

		patientLabel = patientName + ", " + patientAge + "\n" + patientHospitalNumber;

		health = 2;
		deathTimer = 20000;
		targetTimer = 400;
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
	

	
}