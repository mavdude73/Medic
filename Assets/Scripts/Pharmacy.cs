using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Pharmacy : MonoBehaviour
{
	GameObject[] medicineButton;
	GameObject pharmacyMenu;

	GameObject pharmacyProgressBar;
	GameObject pharmacyProgressText;
	public bool PharmacyProcessingBool = false;

	Queue<string> pharmacyQueue = new Queue<string>();

	int difficulty = 0;
	float timer;
	Item medicine;
	float elapsedTime = 0f;
	public string sponsor;
	
	List<Item> pharmacyDrugItems = new List<Item>();

	void Start()
	{
		pharmacyMenu = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
		pharmacyProgressBar = pharmacyMenu.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
		pharmacyProgressText = pharmacyMenu.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
		pharmacyMenu.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate{ClosePharmacyScreen();} );

		PharmacySetup(difficulty);
	}

	void PharmacySetup(int difficulty)
	{
		List<string> startingMedicineList = new List<string>();

		if(difficulty >= 0)
		{
			startingMedicineList.Add("Amoxcitol");
			startingMedicineList.Add("Placebolin");
			startingMedicineList.Add("Synapticol");
			startingMedicineList.Add("Tixabrufen");
		}

		if(difficulty > 0)
		{
			startingMedicineList.Add("Derpcillium");
			startingMedicineList.Add("Hypnotol");
			startingMedicineList.Add("Adamaxine");
			startingMedicineList.Add("Hydromax");
			startingMedicineList.Add("Pseudodrine");
		}

		for(int b = 0; b < startingMedicineList.Count; b++)
		{
			GameObject button = (GameObject)Instantiate(Resources.Load("MedicineButton"));
			button.name = "Button"+b;
			button.transform.SetParent(pharmacyMenu.transform.GetChild(4).transform, false);
		}

		medicineButton = GameObject.FindGameObjectsWithTag("MedicineButton");


		List<PharmacySponsors> drugSponsorList = new List<PharmacySponsors>();
		drugSponsorList.Add(new PharmacySponsors(0, "Red Company", "RedPotion", new Color32(255, 92, 92, 255)));
		drugSponsorList.Add(new PharmacySponsors(1, "Blue Company", "BluePotion", new Color32(0, 212, 251, 255)));
		drugSponsorList.Add(new PharmacySponsors(2, "Purple Company", "PurplePotion", new Color32(255, 114, 234, 255)));

		int k = 30;

		foreach(GameObject mB in medicineButton)
		{
			if(!mB.GetComponent<Image>().IsActive())
			{
				mB.GetComponent<Image>().enabled = true;
				int rng1 = Random.Range (0,startingMedicineList.Count);
				string drugSelected = startingMedicineList [rng1];						// random drug name
				startingMedicineList.RemoveAt(rng1);

				int rng2 = Random.Range (0,drugSponsorList.Count);
				PharmacySponsors sponsorSelected = drugSponsorList [rng2];				// random drug sponsor (Astra Zenica), in this case "RedPotion" etc.
//				drugSponsorList.RemoveAt(rng2);

				pharmacyDrugItems.Add (new Item (k, sponsorSelected.sponsorIconName, "Drug", drugSelected, -1, null));
				k++;
				mB.GetComponent<Image>().color = sponsorSelected.sponsorColor32;


				mB.GetComponentInChildren<Text>().text = drugSelected;
				mB.GetComponent<Button>().onClick.AddListener(delegate{Medicine(drugSelected, sponsorSelected.sponsorName);} );
			}
		}

		pharmacyMenu.SetActive(false);
	}


	
	public void OpenPharmacyScreen(string name)
	{
		if (this.gameObject.name == name && !pharmacyMenu.activeSelf)
		{
			pharmacyMenu.SetActive(true);
		} 
	}

	void ClosePharmacyScreen()
	{
		if (pharmacyMenu.activeSelf)
		{
			pharmacyMenu.SetActive(false);
		}
	}
	
	public void Medicine(string drugName, string sponsor)
	{
		pharmacyQueue.Enqueue (drugName);
		StartCoroutine (ProcessPharmacyRequest());
	}


	IEnumerator ProgressBar(float fTimer)
	{
		if (fTimer - elapsedTime >= 0)
		{
			elapsedTime += Time.deltaTime;
			pharmacyProgressBar.GetComponent<Image>().fillAmount = elapsedTime/fTimer;
			pharmacyProgressText.GetComponent<Text>().text = "Processing...";
			yield return new WaitForSeconds(0);
			StartCoroutine(ProgressBar(fTimer));
		}
	}


	IEnumerator ProcessPharmacyRequest()
	{
		if (pharmacyQueue.Count > 0 && !PharmacyProcessingBool)
		{
			elapsedTime = 0f;
			string drugName = pharmacyQueue.Peek ();

			for(int d = 0; d < pharmacyDrugItems.Count; d++)
			{
				if(pharmacyDrugItems[d].itemDesc == drugName)
				{
					sponsor = pharmacyDrugItems[d].itemName;
					medicine = pharmacyDrugItems[d];
				}
			}

			PharmacyProcessingBool = true;

			if (sponsor == "Company x")
			{
				timer = 1f;
			}
			else
			{
				timer = 3f;
			}

			StartCoroutine(ProgressBar(timer));

			yield return new WaitForSeconds(timer);


			float rngVectModifierX = Random.Range (0.8f, 1.8f);
			float rngVectModifierY = Random.Range (-0.7f, 0.7f);
			Vector3 posi = new Vector3 (transform.position.x + rngVectModifierX, transform.position.y + rngVectModifierY);
			
			GameObject itemAsGameObject = (GameObject)Instantiate (Resources.Load<GameObject> ("DroppedItem"), posi, Quaternion.identity);
			itemAsGameObject.GetComponent<DroppedItem> ().item = medicine;
			
			
			itemAsGameObject.GetComponentInChildren<SpriteRenderer> ().sprite = medicine.itemIcon;
			
			// temporary fix until sprite scale standardised
			GameObject spriteObj = itemAsGameObject.GetComponentInChildren<SpriteRenderer> ().gameObject;
			spriteObj.transform.localScale = new Vector3 (0.13f, 0.13f, 1f); 
			//
			
			itemAsGameObject.transform.SetParent (GameObject.Find ("Flooritems").transform, true);
			itemAsGameObject.name = medicine.itemName;

			pharmacyQueue.Dequeue ();
			PharmacyProcessingBool = false;
			pharmacyProgressBar.GetComponent<Image>().fillAmount = 0f;
			pharmacyProgressText.GetComponent<Text>().text = "Open";
			StartCoroutine (ProcessPharmacyRequest());
		}
	}


	
	void Update()
	{
		
	}
	
//	public void setupBtn()
//    {
//        string param = "bar";
//        btn.GetComponent<Button>().onClick.AddListener(delegate { btnClicked(param); });
//    }
//       
//    public void btnClicked(string param)
//    {
//        Debug.Log("foo " + param);
//    }
 
}
