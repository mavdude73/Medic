using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float fixationSpeed;
	public GameObject sprite;


	void FixedUpdate()
	{
		
	}
	
	void Update()
	{
		PlayerMovement();
		MouseDirection();
	}
	
	
	void SpriteRotation(int degree)
	{
		sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, Quaternion.Euler(0f, 0f, degree), fixationSpeed);
	}
	
	void MouseDirection ()
	{
		float moveHorizonal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		if(moveVertical == 0 && moveHorizonal == 0)
		{
			var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 deltaPosition = (mousePosition - sprite.transform.position);  
			float atan2 = Mathf.Atan2 (deltaPosition.y, deltaPosition.x);
			sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg + 90), fixationSpeed);
			sprite.transform.eulerAngles = new Vector3 (0, 0, sprite.transform.eulerAngles.z);
		}
		
		
		if(moveHorizonal > 0)
		{
			SpriteRotation(90);
		}
		else if(moveHorizonal < 0)
		{
			SpriteRotation(270);
		}
		else if(moveVertical > 0)
		{
			SpriteRotation(180);
		}
		else if(moveVertical < 0)
		{
			SpriteRotation(0);
		}
		
				
	}
	
	void PlayerMovement()
	{
		transform.Translate(-Vector2.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed, Space.Self);
		transform.Translate(-Vector2.up * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.Self);
	}





}