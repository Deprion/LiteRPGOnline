using UnityEngine;
using UnityEngine.UI;

public class LoginData : MonoBehaviour
{
    public InputField LoginField, PasswordField;
    public bool SaveDataLogin()
    {
        if (LoginField.text.ToString() != null
            && PasswordField.text.ToCharArray().Length >= 6)
        {
            PlayerPrefs.SetString("Nickname", LoginField.text.ToString());
            PlayerPrefs.SetString("Password", PasswordField.text.ToString());
            return true;
        }
        else
        {
            return false;
        }
    }
}
