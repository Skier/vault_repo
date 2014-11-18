-- ------------------------------------------------------------------
-- MySQL Migration Toolkit - Migration script
-- ------------------------------------------------------------------

-- ------------------------------------------------------------------
-- Initialize the migration environment

Migration:initMigration()

-- set options
doWriteCreateScript= false
doWriteInsertScript= false
grtV.setGlobal("/migration/applicationData/reverseEngineerOnlyTableObjects", 0)


-- ------------------------------------------------------------------
-- checkpoint 0
-- Set source and target connection

do
  -- Set source connection
  print("Set source connection.")
  
  grtV.setGlobal("/migration/sourceConnection", {
    name= "sourceConn",
    _id= grt.newGuid(),
    driver= "{9437F83E-FF9E-4831-9132-B4502303C229}",
    parameterValues= {
      database= "dalworth_server",
      domain= "",
      host= "127.0.0.1",
      jdbcConnStr= "jdbc:jtds:sqlserver://127.0.0.1;user=sa;password=gfhjkm;namedPipe=true",
      password= "",
      port= "1433",
      username= "sa"
    },
    modules= {
      MigrationModule= "MigrationMssql",
      ReverseEngineeringModule= "ReverseEngineeringMssql",
      TransformationModule= ""
    }
  });
  
  -- set struct and types
  grtS.set(grtV.getGlobal("/migration/sourceConnection"), "db.mgmt.Connection")
  grtV.setContentType(grtV.getGlobal("/migration/sourceConnection/parameterValues"), "string")
  grtV.setContentType(grtV.getGlobal("/migration/sourceConnection/modules"), "string")
  
  sourceConn= grtV.getGlobal("/migration/sourceConnection")
  sourceRdbmsName= grtV.toLua(sourceConn.driver.owner.name)
  
  -- Set target connection
  print("Set target connection.")
  
  grtV.setGlobal("/migration/targetConnection", {
    name= "targetConn",
    _id= grt.newGuid(),
    driver= "{8E33CDBA-2B8D-4221-96C4-506D398BC377}",
    parameterValues= {
      host= "localhost",
      jdbcConnStr= "",
      password= "++Winston",
      port= "3306",
      username= "root"
    },
    modules= {
      MigrationModule= "MigrationMysql",
      ReverseEngineeringModule= "ReverseEngineeringMysqlJdbc",
      TransformationModule= "TransformationMysqlJdbc"
    }
  });
  
  -- set struct and types
  grtS.set(grtV.getGlobal("/migration/targetConnection"), "db.mgmt.Connection")
  grtV.setContentType(grtV.getGlobal("/migration/targetConnection/parameterValues"), "string")
  grtV.setContentType(grtV.getGlobal("/migration/targetConnection/modules"), "string")
  
  targetConn= grtV.getGlobal("/migration/targetConnection")
  targetRdbmsName= grtV.toLua(targetConn.driver.owner.name)
  
  
  -- Test connections
  print("Test source connection to " .. sourceRdbmsName .. " ...")
  
  res= grtM.callFunction(grtV.toLua(sourceConn.modules.ReverseEngineeringModule), "getVersion", sourceConn)
  grt.exitOnError("The connection to the source " .. sourceRdbmsName .. " database could not be established.")
  
  
  print("Test target connection to " .. targetRdbmsName .. " ...")
  
  res= grtM.callFunction(grtV.toLua(targetConn.modules.ReverseEngineeringModule), "getVersion", targetConn)
  grt.exitOnError("The connection to the target " .. targetRdbmsName .. " database could not be established.")
  
  -- store target version for the migration process
  grtV.setGlobal("/migration/targetVersion", res)
end

-- ------------------------------------------------------------------
-- checkpoint 1
-- Do the reverse engineering

do
  print("Reverse engineering " .. sourceRdbmsName .. " ...")
  
  res= grtM.callFunction(grtV.toLua(sourceConn.modules.ReverseEngineeringModule), "reverseEngineer", 
    {sourceConn, {"dalworth_server.dbo"}}
  )
  grt.exitOnError("The source " .. sourceRdbmsName .. " database could not be reverse engineered")
  
  grtV.setGlobal("/migration/sourceCatalog", res)
end

-- ------------------------------------------------------------------
-- checkpoint 2
-- Migration methods and ignore list

do
  print("Get available migration methods.")
  
  res= grtM.callFunction(grtV.toLua(sourceConn.modules.MigrationModule), "migrationMethods", nil)
  grt.exitOnError("The migration methods cannot be fetched.")
  
  grtV.setGlobal("/migration/migrationMethods", res)


  -- generate an ignore list
  print("Setting up ignore list.")
  
  grtV.setGlobal("/migration/ignoreList", {"db.mssql.Routine:*", 
  "db.mssql.Routine:*", 
  "db.mssql.View:*", 
  "db.mssql.View:*"})
end

-- ------------------------------------------------------------------
-- checkpoint 3
-- Set object mappings and to migration

