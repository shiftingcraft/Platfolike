using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPannel;

    public void ChangeSettingsState(bool state)
    {
        _settingsPannel.SetActive(state);
    }
}
