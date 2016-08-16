using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatientInvestigations : MonoBehaviour {

	PatientData pd;
	Inventory inv;
	GameObject samplePrefab;
	
		
	void Awake ()
	{
		pd = this.gameObject.GetComponent<PatientData> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		samplePrefab = Resources.Load<GameObject>("SamplePrefab");
	}
	
	public void ObtainBloodSample(GameObject obj, bool isMouse, int hotkey) // code 6 = empty blood syringe
	{
		if(gameObject == obj)
		{
			if(!inv.mouseOverHotbar && !pd.patientDead)
			{
				if (!isMouse && inv.Items[hotkey].itemName == "Syringe")
				{
					inv.Items[hotkey] = new Item (); 											// sets the empty syringe slot to empty	
					inv.Items[hotkey] = GenerateSampleFunction("Blood"); 								// sets the empty slot into a blood filled syringe				
				}
				else if (isMouse && inv.draggedItem.itemName == "Syringe")
				{
					inv.draggedItem = new Item (); 												// sets the empty syringe slot to empty	
					inv.draggedItem = GenerateSampleFunction("Blood"); 								// sets the empty slot into a blood filled syringe
					inv.draggedItemGameobject.GetComponent<Image>().sprite = inv.draggedItem.itemIcon;
				}
			}
		}
	}

	Item GenerateSampleFunction(string sampleName)
	{
		GameObject sampleGameobject = (GameObject)Instantiate(samplePrefab);
		sampleGameobject.transform.SetParent(GameObject.Find (sampleName).transform, false);					//this.gameObject.transform
		sampleGameobject.name = sampleName+"sample" + pd.visitorNumber;
		Item item = new Item(-1, sampleName, "Sample", "Some bodily sample that needs analysing", pd.visitorNumber, sampleGameobject);
		
		BloodSample bs = sampleGameobject.GetComponent<BloodSample>();
		bs.visitorID = pd.visitorNumber;
		bs.visitorName = pd.patientName;
		bs.hospitalID = pd.patientHospitalNumber;
		bs.bloodresult = pd.blood[0];
		bs.itemdata = item;
		
		return item;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
}
