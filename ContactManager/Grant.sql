IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\ContactManagement')
BEGIN
    CREATE LOGIN [IIS APPPOOL\DefaultAppPool] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [ContactInfoDB] 
  FOR LOGIN [IIS APPPOOL\ContactManagement]
GO
EXEC sp_addrolemember 'db_owner', 'ContactInfoDB'
GO