using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadOnClick : MonoBehaviour {
	
	public GameObject loadingImage;
	
	public void LoadScene(string name)
	{
		loadingImage.SetActive(true);
		LoadScene(name);
	}



	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}