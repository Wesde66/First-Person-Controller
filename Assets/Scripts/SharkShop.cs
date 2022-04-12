using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private AudioSource _WeaponCollect;
    // Start is called before the first frame update
    void Start()
    {
        _WeaponCollect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    if (player.hasCoin == true)
                    {
                        player.hasCoin = false;
                        player.WeaponOnOff();

                        _WeaponCollect.Play();
                    }
                    else
                    {
                        Debug.Log("Get Lost");
                    }
                }
            }
        }
    }
}
