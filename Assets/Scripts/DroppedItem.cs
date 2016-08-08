using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroppedItem : MonoBehaviour {

	public Item item;
	GameObject player1;
	bool playerInZone;
	Inventory inv;
	
	void Awake()
	{
		player1 = GameObject.Find ("Player1");
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
	}
	
	void Update()
	{
		PickMeUp();		
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
	
	
	void PickMeUp()
	{
		if(!playerInZone)
		{
			return;
		}
		else if (!inv.draggingItemBool && Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
			
			if(hit)
			{
//				Debug.Log(item.itemName);
//				Debug.Log(inv.draggedItem.itemName);
//				inv.draggedItem = item;
//				inv.draggingItemBool = true;
//				inv.draggedItemGameobject.SetActive(true);
//				inv.draggedItemGameobject.GetComponent<Image>().sprite = inv.draggedItem.itemIcon;
				
				inv.addItemIfEmpty(item);
				
				
				Destroy(this.gameObject);
			}
		}
	}
	
}