do
  print("Set object mappings.")
  
  grtV.setGlobal("/migration/mappingDefaults", {
    {
        _id = "{B6129178-9B75-4115-908B-AA18B359B268}", 
        method = "2d3e03ac2a4dbcbe:-78c6658d:119a87d9add:-790c-78c6658d:119a87d9add:-790b", 
        methodName = "migrateRoutineToMysql", 
        moduleName = "MigrationMssql", 
        paramGroupName = "Standard parameters", 
        params = {
          Skip = "no"
        }, 
        sourceStructName = "db.mssql.Routine"
      },
    {
        _id = "{AAA5CD60-9203-492F-8878-E61CA17D614D}", 
        method = "2d3e03ac2a4dbcbe:-78c6658d:119a87d9add:-7910-78c6658d:119a87d9add:-790f", 
        methodName = "migrateViewToMysql", 
        moduleName = "MigrationMssql", 
        paramGroupName = "Standard parameters", 
        params = {
          forceCheckOption = "no"
        }, 
        sourceStructName = "db.mssql.View"
      },
    {
        _id = "{A7337522-3911-4AA0-89F1-6FD38AB45E4C}", 
        method = "2d3e03ac2a4dbcbe:-78c6658d:119a87d9add:-791c-78c6658d:119a87d9add:-791b", 
        methodName = "migrateTableToMysql", 
        moduleName = "MigrationMssql", 
        paramGroupName = "Data consistency", 
        params = {
          addAutoincrement = "yes", 
          engine = "INNODB"
        }, 
        sourceStructName = "db.mssql.Table"
      },
    {
        _id = "{489D056C-AE91-40DB-9B8E-D4B6BD4F2FBC}", 
        method = "2d3e03ac2a4dbcbe:-78c6658d:119a87d9add:-7922-78c6658d:119a87d9add:-7921", 
        methodName = "migrateSchemaToMysql", 
        moduleName = "MigrationMssql", 
        paramGroupName = "Latin1", 
        params = {
          charset = "latin1", 
          collation = "latin1_swedish_ci"
        }, 
        sourceStructName = "db.mssql.Schema"
      }  })
  
  grtV.setContentType(grtV.getGlobal("/migration/mappingDefaults"), "dict", "db.migration.Mapping")
  
  grtS.set(grtV.getGlobal("/migration/mappingDefaults/0"), "db.migration.Mapping")
  grtV.setContentType(grtV.getGlobal("/migration/mappingDefaults/0/params"), "string")
  grtS.set(grtV.getGlobal("/migration/mappingDefaults/1"), "db.migration.Mapping")
  grtV.setContentType(grtV.getGlobal("/migration/mappingDefaults/1/params"), "string")
  grtS.set(grtV.getGlobal("/migration/mappingDefaults/2"), "db.migration.Mapping")
  grtV.setContentType(grtV.getGlobal("/migration/mappingDefaults/2/params"), "string")
  grtS.set(grtV.getGlobal("/migration/mappingDefaults/3"), "db.migration.Mapping")
  grtV.setContentType(grtV.getGlobal("/migration/mappingDefaults/3/params"), "string")

  
  -- update _ids
  local mappingDefaults= grtV.getGlobal("/migration/mappingDefaults")
  local migrationMethods= grtV.getGlobal("/migration/migrationMethods")
  for i= 1, grtV.getn(mappingDefaults) do
    for j= 1, grtV.getn(migrationMethods) do
      if grtV.toLua(mappingDefaults[i].moduleName) == grtV.toLua(migrationMethods[j].moduleName) and
        grtV.toLua(mappingDefaults[i].moduleName) == grtV.toLua(migrationMethods[j].moduleName) then
          mappingDefaults[i].method= migrationMethods[j]
      end
    end
  end
  
  print("Do the migration.")
  
  grtM.callFunction(
    grtV.toLua(sourceConn.modules.MigrationModule), "migrate", 
    {"global::/migration", "global::/rdbmsMgmt/rdbms/" .. grtV.toLua(targetConn.driver.owner.name),
      "global::/migration/targetVersion"
    }
  )
  grt.exitOnError("The source objects cannot be migrated.")
end

-- ------------------------------------------------------------------
-- checkpoint 4
-- Generate and execute sql create statements

do
  print("Generate sql create statements.")

  grtM.callFunction(
    grtV.toLua(targetConn.modules.TransformationModule), "generateSqlCreateStatements", 
    {"global::/migration/targetCatalog",
      grtV.getGlobal("/migration/objectCreationParams")
    }
  )
  grt.exitOnError("The SQL create statements cannot be created.")

  -- write sql create script to file
  if doWriteCreateScript then
    print("Write create script.")
    
    res= grtM.callFunction(
      grtV.toLua(targetConn.modules.TransformationModule), "getSqlScript", 
      {"global::/migration/targetCatalog"}
    )
    grt.exitOnError("The SQL create script cannot be created.")
    
    local f= io.open("creates.sql", "w+")
    if f ~= nil then
      f:write(grtV.toLua(res))
      
      f:flush()
      f:close()
    end
  end

  -- create database objects online
  print("Create database objects.")
  
  grtM.callFunction(
    grtV.toLua(targetConn.modules.TransformationModule), "executeSqlStatements", 
    {targetConn, "global::/migration/targetCatalog", "global::/migration/creationLog"}
  )
  grt.exitOnError("The SQL create script cannot be executed.")
end


-- ------------------------------------------------------------------
-- checkpoint 5
-- Bulk data transfer

do
  -- set transfer parameters
  grtV.setGlobal("/migration/dataBulkTransferParams", {
      CreateScript= doWriteInsertScript and "yes" or "no", 
      ScriptFileName="inserts.sql"
    }
  )
  
  grtV.setContentType(grtV.getGlobal("/migration/dataBulkTransferParams"), "string")
  
  print("Execute bulk data transfer")
  
  grtM.callFunction(
    grtV.toLua(sourceConn.modules.MigrationModule), "dataBulkTransfer", 
    {sourceConn, "global::/migration/sourceCatalog", 
      targetConn, "global::/migration/targetCatalog", 
      "global::/migration/dataBulkTransferParams",
      "global::/migration/dataTransferLog"
    }
  )
  grt.exitOnError("The bulk data transfer returned an error.")
  
  print("Migration completed.")
end

-- ------------------------------------------------------------------
