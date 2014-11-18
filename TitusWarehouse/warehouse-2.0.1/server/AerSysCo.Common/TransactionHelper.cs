/*
 *  $Id: TransactionHelper.cs 13254 2006-10-03 14:19:48Z sergeyr $
 *
 *  Copyright(c) 2004 Exceleron Software
 */
using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Data.SqlClient;
using log4net;

namespace AerSysCo.Common
{
public abstract class TransactionHelper
{

    public const string CANNOT_OPEN_CONNECTION = "Cannot open connection";
    public const string UNEXPECTED_EXCEPTION = "Unexpected exception";

    public const string TRANSACTIONAL_INSERT = "TransactionalInsert";
    public const string TRANSACTIONAL_UPDATE = "TransactionalUpdate";
    public const string TRANSACTIONAL_DELETE = "TransactionalDelete";

    private static ILog GetLogger() {
        return LogManager.GetLogger(typeof(TransactionHelper));
    }

    public static void Transaction(object obj, string method, params object[] parameters) {
        Transaction(SQLHelper.CONN_STRING, obj, IsolationLevel.ReadCommitted, method, parameters);
    }

    public static void Transaction(object obj, IsolationLevel level, string method, params object[] parameters) {
        Transaction(SQLHelper.CONN_STRING, obj, level, method, parameters);
    }

    public static void Transaction(string connection, object obj, string method, params object[] parameters) {
        Transaction(connection, obj, IsolationLevel.ReadCommitted, method, parameters);
    }

    public static void Transaction(string connection, object obj, IsolationLevel level, string method, params object[] parameters) {
        Type objType = obj.GetType();
        MethodInfo methodInfo = objType.GetMethod(method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        GetLogger().Debug("Method info for " + method + ": " + methodInfo);
        try {
            using (SqlConnection conn = new SqlConnection(connection)) {
                conn.Open();
                GetLogger().Debug("[Begin transaction.]");
                using (SqlTransaction trans = conn.BeginTransaction(level)) {
                    try {
                        ArrayList newParams = new ArrayList(parameters);
                        newParams.Insert(0, trans);
                        methodInfo.Invoke(obj, newParams.ToArray());
                        trans.Commit();
                        GetLogger().Debug("[Commited.]");
                    } catch (TargetInvocationException tex) {
                        trans.Rollback();
                        GetLogger().Debug("[Rolledbacked cause exception occured. Rethrow inner.]", tex);
                        if ( !(tex.InnerException is RollbackOnlyException) ) {
                            throw tex.InnerException;
                        }
                        if ( tex.InnerException is VersionNotFoundException ) {
                            throw tex.InnerException;
                        }
                    } catch (Exception ex) {
                        trans.Rollback();
                        GetLogger().Debug("[Rolledbacked cause exception occured. Rethrow.]", ex);
                        throw ex;
                    }
                }
            }
        } catch (SqlException ex) {
            throw new SystemException(TransactionHelper.CANNOT_OPEN_CONNECTION, ex);
        }
    }

    public static object QueryTransaction(object obj, string method, 
        params object[] parameters) {

        return QueryTransaction(SQLHelper.CONN_STRING, obj, 
            IsolationLevel.ReadCommitted, method, parameters);
    }

    public static object QueryTransaction(object obj, IsolationLevel level, 
        string method, params object[] parameters) {

        return QueryTransaction(SQLHelper.CONN_STRING, obj, level, 
            method, parameters);
    }

    public static object QueryTransaction(string connection, object obj, 
        IsolationLevel level, string method, params object[] parameters) {

        Type objType = obj.GetType();
        MethodInfo methodInfo = objType.GetMethod(method, 
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        GetLogger().Debug("Method info for " + method + ": " + methodInfo);
        try {
            using (SqlConnection conn = new SqlConnection(connection)) {
                conn.Open();
                GetLogger().Debug("[Begin transaction.]");
                using (SqlTransaction trans = conn.BeginTransaction(level)) {
                    object result = null;
                    try {
                        ArrayList newParams = new ArrayList(parameters);
                        newParams.Insert(0, trans);
                        result = methodInfo.Invoke(obj, newParams.ToArray());
                        trans.Commit();
                        GetLogger().Debug("[Commited.]");

                    } catch (TargetInvocationException tex) {
                        trans.Rollback();
                        GetLogger().Debug("[Rolledbacked cause exception occured. Rethrow inner.]", tex);
                        if ( !(tex.InnerException is RollbackOnlyException) ) {
                            throw tex.InnerException;
                        }
                        if ( tex.InnerException is VersionNotFoundException ) {
                            throw tex.InnerException;
                        }
                    } catch (Exception ex) {
                        trans.Rollback();
                        GetLogger().Debug("[Rolledbacked cause exception occured. Rethrow.]", ex);
                        throw ex;
                    }
                    return result;
                }
            }
        } catch (SqlException ex) {
            throw new SystemException(TransactionHelper.CANNOT_OPEN_CONNECTION, ex);
        }
    }
        
}
}
