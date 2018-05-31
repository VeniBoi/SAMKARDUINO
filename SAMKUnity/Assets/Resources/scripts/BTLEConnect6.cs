using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BTLEConnect6 : MonoBehaviour
{
    public GameObject ShoulderR, ShoulderL, ElbowR, ElbowL, Sensor1Button;
    public Text Sup, TestTextL, TestTextR;
    public QuaternionFunctions3 QF3;
    private int nameCount, maxDevices, autoLockOnStart, valittuLiike, connectedAmount;
    private string Address, Address1, Address2, Address3, Address4, nameHelp, _serviceUUID, _characteristicUUID;
    public bool lockQuat;
    public string[] UUIDs = new string[] { "4569D41F-36CA-4289-F22B-AE2C03321AC3" }; //String array for the sensors' serviceUUID's
    private Quaternion mQuaternionL, mQuaternionR, mLockQuaternionL, mLockQuaternionR, quaternion1, quaternion2, quaternion3, quaternion4;
    private float limbAngle1, limbAngle2, limbAngle3, limbAngle4, zAxisEulerL, zAxisEulerR;
    private Vector3 mEulerAngleL, mEulerAngleR;

    //public GameObject statusLight;
    public RawImage statusImageSensor1;
    public RawImage statusImageSensor2;
    public Texture redLight;
    public Texture orangeLight;
    public Texture greenLight;

    bool isSensor1Subscribed = false;
    bool isSensor3Subscribed = false;

    static byte[] seonsor2msgArrayData;
    static byte[] seonsor1msgArrayData;



    //Gets called when starting the game
    void Start()
    {
        Address1 = "00:07:80:B7:09:8E";                                 //Physical address for the GREEN sensor (L)
        Address3 = "00:07:80:B7:05:55";                                 //Physical address for the YELLOW sensor (R)

        //Temp (sensor testing using BLACK sensor) - To be removed
        //Address1 = "00:07:80:B7:05:66";


        Address2 = "00:07:80:B7:05:66";                                 //Physical address for the BLACK sensor
        Address4 = "00:07:80:B7:09:8F";                                 //Physical address for the BLACK sensor with less tape
        _serviceUUID = "4569d41f-36ca-4289-f22b-ae2c03321ac3";          //Service UUID for the sensors
        _characteristicUUID = "a4a69e93-2dbc-c4d9-d567-628e84241d3d";   //Characteristic UUID for the sensors
        maxDevices = 2;                                                 //Maximum number of devices specisfied
        nameHelp = "Premius";                                           //Used to make the sensor searching easier, the names start with the word 'Premius'
        lockQuat = false;
        autoLockOnStart = 1;
        valittuLiike = 1;
        ToInitialize();                                                 //Initializing bluetooth as a central device


        //Debug.Log("Start() - sensor subscribed = false");
        isSensor1Subscribed = false;
        isSensor3Subscribed = false;


        
    }

    //Gets called once per frame
    void Update()
    {


        // Test if sensors already subscribed
        if (seonsor2msgArrayData == null)
        {
            //TestTextL.text = "sensor2(R) NULL message";
        }
        else
        {
            //TestTextL.text = handleMessage_Premius(seonsor2msgArrayData).ToString();
            Sensor2DataToTransformArm(seonsor2msgArrayData);
        }


        if (seonsor1msgArrayData == null)
        {
            //TestTextL.text = "sensor2(L) NULL message";
        }
        else
        {
            //TestTextL.text = handleMessage_Premius(seonsor2msgArrayData).ToString();
            Sensor1DataToTransformArm(seonsor1msgArrayData);
        }


    }

    //This function is used to scan and find possible sensors to connect to
    public void ToScanPeripherals()
    {
        //ToInitialize();
        Sup.text = "Scanning...";

        statusImageSensor1.texture = redLight;
        statusImageSensor2.texture = redLight;

        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(UUIDs, (address, name) =>   //Start scanning for sensors
        {
            if (name.Contains(nameHelp))     //Looking for if the found name has the word 'Premius' in it
            {
                nameCount++;
                if (nameCount >= maxDevices)                     //If the amount of devices is equal to maximum specified,
                {
                    ToStopScanning();                            //stop scanning for more
                    StartCoroutine(ALittleBreak());
                }
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

        statusImageSensor1.texture = redLight;
        statusImageSensor2.texture = redLight;
    }

    //These functions connect to sensors
    public IEnumerator ToConnect1()
    {
        Sup.text = "Connecting to sensor 1...";

        statusImageSensor1.texture = orangeLight;
        

        BluetoothLEHardwareInterface.ConnectToPeripheral(Address1, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 1.";
        }, (address) =>
        {
        });
        yield return null;
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
    public IEnumerator ToConnect3()
    {
        Sup.text = "Connecting to sensor 3...";

        statusImageSensor2.texture = orangeLight;

        BluetoothLEHardwareInterface.ConnectToPeripheral(Address3, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 3.";
        }, (address) =>
        {
        });
        yield return null;
    }
    public IEnumerator ToConnect4()
    {
        Sup.text = "Connecting to sensor 4...";
        BluetoothLEHardwareInterface.ConnectToPeripheral(Address4, (address) =>
        {
        }, (address, serviceUUID) =>
        {
        }, (address, serviceUUID, characteristicUUID) =>
        {
            Sup.text = "Connected to sensor 4.";
        }, (address) =>
        {
        });
        yield return null;
    }

    //This function initializes the device's bluetooth module
    void ToInitialize()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () => { }, (error) => { });
    }

    //These functions are used to subscribe to the sensors' data
    IEnumerator SubscribeSensor1()
    {
        connectedAmount++;
        Debug.Log("Connected amount is:" + connectedAmount);
        Sup.text = "Subscribing to sensor 1...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address1, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            seonsor1msgArrayData = data;
            Sensor1DataToTransformArm(seonsor1msgArrayData);

            /*
            isSensor1Subscribed = true;
            Sup.text = "Subscribed to sensor 1.";
            statusImageSensor1.texture = greenLight;

            limbAngle1 = handleMessage_Premius(data);
            ShoulderR.transform.eulerAngles = new Vector3(0, 0, ( 90+limbAngle1));     //Insert the handled data to a joint (only Z-axis)

            // Save highest ROM during game play
            if ((limbAngle1 > GameControl.instance.maxRomAchievedLeft) && (GameControl.instance.workoutTimer > 0.0f))
            {
                GameControl.instance.maxRomAchievedRight = limbAngle1;
            }
            */

        });
        yield return null;
    }



    //*************************
    // PLAYER'S LEFT ARM
    //*************************
    void Sensor1DataToTransformArm(byte[] dataArray)
    {
        Sup.text = "Subscribed to sensor 1.";
        statusImageSensor1.texture = greenLight;

        limbAngle1 = handleMessage_Premius(dataArray);

        
        // Map maxROML to 180 degrees (for smaller arm movements)
        if (PlayerPrefs.HasKey("MapToFullROM") && PlayerPrefs.GetInt("MapToFullROM") == 1)
        {
            float maxROML = PlayerPrefs.GetFloat("ROMLSlider") * 5.0f;  // Note: slider value scaled by 5

            if (Mathf.Abs(limbAngle1) >= maxROML)
                ShoulderR.transform.eulerAngles = new Vector3(0, 0, (90 + (180.0f * -1)));
            else
            {
                float adjustedLimbAngle = (180.0f * limbAngle1) / maxROML;
                ShoulderR.transform.eulerAngles = new Vector3(0, 0, (90 + adjustedLimbAngle));
            }
            
        }
        // Physical arm position = Game arm position
        else
        {
            ShoulderR.transform.eulerAngles = new Vector3(0, 0, (90 + limbAngle1));     //Insert the handled data to a joint (only Z-axis)
        }


        TestTextL.text = "Left: " + -1*limbAngle1;

        // Note: LimbAngle1 is -ive due to mirroring -> Need to convert
        // Save highest ROM during game play
        if (((-1*limbAngle1) > GameControl.instance.maxRomAchievedLeft) && (GameControl.instance.workoutTimer > 0.0f))
        {
             GameControl.instance.maxRomAchievedLeft = -1*limbAngle1;
        }
    }


    IEnumerator SubscribeSensor2()
    {
        Sup.text = "Subscribing to sensor 2...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address2, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            Sup.text = "Subscribed to sensor 2.";
            //quaternion2 = handleMessage_Premius(data);                   //Transfer the received data to the function that handles it
            limbAngle2 = handleMessage_Premius(data);
            ElbowR.transform.eulerAngles = new Vector3(0, 0, limbAngle2);        //Insert the handled data to a joint (only Z-axis)
                       
        });
        yield return null;
    }

    
    IEnumerator SubscribeSensor3()
    {
        connectedAmount++;
        Debug.Log("Connected amount is:" + connectedAmount);
        Sup.text = "Subscribing to sensor 3...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address3, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
            seonsor2msgArrayData = data;
            Sensor2DataToTransformArm(seonsor2msgArrayData);

            /*
            isSensor3Subscribed = true;
            Sup.text = "Subscribed to sensor 3.";
            statusImageSensor2.texture = greenLight;

            limbAngle3 = handleMessage_Premius2(data);
            ShoulderL.transform.eulerAngles = new Vector3(0, 0, (270+limbAngle3));        //Insert the handled data to a joint (only Z-axis)

            // Save highest ROM during game play
            if ((limbAngle3 > GameControl.instance.maxRomAchievedRight) && (GameControl.instance.workoutTimer > 0.0f))
            {
                GameControl.instance.maxRomAchievedRight = limbAngle3;
            }
            */

        });
        yield return null;
    }

    //*************************
    // PLAYER'S RIGHT ARM
    //*************************
    void Sensor2DataToTransformArm(byte[] dataArray)
    {
        Sup.text = "Subscribed to sensor 3.";
        statusImageSensor2.texture = greenLight;

        limbAngle3 = handleMessage_Premius2(dataArray);

        // Map maxROML to 180 degrees (for smaller arm movements)
        if (PlayerPrefs.HasKey("MapToFullROM") && PlayerPrefs.GetInt("MapToFullROM") == 1)
        {
            float maxROML = PlayerPrefs.GetFloat("ROMRSlider") * 5.0f;  // Note: slider value scaled by 5

            if (Mathf.Abs(limbAngle3) >= maxROML)
                ShoulderL.transform.eulerAngles = new Vector3(0, 0, (270 + (180.0f)));
            else
            {
                float adjustedLimbAngle = (180.0f * limbAngle3) / maxROML;
                ShoulderL.transform.eulerAngles = new Vector3(0, 0, (270 + adjustedLimbAngle));
            }

        }
        // Physical arm position = Game arm position
        else
        {
            ShoulderL.transform.eulerAngles = new Vector3(0, 0, (270 + limbAngle3));     //Insert the handled data to a joint (only Z-axis)
        }



        //ShoulderL.transform.eulerAngles = new Vector3(0, 0, (270 + limbAngle3));        //Insert the handled data to a joint (only Z-axis)

        TestTextR.text = "Right: " + limbAngle3;
        // Save highest ROM during game play
        if ((limbAngle3 > GameControl.instance.maxRomAchievedRight) && (GameControl.instance.workoutTimer > 0.0f))
        {
            GameControl.instance.maxRomAchievedRight = limbAngle3;
        }
    }



    IEnumerator SubscribeSensor4()
    {
        connectedAmount++;
        Debug.Log("Connected amount is:" + connectedAmount);
        Sup.text = "Subscribing to sensor 4...";
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(Address4, _serviceUUID, _characteristicUUID, (deviceAddress, notification) =>
        {
        }, (deviceAddress2, characteristic, data) =>
        {
        Sup.text = "Subscribed to sensor 4.";
        mLockQuaternionR = handleMessage_PremiusLockAngle(data);
        });
        yield return null;
    }

    public void ToLockQuaternion()
    {
        StartCoroutine(LockQuaternion());
    }

    public IEnumerator LockQuaternion()
    {
        yield return new WaitForSeconds(5f);
        mLockQuaternionR = mQuaternionR;
        mLockQuaternionL = mQuaternionL;
        StopCoroutine(LockQuaternion());
    }

    //This function handles the information received from the sensor

    float handleMessage_Premius(byte[] msgArray)
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

        mQuaternionL = QF3.normalize(valueX, valueY, valueZ, valueW);     
        QF3.set(mQuaternionL);
        mEulerAngleL = mQuaternionL.eulerAngles - mLockQuaternionL.eulerAngles;
        float angle = QF3.getQuaternionAngle(mQuaternionL, mLockQuaternionL);
        Debug.Log("Angle value is: " + angle);
        zAxisEulerL = -mEulerAngleL.z;

        zAxisEulerL = AdjustAngleTo180Deg(zAxisEulerL);
        //TestTextL.text = "Left: " + zAxisEulerL;
        return zAxisEulerL;
    }

    float handleMessage_Premius2(byte[] msgArray)
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

        mQuaternionR = QF3.normalize(valueX, valueY, valueZ, valueW);
        QF3.set(mQuaternionR);
        mEulerAngleR = mQuaternionR.eulerAngles - mLockQuaternionR.eulerAngles;
        float angle = QF3.getQuaternionAngle(mQuaternionR, mLockQuaternionR);
        Debug.Log("Angle value is: " + angle);
        zAxisEulerR = -mEulerAngleR.z;

        zAxisEulerR = AdjustAngleTo180Deg(zAxisEulerR);
        //TestTextR.text = "Right: " + zAxisEulerR;
        return zAxisEulerR;
    }

    // Adjest given angle between +/- 180 degrees
    float AdjustAngleTo180Deg(float angle)
    {
        if (angle > 180.0f)
            angle -= 360.0f;
        else if (angle < -180.0f)
            angle += 360.0f;

        return angle;
    }


    public Quaternion handleMessage_PremiusLockAngle(byte[] msgArray)
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

        mQuaternionR = QF3.normalize(valueX, valueY, valueZ, valueW);     
        QF3.set(mQuaternionR);
        return mQuaternionR;
    }

    public void Select1()
    {
        //TIE THE SENSORS TO GAMEOBJECTS
        valittuLiike = 1;
    }

    //Handles the "automatic" connection by doing little pauses
    IEnumerator ALittleBreak()
    {
        Debug.Log("LEL");
        StartCoroutine(ToConnect1());
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(ToConnect3());
        //yield return new WaitForSecondsRealtime(2f);
        //StartCoroutine(ToConnect4());
        yield return new WaitForSecondsRealtime(3f);
        StartCoroutine(SubscribeSensor1());
        yield return new WaitForSecondsRealtime(5f);
        StartCoroutine(SubscribeSensor3());
        //yield return new WaitForSecondsRealtime(5f);
        //StartCoroutine(SubscribeSensor4());
    }
}