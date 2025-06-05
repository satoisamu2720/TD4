using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    private int lastammo = 0;

    // Update is called once per frame
    void Update()
    {
        int currentAmmo = StetusScript.Instance.Bullet;

        if (currentAmmo != lastammo)
        {
            ammoText.text = "íeêî: " + StetusScript.Instance.Bullet;
            lastammo = currentAmmo;
        }
    }
}