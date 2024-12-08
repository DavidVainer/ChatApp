BEGIN

    USE ChatApp;

    INSERT INTO Users (Id, Email, Password, DisplayName, CreatedAt, Deleted)
    VALUES (
        NEWID(),
        'test@test.com',
        '$2a$11$s/7YYsxnSuLie81qt6JcZ.ZaL60OAVVWu8wfdtFXA1RUfi4Cp5GQG',
        'TestUser',
        GETDATE(),
        0
    );

    INSERT INTO Users (Id, Email, Password, DisplayName, CreatedAt, Deleted)
    VALUES (
        NEWID(),
        'test2@test.com',
        '$2a$11$JrD/IJXB6MrUxACyzUYr6O8ZbBzBorTCuU.1Ulx4jYT7iHGCx9puq',
        'TestUser2',
        GETDATE(),
        0
    );

    INSERT INTO Rooms (Id, Name, CreatedAt, Deleted)
    VALUES (
        NEWID(),
        'Test Room',
        GETDATE(),
        0
    );
END