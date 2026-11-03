using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputController : MonoBehaviour
{
    private IMenuActions _actions;
    [SerializeField]
    private bool _isMainMenu = true;

    private void Start()
    {
        GlobalInputController.Instance.SetContext(GlobalInputController.InputContext.MENU);

        _actions = GlobalInputController.Instance.MenuActions;
        if(_isMainMenu)
            _actions.Start.performed += ctx => StartGame();
        else
            _actions.Start.performed += ctx => GoToMainMenu();
        _actions.Exit.performed += ctx => ExitGame();
        _actions.Controls.performed += ctx => GoToControls();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
