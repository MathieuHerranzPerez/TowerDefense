using UnityEngine.UI;
using UnityEngine;

public class Shop2 : MonoBehaviour {

    [Header("NEED the same size")]
    public Turret[] turretBlueprintArray;
    public Image[] imageTurretArray;


    private int size;
    private int currentBlueprintIndex = 0;
    private int previousBlueprintIndex = 0;

    BuildManager buildManager;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    // Use this for initialization
    void Start ()
    {
        buildManager = BuildManager.GetInstance();
        size = turretBlueprintArray.Length;
        imageTurretArray[currentBlueprintIndex].enabled = true;
        for(int i = 1; i < size; ++i)
        {
            imageTurretArray[i].enabled = false;
        }
        // set the first turret at selected
        buildManager.SetTurretToBuild(turretBlueprintArray[currentBlueprintIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                if (i < size)
                {
                    currentBlueprintIndex = i;
                }
            }
        }
        if(previousBlueprintIndex != currentBlueprintIndex)
        {
            imageTurretArray[previousBlueprintIndex].enabled = false;
            imageTurretArray[currentBlueprintIndex].enabled = true;
            previousBlueprintIndex = currentBlueprintIndex;
            // change the turret in the buildManager
            buildManager.SetTurretToBuild(turretBlueprintArray[currentBlueprintIndex]);
        }
    }
}
