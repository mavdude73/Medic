using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroppedItem : MonoBehaviour {

	public Item item;
	GameObject player;
	public bool playerInZone;
	public string thisGameobjectname;
	Inventory inv;
	UIManager uim;
	PlayerController pc;
	
	void Awake()
	{
		player = GameObject.Find ("Player1");
		pc = GameObject.Find ("Player1").GetComponent<PlayerController>();
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
		thisGameobjectname = this.gameObject.name;

		if (!pc.itemOnCursor && Input.GetButtonDown("LMB"))
		{
			if(thisGameobjectname == pc.rayHitobject && item.itemName != "Bloodspill")
			{
				if(inv.AddItemIfEmpty(item))
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
