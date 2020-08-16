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
    public int playerNo;
    public string gameNoToBeAdded;


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
                CurrentUserNot = true;
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
    public void AddCoordinatesToDatabse(string playerNo1,string cardName,string positionx, string positiony, string positionz)
    {
        print("Add Coordinates To database Called");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        string userid = this.GetUserIdOfPlayer();
        Debug.Log("User Id Of Player " + userid);
        reference.Child("game2").Child(gameNoToBeAdded).Child(userid).Child(cardName).Child("positionX").SetValueAsync(positionx);
        reference.Child("game2").Child(gameNoToBeAdded).Child(userid).Child(cardName).Child("positionY").SetValueAsync(positiony);
        reference.Child("game2").Child(gameNoToBeAdded).Child(userid).Child(cardName).Child("positionZ").SetValueAsync(positionz);
    }
    public void RetreiveCoordinateFromDatabse(string playerNo1, string cardName, string positionx, string positiony, string positionz)
    {
        print("Retreive Coordinates From database Called");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        string userid = this.GetUserIdOfPlayer();
        Debug.Log("User Id Of Player " + userid);
        FirebaseDatabase.DefaultInstance.GetReference("game2").GetValueAsync().ContinueWith(task =>
        {
            List<string> data = new List<string>();
            DataSnapshot snapshot = task.Result;
            bool userfound = false;
            
            if (snapshot.ChildrenCount > 0)
            {
                // Game No ke andar gaya
                foreach (var gamNo in snapshot.Children)
                {
                   foreach(var useid in gamNo.Children )
                    {
                        // This Is Player1 Data
                        if (useid.Key==userid)
                        {
                            userfound = true;
                            foreach (var cardNam in useid.Children)
                            {
                                GameObject gb = GameObject.Find(cardNam.Key).gameObject;
                                Vector3 positon = new Vector3();
                                // Gives Card Name
                                foreach (var pos in cardNam.Children)
                                {
                                    
                                    if(pos.Key== "positionX")
                                    {
                                        Debug.Log("Position x"+pos.Value);
                                    }
                                    else if (pos.Key == "positionY")
                                    {

                                    }
                                    else if (pos.Key == "positionZ")
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            // This Is Player2 Data
                            foreach (var cardNam in useid.Children)
                            {
                                // Gives Card Name
                                foreach (var pos in cardNam.Children)
                                {
                                    if (pos.Key == "positionX")
                                    {

                                    }
                                    else if (pos.Key == "positionY")
                                    {

                                    }
                                    else if (pos.Key == "positionZ")
                                    {

                                    }
                                }
                            }


                        }
                    }
                   if (userfound)
                    {
                        break;
                    }
                }
               
            }
        });




    }
    public string GetUserIdOfPlayer()
    {
        Debug.Log("UserID Called");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        return auth.CurrentUser.UserId.ToString();
    }
    public void AddPlayerToGame(string userId,string gameNo,long childrenCount)
    {
        Debug.Log("Reference Called");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Game user1 = new Game(userId);
        string json = JsonUtility.ToJson(user1);
        Debug.Log("Json Conversion Called");
        
        if (childrenCount%2==0)
        {
            print("Mod 1 called game no =" + gameNo);
            playerNo = 1;
            gameNoToBeAdded = gameNo;
            reference.Child("game").Child(gameNo).Child("userid").SetValueAsync(userId);
        }
        else if (childrenCount % 2 == 1)
        {
            print("Mod 2 called game no ="+gameNo);
            playerNo = 2;
            gameNoToBeAdded = gameNo;
            reference.Child("game").Child(gameNo).Child("userid1").SetValueAsync(userId);
        }
    }
    public void AddToGame()
    {
        
            string userid = this.GetUserIdOfPlayer();
            Debug.Log("User Id Of Player " + userid);
            bool gameComplete = false;
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
                        print("First if Condition Called key = "+i.Key);
                        this.AddPlayerToGame(userid, i.Key,i.ChildrenCount);
                        gameComplete = true;
                        break;
                        //}

                    }
                }
                string lastKey = "1";

                foreach (var j in snapshot.Children)
                {
                    lastKey = j.Key;
                }
                if (!gameComplete)
                {
                    print("First 2nd Condition Called");
                    this.AddPlayerToGame(userid, (int.Parse(lastKey) + 1).ToString(),0);
                }
            }
            else
            {
                print("First else Condition Called");
                this.AddPlayerToGame(userid, "1",0);
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
