using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float fixationSpeed;
	public string rayHitobject;
	public GameObject sprite;
	public GameObject player1;
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

	void Start()
	{
		sluiceItems = new SluiceItems();
		pharmacy = GameObject.Find("Pharmacy").GetComponent<Pharmacy>();
		pcTerminal = GameObject.Find("PCTerminal").GetComponent<PCTerminal>();
		laboratory = GameObject.Find("Laboratory").GetComponent<Laboratory>();
		inv =  GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory> ();
	}

	void FixedUpdate()
	{
		
	}
	
	void Update()
	{
		PlayerMovement();
		MouseDirection();
		playerVector3 = transform.position;
		Raycasting();		
	}
	

	private void Raycasting ()
	{
		rayHitobject = null;

		if (Input.GetButtonDown("LMB"))
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
			Vector3 deltaPosition = (mousePosition - playerVector3);

			RaycastHit2D hit = Physics2D.Raycast (playerVector3, deltaPosition, 1f);
			
			Debug.DrawLine(playerVector3, hit.point);

			if(hit == false)
			{
				return;
			}
					
			if(hit.collider.gameObject.name == null)
			{
				return;
			}

			if(hit.collider.gameObject.name != null)
			{
				rayHitobject = hit.collider.gameObject.name;
				Debug.Log(rayHitobject);

				sluiceItems.ItemPickup(hit.collider.gameObject.name);
				pharmacy.OpenPharmacyScreen(hit.collider.gameObject.name);
				pcTerminal.OpenComputerScreen(hit.collider.gameObject.name);
				laboratory.CheckForSamples(true, true, -1);

				if(hit.collider.gameObject.name.Contains("Patient"))
				{
					patientZone = GameObject.Find(hit.collider.gameObject.name).GetComponent<PatientZone>();
					patientZone.OpenMedicalRecord(itemOnCursor, hit.collider.gameObject.name);

					patientInvestigations = GameObject.Find(hit.collider.gameObject.name).GetComponent<PatientInvestigations>();
					patientInvestigations.ObtainBloodSample(true, true, -1);

					patientTreatment = GameObject.Find(hit.collider.gameObject.name).GetComponent<PatientTreatment>();
					patientTreatment.AdministerTreatment(true, true, -1);
				}

				if(GameObject.Find(hit.collider.gameObject.name).GetComponent<DroppedItem>())
				{
					droppedItem = GameObject.Find(hit.collider.gameObject.name).GetComponent<DroppedItem>();
					droppedItem.PickUpItem(hit.collider.gameObject.name, true, -1);
				}

				return;
			}
		}


	}


	public void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.name.Contains("ThePatient"))
		{
			patientInvestigations = GameObject.Find(other.gameObject.name).GetComponentInParent<PatientInvestigations>();
			if(HotkeyPress() >= 0)
			{
				patientInvestigations.ObtainBloodSample(true, false, HotkeyPress());
			}

			patientTreatment = GameObject.Find(other.gameObject.name).GetComponentInParent<PatientTreatment>();
			if(HotkeyPress() >= 0)
			{
				patientTreatment.AdministerTreatment(true, false, HotkeyPress());
			}
		}

		if(other.gameObject.name == "Lab" && HotkeyPress() >= 0)
		{
			laboratory.CheckForSamples(true, false, HotkeyPress());
		}

		if(other.gameObject.name.Contains("spill"))
		{
			droppedItem = GameObject.Find(other.gameObject.name).GetComponent<DroppedItem>();
			if(HotkeyPress() >= 0)
			{
				droppedItem.PickUpItem(other.gameObject.name, false, HotkeyPress());
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