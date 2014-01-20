--Client Virtuoso DB - Tables

--temporary event table
create table EventFrameworkAtomicEvents
(
  "Status" SMALLINT NOT NULL, --0=not delivered ; 1=delivered
  "Occurence" DATETIME NOT NULL,
  "TriggerName" VARCHAR NOT NULL,
  "InternalSource" VARCHAR, --e.g. Tablename, graph
  "Row" ANY            --return values
);

create table EventFrameworkTriggerConditions
(
  "TriggerName" VARCHAR NOT NULL,
  "TriggerType" VARCHAR NOT NULL,
  "TableName" VARCHAR NOT NULL,
  "ParamArray" ANY,
  "Condition" LONG VARCHAR NOT NULL,
  PRIMARY KEY ("TriggerName")
);

create table EventFrameworkConstants
(
  "Key" VARCHAR,
  "Value" ANY NOT NULL,
  PRIMARY KEY ("Key")
);

commit work;