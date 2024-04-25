﻿CREATE TABLE IF NOT EXISTS "PARAMETER" (
	"ID"	INTEGER,
	"TYPE"	TEXT NOT NULL,
	"NAME"	TEXT NOT NULL,
	"VALUE"	TEXT NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "USER" (
	"ID"	INTEGER,
	"NAME"	TEXT NOT NULL,
	"PASSWORD"	INTEGER NOT NULL,
	"LOCK_TIME"	DATETIME DEFAULT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "USER_LOGIN_LOG" (
	"ID"	INTEGER,
	"NAME"	TEXT NOT NULL,
	"IS_SUCCESSFUL"	INTEGER NOT NULL,
	"LOG_TIME"	DATETIME DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY("ID" AUTOINCREMENT)
);