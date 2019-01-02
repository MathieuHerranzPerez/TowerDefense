using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeTurretButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NodeUI nodeUI;

    public GameObject ui;
    public Text damages;
    public Text range;
    public Text fireRate;
    public Text slowAmount;

    private Turret turretFocused;

    public void OnPointerEnter(PointerEventData eventData)
    {
        turretFocused = nodeUI.GetTurretFocused();

        damages.text = turretFocused.damagesUP.ToString();
        range.text = turretFocused.rangeUP.ToString();
        fireRate.text = turretFocused.fireRateUP.ToString();
        slowAmount.text = turretFocused.slowUP.ToString();

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
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


}
