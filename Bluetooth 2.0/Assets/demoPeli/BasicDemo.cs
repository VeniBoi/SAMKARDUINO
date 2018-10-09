using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TechTweaking.Bluetooth;

public class BasicDemo : MonoBehaviour {

	static public bool onLiitytty;
	
	public Text Sensori0;
	public Text Sensori1;
	public Text Sensori2;
	public Text Sensori3;
	public Text Sensori4;
	public Text Sensori5;
	public Text Sensori6;
	public Text Sensori7;
	public Text Sensori8;

	static public int S0;
	static public int S1;
	static public int S2;
	static public int S3;
	static public int S4;
	static public int S5;
	static public int S6;
	static public int S7;
	static public int S8;

	
	

	private  BluetoothDevice device;
	public Text statusText;
	public Text nimiInput;
	public string arduinoData;
	static public string publicMAC;
	public Text makki2;

	static public int mappedValue0;
	static public int mappedValue1;
	static public int mappedValue2;
	static public int mappedValue3;
	static public int mappedValue4;
	static public int mappedValue5;
	static public int mappedValue6;
	static public int mappedValue7;
	static public int mappedValue8;

	public GameObject playPaneeli;
	public GameObject connectPaneeli;
	public GameObject hakuPaneeli, visualisationPanel;
	public GameObject restartNappi, dataPaneeli1, dataPaneeli2, easyConnectPanel;

	public bool onYhteys;

	public Text yhdistetaan;
	public Image connectImage;
	


	// Use this for initialization
	void Awake () {

		


		Screen.sleepTimeout = SleepTimeout.NeverSleep;      //ASETETAAN NÄYTTÖ NIIN ETTEI SE SAMMU!
		
		BluetoothAdapter.enableBluetooth();//Force Enabling Bluetooth


		device = new BluetoothDevice();

		/*
		 * We need to identefy the device either by its MAC Adress or Name (NOT BOTH! it will use only one of them to identefy your device).
		 *
		 *---------- MacAdress property
		 * Using the MAC adress is the best choice because the device doesn't have to be paired/bonded!
		 * 
		 * ----------Name property
		 * Identefy a device by its name using the Property 'BluetoothDevice.Name' require the remote device to be paired
		 * but you can try to alter the parameter 'allowDiscovery' of the Connect(int attempts, int time, bool allowDiscovery) method. 
		 * allowDiscovery will start a heavy discovery process (if the remote device weren't paired). This will take time 12 to 25 seconds.
		 * So it's better to use the 'BluetoothDevice.MacAdress' property. It doesn't need previuos pairing/bonding.
		 */



		//device.Name = "MATTIHIRVONEN";					//Haetaan nimi inputfieldistä. 
		//device.MacAddress = "98:D3:31:F5:29:55";

		/*
		 *  Note: The library will fill the properties device.Name and device.MacAdress with the right data after succesfully connecting.
		 * 
		 *  Moreover, any BluetoothDevice instance returned by a method or event of this library will have both properties (Name & MacAdress) filled with the right data
		 */


		//You might need th following:
		//this.device.UUID = UUID; //This is not required for HC-05/06 devices and many other electronic bluetooth modules.
		/*
		 * Quoting docs: A uuid is a Universally Unique Identifier (UUID) standardized 128-bit format for a string ID used to uniquely identify information. 
		 * It's used to uniquely identify your application's Bluetooth service.
		 * Check out getUUIDs(), if you don't know what UUID to use.
		 */
	}

	public void connect() {


		statusText.text = "Status: attempting to connect...";

		/*
		 * Notice that there're more than one connect() method, check out the docs to read about them.
		 * a simple device.connect() is equivalent to connect(3, 1000, false) which will make 3 connection attempts
		 * before failing completly, each attempt will cost at least 1 second = 1000 ms.
		 * -----------
		 * To alter that  check out the following methods in the docs :
		 * connect (int attempts, int time, bool allowDiscovery) 
		 * normal_connect (bool isBrutal, bool isSecure)
		 */
		device.connect();

	}

	public void disconnect() {
		device.close();
	}

	

	public void sendHello() {
		if (device != null) {
			/*
			 * Send and Read works only with bytes. You need to convert everything to bytes.
			 * Different devices with different encoding is the reason for this. You should know what encoding you're using.
			 * In the method call below I'm using the ASCII encoding to send "Hello" + a new line.
			 */
			device.send (System.Text.Encoding.ASCII.GetBytes ("Hello\n"));
		}
	}


