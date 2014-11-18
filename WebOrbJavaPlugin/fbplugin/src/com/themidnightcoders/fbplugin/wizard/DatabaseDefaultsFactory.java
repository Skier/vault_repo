package com.themidnightcoders.fbplugin.wizard;

import com.themidnightcoders.DatabaseInfoType;

public class DatabaseDefaultsFactory
{

    public static DatabaseDefaults getDefaults(DatabaseInfoType type) {
        switch ( type.getDiscriminator() ) {
            case DatabaseInfoType.DISC_MSSQL:
                return new DatabaseDefaults("localhost", "", "sa", "");

            case DatabaseInfoType.DISC_MYSQL:
                return new DatabaseDefaults("localhost", "3306", "root", "");

            case DatabaseInfoType.DISC_ORACLE:
                return new DatabaseDefaults("TNSNAME", "", "system", "");

            case DatabaseInfoType.DISC_POSTGRESQL:
                return new DatabaseDefaults("localhost", "5432", "postgres", "");

            default:
                throw new RuntimeException("Database type does not supported " + type);
        }
    }

}