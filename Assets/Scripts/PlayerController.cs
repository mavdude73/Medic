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
	
	
	void SpriteOrientation(int degree)
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