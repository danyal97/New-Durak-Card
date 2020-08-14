using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;


using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Connect;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game
{
    public string userId { get; set; }

       

    public Game()
    {
    }

    public Game(string userId)
    {
        this.userId = userId;
        
    }
}
public class FirebaseScript:MonoBehaviour
{

    DatabaseReference reference;
    FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    bool CurrentUserNot = false;
    public static bool firebaseReady;
    // Start is called before the first frame update
    void Start()
    {


        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase is ready for use.");
                firebaseReady = true;
            }
            else
            {
                firebaseReady = false;
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

     void Update()
    {
        if(firebaseReady)
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://durakcard-2a29c.firebaseio.com/");
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            //auth.SignOut();
            InitializeFirebase();
        }
        
        
    }
    void InitializeFirebase()
    {
        
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {

                Debug.Log("Not Signed in " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                
                Debug.Log("already Signed in " + user.UserId);
                //SceneManager.LoadScene("SampleScene");
                SceneManager.LoadScene("MenuScene");


                //auth.SignOut();
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    public  void CheckIfReady()
    {

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                firebaseReady = true;

                Debug.Log("Firebase is ready for use.");

               

                // Create and hold a reference to your FirebaseApp, i.e.
                //   app = Firebase.FirebaseApp.DefaultInstance;
                // where app is a Firebase.FirebaseApp property of your application class.

                // Set a flag here indicating that Firebase is ready to use by your
                // application.
            }
            else
            {
                firebaseReady = false;
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    public string GetUserIdOfPlayer()
    {
        Debug.Log("UserID Called");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        return auth.CurrentUser.UserId.ToString();
    }
    public void AddPlayerToGame(string userId,string gameNo)
    {
        Debug.Log("Reference Called");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Game user = new Game(userId);
        string json = JsonUtility.ToJson(user);
        Debug.Log("Json Conversion Called");
        print(user.userId+"game no"+ gameNo);
       
        reference.Child("game").Child(gameNo).SetRawJsonValueAsync(json);
    }
    public void AddToGame()
    {
        
            string userid = this.GetUserIdOfPlayer();
            Debug.Log("User Id Of Player " + userid);
            bool gameComplete = false;
        this.AddPlayerToGame(userid, "1");
        print("Player Added");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

            FirebaseDatabase.DefaultInstance.GetReference("game").GetValueAsync().ContinueWith(task =>
            {
                List<string> data = new List<string>();
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount > 0)
                {
                    foreach (var i in snapshot.Children)
                    {
                        if (i.ChildrenCount == 1)
                        {
                            //if (userid!=i.Child("userId").Value.ToString())
                            //{
                                this.AddPlayerToGame(userid, i.Key);
                                gameComplete = true;
                                break;
                            //}
                            
                        }
                        

                    }
                    string lastKey="1";

                    foreach (var j in snapshot.Children)
                    {
                        lastKey = j.Key;
                    }
                    if (gameComplete)
                    {
                        this.AddPlayerToGame(userid, lastKey + 1);
                    }
                }
                else
                {
                    this.AddPlayerToGame(userid, "1");
                }
            });
        
        


        //reference
        //.GetValueAsync().ContinueWith(task =>
        //{
        //    Debug.Log("No Successfully Add Player To Game");
        //    if (task.IsFaulted)
        //    {
        //        Debug.Log("No Successfully Add Player To Game");
        //       // Handle the error...
        //   }
        //    else if (task.IsCompleted)
        //    {
        //        Dictionary<string, object> data = new Dictionary<string, object>();
        //        Debug.Log("task Completed");
        //        DataSnapshot snapshot = task.Result;
                
        //        Debug.Log("Data Type Of Snapshot " + snapshot.Child("game").Value);
                

        //    }
        //});
       
        
        
       



    }

}
