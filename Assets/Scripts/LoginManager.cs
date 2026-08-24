using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField id;

    [SerializeField]
    private TMP_InputField password;

    [SerializeField]
    private TMP_Text notify;

    private void Start()
    {
        notify.text = "";
    }

    public void SaveUserData()
    {
        if (!CheckInput(id.text, password.text))
            return;

        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, password.text);
            notify.text = "아이디 생성이 완료됐습니다.";
        } 
        else
        {
            notify.text = "이미 존재하는 아이디입니다.";
        }
    }

    public void CheckUserDate()
    {
        if (!CheckInput(id.text, password.text))
            return;

        string pwd = PlayerPrefs.GetString(id.text);

        if (password.text == pwd)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            notify.text = "입력하신 아이디와 패스워드가 일치하지 않습니다.";
        }
    }

    private bool CheckInput(string id, string pwd)
    {
        
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pwd))
        {
            notify.text = "아이디 또는 패스워드를 입력해주세요.";
            return false;
        }

        return true;
    }
}
