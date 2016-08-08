using UnityEngine;
using System.Collections;

public class SluiceItems: MonoBehaviour
{
	
	
	private GameObject player1;
	Inventory inv;
	bool playerInZone;
	
	void Awake ()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		player1 = GameObject.Find ("Player1");
	//	playerData = player1.GetComponent<PlayerData>();
	//	player2 = GameObject.Find ("FPSController2");
	//	player3 = GameObject.Find ("FPSController3");
	}
	
	public void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == player1)
		{
		playerInZone = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		if(other.gameObject == player1)
		{
		playerInZone = false;
		}
	}

	void ItemPickup()
	{
		if (!playerInZone)
		{
			return;
		}
		
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

			if(hit)
			{
				if (hit.collider.gameObject.name == "Pillow") {
					inv.addItem(2);
				}
				
				if (hit.collider.gameObject.name == "Scissors") {
					inv.addItem(5);
				}
				
				if (hit.collider.gameObject.name == "Scalpel") {
					inv.addItem(3);
				}
				
				if (hit.collider.gameObject.name == "Defibrillator") {
					inv.addItem(4);
				}
				
				if (hit.collider.gameObject.name == "Syringe") {
					inv.addItem(6);
				}
			}
		}
		
	}


	void Update(){
			ItemPickup ();
		}

}