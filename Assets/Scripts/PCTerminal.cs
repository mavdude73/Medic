using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PCTerminal : MonoBehaviour {

	
	UIManager uim;


	void Awake ()
	{
		uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager> ();
	}
	
	public void OpenComputerScreen(string name)
	{
		if (name == "PCTerminal" && !uim.computerScreen.activeSelf && !uim.medicalPages[0].activeSelf)
		{
			uim.computerScreen.SetActive(true);
		}

	}

	void CloseComputerScreen()
	{
		if (uim.computerScreen.activeSelf)
		{
			uim.computerScreen.SetActive(false);
		}
	}


	
	void Update()
	{
			
	}

	
}
