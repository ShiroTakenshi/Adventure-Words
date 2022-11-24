using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    Gerak KomponenGerak;
    // Start is called before the first frame update
    void Start()
    {
        KomponenGerak = GameObject.Find("Player").GetComponent<Gerak>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player")
        {
            KomponenGerak.heart--;
            KomponenGerak.play_again=true;
            print ("Checkpoint");
        }
    }
}
