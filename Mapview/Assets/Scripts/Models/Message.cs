﻿using System;
using System.Text;

namespace Assets.Scripts.Models
{
    public enum Type
    {
        Connect,
        Coordinate,
        Drawing,
        Disconnect,
        Corners,
        Default
    }

    [Serializable]
    public class Message
    {
        public string id;
        public Type type;
        public string message;

        public Message(string id, Type type, string message)
        {
            this.id = id;
            this.type = type;
            this.message = message;
        }

        public Message(string m)
        {
            string[] subs = m.Split('%');
            bool a = int.TryParse(subs[0], out int t);
            if (a)
            {
                type = (Type)t;
                switch (type)
                {
                    case Type.Connect:
                        id = subs[1];
                        message = "";
                        break;
                    case Type.Coordinate:
                        id = subs[1];
                        message = subs[2];
                        break;
                    case Type.Drawing:
                        id = subs[1];
                        message = subs[2];
                        break;
                    case Type.Disconnect:
                        id = subs[1];
                        message = "";
                        break;
                    case Type.Corners:
                        id = subs[1];
                        message = subs[2];
                        break;
                    default:
                        id = "";
                        message = "";
                        break;
                }
            }
            else
            {
                type = Type.Default;
                id = "";
                message = "";
            }
        }

        public override string ToString()
        {
            return $"Id{id}, message: {message}";
        }

        public byte[] ToByteArray()
        {
            string s = type switch
            {
                Type.Connect => $"0%{id}",
                Type.Coordinate => $"1%{id}%{message}",
                Type.Drawing => $"2%{id}%{message}",
                Type.Disconnect => $"3%{id}",
                Type.Corners => $"4%{id}%{message}",
                _ => "",
            };
            byte[] array = Encoding.UTF8.GetBytes(s);
            return array;
        }
    }
}
