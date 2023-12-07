//using Assets.Scripts.Models;
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;
////using Vuforia;
//using static UnityEngine.GraphicsBuffer;

//public class ARController : MonoBehaviour
//{
//    public static ARController Instance;
//    private Information information;

//    [SerializeField]
//    private TcpMarauderClient client = null;

//    private List<MarauderPath> paths;

//    private bool connected = false;
//    private bool tracked = false;
//    public bool Tracked { get => tracked; set => tracked = value; }

//    [SerializeField]
//    GameObject ARCamera = null;

//    public void UpdateDatas()
//    {
//        string s = client.GetS();
//        Message m = new Message(s);
//        switch (m.type)
//        {
//            case 0:
//                Debug.Log("Connected");
//                break;
//            case (Assets.Scripts.Models.Type)1:
//                break;
//            case (Assets.Scripts.Models.Type)2:
//                // TODO: megkapja a rajzokat és kirajzolja
//                break;
//            case (Assets.Scripts.Models.Type)3:
//                Debug.Log("Disconnect");
//                break;
//            default:
//                break;
//        }
//    }

   
//    void Start()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }

//        Init();
//        InvokeRepeating(nameof(SendCoordinates), 0, 0.5f);
//        paths = new List<MarauderPath>();
//    }

//    public void SetTargets()
//    {
//        string dataSetPath = "Vuforia/Marauder.xml";

//        for (int i = 0; i < information.targets.Count; i++)
//        {
//            string[] coordinates = information.targets[i].Split(';');

//            //var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(dataSetPath,i.ToString());
//            //mImageTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
//            //mImageTarget.transform.position = new Vector3(float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));
//        }
//    }

//    public async void Init()
//    {
//        try
//        {
//            PlayerPrefs.SetInt("Tracked", 0);
//            // megkapja az infókat az API-tól
//            var i = await ServerConnection.Instance.GetInformation();
//            information = new Information
//            {
//                port = i.port,
//                address = i.address,
//                walls = i.walls,
//                targets = i.targets
//            };
//            SetTargets();
//            // csatlakozik a TCP szerverhez
//            ConnectToServer();
//            // feliratkozik a TCP szerver eseményeire
//            client.NotifyParentEvent += new NotifyControllerDelegate(UpdateDatas);
//        }
//        catch (System.Exception e)
//        {
//            Debug.Log(e.ToString());
//        }
//    }

    

//    public void ConnectToServer()
//    {
//        client.ApplyInputAndConnect(information.address, information.port);
//        connected = true;
//    }

//    public void SendCoordinates()
//    {
//        if (PlayerPrefs.GetInt("Tracked") == 1)
//        {
//            var position = ARCamera.transform.position;
//            Message m = new Message("ARUser", (Assets.Scripts.Models.Type)1, position.x + ";" + position.y + ";" + position.z);
//            client.SendAsynch(m.ToByteArray());
//        }
//    }

//    void OnApplicationQuit()
//    {
//        if (connected)
//        {
//            //Message m = new Message(currentUser.name, (Assets.Scripts.Models.Type)3, "");
//            //client.SendAsynch(m.ToByteArray());
//        }
//    }
//}
