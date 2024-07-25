using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private Button spawnTargetButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_Dropdown projectileDropdown;
    [SerializeField] private Toggle modificationCheckbox;
    [SerializeField] private Target target;
    [SerializeField] private Bow bow;

    private void Start()
    {
        spawnTargetButton.onClick.AddListener(SpawnTarget);
        exitButton.onClick.AddListener(Exit);
        projectileDropdown.onValueChanged.AddListener(delegate { SelectProjectileType(projectileDropdown); });
        modificationCheckbox.onValueChanged.AddListener(OnModificationToggleChanged);
    }
    
    private void SpawnTarget()
    {
        target.Appear();
    }

    private void SelectProjectileType(TMP_Dropdown dropdown)
    {
        string selectedOption = dropdown.options[dropdown.value].text;
        bow.SelectProjectileType(selectedOption);

    }
    private void OnModificationToggleChanged(bool isOn)
    {
        bow.ChangeModifierStatus(isOn);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
