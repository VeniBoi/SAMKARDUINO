using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class windowGraph : MonoBehaviour {

	[SerializeField] private Sprite circleSprite;
	private RectTransform graphContainer;

	private void Awake()
	{
		graphContainer = transform.Find("graphcontainer").GetComponent<RectTransform>();
		createCircle(new Vector2(200, 200));
		List<int> valueList = new List<int> { 1, 3000, 345, 67, 456, 1345, 934, 2546, 1764, 1567, 453, 678, 456, 487, 2987 };
		showGraph(valueList);
	}

	private void createCircle(Vector2 anchoredPosition)
	{
		GameObject gameObject = new GameObject("circle", typeof(Image));
		gameObject.transform.SetParent(graphContainer, false);
		gameObject.GetComponent<Image>().sprite = circleSprite;
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = anchoredPosition;
		rectTransform.sizeDelta = new Vector2(11, 11);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
	}

	private void showGraph(List<int> valueList)
	{
		float graphHeight = graphContainer.sizeDelta.y;
		float yMaximum = 100f;
		float xSize = 50f;
		for (int i = 0; i < valueList.Count; i++)
		{
			float xPosition = i * xSize;
			float yPosition = (valueList[i] / yMaximum * graphHeight);
			createCircle(new Vector2(xPosition, yPosition));
		}
	}
}
