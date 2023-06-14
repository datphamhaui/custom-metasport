using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

using TreeEditor;
using UnityEngine.Networking;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TopText;
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField EmailLoginInput;
    [SerializeField] TMP_InputField PasswordLoginput;
    [SerializeField] GameObject LoginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField UsernameRegisterInput;
    [SerializeField] TMP_InputField EmailRegisterInput;
    [SerializeField] TMP_InputField PasswordRegisterinput;
    [SerializeField] GameObject RegisterPage;

    //[Header("Recovery")]
    //[SerializeField] TMP_InputField EmailRecoveryInput;
    //[SerializeField] GameObject RecoverPage;




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Return))
        {

        }



        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null) inputfield.OnPointerClick(new PointerEventData(EventSystem.current));  //if it's an input field, also set the text caret

                EventSystem.current.SetSelectedGameObject(next.gameObject, new BaseEventData(EventSystem.current));
            }
            //else Debug.Log("next nagivation element not found");

        }


    }
    //--------------------------------------------------------------------------------------------------------
    #region Buttom Functions
    public class RegisterData
    {
        public string username;
        public string email;
        public string password;
    }

    public void Register()
    {
        StartCoroutine(RegisterInfo());
    }

    private IEnumerator RegisterInfo()
    {
        string username = UsernameRegisterInput.text;
        string email = EmailRegisterInput.text;
        string password = PasswordRegisterinput.text;

        RegisterData registerData = new RegisterData
        {
            username = username,
            email = email,
            password = password
        };

        string jsonData = JsonUtility.ToJson(registerData);

        // Tạo yêu cầu HTTP POST đến Strapi để lưu trữ dữ liệu
        UnityWebRequest saveRequest = new UnityWebRequest("http://localhost:1337/api/auth/local/register", "POST");
        byte[] saveData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        saveRequest.uploadHandler = new UploadHandlerRaw(saveData);
        saveRequest.downloadHandler = new DownloadHandlerBuffer();
        saveRequest.SetRequestHeader("Content-Type", "application/json");

        // Gửi yêu cầu và đợi phản hồi
        yield return saveRequest.SendWebRequest();

        if (saveRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data saved on Strapi");

        }
        else
        {
            Debug.Log("Failed to save data on Strapi");
        }
    }
    //-------------------------------------------------------------------------------------
    private const string apiUrl = "http://localhost:1337/api/auth/local";

    public class LoginData
    {
        public string identifier;
        public string password;
    }

    public void Login()
    {
        StartCoroutine(LoginRequest());
    }

    private IEnumerator LoginRequest()
    {

        string email = EmailLoginInput.text;
        string password = PasswordLoginput.text;
        var requestBody = new LoginData
        {
            identifier = email,
            password = password
        };

        var jsonRequestBody = JsonUtility.ToJson(requestBody);
        var request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var responseContent = request.downloadHandler.text;
            var responseData = JsonUtility.FromJson<ResponseData>(responseContent);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else
        {

        }
    }

    private class ResponseData
    {
        public string user;
        public string jwt;
    }

    private void OnLoginSucces(LoginResult result)
    {
        MessageText.text = "Loggin in";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //public void RecoverUser()
    //{
    //    var request = new SendAccountRecoveryEmailRequest
    //    {
    //        Email = EmailRecoveryInput.text,
    //        TitleId = "73C38",
    //    };

    //    PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySucces, OnErrorRecovery);
    //}

    private void OnErrorRecovery(PlayFabError result)
    {
        MessageText.text = "No Email Found";
    }

    private void OnRecoverySucces(SendAccountRecoveryEmailResult obj)
    {
        OpenLoginPage();
        MessageText.text = "Recovery Mail Sent";
    }

    private void OnError(PlayFabError Error)
    {
        MessageText.text = Error.ErrorMessage;
        Debug.Log(Error.GenerateErrorReport());
    }

    private void OnregisterSucces(RegisterPlayFabUserResult Result)
    {
        MessageText.text = "New Account Is Created";
        OpenLoginPage();
    }

    public void OpenLoginPage()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        //RecoverPage.SetActive(false);
        TopText.text = "Login";
    }
    public void OpenRegisterPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        //RecoverPage.SetActive(false);
        TopText.text = "Register";
    }
    public void OpenRecoveryPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        //RecoverPage.SetActive(true);
        TopText.text = "Recovery";
    }

    #endregion
}
