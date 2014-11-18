using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Runtime.CompilerServices;

public delegate void GenericEventHandler();
public delegate void GenericEventHandler<T>(T t);
public delegate void GenericEventHandler<T, U>(T t, U u);
public delegate void GenericEventHandler<T, U, V>(T t, U u, V v);
public delegate void GenericEventHandler<T, U, V, W>(T t, U u, V v, W w);
public delegate void GenericEventHandler<T, U, V, W, X>(T t, U u, V v, W w, X x);
public delegate void GenericEventHandler<T, U, V, W, X, Y>(T t, U u, V v, W w, X x, Y y);
public delegate void GenericEventHandler<T, U, V, W, X, Y, Z>(T t, U u, V v, W w, X x, Y y, Z z);

public static class EventsHelper
{
   delegate void AsyncFire(Delegate del,object[] args);

   static void InvokeDelegate(Delegate del,object[] args)
   {
      ISynchronizeInvoke synchronizer  = del.Target as ISynchronizeInvoke;
      if(synchronizer != null)//Requires thread affinity
      {
         if(synchronizer.InvokeRequired)
         {
            synchronizer.Invoke(del,args);
            return;
         }
      }
      //Not requiring thread afinity or invoke is not required
      del.DynamicInvoke(args);
   }
   [MethodImpl(MethodImplOptions.NoInlining)]
   public static void UnsafeFire(Delegate del,params object[] args)
   {
      if(args.Length > 7)
      {
         Trace.TraceWarning("Too many parameters. Consider a structure to enable the use of the type-safe versions");
      }
      if(del == null)
      {
         return;
      }
      Delegate[] delegates = del.GetInvocationList();
      foreach(Delegate sink in delegates)
      {
         try
         {
            InvokeDelegate(sink,args);
         }
         catch
         {}
      }
   }
   [MethodImpl(MethodImplOptions.NoInlining)]
   public static void UnsafeFireAsync(Delegate del,params object[] args)
   {
      if(args.Length > 7)
      {
         Trace.TraceWarning("Too many parameters. Consider a structure to enable the use of the type-safe versions");
      }
      if(del == null)
      {
         return;
      }
      Delegate[] delegates = del.GetInvocationList();
      AsyncFire asyncFire = InvokeDelegate;
      AsyncCallback cleanUp = delegate(IAsyncResult asyncResult)
                              {
                                 asyncResult.AsyncWaitHandle.Close();
                              };
      foreach(Delegate sink in delegates)
      {
         asyncFire.BeginInvoke(sink,args,cleanUp,null);
      }
   }
   public static void Fire(EventHandler del,object sender,EventArgs e)
   {
      UnsafeFire(del,sender,e);
   }
   public static void Fire<T>(EventHandler<T> del,object sender,T t) where T : EventArgs
   {
      UnsafeFire(del,sender,t);
   }
   public static void Fire(GenericEventHandler del)
   {
      UnsafeFire(del);
   }
   public static void Fire<T>(GenericEventHandler<T> del,T t)
   {
      UnsafeFire(del,t);
   }
   public static void Fire<T,U>(GenericEventHandler<T,U> del,T t,U u)
   {
      UnsafeFire(del,t,u);
   }
   public static void Fire<T,U,V>(GenericEventHandler<T,U,V> del,T t,U u,V v)
   {
      UnsafeFire(del,t,u,v);
   }
   public static void Fire<T,U,V,W>(GenericEventHandler<T,U,V,W> del,T t,U u,V v,W w)
   {
      UnsafeFire(del,t,u,v,w);
   }
   public static void Fire<T,U,V,W,X>(GenericEventHandler<T,U,V,W,X> del,T t,U u,V v,W w,X x)
   {
      UnsafeFire(del,t,u,v,w,x);
   }
   public static void Fire<T,U,V,W,X,Y>(GenericEventHandler<T,U,V,W,X,Y> del,T t,U u,V v,W w,X x,Y y)
   {
      UnsafeFire(del,t,u,v,w,x,y);
   }
   public static void Fire<T,U,V,W,X,Y,Z>(GenericEventHandler<T,U,V,W,X,Y,Z> del,T t,U u,V v,W w,X x,Y y,Z z)
   {
      UnsafeFire(del,t,u,v,w,x,y,z);
   }
   public static void FireAsync(EventHandler del,object sender,EventArgs e)
   {
      UnsafeFireAsync(del,sender,e);
   }
   public static void FireAsync<T>(EventHandler<T> del,object sender,T t) where T : EventArgs
   {
      UnsafeFireAsync(del,sender,t);
   }
   public static void FireAsync(GenericEventHandler del)
   {
      UnsafeFireAsync(del);
   }
   public static void FireAsync<T>(GenericEventHandler<T> del,T t)
   {
      UnsafeFireAsync(del,t);
   }
   public static void FireAsync<T,U>(GenericEventHandler<T,U> del,T t,U u)
   {
      UnsafeFireAsync(del,t,u);
   }
   public static void FireAsync<T,U,V>(GenericEventHandler<T,U,V> del,T t,U u,V v)
   {
      UnsafeFireAsync(del,t,u,v);
   }
   public static void FireAsync<T,U,V,W>(GenericEventHandler<T,U,V,W> del,T t,U u,V v,W w)
   {
      UnsafeFireAsync(del,t,u,v,w);
   }
   public static void FireAsync<T,U,V,W,X>(GenericEventHandler<T,U,V,W,X> del,T t,U u,V v,W w,X x)
   {
      UnsafeFireAsync(del,t,u,v,w,x);
   }
   public static void FireAsync<T,U,V,W,X,Y>(GenericEventHandler<T,U,V,W,X,Y> del,T t,U u,V v,W w,X x,Y y)
   {
      UnsafeFireAsync(del,t,u,v,w,x,y);
   }
   public static void FireAsync<T,U,V,W,X,Y,Z>(GenericEventHandler<T,U,V,W,X,Y,Z> del,T t,U u,V v,W w,X x,Y y,Z z)
   {
      UnsafeFireAsync(del,t,u,v,w,x,y,z);
   }
}