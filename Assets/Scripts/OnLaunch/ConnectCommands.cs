using Mirror;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class ConnectCommands : NetworkBehaviour
{
    [Command(ignoreAuthority = true)]
    public void CmdResponseDataLogin(string Nick, string Pas)
    {
        MySqlCommand command = new MySqlCommand
            ($"SELECT all FROM Users WHERE Nick = {Nick} AND Pas = {Pas}");
        if (command.ExecuteScalar() != null) RpcResponseConnect(true);
    }
    [Command(ignoreAuthority = true)]
    public void CmdSaveDataToServer(string Nick, string Pas)
    {
        MySqlCommand command = new MySqlCommand
            ($"INSERT INTO Users(Nick, Pas) VALUES ({Nick}, {Pas})");
        command.ExecuteNonQuery();
    }
    [Command(ignoreAuthority = true)]
    public void CmdCheckAvailableNickname(string Nick)
    {
        RpcReturnAvailableNickname(SQLManager.Instance.CheckNickname(Nick));
    }
    [TargetRpc]
    public void RpcReturnAvailableNickname(bool isFree)
    {
        ConnectManager.isFreeNick = isFree;
        Debug.Log(isFree);
    }
    [TargetRpc]
    public void RpcResponseConnect(bool connect)
    {
        if (connect) SceneManager.LoadScene(1);
    }
}
