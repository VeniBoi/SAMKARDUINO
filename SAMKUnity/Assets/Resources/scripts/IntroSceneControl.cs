using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneControl : MonoBehaviour {

    // Instance for IntroSceneControl
    public static IntroSceneControl introInstance;         //A reference to our game control script so we can access it statically.

    //public Camera myCamera;
    public GameObject goalieHead;
    public float shake = 0.0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private float ShakeY = 0.0f;
    private float ShakeX = 0.0f;
    private float ShakeYSpeed = 0.01f;
    private float ShakeXSpeed = 0.01f;

    void Awake()
    {
        //If we don't currently have a game control
        if (introInstance == null)
        {
            //...set this one to be it...
            //DontDestroyOnLoad(gameObject);
            introInstance = this;
        }
        //...otherwise...
        else if (introInstance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*
        if (shake > 0)
        {
            myCamera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0.0f;
        }
        */


        Vector2 _newPosition = new Vector2(ShakeX, ShakeY);
        if (ShakeY < 0)
        {
            ShakeY *= ShakeYSpeed;
            ShakeX *= ShakeXSpeed;
        }
        ShakeY = -ShakeY;
        ShakeX = -ShakeX;
        goalieHead.transform.Translate(_newPosition, Space.Self);

    }

    public void setShake(float someX, float someY)
    {
        ShakeX = someX;
        ShakeY = someY;
    }


    public void StartButtonClicked()
    {
        SceneManager.LoadScene("Hockey");
    }

}
