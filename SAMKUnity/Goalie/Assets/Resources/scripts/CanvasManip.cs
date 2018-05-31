using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManip : MonoBehaviour {

    public Font myFont;
    public GameObject ShoulderRDemo, BreakPanel, Target;
    public Quaternion ShoulderRDemoLiike1, ShoulderRDemoLiikeMiddle, ShoulderRDemoLiike2, startPos;
    private int way, coroutineRundaa;
    public int scoreAmount;
    public Text myText, ROMLAmount, ROMRAmount, RepsAmount, SetsAmount, RecoveryTimeAmount, WeighingAmount, MaxTimeToTargetAmount, ScoreValue, BreakScoreText;
    public Slider ROMLSlider, ROMRSlider, RepsSlider, SetsSlider, RecoveryTimeSlider, WeighingSlider, MaxTimeToTargetSlider;

    // Use this for initialization
    void Start()
    {
        coroutineRundaa = 1;
        //ShoulderRDemoLiike1 = new Quaternion(0.6f, 0.4f, 0.7f, 0.3f);
        //ShoulderRDemoLiike2 = new Quaternion(-0.4f, 0.6f, -0.5f, 0.6f);
        ShoulderRDemoLiike1 = new Quaternion(0.0f, 0.0f, 0.6f, 0.8f);
        ShoulderRDemoLiikeMiddle = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        ShoulderRDemoLiike2= new Quaternion(0.0f, 0.0f, -0.4f, 0.9f);
        startPos = ShoulderRDemo.transform.rotation;
        /*GameObject newGO = new GameObject("myTextGO");
        newGO.transform.SetParent(this.transform);
        myText = newGO.AddComponent<Text>();
        myText.horizontalOverflow = HorizontalWrapMode.Overflow;
        myText.verticalOverflow = VerticalWrapMode.Overflow;
        myText.font = myFont;
        myText.fontSize = 32;
        myText.text = "Test text";
        myText.transform.position = new Vector3(700, 500, 1);*/
        way = 1;
    }

    // Update is called once per frame
    void Update ()
    {
        ScoreValue.text = scoreAmount.ToString();
        ROMLAmount.text = ROMLSlider.value.ToString() + "°";
        ROMRAmount.text = ROMRSlider.value.ToString() + "°";
        RepsAmount.text = RepsSlider.value.ToString();
        SetsAmount.text = SetsSlider.value.ToString();
        RecoveryTimeAmount.text = RecoveryTimeSlider.value.ToString();
        WeighingAmount.text = WeighingSlider.value.ToString();
        if (MaxTimeToTargetSlider.value < 20.0f)
        {
            MaxTimeToTargetAmount.fontSize = 16;
            MaxTimeToTargetAmount.text = MaxTimeToTargetSlider.value.ToString();
        }
        else
        {
            MaxTimeToTargetAmount.fontSize = 24;
            MaxTimeToTargetAmount.text = "∞";
        }
        //Debug.Log("Olkapaa oikea:" + ShoulderRDemo.transform.rotation.ToString());
        //Debug.Log(way);
        //ShoulderRDemo.transform.rotation = Quaternion.Lerp(ShoulderRDemoLiike1, ShoulderRDemoLiike2, 0.5f * Time.time);
        StartCoroutine(CoroutineLoopTest());
        if (scoreAmount == RepsSlider.value)
        {
            BreakPanel.SetActive(true);
            Target.SetActive(false);
            BreakScoreText.text = "Well done! You saved " + scoreAmount + " pucks! \n Next round begins in " + RecoveryTimeSlider.value.ToString() + " seconds!";
        }
    }

    public void HowToPlay()
    {
        //ShoulderRDemo.transform.rotation = ShoulderRDemoLiike1;
        if (way == 1)
        { StartCoroutine(ArmLerp(ShoulderRDemoLiike1, ShoulderRDemoLiikeMiddle, 1f)); }
        if (way == 2)
        { StartCoroutine(ArmLerp(ShoulderRDemoLiikeMiddle, ShoulderRDemoLiike2, 1f)); }
    }

    IEnumerator ArmLerp(Quaternion startPoint, Quaternion endPoint, float duration)
    {
        myText.text = "Move your hand smoothly \n between the two boxes";
        yield return new WaitForSeconds(2.0f);
        float timeRemaining = duration;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            ShoulderRDemo.transform.rotation = Quaternion.Lerp(startPoint, endPoint, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;

        }
        { way = 2; }
        if (way == 2)
        { StartCoroutine(ArmLerpPart2(ShoulderRDemoLiikeMiddle, ShoulderRDemoLiike2, 1f)); }
    }
    IEnumerator ArmLerpPart2(Quaternion startPoint, Quaternion endPoint, float duration)
    {
        float timeRemaining = duration;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            ShoulderRDemo.transform.rotation = Quaternion.Lerp(startPoint, endPoint, Mathf.InverseLerp(duration, 0, timeRemaining));
            yield return null;

        }
        { way = 3; }
        myText.text = "You get points for hitting \n the box and for holding \n the position for n seconds";
        //if (ShoulderRDemo.transform.rotation == ShoulderRDemoLiike2)
        //{ StartCoroutine(ArmLerp(ShoulderRDemoLiike1, ShoulderRDemoLiikeMiddle, 1f)); }
    }

    IEnumerator CoroutineLoopTest()
    {
        //Debug.Log("Looppaa" + Time.time);

        yield return new WaitForSeconds(0.1f);
    }
}
