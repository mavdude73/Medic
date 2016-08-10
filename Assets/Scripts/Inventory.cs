﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {


	public List<GameObject> Slots = new List<GameObject> ();
	public List<Item> Items = new List<Item> ();
	public GameObject slots;
	public GameObject draggedItemGameobject;
	public bool draggingItemBool = false;
	public Item draggedItem;
	public int draggedItemSlotOrigin;
	public Transform flooritemtransform;
	public PlayerController pc;
	ItemDatabase db;
	UIManager uim;
	GameObject player;
	int hotkey;	
//	UIManager uim;
//	int x = -160;
	public int slotCount = 1;
//	int x = 0;
//	int y = 0;

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
			db = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase>();
			uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
			
			CreateHotbar ();
//			addItem (0);
		}

	void Update()
	{
		if(draggingItemBool)
		{
			Vector3 posi = (Input.mousePosition - GameObject.FindGameObjectWithTag("UIManager").GetComponent<RectTransform>().localPosition);
			draggedItemGameobject.GetComponent<RectTransform>().localPosition = new Vector3 (posi.x + 15, posi.y - 15, posi.z);
		}
		HitObjectCheck();
	}

	
	
	public RaycastHit2D HitObjectCheck()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
		Vector3 deltaPosition = (mousePosition - pc.playerVector3);
		
		//		Ray ray = Camera.main.ScreenPointToRay (deltaPosition);
		
		//		Vector3 direction = new Vector3(ray.origin.x - pc.playerVector3.x, ray.origin.y - pc.playerVector3.y, pc.playerVector3.z);
		RaycastHit2D hit = Physics2D.Raycast (pc.playerVector3, deltaPosition, 0.9f);
		
		
		//		RaycastHit2D nohit = Physics2D.Raycast (new Vector3(0,0,0), new Vector3(0,0,0), 0f);
		
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
	

	
	
	
	public void showDraggedItem(Item item, int slotnumber)
	{
		draggedItemSlotOrigin = slotnumber;
		draggedItemGameobject.SetActive(true);
		draggedItem = item;
		draggingItemBool = true;
		draggedItemGameobject.GetComponent<Image>().sprite = item.itemIcon;
	}
	
	public void closeDraggedItem()
	{
		draggingItemBool = false;
		draggedItemGameobject.SetActive(false);
		draggedItem = new Item();
	}
	
	public void DropItem()
	{
		if(!draggingItemBool)
		{
			return;
		}
		else if(Input.GetButtonDown("RMB"))
		{
			
			if(draggedItem.itemType == "Sample")
			{
				Vector3 posi = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1f);
				
				GameObject itemAsGameObject = (GameObject)Instantiate(Resources.Load<GameObject>(draggedItem.itemName+"spill"), posi, Quaternion.identity);
				itemAsGameObject.GetComponent<DroppedItem>().item = db.items[0];
				itemAsGameObject.transform.SetParent(flooritemtransform, true);
				itemAsGameObject.name = draggedItem.itemName+"spill";
				closeDraggedItem();
			}
			else
			{
				Vector3 posi = new Vector3(player.transform.position.x + 0.75f, player.transform.position.y);
				
				GameObject itemAsGameObject = (GameObject)Instantiate(draggedItem.itemModel, posi, Quaternion.identity);
				itemAsGameObject.GetComponent<DroppedItem>().item = draggedItem;
				itemAsGameObject.GetComponentInChildren<SpriteRenderer>().sprite = draggedItem.itemIcon;
				itemAsGameObject.transform.SetParent(flooritemtransform, true);
				itemAsGameObject.name = draggedItem.itemName;
				closeDraggedItem();
			}
		}
	}

	public bool checkHasItem (int itemid)
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


	public void addItem(int itemid)
	{
		for (int i = 0; i < db.items.Count; i++)
		{
			if(db.items[i].itemID == itemid)
			{
				Item item = db.items[i];
				addItemIfEmpty(item);
				break;
			}
		}
	}


	public bool addItemIfEmpty(Item item)
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
	
	public void deleteItem0()
	{
		if(Items[uim.HotkeyPress()].itemName != null)
		{
			Destroy(Items[uim.HotkeyPress()].itemObj);
		}
		Items[uim.HotkeyPress()] = new Item();
	}
	

}
