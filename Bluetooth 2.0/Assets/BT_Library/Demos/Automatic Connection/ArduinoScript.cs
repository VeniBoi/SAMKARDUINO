
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO.Ports;
using System.IO;
using UnityEngine.UI;



public class ArduinoScript : MonoBehaviour
{
	public Text Sensori0;
	public Text Sensori1;
	public Text Sensori2;
	public Text Sensori3;
	public Text Sensori4;
	public Text Sensori5;
	public Text Sensori6;
	public Text Sensori7;
	public Text Sensori8;

	public int S0;
	public int S1;
	public int S2;
	public int S3;
	public int S4;
	public int S5;
	public int S6;
	public int S7;
	public int S8;

	public Color fullColor;
	public Color lowColor;

	public GameObject kuutio1;

	public string message;

	public int testi = 20;
	


	//SerialPort sp = new SerialPort("\\\\.\\COM7", 9600);    //Avataan uusi serialportti (Saattaa muuttua bluetoothin kanssa(?))

	// Use this for initialization
	void Start()
	{

		/*string[] ports = SerialPort.GetPortNames();

		foreach (string port in ports)
		{                                               // <<--- Tässä kohtaa haetaan kaikkien porttien nimet ja laitetaan ne
			Debug.Log(port);                            //		  arrayhin talteen.
		}
		sp.Open();                                      //Asetetaan serialportti auki, jotta data tulee perille
		sp.ReadTimeout = 100;                           //Pieni Timeoutti ettei Unity kaadu

	*/

	}

	// Update is called once per frame
	void Update()
	{



		//if (sp.IsOpen)      //Jos serialportti on auki niin ---->
		{


			//message = sp.ReadLine();        //Asetetaan muuttujan "message" arvoksi funktio, joka lukee arduinon lähettämän datan


			//--------------------------HUOM!!!! ReadLine, EI ReadByte!! Koska data lähetetään stringinä.------------------------------



			string str = message;               //Asetetaan muuttujan str arvo (Eli laitetaan sille arvoksi se mitä arduino lähettää)
			string[] minmax = str.Split(',');   //Otetaan kahden arvon välistä pilkku pois
			int A0 = int.Parse(minmax[0]);      //Muutetaan saatu stringi integeriksi
			int A1 = int.Parse(minmax[1]);      //Sama tässä
			int A2 = int.Parse(minmax[2]);
			int A3 = int.Parse(minmax[3]);
			int A4 = int.Parse(minmax[4]);
			int A5 = int.Parse(minmax[5]);
			int A6 = int.Parse(minmax[6]);
			int A7 = int.Parse(minmax[7]);
			int A8 = int.Parse(minmax[8]);


			//				TÄSSÄ KOHTAA SAATUJA ARVOJA ON KAKSI JOITA VOIDAAN KÄYTTÄÄ!!
			//			SAATUJA ARVOJA HAETAAN TOISISTA SCRIPTEISTÄ NIIDEN NIMILLÄ, ELI NILLE ANNETUILLA ARVOILLA!
			//	KATSO ALEMPAA "vihrea" & "speed"! MUUTTUJALLA VIHREA ON TOISESSA SCRIPTISSÄ HAETTU NOPEUS PYÖRIMISELLE (VIHREÄ KUUTIO)
			//---------------------------------------------------------------------------------------------------------------------------


			S0 = A0;  //Asetetaan näille muuttujille arraysta saadut arvot. (Helpompi käsitellä) vvv
			S1 = A1;
			S2 = A2;
			S3 = A3;
			S4 = A4;
			S5 = A5;
			S6 = A6;
			S7 = A7;
			S8 = A8;
			


		}

		
		
		transform.Rotate(Vector3.up, S7 * Time.deltaTime); //Pyörintä kuutiolle
		SetCountText();
	}

	void SetCountText()
	{
		Sensori0.text = " " + S0.ToString();
		Sensori1.text = " " + S1.ToString();
		Sensori2.text = " " + S2.ToString();
		Sensori3.text = " " + S3.ToString();
		Sensori4.text = " " + S4.ToString();
		Sensori5.text = " " + S5.ToString();
		Sensori6.text = " " + S6.ToString();
		Sensori7.text = " " + S7.ToString();
		Sensori8.text = " " + S8.ToString();
	}
}

