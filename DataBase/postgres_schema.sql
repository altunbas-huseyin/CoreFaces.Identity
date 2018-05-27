-- Table: public."Jwt"

-- DROP TABLE public."Jwt";

CREATE TABLE public."Jwt"
(
    "UserId" uuid,
    "Token" uuid,
    "DeadLine" date,
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT jwt_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Jwt"
    OWNER to postgres;



    -- Table: public."Permission"

-- DROP TABLE public."Permission";

CREATE TABLE public."Permission"
(
    "UserId" uuid,
    "Name" character varying COLLATE pg_catalog."default",
    "Description" character varying COLLATE pg_catalog."default",
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT permission_pkey PRIMARY KEY ("Id"),
    CONSTRAINT "Permission_IX_001" UNIQUE ("UserId", "Name")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Permission"
    OWNER to postgres;



    -- Table: public."Role"

-- DROP TABLE public."Role";

CREATE TABLE public."Role"
(
    "UserId" uuid NOT NULL,
    "Name" character varying COLLATE pg_catalog."default",
    "Description" character varying COLLATE pg_catalog."default",
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT role_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Role"
    OWNER to postgres;




    -- Table: public."RolePermission"

-- DROP TABLE public."RolePermission";

CREATE TABLE public."RolePermission"
(
    "UserId" uuid,
    "PermissionId" uuid,
    "RoleId" uuid,
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT rolepermission_pkey PRIMARY KEY ("Id"),
    CONSTRAINT fk_rolepermission_permission FOREIGN KEY ("PermissionId")
        REFERENCES public."Permission" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."RolePermission"
    OWNER to postgres;




    -- Table: public."Status"

-- DROP TABLE public."Status";

CREATE TABLE public."Status"
(
    "Name" character varying COLLATE pg_catalog."default",
    "Description" character varying COLLATE pg_catalog."default",
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT status_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Status"
    OWNER to postgres;






    -- Table: public."Test"

-- DROP TABLE public."Test";

CREATE TABLE public."Test"
(
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "FirstName" character varying COLLATE pg_catalog."default" NOT NULL,
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT "Test_Class_ID" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Test"
    OWNER to postgres;





    -- Table: public."User"

-- DROP TABLE public."User";

CREATE TABLE public."User"
(
    "ParentId" uuid NOT NULL,
    "Email" character varying COLLATE pg_catalog."default",
    "Password" character varying COLLATE pg_catalog."default",
    "Name" character varying COLLATE pg_catalog."default",
    "SurName" character varying COLLATE pg_catalog."default",
    "Extra1" text COLLATE pg_catalog."default",
    "Extra2" text COLLATE pg_catalog."default",
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT user_pkey PRIMARY KEY ("Id"),
    CONSTRAINT "IX_001" UNIQUE ("ParentId", "Email")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."User"
    OWNER to postgres;








    -- Table: public."UserRole"

-- DROP TABLE public."UserRole";

CREATE TABLE public."UserRole"
(
    "UserId" uuid,
    "RoleId" uuid,
    "Id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "StatusId" uuid,
    "Name" text COLLATE pg_catalog."default",
    "OwnerId" uuid,
    "CreateDate" timestamp without time zone,
    "UpdateDate" timestamp without time zone,
    CONSTRAINT userrole_pkey PRIMARY KEY ("Id"),
    CONSTRAINT fk_userrole_user FOREIGN KEY ("UserId")
        REFERENCES public."User" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."UserRole"
    OWNER to postgres;

-- Index: fki_test

-- DROP INDEX public.fki_test;

CREATE INDEX fki_test
    ON public."UserRole" USING btree
    (UserId)
    TABLESPACE pg_default;