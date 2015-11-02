//using UnityEngine;
//using System.Collections;
//using Regame;

//public class TestMessage : MonoBehaviour
//{

//    // Use this for initialization
//    void Awake()
//    {
//        Message.RegeditMessageHandle<string>("123", f);
//        Message.RegeditMessageHandle<int>("123", g);
//    }

//    void OnDestroy()
//    {
//        Message.RaiseOneMessage<string>("123", this, "right");
//        Message.UnregeditMessageHandle<string>("123", f);
//        Message.UnregeditMessageHandle<int>("123", g);
//        Message.RaiseOneMessage<string>("123", this, "something is wrong");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Message.RaiseOneMessage<string>("123", this, "0");
//        }
//        else if (Input.GetMouseButtonDown(1))
//        {
//            Message.RaiseOneMessage<int>("123", this, 0);
//        }
//    }

//    void f(object sender, string e)
//    {
//        print("f: " + e);
//    }

//    void g(object sender, int e)
//    {
//        print("g: " + e);
//    }
//}