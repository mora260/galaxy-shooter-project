using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    // Called on the OnClick Event of New Game
    public void LoadGame() {
        SceneManager.LoadScene(1);
    }
}
