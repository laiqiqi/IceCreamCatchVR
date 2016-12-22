using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour {

    public enum UIType
    {
        Time,
        Score,
    }
    public UIType uitype;
    private Text UIText;
	// Use this for initialization
	void Start ()
    {
        UIText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(uitype == UIType.Score)
            UIText.text = IceCornCtrl.Score + "  pt";
        if (uitype == UIType.Time)
            UIText.text = (int)IceCornCtrl.GameTime + " : 00";
    }
}
