using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Run,
        GameOver
    }

    public static GameManager gm;

    public GameState gState;

    [SerializeField]
    private GameObject gameLabel;

    private TMP_Text gameText;

    private PlayerMove player;

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
            gameLabel.SetActive(true);
            gameText.text = "Game Over";

            gameText.color = new Color32(255, 0, 0, 255);

            gState = GameState.GameOver;
        }
    }

    private IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);

        gameText.text = "Go!";

        yield return new WaitForSeconds(0.5f);

        gameLabel.SetActive(false);

        gState = GameState.Ready;
    }
}
