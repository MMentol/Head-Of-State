using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildUIEntry : MonoBehaviour
{
    [Header("Structure to Place")]
    [SerializeField] public StructureChooser structureChooser;
    [SerializeField] public Structure _buildingPrefab;
    [Header("UI Elements")]
    [SerializeField] public TMP_Text _buildingName;
    [SerializeField] public TMP_Text _woodText;
    [SerializeField] public TMP_Text _stoneText;
    [SerializeField] public TMP_Text _metalText;
    [SerializeField] public Button _buildButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "BuildEntry: " + _buildingPrefab.name;
        _buildButton.onClick.AddListener(() => structureChooser.ChooseStructure(_buildingPrefab.gameObject));
        _buildingName.text = _buildingPrefab.name;
        _woodText.text = "Wood: " + _buildingPrefab._woodCost;
        _stoneText.text = "Stone: " + _buildingPrefab._stoneCost;
        _metalText.text = "Metal: " + _buildingPrefab._metalCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
