using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine.UI;

public partial class Plotter : Singleton<Plotter>
{
    public GameObject tooltip;
    Dictionary<string, List<string>> dictionary;
    int numEntries = 0;
    public GameObject xTick;

    public GameObject yTick;
    public GameObject zTick;

    public void ToggleColor()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("point"))
        {
            g.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    // Use this for initialization
    void Start()
    {
        CreateDataset();
        ScatterPlot3D s = new ScatterPlot3D(dictionary, numEntries, xTick, yTick, xTick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            Plotter.Instance.ToggleColor();
    }

    void CreateDataset()
    {
        TextAsset dataAsset = Resources.Load("Hartnagel") as TextAsset;
        string text = dataAsset.text;

        string[] lines = text.Split('\n');
        int size = lines[0].Split(',').Length;
        List<string>[] arrayAttributes = new List<string>[size];

        for (int i = 0; i < arrayAttributes.Length; i++)
        {
            arrayAttributes[i] = new List<string>();
        }

        foreach (string line in lines)
        {
            string[] attributes = line.Split(',');
            for (int i = 0; i < attributes.Length; i++)
            {
                if (i == 0)
                    continue;

                arrayAttributes[i].Add(attributes[i]);
            }
        }

        dictionary = new Dictionary<string, List<string>>();
        dictionary.Add("id", arrayAttributes[0]);
        dictionary.Add("year", arrayAttributes[1]);
        dictionary.Add("tfr", arrayAttributes[2]);
        dictionary.Add("partic", arrayAttributes[3]);
        dictionary.Add("degrees", arrayAttributes[4]);
        dictionary.Add("fconvict", arrayAttributes[5]);
        dictionary.Add("ftheft", arrayAttributes[6]);
        dictionary.Add("mconvict", arrayAttributes[7]);
        dictionary.Add("mtheft", arrayAttributes[8]);

        numEntries = arrayAttributes[7].Count;
    }
}
