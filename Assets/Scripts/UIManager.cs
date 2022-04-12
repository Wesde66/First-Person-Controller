using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _ammoText;
    [SerializeField] Image _coinImg;
    // Start is called before the first frame update
    void Start()
    {
        _coinImg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AmmoCount(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void CoinDisplay()
    {
        _coinImg.enabled = true;
    }
}
