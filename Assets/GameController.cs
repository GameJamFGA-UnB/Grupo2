using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOver;

    public void Start(){
        instance = this;
    }

    public void StartGame(string lvlName){
        SceneManager.LoadScene(lvlName);
    }

    public void ShowGameOver(){
        gameOver.SetActive(true);
    }
}
