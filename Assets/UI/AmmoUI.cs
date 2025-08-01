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
        if (Gun.Instance != null)
        {
            int currentAmmo = Gun.Instance.GetCurrentAmmo();
            if (currentAmmo != lastammo)
            {
                ammoText.text = ": " + Gun.Instance.GetCurrentAmmo();
                lastammo = currentAmmo;
            }
        }
    }
}