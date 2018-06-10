SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS [CenterType] (
  [Id] INTEGER  NOT NULL
, [Value] text NOT NULL
, CONSTRAINT [sqlite_master_PK_CenterType] PRIMARY KEY ([Id])
);
CREATE TABLE IF NOT EXISTS [Center] (
  [Id] INTEGER  NOT NULL
, [Name] text NOT NULL
, [StreetAddress] text NULL
, [CenterTypeId] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_Center] PRIMARY KEY ([Id])
, FOREIGN KEY ([CenterTypeId]) REFERENCES [CenterType] ([Id]) ON DELETE RESTRICT ON UPDATE CASCADE
);
CREATE TABLE IF NOT EXISTS [Appointment] (
  [Id] INTEGER  NOT NULL
, [ClientFullName] text NOT NULL
, [Date] date NOT NULL
, [Center] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_Appointment] PRIMARY KEY ([Id])
, FOREIGN KEY ([Center]) REFERENCES [Center] ([Id]) ON DELETE RESTRICT ON UPDATE CASCADE
);
COMMIT;