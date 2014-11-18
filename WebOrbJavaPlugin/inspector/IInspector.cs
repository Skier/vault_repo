using System;
using System.Collections;
using System.Text;

namespace Weborb.Data.Inspector2
{
    public interface IInspector
    {
        String[] GetDatabases();
        String[] GetTables( string database );
        ColumnInfo[] GetColumns( string database, string table );
        QueryResult TestQuery( string database, string query );
        string Ping();
        void GenerateSourceCode( string destdir, string database, string table, string query );
        void CompileGeneratedSources(string destdir, string table);
    }
}
