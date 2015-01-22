This is a modify version of the NLua to make it work with Unity.
NLua is the bind between Lua world and the .NET world.
This version support lua 5.2.3 and use LuaInterface 2.0.4

To use this library you must have Unity Pro
Support platform: WIndows 32/64, Max OS, iOS, Android.

For detail information: https://github.com/NLua/NLua
The NLua guide can be find at 'Assets/NLua/Doc/'
This version maintainer: ZHing, zhing2006@hotmail.com

Usage:
Copy all things from 'Assets/NLua/Plugins/' to your project
Plugins directory.

iOS Limit:
iOS does NOT support jit, so some features of the lua is limited.
You can check NLua source by 'UNITY_IPHONE' defines.