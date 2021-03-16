using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using System.Collections;

public class ConnectManager : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject ShowError;
    public GameObject SQL;
    private GameObject NetworkManager;
    private ConnectCommands ccm;
    public static bool isFreeNick = true;

#if UNITY_SERVER
    void Start()
    {
        GameObject.FindGameObjectWithTag("NetworkManager").
            GetComponent<NetworkManager>().StartServer();
        SQL.SetActive(true);
        SceneManager.LoadScene(1);
    }
#endif
#if UNITY_ANDROID
    void Start()
    {
        NetworkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        NetworkManager.GetComponent<NetworkManager>().StartClient();
        ccm = GameObject.FindGameObjectWithTag("Player").GetComponent<ConnectCommands>();
        if (PlayerPrefs.HasKey("Nickname") && PlayerPrefs.HasKey("Password"))
        {
            Debug.Log("access");
            Debug.Log(PlayerPrefs.GetString("Nickname"));
            ccm.CmdResponseDataLogin
            (PlayerPrefs.GetString("Nickname"), PlayerPrefs.GetString("Password"));
        }
        else
        {
            Debug.Log("refuse");
            LoginPanel.SetActive(true);
        }
    }
#endif
    public void Connect()
    {
        if (GetComponent<LoginData>().SaveDataLogin())
        {
            ccm.CmdCheckAvailableNickname(PlayerPrefs.GetString("Nickname"));
            if (isFreeNick)
            {
                if (!PlayerPrefs.HasKey("AlreadySaved"))
                {
                    ccm.CmdSaveDataToServer
                        (PlayerPrefs.GetString("Nickname"), PlayerPrefs.GetString("Password"));
                    PlayerPrefs.SetInt("AlreadySaved", 1);
                }
                SceneManager.LoadScene(1);
            }
            else 
            {
                StartCoroutine(HideShowError());
            }
        }
        else
        {
            StartCoroutine(HideShowError());
        }
    }
    private IEnumerator HideShowError()
    {
        ShowError.SetActive(true);
        yield return new WaitForSeconds(4f);
        ShowError.SetActive(false);
    }
}
