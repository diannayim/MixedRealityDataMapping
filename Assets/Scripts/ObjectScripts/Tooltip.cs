using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {
    List<GameObject> hidden = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        hidden.Add(other.gameObject);
        other.gameObject.SetActive(false);

        this.GetComponent<BoxCollider>().size = new Vector3(180f, 150f, 10f);
    }

    public void Exit()
    {
        foreach (GameObject g in hidden)
            g.SetActive(true);
        Destroy(this.gameObject);
    }
}
