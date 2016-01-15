using System;
using System.Collections.Generic;

namespace UnityTool.Libgame
{
    public class NONTYPE
    {

    }

    class MessageManager<TEventArgs>
    {
        static Dictionary<string, MessageHandle<TEventArgs>> handles = new Dictionary<string, MessageHandle<TEventArgs>>();

        static public void AddEvent(string messageName, MessageHandle<TEventArgs> function)
        {
            if (handles.ContainsKey(messageName))
            {
                handles[messageName] += function;
            }
            else
            {
                handles.Add(messageName, function);
            }
        }

        static public void RemoveEvent(string messageName, MessageHandle<TEventArgs> function)
        {
            if (handles.ContainsKey(messageName))
            {
                handles[messageName] -= function;
                if (handles[messageName] == null)
                {
                    handles.Remove(messageName);
                }

            }
            else
            {
                ;
            }
        }

        static public void RunEvent(string messageName, object sender, TEventArgs e)
        {
            if (handles.ContainsKey(messageName))
            {
                handles[messageName](messageName, sender, e);
            }
            else
            {
                ;
            }
        }
    }

    public delegate void MessageHandle<TEventArgs>(String messageName, object sender, TEventArgs e);
    public class Message
    {
        static public void RegeditMessageHandle<TEventArgs>(String messageName, MessageHandle<TEventArgs> function)
        {
            MessageManager<TEventArgs>.AddEvent(messageName, function);
        }
        static public void UnregeditMessageHandle<TEventArgs>(String messageName, MessageHandle<TEventArgs> function)
        {
            MessageManager<TEventArgs>.RemoveEvent(messageName, function);
        }
        static public void RaiseOneMessage<TEventArgs>(String messageName, object sender, TEventArgs e)
        {
            MessageManager<TEventArgs>.RunEvent(messageName, sender, e);
        }
    }
}