using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageScreenScript : MonoBehaviour {


	public GameObject LanguageScreen, DataButton, EasyConnectPanel, LanguageGameobject;
	
	public void English()
	{
		DataButton.SetActive(true);
		LanguageScreen.SetActive(false);
		EasyConnectPanel.SetActive(true);
	}

	public void Finnish()
	{
		LanguageScript.Lang = 1;
		LanguageGameobject.GetComponent<LanguageScript>().SetLanguage();
		DataButton.SetActive(true);
		LanguageScreen.SetActive(false);
		EasyConnectPanel.SetActive(true);
	}
}
