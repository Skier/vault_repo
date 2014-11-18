How to install eCatalog

1. Create virtual directory 'weborb'. Full path will look like 'C:\Inetpub\wwwroot\weborb'. Extract files from 'weborb.zip' to this directory.
2. Create virtual directory 'DocumentStorage' for images (full path like 'C:\Inetpub\wwwroot\DocumentStorage'). Create there subdirectory 'PDF' and copy in this subdirectory folders with images.
3. Run 'create.sql' script on target database.
4. Check and correct 'Web.config' file in 'weborb' directory (section 'appSettings', key 'connectionStringTC').
5. Now eCatalog is available ('http://YOUR_SERVER/weborb/eCatalog/ecatalog.asp').
