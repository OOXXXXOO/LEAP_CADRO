using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {
    public GameObject scrollview;
    void Start()
    {
        scrollview.SetActive(false);
    }
	

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
    public void Nextmenu()
    {
        scrollview.SetActive(true);
    }

    public void GotoAgainMainMenu()
    {
        scrollview.SetActive(false);
    }
}
