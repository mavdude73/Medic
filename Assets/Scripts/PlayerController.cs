using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float fixationSpeed;
	public GameObject hitGameobject;
	GameObject sprite;
	public Vector3 playerVector3;
	public bool itemOnCursor;
	private SluiceItems sluiceItems;
	private Pharmacy pharmacy;
	private PCTerminal pcTerminal;
	private Laboratory laboratory;
	private Inventory inv;
	private PatientZone patientZone;
	private PatientInvestigations patientInvestigations;
	private PatientTreatment patientTreatment;
	private DroppedItem droppedItem;
	private GameObject[] slotManager;

	void Start()
	{
		sluiceItems = new SluiceItems();
		inv =  GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory> ();
		slotManager = GameObject.FindGameObjectsWithTag("Slot");
		sprite = transform.GetChild(0).gameObject;
	}

	void FixedUpdate()
	{
		
	}
	
	void Update()
	{
		PlayerMovement();
		MouseDirection();
		Raycasting();
		LMBInteractions();	
		RMBInteractions();	
	}


	private void Raycasting ()
	{
		hitGameobject = null;

		if (Input.GetButtonDown("LMB"))
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
			Vector3 deltaPosition = (mousePosition - transform.position);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, deltaPosition, 1f);
			
			Debug.DrawLine(transform.position, hit.point);

			if(hit == false)
			{
				return;
			}
					
			if(hit.collider.gameObject == null)
			{
				return;
			}

			if(hit.collider.gameObject != null)
			{
				hitGameobject = hit.collider.gameObject;
				Debug.Log(hitGameobject);

				sluiceItems.ItemPickup(hitGameobject.name);

				if(hitGameobject.GetComponent<Laboratory>())
				{
					hitGameobject.GetComponent<Laboratory>().CheckForSamples(hitGameobject, true, -1);
				}

				if(hitGameobject.GetComponent<PCTerminal>())
				{
					hitGameobject.GetComponent<PCTerminal>().OpenComputerScreen(hitGameobject);
				}

				if(hitGameobject.GetComponent<Pharmacy>())
				{
					hitGameobject.GetComponent<Pharmacy>().OpenPharmacyScreen(hitGameobject);
				}

				if(hitGameobject.GetComponent<PatientZone>())
				{
					hitGameobject.GetComponent<PatientZone>().OpenMedicalRecord(hitGameobject, itemOnCursor);
					hitGameobject.GetComponent<PatientInvestigations>().ObtainBloodSample(hitGameobject, true, -1);
					hitGameobject.GetComponent<PatientTreatment>().AdministerTreatment(hitGameobject, true, -1);
				}

				if(hitGameobject.GetComponent<DroppedItem>())
				{
					hitGameobject.GetComponent<DroppedItem>().PickUpItem(hitGameobject, true, -1);
				}

				return;
			}
		}
	}


	public void OnTriggerStay2D(Collider2D other)
	{
		if(HotkeyPress() >= 0)
		{
			if(other.gameObject.GetComponent<PatientZone>())
			{
				Debug.Log("ping");
				other.gameObject.GetComponent<PatientInvestigations>().ObtainBloodSample(other.gameObject, false, HotkeyPress());
				other.gameObject.GetComponent<PatientTreatment>().AdministerTreatment(other.gameObject, false, HotkeyPress());
			}

			if(other.gameObject.GetComponent<Laboratory>())
			{
				other.gameObject.GetComponent<Laboratory>().CheckForSamples(other.gameObject, false, HotkeyPress());
			}

			if(other.gameObject.GetComponent<DroppedItem>())
			{
				other.gameObject.GetComponent<DroppedItem>().PickUpItem(other.gameObject, false, HotkeyPress());
			}
		}
	}


	void LMBInteractions()
	{
		if(Input.GetButtonDown("LMB"))
		{
			foreach(GameObject sM in slotManager)
			{
				sM.GetComponent<SlotManager>().ClickOnHotbar("left", itemOnCursor);
			}
		}
	}


	void RMBInteractions()
	{
		if(Input.GetButtonDown("RMB"))
		{
			foreach(GameObject sM in slotManager)
			{
				sM.GetComponent<SlotManager>().ClickOnHotbar("right", itemOnCursor);
			}
		}
	}



	private KeyCode[] keyCodes =
	{
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
	};

	private int HotkeyPress ()
	{
		for(int i = 0 ; i < inv.Items.Count; i ++ )
		{
			if(Input.GetKeyDown(keyCodes[i]))
			{
				return i;
			}
		}
		return -1;
	}


	
	void SpriteOrientation(int degree)
	{
		sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, Quaternion.Euler(0f, 0f, degree), fixationSpeed);
	}
	
	void MouseDirection ()
	{
		float moveHorizonal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
//		if(moveVertical == 0 && moveHorizonal == 0)
//		{
//			var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			Vector3 deltaPosition = (mousePosition - sprite.transform.position);  
//			float atan2 = Mathf.Atan2 (deltaPosition.y, deltaPosition.x);
//			sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 90), fixationSpeed);
//			sprite.transform.eulerAngles = new Vector3 (0, 0, sprite.transform.eulerAngles.z);
//		}
//		
		
		if(moveHorizonal > 0)  // D
		{
			if(moveVertical == 0)
			{
				SpriteOrientation(90);
			}
			else if(moveVertical < 0) // W + D
			{
				SpriteOrientation(45);
			}
			else if(moveVertical > 0) // S + D
			{
				SpriteOrientation(135);
			}
		}
		else if(moveHorizonal < 0) // A
		{
			if(moveVertical == 0) 
			{
				SpriteOrientation(270);
			}
			else if(moveVertical < 0) // A + W
			{
				SpriteOrientation(315); 
			}
			else if(moveVertical > 0) // A + S
			{
				SpriteOrientation(225);
			}
		}
		else if(moveVertical > 0) // W
		{
			SpriteOrientation(180);
		}
		else if(moveVertical < 0) // S
		{
			SpriteOrientation(0);
		}
		
				
	}
	
	void PlayerMovement()
	{
		if(Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
		{
			transform.Translate(-Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed * 0.8f, Space.Self);
			transform.Translate(-Vector2.up * Input.GetAxis("Vertical") * Time.deltaTime * speed * 0.8f, Space.Self);
		}
		else 
		{
			transform.Translate(-Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed, Space.Self);
			transform.Translate(-Vector2.up * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.Self);
		}
	}


}