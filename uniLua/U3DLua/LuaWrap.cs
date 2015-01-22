using System;
using UniLua;
using System.Runtime.InteropServices;


namespace UniLua
{
    public struct LuaMethod
    {
        public string name;
        public CSharpFunctionDelegate func;

        public LuaMethod(string str, CSharpFunctionDelegate f)
        {
            name = str;
            func = f;
        }
    };

    public struct LuaField
    {
        public string name;
        public CSharpFunctionDelegate getter;
        public CSharpFunctionDelegate setter;

        public LuaField(string str, CSharpFunctionDelegate g, CSharpFunctionDelegate s)
        {
            name = str;
            getter = g;
            setter = s;        
        }
    };

    public struct LuaEnum
    {
        public string name;
        public object val;

		public LuaEnum(string str, object v)
        {
            name = str;
            val = v;
        }
    }

    public class NoToLuaAttribute : System.Attribute
    {
        public NoToLuaAttribute()
        {
            //
        }
    }

    public interface ILuaWrap 
    {
        void Register();
    }

    public class LuaStringBuffer
    {
        public LuaStringBuffer(IntPtr source,int len)
        {
            buffer = new byte[len];
            Marshal.Copy(source, buffer, 0, len);        
        }

        public byte[] buffer = null;    
    }
}