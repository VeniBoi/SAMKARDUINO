using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BTLEConnect6 : MonoBehaviour
{
    public GameObject ShoulderR, ShoulderL, ElbowR, ElbowL, Sensor1Button;
    public Text Sup;
    public QuaternionFunctions3 QF3;
    private int nameCount, maxDevices, autoLockOnStart, valittuLiike;
    private string Address, Address1, Address2, Address3, Address4, nameHelp, _serviceUUID, _characteristicUUID;
    public bool lockQuat;
    public string[] UUIDs = new string[] { "4569D41F-36CA-4289-F22B-AE2C03321AC3" }; //String array for the sensors' serviceUUID's
    private Quaternion mQuaternion, mLockQuaternion, quaternion1, quaternion2, quaternion3, quaternion4;

    //Gets called when starting the game
    void Start()
    {
        Address1 = "00:07:80:B7:09:8E";                                 //Physical address for the GREEN sensor
        Address2 = "00:07:80:B7:05:55";                                 //Physical address for the YELLOW sensor
        Address3 = "00:07:80:B7:05:66";                                 //Physical address for the BLACK sensor
        Address4 = "00:07:80:B7:09:8F";                                 //Physical address for the BLACK sensor with less tape
        _serviceUUID = "4569d41f-36ca-4289-f22b-ae2c03321ac3";          //Service UUID for the sensors
        _characteristicUUID = "a4a69e93-2dbc-c4d9-d567-628e84241d3d";   //Characteristic UUID for the sensors
        maxDevices = 4;                                                 //Maximum number of devices specisfied
        nameHelp = "Premius";    //Used to make the sensor searching easier, the names start with the word 'Premius'
        lockQuat = false;
        autoLockOnStart = 1;
        valittuLiike = 1;
        ToInitialize();          //Initializing bluetooth as a central device
    }

    //Gets called once per frame
    void Update()
    {

    }

    //This function is used to scan and find possible sensors to connect to
    public void ToScanPeripherals()
    {
        Sup.text = "Scanning...";
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(UUIDs, (address, name) =>   //Start scanning for sensors
        {
            if (name.Contains(nameHelp))     //Looking for if the found name has the word 'Premius' in it
            {
                //Sensor1Button.SetActive(true);
                nameCount++;
                if (nameCount >= maxDevices)                     //If the amount of devices is equal to maximum specified,
                    ToStopScanning();                           //stop scanning for more
            }
        }, (address, name, rssi, advertisingInfo) =>
        {
        });
    }

    //This function stops scanning for sensors
    public void ToStopScanning()
    {
        BluetoothLEHardwareInterface.StopScan();
        Sup.text = "Not scanning";
    }

    //This function connects to a sensor
    public void ToConnect1()
    {
        Sup.text = "Connecting to sensor 1...";
        BluetoothLEHardwareInterface.ConnectToPeripheral(Address1, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 1.";
            StartCoroutine(SubscribeSensor1());                                 //Subscribes the sensor(=get data from it)
        }, (address) =>
        {
        });
    }
    public void ToConnect2()
    {
        Sup.text = "Connecting to sensor 2...";
        BluetoothLEHardwareInterface.ConnectToPeripheral(Address2, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 2.";
            StartCoroutine(SubscribeSensor2());                                 //Subscribes the sensor(=get data from it)
        }, (address) =>
        {
        });
    }
    public void ToConnect3()
    {
        Sup.text = "Connecting to sensor 3...";
        BluetoothLEHardwareInterface.ConnectToPeripheral(Address3, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 3.";
            StartCoroutine(SubscribeSensor3());                                 //Subscribes the sensor(=get data from it)
        }, (address) =>
        {
        });
    }
    public void ToConnect4()
    {
        Sup.text = "Connecting to sensor 4...";
        BluetoothLEHardwareInterface.ConnectToPeripheral(Address4, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 4.";
            StartCoroutine(SubscribeSensor4());                                 //Subscribes the sensor(=get data from it)
        }, (address) =>
        {
        });
    }

    //This function initializes the device's bluetooth module
    void ToInitialize()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () => { }, (error) => { });
    }

    //This function is used to subscribe to the sensors data
    IEnumerator SubscribeSensor1()
    {
        Sup.text = "Subscribing to sensor 1...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address1, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            Sup.text = "Subscribed to sensor 1.";
            quaternion1 = handleMessage_Premius(data);                   //Transfer the received data to the function that handles it
            ShoulderR.transform.eulerAngles = new Vector3(0, 0, quaternion1.eulerAngles.z);     //Insert the handled data to a joint (only Z-axis)
        });
        yield return null;
    }
    IEnumerator SubscribeSensor2()
    {
        Sup.text = "Subscribing to sensor 2...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address2, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            Sup.text = "Subscribed to sensor 2.";
            quaternion2 = handleMessage_Premius(data);                   //Transfer the received data to the function that handles it
            ElbowR.transform.eulerAngles = new Vector3(0, 0, quaternion2.eulerAngles.z);        //Insert the handled data to a joint (only Z-axis)
        });
        yield return null;
    }
    IEnumerator SubscribeSensor3()
    {
        Sup.text = "Subscribing to sensor 3...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address3, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            Sup.text = "Subscribed to sensor 3.";
            quaternion3 = handleMessage_Premius(data);                   //Transfer the received data to the function that handles it
            ShoulderL.transform.eulerAngles = new Vector3(0, 0, quaternion3.eulerAngles.z);        //Insert the handled data to a joint (only Z-axis)

        });
        yield return null;
    }
    IEnumerator SubscribeSensor4()
    {
        Sup.text = "Subscribing to sensor 4...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address4, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            Sup.text = "Subscribed to sensor 4.";
            quaternion4 = handleMessage_Premius(data);                   //Transfer the received data to the function that handles it
            ElbowL.transform.eulerAngles = new Vector3(0, 0, quaternion4.eulerAngles.z);        //Insert the handled data to a joint (only Z-axis)
        });
        yield return null;
    }



    //This function handles the information received from the sensor

    Quaternion handleMessage_Premius(byte[] msgArray)
    {


        //Luodaan taulukko numeroarvoille.
        //Create an array for numbers
        int[] numbers = new int[9];
        for (int i = 0; i < msgArray.Length; i++)
        {
            numbers[i] = (int)msgArray[i];
        }

        //Käydään läpi jokainen msg arvo ja muutetaan bytestä unsigned intiksi.
        //Go through each message and convert from byte to unsigned int
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] < 0)
            {
                foreach (byte byteValue in msgArray)
                {
                    numbers[i] = System.Convert.ToInt32(byteValue);
                }
            }
        }

        int system = (numbers[0] & 192) >> 6;
        int gyro = (numbers[0] & 48) >> 4;
        int acc = (numbers[0] & 12) >> 2;
        int mag = (numbers[0] & 3);

        //Get quaternion data
        long w = ((numbers[2] << 8) & 65280 | numbers[1]);
        long x = ((numbers[4] << 8) & 65280 | numbers[3]);
        long y = ((numbers[6] << 8) & 65280 | numbers[5]);
        long z = ((numbers[8] << 8) & 65280 | numbers[7]);

        int signW = numbers[2] & 192;
        int signX = numbers[4] & 192;
        int signY = numbers[6] & 192;
        int signZ = numbers[8] & 192;

        //If negative, swap 0-> 1 and 1 -> 0
        if (signW != 0)
        {
            w = ~w;
            w = 65535 + w;
        }
        if (signX != 0)
        {
            x = ~x;
            x = 65535 + x;
        }
        if (signY != 0)
        {
            y = ~y;
            y = 65535 + y;
        }
        if (signZ != 0)
        {
            z = ~z;
            z = 65535 + z;
        }

        //Scale
        float scale = 1.0f / 16384;

        //Scale values
        float valueW = w * scale;
        float valueX = x * scale;
        float valueY = y * scale;
        float valueZ = z * scale;

        //Swap sign if needed
        if (signW != 0) { valueW *= -1; }
        if (signX != 0) { valueX *= -1; }
        if (signY != 0) { valueY *= -1; }
        if (signZ != 0) { valueZ *= -1; }

        mQuaternion = QF3.normalize(valueW, valueX, valueY, valueZ);     //New test
        QF3.set(mQuaternion);
        return mQuaternion;
        if (!lockQuat)
        {
            mLockQuaternion = mQuaternion;
            autoLockOnStart += 1;
            if (autoLockOnStart > 15)
            {
                lockQuat = true;
            }
        }
        else
        {
            float angle = QF3.getQuaternionAngle(mQuaternion, mLockQuaternion);
            QF3.countRotations_Premius(angle);
        }
    }

    public void Select1()
    {
        //TIE THE SENSORS TO GAMEOBJECTS
        valittuLiike = 1;
    }
}