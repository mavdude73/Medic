using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroppedItem : MonoBehaviour {

	public Item item;
	GameObject player;
	bool playerInZone;
	Inventory inv;
	UIManager uim;
	
	void Awake()
	{
		player = GameObject.Find ("Player1");
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
	}
	
	void Update()
	{
		PickMeUp();		
	}
	
	public void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == player)
		{
			playerInZone = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		if(other.gameObject == player)
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
		else if (!inv.draggingItemBool && inv.HitObjectCheck() && Input.GetButtonDown("LMB"))
		{
			if(item.itemName != "Bloodspill")
			{
				if(inv.addItemIfEmpty(item))
				{
					Destroy(this.gameObject);
				}				
			}

		}
		else if(uim.HotkeyPress() >= 0 && inv.Items[uim.HotkeyPress()].itemName == "Mop")
		{
			Destroy(this.gameObject);
			Debug.Log("Clean up in Aisle 7");
		}
		else if(Input.GetButtonDown("LMB") && inv.draggedItem.itemName == "Mop")
		{
			Destroy(this.gameObject);
			Debug.Log("Clean up in Aisle 7");
		}

	}
	
}
