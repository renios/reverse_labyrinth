using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnViewer : MonoBehaviour {

	private int turn;
	private Text text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text>();
		initialize();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Turn " + turn;
	}

	public void Increase()
	{
		turn++;
	}

	void initialize()
	{
		turn = 0;
	}
}
