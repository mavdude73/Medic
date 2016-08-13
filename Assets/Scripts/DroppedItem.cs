using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroppedItem : MonoBehaviour {

	public Item item;
	GameObject player;
	public bool playerInZone;
	public bool hitByRaycast = false;
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
//		PickMeUp();	
		HitSpecificObject("Syringe");

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
	
	RaycastHit2D HitObjectCheck()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
		Vector3 deltaPosition = (mousePosition - pc.playerVector3);

		RaycastHit2D hit = Physics2D.Raycast (pc.playerVector3, deltaPosition, 2f);
		
		Debug.DrawLine(pc.playerVector3, hit.point);
		return hit;	
		
	}
	
	void HitSpecificObject(string objectName)
	{
		if(HitObjectCheck())
		{
//			Debug.Log(HitObjectCheck().collider.gameObject.name);
//			if(HitObjectCheck().collider.gameObject.GetComponent<DroppedItem>().item.floorID == item.floorID)
//			{
//				Debug.Log("ping");
//				hitByRaycast = true;
//				return;
//
//			}
		}
		hitByRaycast = false;
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
