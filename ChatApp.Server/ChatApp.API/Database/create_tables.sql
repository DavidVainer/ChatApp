BEGIN
    USE ChatApp;

	CREATE TABLE Users (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL,
        Password NVARCHAR(255) NOT NULL,
        DisplayName NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME NOT NULL
    );

    CREATE TABLE Rooms (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME NOT NULL
    );

    CREATE TABLE Messages (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        RoomId UNIQUEIDENTIFIER NOT NULL,
        SenderId UNIQUEIDENTIFIER NOT NULL,
        Content NVARCHAR(MAX) NOT NULL,
        SentAt DATETIME NOT NULL

        CONSTRAINT FK_Messages_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id) ON DELETE CASCADE,
        CONSTRAINT FK_Messages_Users FOREIGN KEY (SenderId) REFERENCES Users(Id) ON DELETE CASCADE
    );

    CREATE TABLE RoomParticipants (
        RoomId UNIQUEIDENTIFIER NOT NULL,
        UserId UNIQUEIDENTIFIER NOT NULL,
        JoinedAt DATETIME NOT NULL,

        CONSTRAINT FK_RoomParticipants_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id) ON DELETE CASCADE,
        CONSTRAINT FK_RoomParticipants_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
    );

	CREATE TABLE MessageStatuses (
		MessageId UNIQUEIDENTIFIER NOT NULL,
		RoomId UNIQUEIDENTIFIER NOT NULL,
		UserId UNIQUEIDENTIFIER NOT NULL,
		SeenAt DATETIME NOT NULL,

		CONSTRAINT FK_MessageStatuses_Messages FOREIGN KEY (MessageId) REFERENCES Messages(Id) ON DELETE CASCADE,
		CONSTRAINT FK_MessageStatuses_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION,
		CONSTRAINT FK_MessageStatuses_Rooms FOREIGN KEY (RoomId) REFERENCES Rooms(Id) ON DELETE NO ACTION
	);
END