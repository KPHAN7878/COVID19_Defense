using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using TMPro;


public class AuthManager : MonoBehaviour
{   
    

    //Base Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;


    //Login Variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;



    //Register Variables
    [Header("Register")]
    public TMP_InputField emailRegisterField;
    public TMP_InputField usernameRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordVerifyField;

    [Header("Reset_Password")]

    public TMP_InputField recoveryEmailField ; 

    //Status Message for login and register
    [Header("Status")]
    public TMP_Text statusMessage;

    //Login and Register UI Variables
    [Header("UI Components")]
    public GameObject loginUI;
    public GameObject registerUI;

    
    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Unable to access all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
    }


    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }


    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    public void ResetButton()
    {
        StartCoroutine(Reset_Password(recoveryEmailField.text));
    }

    // Loads the main menu just in case the login and register buttons dont work
    public void Loadmenus()
    {
        SceneManager.LoadScene(2);
    }


    public void LoginToRegisterButton()
    {
        statusMessage.text = "";
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    
        
    }

    public void RegisterToLoginButton()
    {
        statusMessage.text = "";
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        
    } 

    public void resetPasswordButton() //loads the reset buttonscene
    {
        SceneManager.LoadScene(4);
    }

    public void goBackButton() //loads the reset loginScene
    {   
        SceneManager.LoadScene(1);
    }


   

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            statusMessage.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            statusMessage.text = "Logged In";

            SceneManager.LoadScene(2);
        }
    }


    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            statusMessage.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordVerifyField.text)
        {
            //If the password does not match show a warning
            statusMessage.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                statusMessage.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        statusMessage.text = "Username Set Failed!";
                    }
                    else
                    {
                        statusMessage.text = "";
                        loginUI.SetActive(true);
                        registerUI.SetActive(false);
                    }
                }
            }
        }
    }

   private IEnumerator Reset_Password(string _email)
    {

         //Call the Firebase auth signin function passing the email and password
        var ResetTask = auth.SendPasswordResetEmailAsync(_email);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ResetTask.IsCompleted);
    
    if (_email==null)
    {
        Debug.LogError("Empty.");
    }
    if (_email != null) {
    ResetTask.ContinueWith(task => {
        if (task.IsCanceled) {
            statusMessage.text = "Error Occured";
        Debug.LogError("SendPasswordResetEmailAsync was canceled.");
        return;
        }
        if (task.IsFaulted) {
            statusMessage.text = "Error Occured";
        Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
        
        return;
        }

        Debug.Log("Password reset email sent successfully.");
         //SceneManager.LoadScene(1);
    });
    }

    }
    
}        
  

    
