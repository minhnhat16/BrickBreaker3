using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadtest : MonoBehaviour
{
    public GameObject dadPref;
    public GameObject sonPref;
    public GameObject dadIns;
    public GameObject sonIns;
    // Start is called before the first frame update
    void Start()
    {
        LoadObject();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadObject()
    {
        dadIns = Instantiate(dadPref, transform);
        dadPref.SetActive(true);
        
        sonIns = Instantiate(sonPref, transform);
        sonPref.SetActive(true);

        sonIns.transform.SetParent(dadIns.transform);

    }
    
}
