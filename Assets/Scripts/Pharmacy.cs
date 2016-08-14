using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pharmacy : MonoBehaviour
{
	
	
	GameObject player;
	UIManager uim;
	Inventory inv;
	Transform floorItemTransform;


	public bool PharmacyProcessingBool = false;
	public Queue<int> pharmacyQueue = new Queue<int>();

	float timer;
	float elapsedTime = 0f;
	public string sponsor;
	


	void Awake()
	{
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
		player = GameObject.Find("Player1");
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		floorItemTransform = GameObject.Find ("Flooritems").transform;
	}
	
	public void OpenPharmacyScreen(string name)
	{
		if (name == "Pharmacy" && !uim.pharmacyMenu.activeSelf)
		{
			uim.pharmacyMenu.SetActive(true);
		} 
	}

	void ClosePharmacyScreen()
	{
		if (uim.pharmacyMenu.activeSelf)
		{
			uim.pharmacyMenu.SetActive(false);
		}
	}
	
	public void Medicine(int i)
	{
		pharmacyQueue.Enqueue (i);
		StartCoroutine (ProcessPharmacyRequest());
	}


	IEnumerator ProgressBar(float fTimer)
	{
		if (fTimer - elapsedTime >= 0)
		{
			elapsedTime += Time.deltaTime;
			uim.pharmacyProgressBar.GetComponent<Image>().fillAmount = elapsedTime/fTimer;
			uim.pharmacyProgressText.GetComponent<Text>().text = "Processing...";
			yield return new WaitForSeconds(0);
			StartCoroutine(ProgressBar(fTimer));
		}
	}


	void CheckForPharmacyUpgrade(int i)
	{
		if (inv.PharmacyRequest (i).itemName == sponsor)
		{
			timer = 1f;
		}
		else
		{
			timer = 3f;
		}
	}

	IEnumerator ProcessPharmacyRequest()
	{
		if (pharmacyQueue.Count > 0 && !PharmacyProcessingBool)
		{
			elapsedTime = 0f;
			int i = pharmacyQueue.Peek ();
			PharmacyProcessingBool = true;
			CheckForPharmacyUpgrade(i);

			StartCoroutine(ProgressBar(timer));

			yield return new WaitForSeconds(timer);


			float rngVectModifierX = Random.Range (0.8f, 1.7f);
			float rngVectModifierY = Random.Range (-0.6f, 0.6f);
			Vector3 posi = new Vector3 (transform.position.x + rngVectModifierX, transform.position.y + rngVectModifierY);
			
			GameObject itemAsGameObject = (GameObject)Instantiate (Resources.Load<GameObject> ("DroppedItem"), posi, Quaternion.identity);
			Item medicine = inv.PharmacyRequest (i);
			itemAsGameObject.GetComponent<DroppedItem> ().item = medicine;
			
			
			itemAsGameObject.GetComponentInChildren<SpriteRenderer> ().sprite = medicine.itemIcon;
			
			// temporary fix until sprite scale standardised
			GameObject spriteObj = itemAsGameObject.GetComponentInChildren<SpriteRenderer> ().gameObject;
			spriteObj.transform.localScale = new Vector3 (0.13f, 0.13f, 1f); 
			//
			
			itemAsGameObject.transform.SetParent (floorItemTransform, true);
			itemAsGameObject.name = medicine.itemName;

			pharmacyQueue.Dequeue ();
			PharmacyProcessingBool = false;
			uim.pharmacyProgressBar.GetComponent<Image>().fillAmount = 0f;
			uim.pharmacyProgressText.GetComponent<Text>().text = "Open";
			StartCoroutine (ProcessPharmacyRequest());
		}
	}


	
	void Update()
	{
		
	}
	
	
}