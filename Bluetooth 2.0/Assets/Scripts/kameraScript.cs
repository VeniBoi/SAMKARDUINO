using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kameraScript : MonoBehaviour
{
	public float transitionDuration = 2.5f;
	public Transform target;
	public GameObject laskuri;
	public GameObject playPaneeli;
	public GameObject restartButton;
	public GameObject seuraavatasoPaneeli;
	public GameObject lumilautaPisteet;
	public GameObject menuNappi;
	public GameObject searchMenu;
	public GameObject lumilautaReady;
	public GameObject lumisotaPanel;
	public GameObject lumisotaPeli;
	public GameObject lumisotaReadyPanel;
	public GameObject maalitaulu;
	public GameObject restartPanel;
	public GameObject panel2, seuraavaSceneLumilautailu;
	public GameObject oikeaylaRestart, datanappi, visualisaatioNappiBack, visualisaatioSetit;
	public GameObject SkipNappi1, SkipNappi2;


	public GameObject finalPanel;
	public Text totalPointsText;

	static public int totalPoints;

	static public int lumisotaPisteet;
	public Text lumisotaPisteetText;


	public Transform player;
	public Transform playerLumi;
	public Vector3 offset;
	public Vector3 stoppiOffset;

	static public bool playPainettu;
	public bool kameraKohdalla;
	static public bool pelaajaPaikalla;
	static public bool lumilautaKohdalla;
	static public bool seuraavatasoPainettu;

	public Text timerText;
	public float mainTimer;
	public bool aikaLoppu;

	public float timer;
	public bool canCount;

	public float spawnTime;

	public Transform[] spawnpoints;

	public void maalitauluSpawn()
	{
		int[] spawnint = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
		string test = (Random.Range(0, spawnint.Length).ToString());

		GameObject taulu = Instantiate(maalitaulu) as GameObject;
		taulu.transform.SetParent(GameObject.FindGameObjectWithTag(test).transform, false);

	}

	public void timerChange(float newSpawnTime)
	{
		spawnTime = newSpawnTime;
	}

	public void aloitaSpawnaus()
	{
		spawnTime = 2f;
		InvokeRepeating("maalitauluSpawn", 0.1f, spawnTime);
	}

	public void lopetaSpawnaus()
	{
		CancelInvoke();
	}

	// Use this for initialization
	void Start()
	{
		
		timer = mainTimer;
		aikaLoppu = true;
		canCount = false;
		

		lumisotaPisteet = 0;
		totalPoints = 0;
		//Paneeli2.GetComponent<Image>().enabled =  false;
		seuraavatasoPainettu = false;
		lumilautaKohdalla = false;
		kameraKohdalla = false;
		playPainettu = false;
		laskuri.SetActive(false);
		if (LanguageScript.Lang == 1)
		{
			lumisotaPisteetText.text = "Pisteet: " + lumisotaPisteet.ToString();
		}
		else
		{
			lumisotaPisteetText.text = "Score: " + lumisotaPisteet.ToString();
		}
		

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown("p"))
		{
			totalPoints = totalPoints + 10;
		}

		if (canCount == false)
		{
			Destroy(GameObject.FindGameObjectWithTag("maalitaulu"));
		}

		if (timer >= 0.0f && canCount)
		{
			
			timer -= Time.deltaTime;
			if(LanguageScript.Lang == 1)
			{
				timerText.text = "Aika: " + timer.ToString("F0");
			}
			else
			{
				timerText.text = "Time Left: " + timer.ToString("F0");
			}
			
		}

		if (timer <= 0.0f && aikaLoppu == true)
		{
			totalPoints = totalPoints + lumisotaPisteet;
			aikaLoppu = false;
			totalPointsText.text = totalPoints.ToString("F0");
			finalPanel.SetActive(true);
			lumisotaPeli.SetActive(false);
			lumisotaPanel.SetActive(false);
			lopetaSpawnaus();
		}

	
		/*else if (timer <= 0.0f && !doOnce)
		{
			canCount = false;
			doOnce = true;
			timerText.text = "0.00";
			timer = 0.0f;
		}
		*/


		if (kameraKohdalla == true)
		{
			transform.position = player.position + offset;
		}

		if (lumilautaKohdalla == true)
		{
			Quaternion.Euler(15, 0, 0);
			transform.position = playerLumi.position + offset;
		}

		/*if (BasicDemo.S0 == 0 && BasicDemo.S1 == 0 && BasicDemo.S2 == 0 && BasicDemo.S3 == 0 && BasicDemo.S4 == 0 && BasicDemo.S5 == 0 && BasicDemo.S6 == 0 && BasicDemo.S7 == 0 && BasicDemo.S8 == 0)
		{
			hyppyScript.pelaajaValmis = true;
			//Paneeli2.SetActive(false);
		}

		*/
		/*if (hyppyScript.pelaajaValmis == true)
		{
			
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
		}
		*/
	}


	public void aloitaAlusta()
	{
		restartPanel.SetActive(true);
		Time.timeScale = 0f;
	}

	public void restartYes()
	{
			lumilautaKohdalla = false;
			kameraKohdalla = false;
			transform.position = new Vector3(990, 817, 35);
			finalPanel.SetActive(false);
			playPaneeli.SetActive(true);
			canCount = false;
			aikaLoppu = true;
			mainTimer = 30f;
			lumisotaPisteet = 0;
			totalPoints = 0;
			SkipNappi1.SetActive(false);
			SkipNappi2.SetActive(false);
			hyppyScript.hyppyPisteet = 0;
			if(LanguageScript.Lang == 1)
			{
			lumisotaPisteetText.text = "Pisteet: " + lumisotaPisteet.ToString();
			}
			else
			{
			lumisotaPisteetText.text = "Score: " + lumisotaPisteet.ToString();
			}
			timer = mainTimer;
			GameObject.Find("Pelaaja").GetComponent<hyppyScript>().Restart();
			Time.timeScale = 1f;
			restartPanel.SetActive(false);
			datanappi.SetActive(true);
			panel2.SetActive(false);
			seuraavaSceneLumilautailu.SetActive(false);
			restartButton.SetActive(false);
			//GameObject.Find("Pelaaja2").GetComponent<lumilautaScript>().Restart();

	}

	public void restartNo()
	{
		restartPanel.SetActive(false);
		Time.timeScale = 1f;
	}

	public void sammutus()
	{
		Application.Quit();
	}


	public void liikutus()
	{
		//Paneeli2.GetComponent<Image>().enabled = false;
		hyppyScript.restartBool = true;
		playPainettu = true;
		
		if (playPainettu ==  true)
		{
			StartCoroutine(Transition());
			pelaajaPaikalla = true;
		}
		
	}

	public void paneelit()
	{
		hyppyScript.hyppyPisteet = 0;
		lumisotaPisteet = 0;
		totalPoints = 0;
		playPaneeli.SetActive(false);
		restartButton.SetActive(true);
		panel2.SetActive(true);
		menuNappi.SetActive(true);
		datanappi.SetActive(false);
		SkipNappi1.SetActive(true);

	}


	public void kameraKaannos()
	{
		StartCoroutine(TransitionLaskeuduttu());
	}
	
	public void lumilautaTasoon()
	{
		seuraavatasoPaneeli.SetActive(false);
		lumilautaKohdalla = true;
		kameraKohdalla = false;
		seuraavatasoPainettu = true;
		lumilautaPisteet.SetActive(true);
		lumilautaReady.SetActive(true);
	}

	public void lumisotaTasoon()
	{
		lumilautaKohdalla = false;
		transform.position = new Vector3(990, 817, 35);
		lumisotaPanel.SetActive(false);
		lumisotaReadyPanel.SetActive(true);
		kameraKohdalla = false;
		
	}

	public void lumisotaPeliAlkaa()
	{
		lumisotaReadyPanel.SetActive(false);
		lumisotaPeli.SetActive(true);
		canCount = true;
		oikeaylaRestart.SetActive(false);
	}

	public void maalitauluunOsuttu()
	{
		
		lumisotaPisteet = lumisotaPisteet + 50;
		if(LanguageScript.Lang == 1)
		{
			lumisotaPisteetText.text = "Pisteet: " + lumisotaPisteet.ToString();
		}
		else
		{
			lumisotaPisteetText.text = "Score: " + lumisotaPisteet.ToString();
		}
		
	}
	
	public void takaisinMenuun()
	{
		transform.position = new Vector3(990, 817, 35);
		menuNappi.SetActive(true);
		playPaneeli.SetActive(true);
		searchMenu.SetActive(true);
	}

	public void Visualisaatioon()
	{
		transform.position = new Vector3(-7f, 21f, -53.9f);
		Camera.main.transform.rotation = Quaternion.Euler(25.5f, 28.8f, 0f);
		playPaneeli.SetActive(false);
		visualisaatioNappiBack.SetActive(true);
		datanappi.SetActive(false);
		visualisaatioSetit.SetActive(true);
	}

	public void PoisVisualisaatiosta()
	{
		transform.position = new Vector3(1102f, 1121f, -201f);
		Camera.main.transform.rotation = Quaternion.Euler(25.5f, 0f, 0f);
		playPaneeli.SetActive(true);
		visualisaatioNappiBack.SetActive(false);
		datanappi.SetActive(true);
		visualisaatioSetit.SetActive(false);
	}

	IEnumerator Transition()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);


			transform.position = Vector3.Lerp(startingPos, target.position, t);

			kameraKohdalla = true;
			
			
			yield return 0;
		}
	}

	IEnumerator TransitionLaskeuduttu()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		Quaternion startingRot = transform.rotation;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / transitionDuration);

			transform.position = player.position + offset;

			transform.position = Vector3.Lerp(startingPos, player.position + stoppiOffset, t);
			transform.rotation = Quaternion.Lerp(startingRot, player.rotation , 0);
			

			yield return 0;
		}
	}

	IEnumerator valmis()
	{

		yield return new WaitForSeconds(0.7f);

		laskuri.SetActive(true);
		
		pelaajaScript.peliAlkanut = true;

	}

	IEnumerator valmisAnalyysi()
	{
		yield return new WaitForSeconds(0.7f);
		
		

	}
}
