using UnityEngine;
using MySql.Data.MySqlClient;

public class SQLManager : MonoBehaviour
{
    public static MySqlConnection SqlCon;
    public static SQLManager Instance;
    private string ConnectStr =
        "Server=80.78.245.134;Port=5050;DataBase=UnityBase;Uid=UnityDev;Pwd=791354862q0";
    private void ConnectToSQLServer()
    {
        SqlCon = new MySqlConnection(ConnectStr);
        Debug.Log("OpenSQL");
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        ConnectToSQLServer();
        Debug.Log("StartSQL");
    }
    public bool CheckNickname(string Nick)
    {
        MySqlCommand command = new MySqlCommand($"SELECT all FROM Users WHERE Nick = {Nick}", SqlCon);
        MySqlDataReader reader = command.ExecuteReader();
        return reader==null;
    }

}