	//############### Reading Data  #####################
	/* Please note that this way of reading is only used in this demo. All other demos use Coroutines(Unity offers many tutorials on Coroutines).
	 * Just to make things simple
	 */
	void Update() {
		
		//device.MacAddress = "98:D3:31:F5:29:55";

		if (device.IsReading) {

			
			byte [] msg = device.read ();
			


			if (msg != null ) {

				/* Send and read in this library use bytes. So you have to choose your own encoding.
				 * The reason is that different Systems (Android, Arduino for example) use different encoding.
				 */
				string content = System.Text.ASCIIEncoding.ASCII.GetString (msg);

				onYhteys = true;
				yhdistetaan.text = "Connected!";
				playPaneeliNakyy();
				arduinoData = content;

				string str = arduinoData;               //Asetetaan muuttujan str arvo (Eli laitetaan sille arvoksi se mitä arduino lähettää)
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

				S0 = A0;  //Asetetaan näille muuttujille arraysta saadut arvot. (Helpompi käsitellä) vvv
				S1 = A1;
				S2 = A2;
				S3 = A3;
				S4 = A4;
				S5 = A5;
				S6 = A6;
				S7 = A7;
				S8 = A8;

				mappedValue0 = (((S0 - 0) * (255 - 0)) / (99 - 0)) + 0;
				mappedValue1 = (((S1 - 0) * (255 - 0)) / (99 - 0)) + 0;
				mappedValue2 = (((S2 - 0) * (255 - 0)) / (99 - 0)) + 0;
				mappedValue3 = (((S3 - 0) * (255 - 0)) / (99 - 0)) + 0;
				mappedValue4 = (((S4 - 0) * (255 - 0)) / (99 - 0)) + 0;				//Tämä muuttaa arvon max 99 --> max 255.
				mappedValue5 = (((S5 - 0) * (255 - 0)) / (99 - 0)) + 0;				//Käytetään muuttamaan väriarvoja.
				mappedValue6 = (((S6 - 0) * (255 - 0)) / (99 - 0)) + 0;
				mappedValue7 = (((S7 - 0) * (255 - 0)) / (99 - 0)) + 0;
				mappedValue8 = (((S8 - 0) * (255 - 0)) / (99 - 0)) + 0;
				

				//setString();
				
			}		
		}
	}

	/*void setString()
	{
		
		Sensori0.text = "Sensori0: " + S0.ToString();
		Sensori1.text = "Sensori1: " + S1.ToString();
		Sensori2.text = "Sensori2: " + S2.ToString();
		Sensori3.text = "Sensori3: " + S3.ToString();				//Asettaa editorissa setettujen tekstien arvon. (funktio kutsutaan update funktiossa).
		Sensori4.text = "Sensori4: " + S4.ToString();
		Sensori5.text = "Sensori5: " + S5.ToString();
		Sensori6.text = "Sensori6: " + S6.ToString();
		Sensori7.text = "Sensori7: " + S7.ToString();
		Sensori8.text = "Sensori8: " + S8.ToString();
	}
	*/

	public void Sammutus()
	{
		Application.Quit();
	}
	
	public void btHaku2()
	{
		StartCoroutine(odotus());
	}

	public void hakuTakas()
	{
		connectPaneeli.SetActive(false);
		playPaneeli.SetActive(false);
		hakuPaneeli.SetActive(true);
	}

	IEnumerator odotus()
	{
		device.MacAddress = BtDiscovery.makki;
		yield return new WaitForSeconds(0.5f);
		if(LanguageScript.Lang == 1)
		{
			yhdistetaan.text = "Yhdistetään: " + device.MacAddress;
		}
		else
		{
			yhdistetaan.text = "Connecting to: " + device.MacAddress;
		}
		connect();
	}

	public void playPaneeliNakyy()
	{
		GetComponent<BtDiscovery>().enabled = false;
		connectPaneeli.SetActive(false);
		playPaneeli.SetActive(true);
	}
}




//VVVV------------------------------TÄLLÄ SAADAAN AUTOMAATTINEN YHTEYS---------------------------------VVVV

/*
 * void Awake ()
	{
		device = new BluetoothDevice ();

		if (BluetoothAdapter.isBluetoothEnabled ()) {
			connect ();
		} else {

			//BluetoothAdapter.enableBluetooth(); //you can by this force enabling Bluetooth without asking the user
			statusText.text = "Status : Please enable your Bluetooth";

			BluetoothAdapter.OnBluetoothStateChanged += HandleOnBluetoothStateChanged;
			BluetoothAdapter.listenToBluetoothState (); // if you want to listen to the following two events  OnBluetoothOFF or OnBluetoothON

			BluetoothAdapter.askEnableBluetooth ();//Ask user to enable Bluetooth

		}
	}
	*/