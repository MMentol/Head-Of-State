using GridMap.Structures.Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureWindow : MonoBehaviour
{
    public static StructureWindow Instance;

    public Structure SelectedStructure;
    [Header("UI Objects")]
    [SerializeField] TMP_Text StructNameTxt;
    [SerializeField] TMP_Text ResidentsTxt;
    [SerializeField] TMP_Text StorageTxt;
    [SerializeField] TMP_Text UpgradeMaterials;
    [SerializeField] Button UpgradeButton;
    [SerializeField] Button DemolishButton;

    [SerializeField] MaterialStorageBase stor;
    [SerializeField] House house;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        this.gameObject.SetActive(false);
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true); //Structure Window Object
        StructNameTxt.text = SelectedStructure.name;
        ResidentsTxt.text = "";
        StorageTxt.text = "";
        UpdateWindow();
    }

    public void SetStructure(Structure selected)
    {
        SelectedStructure = selected;
        stor = selected.gameObject.GetComponent<MaterialStorageBase>();
        house = selected.gameObject.GetComponent<House>();
    }

    public void UpdateWindow()
    {
        //Demolish Button
        DemolishButton.onClick.RemoveAllListeners();
        DemolishButton.onClick.AddListener(() => SelectedStructure._tile.DestroyStructure());
        DemolishButton.onClick.AddListener(() => gameObject.SetActive(false));
        //Clear Upgrade Button Listeners
        UpgradeButton.onClick.RemoveAllListeners();
        //Structure Specific Details
        if (house != null)
        {
            ResidentsTxt.text = "Residents: " + house.PeopleInside.Count + "/" + house.Capacity;
            UpgradeMaterials.text = "Not Upgradable.";
            UpgradeButton.interactable = false;
        }
        if (stor != null)
        {
            StorageTxt.text = "Storage: " + stor.Count + "/" + stor.Capacity;
            UpgradeButton.onClick.AddListener(() => stor.Upgrade());
            UpgradeButton.onClick.AddListener(() => UpdateWindow());

            //Upgrade Materials
            int nextUpg = stor.currentLevel + 1;
            if (nextUpg < stor.CapacityTiers.Length)
                UpgradeMaterials.text = $"Wood:{stor.WoodUpgradeCost[nextUpg]}\r\n\r\n" +
                $"Stone:{stor.StoneUpgradeCost[nextUpg]}\r\n\r\n" +
                $"Metal:{stor.MetalUpgradeCost[nextUpg]}";
            else
            {
                UpgradeMaterials.text = "Max upgrade reached.";
                UpgradeButton.interactable = false;
            }
        }
    }
}
