BEGIN

    USE ChatApp;

    INSERT INTO Users (Id, Email, Password, DisplayName, CreatedAt)
    VALUES (
        NEWID(),
        'test@test.com',
        '$2a$11$s/7YYsxnSuLie81qt6JcZ.ZaL60OAVVWu8wfdtFXA1RUfi4Cp5GQG',
        'Test User',
        GETDATE()
    );
END