using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject RulesPanel;

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameWorld");
    }

    public void OnSettingsButtonClicked()
    {
        SettingsPanel.SetActive(true);
    }

    public void OnRulesButtonClicked()
    {
        RulesPanel.SetActive(true);
    }

    public void OnBackButtonClicked(GameObject panel)
    {
        SettingsPanel.SetActive(false);
        RulesPanel.SetActive(false);
    }
}