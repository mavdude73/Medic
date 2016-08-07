using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {

	public int slotNumber;
	public Item item;
	Image itemImage;
	Inventory inv;


	void Start ()
	{
		inv = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		itemImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
	}
	
	void Update ()
	{
		SlotUpdater ();
	}

	void SlotUpdater()
	{
		if (inv.Items[slotNumber].itemName != null)
		{
			item = inv.Items[slotNumber];
			itemImage.enabled = true;
			itemImage.sprite = inv.Items[slotNumber].itemIcon;
		}
		else
		{
			itemImage.enabled = false;
		}
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (inv.Items[slotNumber].itemName != null)
		{
			Debug.Log (transform.name);
			Debug.Log (item.itemDesc);
		}
	}
	public void OnPointerEnter(PointerEventData data)
	{
//		Debug.Log ("Entered");
	}



}
