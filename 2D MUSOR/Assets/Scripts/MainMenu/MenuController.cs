using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private SettingsController _settingsController;

    public void OpenSettings()
    {
        _settingsController.ChangeSettingsState(true);
    }
    public void Exit()
    {
        Debug.Log("See You Next Time");
        Application.Quit();
    }
    public void
}
