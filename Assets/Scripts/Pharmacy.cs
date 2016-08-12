using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pharmacy : MonoBehaviour {
	
	
	GameObject player;
	UIManager uim;
	Inventory inv;
	Transform floorItemTransform;
	
	bool playerInZone;
	public bool PharmacyProcessingBool = false;
	public Queue<int> pharmacyQueue = new Queue<int>();
	
	
	void Awake ()
	{
		player = GameObject.Find("Player1");
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		floorItemTransform = GameObject.Find ("Flooritems").transform;
	}
	
	void OpenPharmacy()
	{
		if (!uim.pharmacyMenu.activeSelf)
		{
			uim.pharmacyMenu.SetActive(true);
		} 
	}
	
	public void Medicine(int i)
	{
		pharmacyQueue.Enqueue (i);
		StartCoroutine (ProcessPharmacyRequest());
	}
	
	IEnumerator ProcessPharmacyRequest()
	{
		if (pharmacyQueue.Count > 0 && !PharmacyProcessingBool)
		{
			int timer = 5;
			PharmacyProcessingBool = true;
			yield return new WaitForSeconds (timer);
			int i = pharmacyQueue.Peek ();
			
			float rngVectModifierX = Random.Range (0.5f, 1.1f);
			float rngVectModifierY = Random.Range (-0.4f, 0.4f);
			Vector3 posi = new Vector3 (transform.position.x + rngVectModifierX, transform.position.y + rngVectModifierY);
			
			GameObject itemAsGameObject = (GameObject)Instantiate (Resources.Load<GameObject> ("DroppedItem"), posi, Quaternion.identity);
			Item medicine = inv.PharmacyRequest (i);
			itemAsGameObject.GetComponent<DroppedItem> ().item = medicine;
			
			
			itemAsGameObject.GetComponentInChildren<SpriteRenderer> ().sprite = medicine.itemIcon;
			
			// temporary fix until sprite scale standardised
			GameObject spriteObj = itemAsGameObject.GetComponentInChildren<SpriteRenderer> ().gameObject;
			spriteObj.transform.localScale = new Vector3 (0.15f, 0.15f, 1f); 
			//
			
			itemAsGameObject.transform.SetParent (floorItemTransform, true);
			itemAsGameObject.name = medicine.itemName;
			
			pharmacyQueue.Dequeue ();
			PharmacyProcessingBool = false;
			StartCoroutine (ProcessPharmacyRequest());
		}
	}
	
	
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject == player)
		{
			playerInZone = false;
		}
	}
	
	void Update()
	{
		if(!playerInZone)
		{
			return;
		}
		else if(inv.HitSpecificObject("PharmacyCollider") && Input.GetButtonDown ("LMB"))
		{
			OpenPharmacy();			
		}
		
	}
	
	
}