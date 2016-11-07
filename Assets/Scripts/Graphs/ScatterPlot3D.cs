using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScatterPlot3D : MonoBehaviour
{
    GameObject pointParent;
    GameObject xTick, yTick, zTick;

    public ScatterPlot3D(Dictionary<string, List<string>> dictionary, int numEntries, GameObject xTickArg, GameObject yTickArg, GameObject zTickArg)
    {
        xTick = xTickArg;
        yTick = yTickArg;
        zTick = zTickArg;

        List<float> x = new List<float>();
        List<float> y = new List<float>();
        List<float> z = new List<float>();

        pointParent = new GameObject("pointParent");
        pointParent.transform.SetParent(GameObject.Find("Graph").transform);

        List<string> tempx, tempy, tempz;
        dictionary.TryGetValue("tfr", out tempx);
        dictionary.TryGetValue("fconvict", out tempy);
        dictionary.TryGetValue("mconvict", out tempz);

        for (int i = 0; i < numEntries; i++)
        {
            x.Add(float.Parse(tempx[i]));
            y.Add(float.Parse(tempy[i]));
            z.Add(float.Parse(tempz[i]));
        }

        x.Sort();
        y.Sort();
        z.Sort();

        CreateTicks(x[numEntries - 1], y[numEntries - 1], z[numEntries - 1]);

        for (int j = 0; j < numEntries; j++)
        {
            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.AddComponent<Point>();
            point.tag = "point";
            point.transform.parent = pointParent.transform;
            point.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            point.transform.position = new Vector3((x[j] / Math.Abs(x[numEntries - 1]) * 5),
                (y[j] / Math.Abs(y[numEntries - 1]) * 5),
                -(z[j] / Math.Abs(z[numEntries - 1]) * 5));

            Dictionary<string, float> values = new Dictionary<string, float>();
            values.Add("tfr", x[j]);
            values.Add("fconvict", y[j]);
            values.Add("mconvict", z[j]);
            point.SendMessage("AssignValue", values, SendMessageOptions.DontRequireReceiver);
            point.GetComponent<SphereCollider>().isTrigger = true;
            point.AddComponent<Rigidbody>();
            point.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void CreateTicks(float xMax, float yMax, float zMax)
    {
        CreateTicksPerAxis(xMax, GameObject.FindGameObjectWithTag("xAxis").transform.localScale.x, xTick, "XTicks");
        CreateTicksPerAxis(yMax, GameObject.FindGameObjectWithTag("yAxis").transform.localScale.y, yTick, "YTicks");
        CreateTicksPerAxis(zMax, GameObject.FindGameObjectWithTag("zAxis").transform.localScale.z, zTick, "ZTicks");
    }

    void CreateTicksPerAxis(float max, float wholeScale, GameObject prefab, string axis)
    {
        float scale = wholeScale / 2;

        float incrementTickPosition = scale / 10;
        float checkPosition = 0;
        float checkVal = 0;

        while (checkPosition < scale)
        {
            checkPosition += incrementTickPosition;
            checkVal = checkPosition * max;
            Vector3 pos;

            if (axis.Contains("X"))
                pos = new Vector3(checkPosition, 0, 0);
            else if (axis.Contains("Y"))
                pos = new Vector3(0, checkPosition, 0);
            else
                pos = new Vector3(0, 0, -checkPosition);

            GameObject tickObj = (GameObject)Instantiate(prefab, pos, prefab.transform.rotation);
            tickObj.transform.SetParent(GameObject.Find(axis).transform);
            tickObj.GetComponentInChildren<TextMesh>().text = checkVal.ToString();
        }
    }
}
