using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kavelyTestiScript : MonoBehaviour {

	public Text kierrosText, aikaText, initiateText, finishedText, finalTimerAjallaText, runningText, readyToGoText, standupText;
	public GameObject testiIkkuna, playPaneeli, testinAikaisetTekstit, alkuTekstit, runningimage;
	public GameObject finalScreen;
	public float timer;

	static public bool testiPlayPainettu = false;
	bool canCount = false;
	bool testStarted = true;
	bool testiKaynnissa, kierros0, kierros1, kierros2, kierros3,testinappiPainettu;

	public static float testValue1, testValue2, testValue3, testValuesAdded;
	private int testiKierros;


	public void testialkaaNappi()
	{
		testinappiPainettu = true;
		readyToGoText.enabled = true;
		standupText.enabled = true;
	}
	// Use this for initialization
	void Start () {
		testinappiPainettu = false;
		testiKaynnissa = false;
		readyToGoText.enabled = false;
		standupText.enabled = false;
		testiKierros = 1;
		finishedText.text = "";
		initiateText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
		

		if (testinappiPainettu && testiKaynnissa && testiPlayPainettu && /*Input.GetKeyDown("space")*/ BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			runningimage.SetActive(true);
			runningText.enabled = true;
			kierrosText.enabled = true;
			if(LanguageScript.Lang == 1)
			{
				kierrosText.text = "Koe Sykli no." + testiKierros;
			}
			else
			{
				kierrosText.text = "Test Cycle no." + testiKierros;
			}
			aikaText.enabled = false;
			finishedText.enabled = false;
			canCount = true;
			testStarted = true;
			alkuTekstit.SetActive(false);
			testinAikaisetTekstit.SetActive(true);
			testinappiPainettu = false;
		}

		if (canCount)
		{
			timer += Time.deltaTime;
			
		}

		if (testiKaynnissa && canCount && testiKierros == 1 && /*Input.GetKeyDown("a")*/ BasicDemo.S3 > 30 && BasicDemo.S1 > 30 )
		{
			runningimage.SetActive(false);
			runningText.enabled = false;
			kierrosText.enabled = false;
			finishedText.enabled = true;
			if (LanguageScript.Lang == 1)
			{
				finishedText.text = "Päätetty Sykli no." + testiKierros;
			}
			else
			{
				finishedText.text = "Finished Cycle no." + testiKierros;
			}
			canCount = false;
			testiKierros++;
			testValue1 = timer;
			timer = 0;
			Debug.Log("Eka kierros ohi " + testValue1);
			aikaText.enabled = true;
			if (LanguageScript.Lang == 1)
			{
				aikaText.text = "Aika: " + testValue1.ToString("F0") + "s";
			}
			else
			{
				aikaText.text = "Time: " + testValue1.ToString("F0") + "s";
			}
			testinappiPainettu = true;
		}

		if (testiKaynnissa && canCount && testiKierros == 2 && /*Input.GetKeyDown("a")*/ BasicDemo.S3 > 30 && BasicDemo.S1 > 30)
		{
			runningimage.SetActive(false);
			runningText.enabled = false;
			kierrosText.enabled = false;
			finishedText.enabled = true;
			if (LanguageScript.Lang == 1)
			{
				finishedText.text = "Päätetty Sykli no." + testiKierros;
			}
			else
			{
				finishedText.text = "Finished Cycle no." + testiKierros;
			}
			canCount = false;
			testiKierros++;
			testValue2 = timer;
			timer = 0;
			Debug.Log("Toka kierros ohi " + testValue2);
			aikaText.enabled = true;
			if (LanguageScript.Lang == 1)
			{
				aikaText.text = "Aika: " + testValue2.ToString("F0") + "s";
			}
			else
			{
				aikaText.text = "Time: " + testValue2.ToString("F0") + "s";
			}
			testinappiPainettu = true;
		}

		if (testiKaynnissa && canCount && testiKierros == 3 && /*Input.GetKeyDown("a")*/ BasicDemo.S3 > 30 && BasicDemo.S1 > 30)
		{
			canCount = false;
			testValue3 = timer;
			timer = 0;
			Debug.Log("kolmas kierros ohi " + testValue3);
			testValuesAdded = (testValue1 + testValue2 + testValue3) / 3;
			Debug.Log("Keskiarvo ja lopullinen tulos: " + testValuesAdded);
			finalTimerAjallaText.text = testValuesAdded.ToString("F2") + "s";
			finalScreen.SetActive(true);
			testinAikaisetTekstit.SetActive(false);
			testiKaynnissa = false;
			testinappiPainettu = true;
		}

	}

	public void takaisinAlkuun()
	{
		testValue1 = 0;
		testValue2 = 0;
		testValue3 = 0;
		testValuesAdded = 0;
		testiKierros = 1;
		timer = 0;

		testiIkkuna.SetActive(false);
		finalScreen.SetActive(false);
		playPaneeli.SetActive(true);
		alkuTekstit.SetActive(true);
		standupText.enabled = false;
		readyToGoText.enabled = false;
	}

	public void testiin()
	{
		testiKaynnissa = true;
		playPaneeli.SetActive(false);
		testiIkkuna.SetActive(true);
		testiPlayPainettu = true;
	}
}
