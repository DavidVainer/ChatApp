IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ChatApp')
BEGIN
    CREATE DATABASE ChatApp;
END