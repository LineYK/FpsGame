using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    public static GameManager gm;

    public GameState gState;

    [SerializeField]
    private GameObject gameLabel;

    private TMP_Text gameText;

    private PlayerMove player;

    // 옵션 화면 UI
    [SerializeField]
    private GameObject gameOption;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    private void Start()
    {
        gState = GameState.Ready;

        gameText = gameLabel.GetComponent<TMP_Text>();

        gameText.text = "Ready...";
        gameText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(ReadyToStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (player.Hp <= 0)
        {
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0f);

            gameLabel.SetActive(true);
            gameText.text = "Game Over";

            gameText.color = new Color32(255, 0, 0, 255);

            Transform buttons = gameText.transform.GetChild(0);
            buttons.gameObject.SetActive(true);

            gState = GameState.GameOver;
        }
    }

    private IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);

        gameText.text = "Go!";

        yield return new WaitForSeconds(0.5f);

        gameLabel.SetActive(false);

        gState = GameState.Run;
    }

    public void OpenOptionWindow()
    {
        gameOption.SetActive(true);

        // 게임 속도 0배속
        Time.timeScale = 0f;

        gState = GameState.Pause;
    }

    public void CloseOptionWindow()
    {
        gameOption.SetActive(false);

        // 게임 속도 1배속
        Time.timeScale = 1f;

        gState = GameState.Run;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        // 현재 씬 번호를 다시 로드한다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
