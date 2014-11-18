using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{

public abstract class TraceableSvc {
    public static void FromReader(SqlDataReader rdr, Traceable val) {
        val.lastUpdateDate = rdr.GetDateTime(rdr.GetOrdinal("LastUpdateDate"));
        val.createdByUser = rdr.GetString(rdr.GetOrdinal("CreatedByUser"));
        val.dateCreated = rdr.GetDateTime(rdr.GetOrdinal("DateCreated"));
    }
};

}