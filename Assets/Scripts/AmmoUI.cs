using UnityEngine.UI;
using UnityEngine;

public class AmmoUI : MonoBehaviour {

    public Image AmmoBar;

    [SerializeField]
    private WeaponManager weaponManager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AmmoBar.fillAmount = (float) weaponManager.GetWeaponCurrentAmmo() / (float)weaponManager.GetWeaponMaxAmmo();
    }
}
