using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour {
    private KeyValuePair<string, float> x, y, z;
    public GameObject textAsset = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void AssignValue (Dictionary<string, float> dict)
    {
        x = dict.ElementAt(0);
        y = dict.ElementAt(1);
        z = dict.ElementAt(2);
    }

    void OnGazeEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        ToTextDisplay();
    }

    void ToTextDisplay()
    {
        if (textAsset == null)
        {
            textAsset = (GameObject)Instantiate(Plotter.Instance.tooltip, new Vector3(), Quaternion.identity);
            textAsset.tag = "tooltip";
            textAsset.transform.SetParent(gameObject.transform);
            textAsset.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.4f, gameObject.transform.position.z);
            PointToString();
        }
    }

    void PointToString()
    {
        Text[] texts = textAsset.GetComponentsInChildren<Text>();

        texts[0].text = x.Key + ":\n" + y.Key + ":\n" + z.Key + ":";
        texts[1].text = x.Value + "\n" + y.Value + "\n" + z.Value;
    }

    void OnGazeLeave()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("tooltip"))
        {
            g.SendMessage("Exit", SendMessageOptions.DontRequireReceiver);
        }

        textAsset = null;
    }
}
