BEGIN
    USE ChatApp;

	CREATE TABLE Users (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL,
        Password NVARCHAR(255) NOT NULL,
        DisplayName NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME NOT NULL,
        Deleted BIT NOT NULL
    );

    CREATE TABLE Rooms (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME NOT NULL,
        Deleted BIT NOT NULL
    );

    CREATE TABLE Messages (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        RoomId UNIQUEIDENTIFIER NOT NULL,
        SenderId UNIQUEIDENTIFIER NOT NULL,
        Content NVARCHAR(MAX) NOT NULL,
        SentAt DATETIME NOT NULL,
        Deleted BIT NOT NULL

        CONSTRAINT FK_Messages_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id),
        CONSTRAINT FK_Messages_Users FOREIGN KEY (SenderId) REFERENCES Users(Id)
    );

    CREATE TABLE RoomParticipants (
        RoomId UNIQUEIDENTIFIER NOT NULL,
        UserId UNIQUEIDENTIFIER NOT NULL,
        JoinedAt DATETIME NOT NULL,

        CONSTRAINT FK_RoomParticipants_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id),
        CONSTRAINT FK_RoomParticipants_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
    );

	CREATE TABLE MessageStatuses (
		MessageId UNIQUEIDENTIFIER NOT NULL,
		RoomId UNIQUEIDENTIFIER NOT NULL,
		UserId UNIQUEIDENTIFIER NOT NULL,
		SeenAt DATETIME NOT NULL,

		CONSTRAINT FK_MessageStatuses_Messages FOREIGN KEY (MessageId) REFERENCES Messages(Id),
		CONSTRAINT FK_MessageStatuses_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
		CONSTRAINT FK_MessageStatuses_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
	);
END