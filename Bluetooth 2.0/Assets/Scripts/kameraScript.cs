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


	public GameObject finalPanel;
	public Text totalPointsText;

	static public float totalPoints;

	static public float lumisotaPisteet;
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

	public Transform[] spawnpoints;

	public void maalitauluSpawn()
	{
		int[] spawnint = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
		string test = (Random.Range(0, spawnint.Length).ToString());

		GameObject taulu = Instantiate(maalitaulu) as GameObject;
		taulu.transform.SetParent(GameObject.FindGameObjectWithTag(test).transform, false);

	}


	// Use this for initialization
	void Start()
	{
		InvokeRepeating("maalitauluSpawn", 0.5f, 2f);

		
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
		lumisotaPisteetText.text = "Score: " + lumisotaPisteet.ToString();

	}

	// Update is called once per frame
	void Update()
	{
	
		if (canCount == false)
		{
			Destroy(GameObject.FindGameObjectWithTag("maalitaulu"));
		}

		if (timer >= 0.0f && canCount)
		{
			
			timer -= Time.deltaTime;
			timerText.text = "Time Left: " + timer.ToString("F0");
		}

		if (timer <= 0.0f && aikaLoppu == true)
		{
			Debug.Log("Aika loppu");
			Debug.Log(timer);
			totalPoints = totalPoints + lumisotaPisteet;
			aikaLoppu = false;
			totalPointsText.text = totalPoints.ToString("F0");
			finalPanel.SetActive(true);
			lumisotaPeli.SetActive(false);
			lumisotaPanel.SetActive(false);
			
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
		finalPanel.SetActive(false);
		playPaneeli.SetActive(true);
		canCount = false;
		aikaLoppu = true;
		mainTimer = 10f;
		lumisotaPisteet = 0;
		totalPoints = 0;
		lumisotaPisteetText.text = "Score: " + lumisotaPisteet.ToString();
		timer = mainTimer;
		GameObject.Find("Pelaaja").GetComponent<hyppyScript>().Restart();
		//GameObject.Find("Pelaaja2").GetComponent<lumilautaScript>().Restart();
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
		playPaneeli.SetActive(false);
		restartButton.SetActive(true);
		menuNappi.SetActive(true);
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
		
	}

	public void lumisotaPeliAlkaa()
	{
		lumisotaReadyPanel.SetActive(false);
		lumisotaPeli.SetActive(true);
		canCount = true;
	}

	public void maalitauluunOsuttu()
	{
		Debug.Log("Lisää niitä pisteitä boi");
		lumisotaPisteet = lumisotaPisteet + 50;
		lumisotaPisteetText.text = "Score: " + lumisotaPisteet.ToString(); 
	}
	
	public void takaisinMenuun()
	{
		transform.position = new Vector3(990, 817, 35);
		menuNappi.SetActive(true);
		playPaneeli.SetActive(true);
		searchMenu.SetActive(true);
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
