using Mirror;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class ConnectCommands : NetworkBehaviour
{
    [Command]
    public void CmdResponseDataLogin(string Nick, string Pas)
    {
        MySqlCommand command = new MySqlCommand
            ($"SELECT all FROM Users WHERE Nick = {Nick} AND Pas = {Pas}");
        if (command.ExecuteScalar() != null) TargetResponseConnect(true);
    }
    [Command]
    public void CmdSaveDataToServer(string Nick, string Pas)
    {
        MySqlCommand command = new MySqlCommand
            ($"INSERT INTO Users(Nick, Pas) VALUES ({Nick}, {Pas})");
        command.ExecuteNonQuery();
    }
    [Command]
    public void CmdCheckAvailableNickname(string Nick)
    {
        TargetReturnAvailableNickname(SQLManager.Instance.CheckNickname(Nick));
    }
    [TargetRpc]
    public void TargetReturnAvailableNickname(bool isFree)
    {
        ConnectManager.isFreeNick = isFree;
        Debug.Log(isFree);
    }
    [TargetRpc]
    public void TargetResponseConnect(bool connect)
    {
        if (connect) SceneManager.LoadScene(1);
    }
}
