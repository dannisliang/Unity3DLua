﻿
using System;
using System.Collections;
using System.Runtime.InteropServices;




public class Lua52Native
{
    private const string LUA_DLL = "lua";

    [DllImport(LUA_DLL , EntryPoint = "lua_newstate")]
    public static extern IntPtr lua_newstate(IntPtr f, IntPtr ud);
    [DllImport(LUA_DLL , EntryPoint = "lua_close")]
    public static extern void lua_close(IntPtr L);

    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_newthread (IntPtr L);
    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_atpanic(IntPtr L, IntPtr panicf);

    [DllImport(LUA_DLL , EntryPoint = "lua_version")]
    public static extern double lua_version(IntPtr L);

    /*
    ** basic stack manipulation
    */
    [DllImport(LUA_DLL)]
    public static extern int   lua_absindex(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern int   lua_gettop(IntPtr L);
    [DllImport(LUA_DLL)]
    public static extern void  lua_settop(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_pushvalue(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_remove(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_insert(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_replace(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_copy(IntPtr L, int fromidx, int toidx);
    [DllImport(LUA_DLL)]
    public static extern int   lua_checkstack(IntPtr L, int sz);

    [DllImport(LUA_DLL)]
    public static extern void  lua_xmove(IntPtr from, IntPtr to, int n);

    /*
    ** access functions (stack -> C)
    */

    [DllImport(LUA_DLL)]
    public static extern int lua_isnumber(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern int lua_isstring(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern int lua_iscfunction(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern int lua_isuserdata(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern int lua_type(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern string lua_typename(IntPtr L, int tp);

    [DllImport(LUA_DLL)]
    public static extern double lua_tonumberx(IntPtr L, int idx, IntPtr isnum);
    [DllImport(LUA_DLL)]
    public static extern int lua_tointegerx(IntPtr L, int idx, IntPtr isnum);
    [DllImport(LUA_DLL)]
    public static extern uint lua_tounsignedx(IntPtr L, int idx, IntPtr isnum);
    [DllImport(LUA_DLL)]
    public static extern int lua_toboolean(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    // public static extern const string lua_tolstring(IntPtr L, int idx, size_t *len);
    public static extern string lua_tolstring(IntPtr L, int idx, IntPtr len);
    [DllImport(LUA_DLL)]
    public static extern int lua_rawlen(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_tocfunction(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_touserdata(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_tothread(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_topointer(IntPtr L, int idx);

    /*
    ** Comparison and arithmetic functions
    */

    // #define LUA_OPADD   0   /* ORDER TM */
    // #define LUA_OPSUB   1
    // #define LUA_OPMUL   2
    // #define LUA_OPDIV   3
    // #define LUA_OPMOD   4
    // #define LUA_OPPOW   5
    // #define LUA_OPUNM   6

    [DllImport(LUA_DLL)]
    public static extern void  lua_arith(IntPtr L, int op);

    // #define LUA_OPEQ    0
    // #define LUA_OPLT    1
    // #define LUA_OPLE    2

    [DllImport(LUA_DLL)]
    public static extern int   lua_rawequal(IntPtr L, int idx1, int idx2);
    [DllImport(LUA_DLL)]
    public static extern int   lua_compare(IntPtr L, int idx1, int idx2, int op);


    /*
    ** push functions (C -> stack)
    */
    [DllImport(LUA_DLL)]
    public static extern void lua_pushnil(IntPtr L);
    [DllImport(LUA_DLL)]
    public static extern void lua_pushnumber(IntPtr L, double n);
    [DllImport(LUA_DLL)]
    public static extern void lua_pushinteger(IntPtr L, int n);
    [DllImport(LUA_DLL)]
    public static extern void lua_pushunsigned(IntPtr L, uint n);
    [DllImport(LUA_DLL)]
    public static extern string lua_pushlstring(IntPtr L, string s, int l);
    [DllImport(LUA_DLL)]
    public static extern string lua_pushstring(IntPtr L, string s);
    // [DllImport(LUA_DLL)]
    // public static extern const string lua_pushvfstring(IntPtr L, const string fmt,
    //                                                       va_list argp);
    // [DllImport(LUA_DLL)]
    // public static extern const string lua_pushfstring(IntPtr L, const string fmt, ...);
    [DllImport(LUA_DLL)]
    public static extern void  lua_pushcclosure(IntPtr L, IntPtr fn, int n);
    [DllImport(LUA_DLL)]
    public static extern void  lua_pushboolean (IntPtr L, int b);
    [DllImport(LUA_DLL)]
    public static extern void  lua_pushlightuserdata(IntPtr L, IntPtr p);
    [DllImport(LUA_DLL)]
    public static extern int   lua_pushthread(IntPtr L);

    /*
    ** get functions (Lua -> stack)
    */
    [DllImport(LUA_DLL)]
    public static extern void  lua_getglobal(IntPtr L, string var);
    [DllImport(LUA_DLL)]
    public static extern void  lua_gettable(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_getfield(IntPtr L, int idx, string k);
    [DllImport(LUA_DLL)]
    public static extern void  lua_rawget(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_rawgeti(IntPtr L, int idx, int n);
    [DllImport(LUA_DLL)]
    public static extern void  lua_rawgetp(IntPtr L, int idx, IntPtr p);
    [DllImport(LUA_DLL)]
    public static extern void  lua_createtable(IntPtr L, int narr, int nrec);
    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_newuserdata(IntPtr L, int sz);
    [DllImport(LUA_DLL)]
    public static extern int   lua_getmetatable(IntPtr L, int objindex);
    [DllImport(LUA_DLL)]
    public static extern void  lua_getuservalue(IntPtr L, int idx);

    /*
    ** set functions (stack -> Lua)
    */
    [DllImport(LUA_DLL)]
    public static extern void  lua_setglobal(IntPtr L, string var);
    [DllImport(LUA_DLL)]
    public static extern void  lua_settable(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_setfield(IntPtr L, int idx, string k);
    [DllImport(LUA_DLL)]
    public static extern void  lua_rawset(IntPtr L, int idx);
    [DllImport(LUA_DLL)]
    public static extern void  lua_rawseti(IntPtr L, int idx, int n);
    [DllImport(LUA_DLL)]
    public static extern void  lua_rawsetp(IntPtr L, int idx, IntPtr p);
    [DllImport(LUA_DLL)]
    public static extern int   lua_setmetatable(IntPtr L, int objindex);
    [DllImport(LUA_DLL)]
    public static extern void  lua_setuservalue(IntPtr L, int idx);


    /*
    ** 'load' and 'call' functions (load and run Lua code)
    */
    [DllImport(LUA_DLL)]
    public static extern void  lua_callk(IntPtr L, int nargs, int nresults, int ctx,
                               IntPtr k);
    // #define lua_call(L,n,r)     lua_callk(L, (n), (r), 0, NULL)

    [DllImport(LUA_DLL)]
    public static extern int   lua_getctx(IntPtr L, IntPtr ctx);

    [DllImport(LUA_DLL)]
    public static extern int   lua_pcallk(IntPtr L, int nargs, int nresults, int errfunc,
                                int ctx, IntPtr k);
    // #define lua_pcall(L,n,r,f)  lua_pcallk(L, (n), (r), (f), 0, NULL)

    [DllImport(LUA_DLL)]
    public static extern int   lua_load(IntPtr L, IntPtr reader, IntPtr dt,
                                            string chunkname,
                                            string mode);

    [DllImport(LUA_DLL)]
    public static extern int lua_dump(IntPtr L, IntPtr writer, IntPtr data);

    /*
    ** coroutine functions
    */
    [DllImport(LUA_DLL)]
    public static extern int  lua_yieldk(IntPtr L, int nresults, int ctx,
                               IntPtr k);
    // #define lua_yield(L,n)      lua_yieldk(L, (n), 0, NULL)
    [DllImport(LUA_DLL)]
    public static extern int  lua_resume(IntPtr L, IntPtr from, int narg);
    [DllImport(LUA_DLL)]
    public static extern int  lua_status(IntPtr L);

    /*
    ** garbage-collection function and options
    */

    // #define LUA_GCSTOP      0
    // #define LUA_GCRESTART       1
    // #define LUA_GCCOLLECT       2
    // #define LUA_GCCOUNT     3
    // #define LUA_GCCOUNTB        4
    // #define LUA_GCSTEP      5
    // #define LUA_GCSETPAUSE      6
    // #define LUA_GCSETSTEPMUL    7
    // #define LUA_GCSETMAJORINC   8
    // #define LUA_GCISRUNNING     9
    // #define LUA_GCGEN       10
    // #define LUA_GCINC       11

    [DllImport(LUA_DLL)]
    public static extern int lua_gc (IntPtr L, int what, int data);

    /*
    ** miscellaneous functions
    */

    [DllImport(LUA_DLL)]
    public static extern int lua_error(IntPtr L);

    [DllImport(LUA_DLL)]
    public static extern int lua_next(IntPtr L, int idx);

    [DllImport(LUA_DLL)]
    public static extern void lua_concat(IntPtr L, int n);
    [DllImport(LUA_DLL)]
    public static extern void lua_len(IntPtr L, int idx);

    [DllImport(LUA_DLL)]
    public static extern IntPtr lua_getallocf(IntPtr L, IntPtr ud);
    [DllImport(LUA_DLL)]
    public static extern void lua_setallocf(IntPtr L, IntPtr f, IntPtr ud);


    /*
    ** ===============================================================
    ** some useful macros
    ** ===============================================================
    */


    // #define lua_tonumber(L,i)   lua_tonumberx(L,i,NULL)
    // #define lua_tointeger(L,i)  lua_tointegerx(L,i,NULL)
    // #define lua_tounsigned(L,i) lua_tounsignedx(L,i,NULL)

    // #define lua_pop(L,n)        lua_settop(L, -(n)-1)

    // #define lua_newtable(L)     lua_createtable(L, 0, 0)

    // #define lua_register(L,n,f) (lua_pushcfunction(L, (f)), lua_setglobal(L, (n)))

    // #define lua_pushcfunction(L,f)  lua_pushcclosure(L, (f), 0)

    // #define lua_isfunction(L,n) (lua_type(L, (n)) == LUA_TFUNCTION)
    // #define lua_istable(L,n)    (lua_type(L, (n)) == LUA_TTABLE)
    // #define lua_islightuserdata(L,n)    (lua_type(L, (n)) == LUA_TLIGHTUSERDATA)
    // #define lua_isnil(L,n)      (lua_type(L, (n)) == LUA_TNIL)
    // #define lua_isboolean(L,n)  (lua_type(L, (n)) == LUA_TBOOLEAN)
    // #define lua_isthread(L,n)   (lua_type(L, (n)) == LUA_TTHREAD)
    // #define lua_isnone(L,n)     (lua_type(L, (n)) == LUA_TNONE)
    // #define lua_isnoneornil(L, n)   (lua_type(L, (n)) <= 0)

    // #define lua_pushliteral(L, s)   \
    //     lua_pushlstring(L, "" s, (sizeof(s)/sizeof(char))-1)

    // #define lua_pushglobaltable(L)  \
    //     lua_rawgeti(L, LUA_REGISTRYINDEX, LUA_RIDX_GLOBALS)

    // #define lua_tostring(L,i)   lua_tolstring(L, (i), NULL)



/**********************************************
    lauxlib.h
***********************************************/

    [DllImport(LUA_DLL)]
    public static extern void luaL_checkversion_(IntPtr L, double ver);
    // #define luaL_checkversion(L)    luaL_checkversion_(L, LUA_VERSION_NUM)

    [DllImport(LUA_DLL)]
    public static extern int luaL_getmetafield(IntPtr L, int obj, string e);
    [DllImport(LUA_DLL)]
    public static extern int luaL_callmeta(IntPtr L, int obj, string e);
    [DllImport(LUA_DLL)]
    public static extern string luaL_tolstring(IntPtr L, int idx, out uint len);
    [DllImport(LUA_DLL)]
    public static extern int luaL_argerror(IntPtr L, int numarg, string extramsg);
    [DllImport(LUA_DLL)]
    public static extern string luaL_checklstring (IntPtr L, int numArg,
                                                              out uint l);
    [DllImport(LUA_DLL)]
    public static extern string luaL_optlstring(IntPtr L, int numArg,
                                              string def, out uint l);
    [DllImport(LUA_DLL)]
    public static extern double luaL_checknumber(IntPtr L, int numArg);
    [DllImport(LUA_DLL)]
    public static extern double luaL_optnumber(IntPtr L, int nArg, double def);

    [DllImport(LUA_DLL)]
    public static extern int luaL_checkinteger(IntPtr L, int numArg);
    [DllImport(LUA_DLL)]
    public static extern int luaL_optinteger(IntPtr L, int nArg,
                                              int def);
    [DllImport(LUA_DLL)]
    public static extern uint luaL_checkunsigned(IntPtr L, int numArg);
    [DllImport(LUA_DLL)]
    public static extern uint luaL_optunsigned(IntPtr L, int numArg,
                                                uint def);

    [DllImport(LUA_DLL)]
    public static extern void luaL_checkstack(IntPtr L, int sz, string msg);
    [DllImport(LUA_DLL)]
    public static extern void luaL_checktype(IntPtr L, int narg, int t);
    [DllImport(LUA_DLL)]
    public static extern void luaL_checkany(IntPtr L, int narg);

    [DllImport(LUA_DLL)]
    public static extern int   luaL_newmetatable(IntPtr L, string tname);
    [DllImport(LUA_DLL)]
    public static extern void  luaL_setmetatable(IntPtr L, string tname);
    [DllImport(LUA_DLL)]
    public static extern IntPtr luaL_testudata(IntPtr L, int ud, string tname);
    [DllImport(LUA_DLL)]
    public static extern IntPtr luaL_checkudata(IntPtr L, int ud, string tname);

    [DllImport(LUA_DLL)]
    public static extern void luaL_where(IntPtr L, int lvl);
    // [DllImport(LUA_DLL)]
    // public static extern int luaL_error(IntPtr L, const string fmt, ...);

    [DllImport(LUA_DLL)]
    public static extern int luaL_checkoption(IntPtr L, int narg, string def,
                                       string[] lst);

    [DllImport(LUA_DLL)]
    public static extern int luaL_fileresult(IntPtr L, int stat, string fname);
    [DllImport(LUA_DLL)]
    public static extern int luaL_execresult(IntPtr L, int stat);

    /* pre-defined references */
    // #define LUA_NOREF       (-2)
    // #define LUA_REFNIL      (-1)

    [DllImport(LUA_DLL)]
    public static extern int luaL_ref(IntPtr L, int t);
    [DllImport(LUA_DLL)]
    public static extern void luaL_unref(IntPtr L, int t, int refi);

    [DllImport(LUA_DLL)]
    public static extern int luaL_loadfilex(IntPtr L, string filename,
                                                   string mode);

    // #define luaL_loadfile(L,f)  luaL_loadfilex(L,f,NULL)

    [DllImport(LUA_DLL)]
    public static extern int luaL_loadbufferx(IntPtr L, string buff, uint sz,
                                       string name, string mode);
    [DllImport(LUA_DLL)]
    public static extern int luaL_loadstring(IntPtr L, string s);

    [DllImport(LUA_DLL , EntryPoint = "luaL_newstate")]
    public static extern IntPtr luaL_newstate();

    [DllImport(LUA_DLL)]
    public static extern int luaL_len(IntPtr L, int idx);

    [DllImport(LUA_DLL)]
    public static extern string luaL_gsub(IntPtr L, string s, string p,
                                                      string r);

    [DllImport(LUA_DLL)]
    public static extern void luaL_setfuncs(IntPtr L, IntPtr l, int nup);

    [DllImport(LUA_DLL)]
    public static extern int luaL_getsubtable(IntPtr L, int idx, string fname);

    [DllImport(LUA_DLL)]
    public static extern void luaL_traceback(IntPtr L, IntPtr L1,
                                      string msg, int level);

    [DllImport(LUA_DLL)]
    public static extern void luaL_requiref(IntPtr L, string modname,
                                     IntPtr openf, int glb);

    /*
    ** ===============================================================
    ** some useful macros
    ** ===============================================================
    */


    // #define luaL_newlibtable(L,l)   \
    //   lua_createtable(L, 0, sizeof(l)/sizeof((l)[0]) - 1)

    // #define luaL_newlib(L,l)    (luaL_newlibtable(L,l), luaL_setfuncs(L,l,0))

    // #define luaL_argcheck(L, cond,numarg,extramsg)  \
    //         ((void)((cond) || luaL_argerror(L, (numarg), (extramsg))))
    // #define luaL_checkstring(L,n)   (luaL_checklstring(L, (n), NULL))
    // #define luaL_optstring(L,n,d)   (luaL_optlstring(L, (n), (d), NULL))
    // #define luaL_checkint(L,n)  ((int)luaL_checkinteger(L, (n)))
    // #define luaL_optint(L,n,d)  ((int)luaL_optinteger(L, (n), (d)))
    // #define luaL_checklong(L,n) ((long)luaL_checkinteger(L, (n)))
    // #define luaL_optlong(L,n,d) ((long)luaL_optinteger(L, (n), (d)))

    // #define luaL_typename(L,i)  lua_typename(L, lua_type(L,(i)))

    // #define luaL_dofile(L, fn) \
    //     (luaL_loadfile(L, fn) || lua_pcall(L, 0, LUA_MULTRET, 0))

    // #define luaL_dostring(L, s) \
    //     (luaL_loadstring(L, s) || lua_pcall(L, 0, LUA_MULTRET, 0))

    // #define luaL_getmetatable(L,n)  (lua_getfield(L, LUA_REGISTRYINDEX, (n)))

    // #define luaL_opt(L,f,n,d)   (lua_isnoneornil(L,(n)) ? (d) : f(L,(n)))

    // #define luaL_loadbuffer(L,s,sz,n)   luaL_loadbufferx(L,s,sz,n,NULL)


    /*
    ** {======================================================
    ** Generic Buffer manipulation
    ** =======================================================
    */

    // typedef struct luaL_Buffer {
    //   string b;  /* buffer address */
    //   size_t size;  /* buffer size */
    //   size_t n;  /* number of characters in buffer */
    //   IntPtr L;
    //   char initb[LUAL_BUFFERSIZE];  /* initial buffer */
    // } luaL_Buffer;


    // #define luaL_addchar(B,c) \
    //   ((void)((B)->n < (B)->size || luaL_prepbuffsize((B), 1)), \
    //    ((B)->b[(B)->n++] = (c)))

    // #define luaL_addsize(B,s)   ((B)->n += (s))

    [DllImport(LUA_DLL)]
    public static extern void luaL_buffinit(IntPtr L, IntPtr B);
    [DllImport(LUA_DLL)]
    public static extern string luaL_prepbuffsize(IntPtr B, uint sz);
    [DllImport(LUA_DLL)]
    public static extern void luaL_addlstring(IntPtr B, string s, uint l);
    [DllImport(LUA_DLL)]
    public static extern void luaL_addstring(IntPtr B, string s);
    [DllImport(LUA_DLL)]
    public static extern void luaL_addvalue(IntPtr B);
    [DllImport(LUA_DLL)]
    public static extern void luaL_pushresult(IntPtr B);
    [DllImport(LUA_DLL)]
    public static extern void luaL_pushresultsize(IntPtr B, uint sz);
    [DllImport(LUA_DLL)]
    public static extern string luaL_buffinitsize(IntPtr L, IntPtr B, uint sz);

    // #define luaL_prepbuffer(B)  luaL_prepbuffsize(B, LUAL_BUFFERSIZE)



}
