using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TechTweaking.Bluetooth;

public class BtDiscovery : MonoBehaviour
{
	public GameObject scrollViewPaneeli;
	public GameObject searchPaneeli;
	public Text searchingText;
	public GameObject connectingPaneeli;


	static public string makki;
	static public string connectInfo;
	public GameObject refreshKuva;
	public bool pyorii;

	public Button deviceButton;
	public Text DeviceText;
	public Text devText;


	Dictionary<string,DeviceData> MacAddressToBluetoothDevice;
	class DeviceData
	{
		public BluetoothDevice device;
		public short RSSI;
		public Button button;
		
		public DeviceData (BluetoothDevice dev, short RSSI)
		{
			this.device = dev;
			this.RSSI = RSSI;
		}
	}



	void Start ()
	{
		pyorii = false;
		BluetoothAdapter.askEnableBluetooth ();

		MacAddressToBluetoothDevice = new Dictionary<string,DeviceData> ();
		BluetoothAdapter.OnDeviceDiscovered += HandleOnDeviceDiscovered;

	}

	void Update()
	{
		if (pyorii == true)
		{
			refreshKuva.transform.Rotate(Vector3.forward * Time.deltaTime * 150, Space.World);
		}

		if (pyorii == false)
		{
			refreshKuva.transform.Rotate(Vector3.forward * Time.deltaTime * 0, Space.World);
		}
	}

	void HandleOnDeviceDiscovered (BluetoothDevice dev, short rssi)
	{
		//Remember : dev != any other devices even if they're targeting the same device(because they're a connection container for the targeted device). 
		//to check if they target the same device check there MAC address
		DeviceData devData;
		if (!MacAddressToBluetoothDevice.ContainsKey (dev.MacAddress)) {
			devData = new DeviceData (dev, rssi);
			MacAddressToBluetoothDevice.Add (dev.MacAddress, devData);
			devData.button = addButton (dev.Name, dev.MacAddress, rssi.ToString ());

		} else {
			devData = MacAddressToBluetoothDevice [dev.MacAddress];
			devData.RSSI = rssi;
		}
		Text txt = devData.button.GetComponentInChildren<Text> ();
		txt.text = dev.Name;//dev.MacAddress + '\n' + dev.Name + '\n' + "RSSI : " + rssi;

		txt.color = new Color (Random.Range (1.0f, 1.0f), Random.Range (1.0f, 1.0f), Random.Range (1.0f, 1.0f));
	}

	public Button addButton (string name, string MacAdress, string RSSI)
	{
		if (MacAddressToBluetoothDevice.Count == 1) {
			deviceButton.interactable = true;
			deviceButton.name = MacAdress;
			
		} else {
			GameObject newButton = GameObject.Instantiate (deviceButton.gameObject) as GameObject;
			newButton.transform.SetParent (deviceButton.transform.parent, false);
			RectTransform rect = newButton.GetComponent<RectTransform> ();
			rect.position += Vector3.down * rect.sizeDelta.y;
			deviceButton = newButton.GetComponent<Button> ();
			deviceButton.name = MacAdress;

		}

		deviceButton.GetComponentInChildren<Text>().text = MacAdress + '\n' + name + '\n' + "RSSI : " + RSSI;

		return deviceButton;
	}

	public void OnButtonClicked ()
	{
		devText.text = "Hakeeko makkinimen?";
		string MAC = EventSystem.current.currentSelectedGameObject.name;
		devText.text = "MAC toString?";
		makki = MAC.ToString();
		devText.text = makki;
		//this.GetComponent<BasicDemo>().btHaku2();
		devText.text = "Meneekö yhdistykseen saakka?";
		StartCoroutine(demoScript());


	}

	public void startDiscovery ()
	{
		/*Discovery is about 15 to 25 seconds procedure*/
		//Since we need a refresh button behaviour. We will cancel any ongoind discovery to start again. like refresh behaviour.
		//BluetoothAdapter.cancelDiscovery();
		//BluetoothAdapter.startDiscovery ();
		//OR we can use refreshDiscovery(), which will just cancel then start Discovery again.
		//BluetoothAdapter.refreshDiscovery ();
		
		StartCoroutine(Haku());

	}

	void OnDestroy ()
	{
		BluetoothAdapter.OnDeviceDiscovered -= HandleOnDeviceDiscovered;
	}
	IEnumerator Haku()
	{
		BluetoothAdapter.refreshDiscovery();
		if(LanguageScript.Lang == 1)
		{
			searchingText.text = "Etsitään...";
		}
		else
		{
			searchingText.text = "Searching...";
		}
		pyorii = true;
		yield return new WaitForSeconds(10f);

		if (LanguageScript.Lang == 1)
		{
			searchingText.text = "Etsi";
		}
		else
		{
			searchingText.text = "Search";
		}
		pyorii = false;
		//searchPaneeli.SetActive(false);
		//scrollViewPaneeli.SetActive(true);


	}

	IEnumerator demoScript()
	{
		devText.text = "Eka kohta meni läpi";
		yield return new WaitForSeconds(0.5f);
		devText.text = "Toka kohta meni läpi?";
		scrollViewPaneeli.SetActive(false);
		connectingPaneeli.SetActive(true);
		this.GetComponent<BasicDemo>().btHaku2();


	}
}