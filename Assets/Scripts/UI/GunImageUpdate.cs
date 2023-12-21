using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunImageUpdate : MonoBehaviour
{
    Image gunImage;
    [SerializeField] CurrentGun playerGun;
    // Start is called before the first frame update
    void Start()
    {
        gunImage = GetComponent<Image>();
        gunImage.sprite = playerGun.currentGun.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        gunImage.sprite = playerGun.currentGun.sprite;
    }
}
