using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeWeaponButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private WeaponManager weaponManager;

    public GameObject ui;
    public Text damages;
    public Text fireRate;
    public Text maxBullets;

    WeaponUpgrade weaponUP;

    public void OnPointerEnter(PointerEventData eventData)
    {
        weaponUP = weaponManager.GetCurrentWeapon().GetWeaponUpgrade();
        damages.text = weaponUP.damage.ToString();
        fireRate.text = weaponUP.fireRate.ToString();
        maxBullets.text = weaponUP.maxBullets.ToString();

        ui.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}

