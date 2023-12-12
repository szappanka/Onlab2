using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Marauder2DController : MonoBehaviour
{
    public static Marauder2DController Instance;
    private readonly List<User> users = new List<User>();
    private Information information;
    [SerializeField]
    private Brush brush;
    [SerializeField]
    private BluePrintDrawer blueprintDrawer;

    [SerializeField]
    private TcpMarauderClient client;

    private List<Vector3> corners = new List<Vector3>();

    private bool connected = false;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
        client.NotifyParentEvent += new NotifyControllerDelegate(UpdateDatas);
        brush.NotifyParentEvent += new NotifyControllerDelegate(SendDrawing);
    }

    // Update is called once per frame
    void Update()
    {
        //if (connected)
        //{
        //    WallInit();
        //}
    }

    public void WallInit()
    {
        for(int i = 0; i < information.walls.Count; i++)
        {
            string[] s = information.walls[i].Split('=');
            string[] startCoords = s[0].Split(';');
            string[] endCoords = s[1].Split(';');
            Vector3 start1 = new Vector3(float.Parse(startCoords[0]), float.Parse(startCoords[1]), float.Parse(startCoords[2]));
            Vector3 end1 = new Vector3(float.Parse(endCoords[0]), float.Parse(endCoords[1]), float.Parse(endCoords[2]));
            Debug.DrawLine(start1, end1);
        }
    }

    private void BlueprintInit(string message)
    {
        // törölük az összes "Blueprint" tages objektumot

        GameObject[] prefabInstances = GameObject.FindGameObjectsWithTag("Blueprint");

        foreach (var instance in prefabInstances)
        {
            Destroy(instance);
        }


        string[] vectors = message.Split('#');
        for (int i = 0; i < vectors.Length; i++)
        {
            Vector3 v = JsonUtility.FromJson<Vector3>(vectors[i]);
            v.y = 1;
            corners.Add(v);
        }
        BlueprintDraw();
    }

    private void BlueprintDraw()
    {
        blueprintDrawer.Init(corners);
        blueprintDrawer.DrawBluePrint();
    }

    public void UpdateDatas()
    {
        string s = client.GetS();
        Message m = new Message(s);
        switch (m.type)
        {
            case 0:
                User u = new User();
                u.SetName(m.id);
                users.Add(u);
                break;
            case (Assets.Scripts.Models.Type)1:
                User user1 = users.Find(x => x.name == m.id);
                user1?.SetCoordinates(m.message);
                break;
            case (Assets.Scripts.Models.Type)2:
                // itt ezt nem kell lekezelni, mert a szervernek nem kell tudnia a rajzolásról
                break;
            case (Assets.Scripts.Models.Type)3:
                User user2 = users.Find(x => x.name == m.id);
                users.Remove(user2);
                break;
            case (Assets.Scripts.Models.Type)4:
                BlueprintInit(m.message);
                break;
            default:
                break;
        }
        Draw(m);
    }

    public void Draw(Message m)
    {

        if (m.type == 0)
        {
            GameObject go = Resources.Load("Prefabs/MapUser") as GameObject;
            GameObject player = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);
            player.name = m.id;
        }
        else if (m.type == (Assets.Scripts.Models.Type)1)
        {

            if (GameObject.Find(m.id) == null) {
                GameObject go = Resources.Load("Prefabs/MapUser") as GameObject;
                GameObject player = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);
                player.name = m.id;
            }

            GameObject mapUser = GameObject.Find(m.id);

            string[] coords = m.message.Split(';');
            Vector3 v = new Vector3(float.Parse(coords[0]), 0f, float.Parse(coords[2]));
            if (mapUser)
            {
                mapUser.transform.position = v;
            } else
            {
                GameObject go = Resources.Load("Prefabs/MapUser") as GameObject;
                GameObject player = Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);
                player.name = m.id;
            }
        }
        else if (m.type == (Assets.Scripts.Models.Type)3)
        {
            GameObject mapUser = GameObject.Find(m.id);
            Destroy(mapUser);
        }
    }


    public void Init()
    {
        try
        {
            // var i = await ServerConnection.Instance.GetInformation();
            information = new Information
            {
                port = 1111,
                // address = "127.0.0.1",
                address = "152.66.169.230",
                walls = null,
                targets = null
            };

            ConnectedToServer();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void ConnectedToServer()
    {
        client.ApplyInputAndConnect(information.address, information.port);
        connected = true;
    }

    public void SendDrawing()
    {
        List<List<Vector3>> drawings = brush.AllData();

        foreach (List<Vector3> drawing in drawings)
        {
            Drawing d = new Drawing(drawing);
            string b = d.ToString();
            Message m = new Message("map", (Assets.Scripts.Models.Type)2, b);
            client.SendAsynch(m.ToByteArray());
        }
    }
}
