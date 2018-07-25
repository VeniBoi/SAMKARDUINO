using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageScript : MonoBehaviour {

	public static int Lang = 0;

	//If Lang=0 -> English
	//If Lang=1 -> Finnish

	//SearchMenu
	[SerializeField]
	Text Search, NoDevices;

	//ConnectingMenu
	[SerializeField]
	Text Back;

	//PlayMenu
	[SerializeField]
	Text Slow1, Slow2, Fast1, Fast2, Slowmotion, SpawnTime, SnowSpeed, Test, Play, Visualize;

	//PeliScreens
	[SerializeField]
	Text Restart;

	//DataScreen
	[SerializeField]
	Text InstructionsText1, InstructionsText2, InstructionsText3, InstructionsText4, InstructionsButton, KierrosText, RunningText, FinishedText, FinalTimerText, EnterNameText, EnterName, Save, DataBack;

	//DataEmail
	[SerializeField]
	Text EnterEmail, EnterNamePatient, EmailExample, NameExample, Send, Delete, BackBtn, FileChosen, VisualBack;

	//Standup 
	[SerializeField]
	Text standup1, standup2, lumisotaready, lumilautaready, lumisotanappi;

	//Finalscene
	[SerializeField]
	Text finalScore, topScores, yourName, savebutton, placeholder, Skip1, Skip2;

	//Walkingscript
	[SerializeField]
	Text BestTimeText;

	public void SetLanguage()
	{
		if (Lang == 1)
		{
			//Search
			Search.text = "Etsi";
			NoDevices.text = "Ei laitteita";
			Back.text = "Takaisin";

			//Play
			Slow1.text = "Hidas";
			Slow2.text = "Hidas";
			Fast1.text = "Nopea";
			Fast2.text = "Nopea";
			Slowmotion.text = "Mäkihypyn hidastus";
			SpawnTime.text = "Taulujen ilmestymisaika";
			SnowSpeed.text = "Lumilaudan nopeus";
			Test.text = "Mene Kokeeseen";
			Play.text = "Mene Peliin";
			Visualize.text = "Visualisoi istuminen";
			Restart.text = "Alkuun";

			//DataScreen
			InstructionsText1.text = "KOKEEN OHJEET";
			InstructionsText2.text = "-Nouse ylös tuolista, kävele merkin ympäri\n (asetetaan 6 metrin päähän) ja istu takaisin alas.\n\n -Toistetaan 3 kertaa.\n\n -Joka kerralla koe aloitetaan nousemalla seisomaan.\n\n -Tulokset näyetään kokeen loputtua.\n\n -Näet näytöltä kun koe on ohi.\n\n !!HUOM!!\n\n HENKIÖN TARVITSEE ISTUA TUOLISSA KUN KOE ALOITETAAN!";
			InstructionsText3.text = "Nouse seisomaan aloittaaksesi kokeen.";
			InstructionsText4.text = "Valmis";
			InstructionsButton.text = "Valmis?";
			KierrosText.text = "Koe Sykli no.1";
			RunningText.text = "Käynnissä";
			FinishedText.text = "Koe ohi!";
			FinalTimerText.text = "Keskiverto Aika Kokeista";
			EnterNameText.text = "Aseta nimi Ja Paina Tallenna";
			Save.text = "Tallenna";
			EnterName.text = "Kirjoita Nimi...";
			DataBack.text = "Takaisin";

			EnterEmail.text = "Anna sähköposti johon haluat tiedoston lähetettävän";
			EnterNamePatient.text = "Anna henkilön nimi";
			EmailExample.text = "Esimerkki@esimerkki.com";
			NameExample.text = "Erkki Esimerkki";
			Send.text = "Lähetä";
			Delete.text = "Poista";
			BackBtn.text = "Menu";
			FileChosen.text = "Tiedosto valittu: -";
			VisualBack.text = "Takaisin";

			standup1.text = "Nouse seisomaan aloittaaksesi pelin";
			standup2.text = "Nouse seisomaan aloittaaksesi pelin";
			lumilautaready.text = "Seuraava Taso";
			lumisotaready.text = "Seuraava Taso";
			lumisotanappi.text = "Kosketa ruutua aloittaaksesi seuraava taso";

			topScores.text = "Parhaat tulokset:";
			finalScore.text = "Pisteet:";
			yourName.text = "Kirjoita nimesi ja paina tallenna";
			savebutton.text = "Tallenna";
			placeholder.text = "Erkki Esimerkki";
			Skip1.text = "Ohita";
			Skip2.text = "Ohita";

			BestTimeText.text = "Paras Aika Kokeista";

		}
		else
		{

		}
	}

	// Use this for initialization
	void Start () {
		
	


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
