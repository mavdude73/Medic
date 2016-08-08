using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropBox : MonoBehaviour, IPointerDownHandler {

	Inventory inv;
	GameObject player;


	void Start ()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		player = GameObject.Find("Player1");
		
	}
	
	void Update ()
	{
		
	}

	

	public void OnPointerDown(PointerEventData data)
	{
		if(inv.draggingItemBool)
		{
			DropItem(inv.draggedItem);
			inv.closeDraggedItem();
		}
	}
	
	void DropItem(Item item)
	{
		GameObject itemAsGameObject = (GameObject)Instantiate(item.itemModel, player.transform.position, Quaternion.identity);
		itemAsGameObject.GetComponent<DroppedItem>().item = item;
		itemAsGameObject.GetComponentInChildren<SpriteRenderer>().sprite = item.itemIcon;
	}

}
