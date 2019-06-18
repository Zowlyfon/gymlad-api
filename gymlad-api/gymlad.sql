[40m[32minfo[39m[22m[49m: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.2.4-servicing-10062 initialized 'GymLadContext' using provider 'Npgsql.EntityFrameworkCore.PostgreSQL' with options: using lazy-loading proxies 
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "People" (
    "Id" bigint NOT NULL,
    "UserName" text NULL,
    "NormalizedUserName" text NULL,
    "Email" text NULL,
    "NormalizedEmail" text NULL,
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text NULL,
    "SecurityStamp" text NULL,
    "ConcurrencyStamp" text NULL,
    "PhoneNumber" text NULL,
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone NULL,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    "FirstName" text NULL,
    "LastName" text NULL,
    "Height" real NOT NULL,
    "Weight" real NOT NULL,
    "DoB" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_People" PRIMARY KEY ("Id")
);

CREATE TABLE "Exercises" (
    "Id" bigint NOT NULL,
    "PersonId" bigint NOT NULL,
    "Name" text NULL,
    "TrainingMax" real NOT NULL,
    CONSTRAINT "PK_Exercises" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Exercises_People_PersonId" FOREIGN KEY ("PersonId") REFERENCES "People" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Workouts" (
    "Id" bigint NOT NULL,
    "PersonId" bigint NOT NULL,
    "Time" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Workouts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Workouts_People_PersonId" FOREIGN KEY ("PersonId") REFERENCES "People" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Sets" (
    "Id" bigint NOT NULL,
    "ExerciseId" bigint NOT NULL,
    "WorkoutId" bigint NOT NULL,
    "Reps" integer NOT NULL,
    "Weight" real NOT NULL,
    CONSTRAINT "PK_Sets" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Sets_Exercises_ExerciseId" FOREIGN KEY ("ExerciseId") REFERENCES "Exercises" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Sets_Workouts_WorkoutId" FOREIGN KEY ("WorkoutId") REFERENCES "Workouts" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Exercises_PersonId" ON "Exercises" ("PersonId");

CREATE INDEX "IX_Sets_ExerciseId" ON "Sets" ("ExerciseId");

CREATE INDEX "IX_Sets_WorkoutId" ON "Sets" ("WorkoutId");

CREATE INDEX "IX_Workouts_PersonId" ON "Workouts" ("PersonId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190615175151_Initial', '2.2.4-servicing-10062');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190616153229_ExercisePersonFK', '2.2.4-servicing-10062');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190616153531_ExercisePersonFK2', '2.2.4-servicing-10062');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190616161113_ExercisePersonFK3', '2.2.4-servicing-10062');

CREATE TABLE "WorkoutTemplates" (
    "Id" bigint NOT NULL,
    "PersonId" bigint NOT NULL,
    "TemplateName" text NULL,
    CONSTRAINT "PK_WorkoutTemplates" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_WorkoutTemplates_People_PersonId" FOREIGN KEY ("PersonId") REFERENCES "People" ("Id") ON DELETE CASCADE
);

CREATE TABLE "SetTemplates" (
    "Id" bigint NOT NULL,
    "ExerciseId" bigint NOT NULL,
    "WorkoutTemplateId" bigint NOT NULL,
    "Reps" integer NOT NULL,
    "Percentage" real NOT NULL,
    CONSTRAINT "PK_SetTemplates" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_SetTemplates_Exercises_ExerciseId" FOREIGN KEY ("ExerciseId") REFERENCES "Exercises" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_SetTemplates_WorkoutTemplates_WorkoutTemplateId" FOREIGN KEY ("WorkoutTemplateId") REFERENCES "WorkoutTemplates" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_SetTemplates_ExerciseId" ON "SetTemplates" ("ExerciseId");

CREATE INDEX "IX_SetTemplates_WorkoutTemplateId" ON "SetTemplates" ("WorkoutTemplateId");

CREATE INDEX "IX_WorkoutTemplates_PersonId" ON "WorkoutTemplates" ("PersonId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190617122824_Templates', '2.2.4-servicing-10062');


