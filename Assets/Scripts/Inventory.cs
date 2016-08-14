using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {


	public List<GameObject> Slots = new List<GameObject> ();
	public List<Item> Items = new List<Item> ();
	public GameObject slots;
	public GameObject draggedItemGameobject;
	public Item draggedItem;
	public int draggedItemSlotOrigin;
	Transform floorItemTransform;
	public bool mouseOverHotbar = false;
	ItemDatabase db;
	GameObject player;
	PlayerController pc;
	int hotkey;	

	int floorIDCount = 0;

	public int slotCount = 1;


	void CreateHotbar()
	{
		int x = (-slotCount + 1) * 40;
		int slotAmount = 0;
//		for (int i = 0; i < 1; i++)
//		{}
		for(int k = 0; k < slotCount; k++)
		{
			GameObject slot = (GameObject)Instantiate(slots);
			Slots.Add(slot);
			Items.Add(new Item());
			slot.transform.SetParent(this.gameObject.transform, false);
			slot.name = "Slot" + k;
			slot.GetComponent<RectTransform>().localPosition = new Vector3(x,0,0);
			slot.GetComponent<SlotManager>().slotNumber = slotAmount;
			x = x + 80;
			slotAmount++;
		}
	}



	// Use this for initialization
	void Awake ()
		{
			player = GameObject.Find ("Player1");
			pc = GameObject.Find ("Player1").GetComponent<PlayerController>();
			floorItemTransform = GameObject.Find ("Flooritems").transform;
			db = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
			
			CreateHotbar ();
//			addItem (0);
		}

	void Update()
	{
		if(pc.itemOnCursor)
		{
			Vector3 posi = (Input.mousePosition - GameObject.FindGameObjectWithTag("UIManager").GetComponent<RectTransform>().localPosition);
			draggedItemGameobject.GetComponent<RectTransform>().localPosition = new Vector3 (posi.x + 15, posi.y - 15, posi.z);
		}
	}

	
	
	public RaycastHit2D HitObjectCheck()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
		Vector3 deltaPosition = (mousePosition - pc.playerVector3);

		RaycastHit2D hit = Physics2D.Raycast (pc.playerVector3, deltaPosition, 2f);
		
		Debug.DrawLine(pc.playerVector3, hit.point);
		return hit;	
		
	}
	
	public bool HitSpecificObject(string objectName)
	{
		if(HitObjectCheck())
		{
			if(HitObjectCheck().collider.gameObject.name == objectName)
			{
				return true;
			}
		}
		return false;
	}
	

	public void IsMouseOverHotbar(bool verdict)
	{
		if (verdict)
		{
			mouseOverHotbar = true;
		}
		else
		{
			mouseOverHotbar = false;
		}
	}
	
	
	public void ShowDraggedItem(Item item, int slotnumber)
	{
		draggedItemSlotOrigin = slotnumber;
		draggedItemGameobject.SetActive(true);
		draggedItem = item;
		pc.itemOnCursor = true;
		draggedItemGameobject.GetComponent<Image>().sprite = item.itemIcon;
	}
	
	public void CloseDraggedItem()
	{
		pc.itemOnCursor = false;
		draggedItemGameobject.SetActive(false);
		draggedItem = new Item();
	}
	
	public void DropItem()
	{
		if(!pc.itemOnCursor)
		{
			return;
		}
		else if(Input.GetButtonDown("RMB"))
		{
			
			if(draggedItem.itemType == "Sample")
			{
				floorIDCount++;

				Vector3 posi = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1f);
				
				GameObject itemAsGameObject = (GameObject)Instantiate(Resources.Load<GameObject>(draggedItem.itemName+"spill"), posi, Quaternion.identity);
				Item clonedItem = new Item(db.items[0].itemID, db.items[0].itemName, db.items[0].itemType, db.items[0].itemDesc, floorIDCount, db.items[0].itemObj);
				itemAsGameObject.GetComponent<DroppedItem>().item = clonedItem;
				itemAsGameObject.transform.SetParent(floorItemTransform, false);
				itemAsGameObject.name = clonedItem.itemName+clonedItem.floorID;
				CloseDraggedItem();
			}
			else
			{
				floorIDCount++;

				Vector3 posi = new Vector3(player.transform.position.x + 0.75f, player.transform.position.y);
				GameObject itemAsGameObject = (GameObject)Instantiate(draggedItem.itemModel, posi, Quaternion.identity);
				Item clonedItem = new Item(draggedItem.itemID, draggedItem.itemName, draggedItem.itemType, draggedItem.itemDesc, floorIDCount, draggedItem.itemObj);
				itemAsGameObject.GetComponent<DroppedItem>().item = clonedItem;
				itemAsGameObject.GetComponentInChildren<SpriteRenderer>().sprite = clonedItem.itemIcon;
				itemAsGameObject.transform.SetParent(floorItemTransform, false);
				itemAsGameObject.name = clonedItem.itemName+clonedItem.floorID;
				CloseDraggedItem();
			}
		}
	}

	public bool CheckHasItem (int itemid)
	{
		for (int i = 0; i < Items.Count; i++)
		{
			if (Items [i].itemID == itemid)
			{
				return true;
			}

		}
		return false;
		
	}

	public Item PharmacyRequest(int itemid)
	{
		for (int i = 0; i < db.items.Count; i++)
		{
			if(db.items[i].itemID == itemid)
			{
				Item item = db.items[i];
				return item;
			}
		}
		return new Item();
	}
	
	public void AddItem(int itemid)
	{
		for (int i = 0; i < db.items.Count; i++)
		{
			if(db.items[i].itemID == itemid)
			{
				Item item = db.items[i];
				AddItemIfEmpty(item);
				break;
			}
		}
	}


	public bool AddItemIfEmpty(Item item)
	{
		for (int k = 0; k < Items.Count; k++)
		{
			if(Items[k].itemName == null)
			{
				Items[k] = item;
				return true;				
			}
		}
		return false;
	}

}
