SELECT 'Please note: the following error is to be expected and has no effect on the framework: \n 22023	 The RDF QM schema object <http://EventFramework/Schema/qm-eventframeworkactions> does not exist, should be of type <http://www.openlinksw.com/schemas/virtrdf#QuadMap>';
--tables
create table EventFrameworkDataSources
(
	  "DSInstance" INTEGER IDENTITY,
	  "Active" SMALLINT NOT NULL DEFAULT 1,
	  "DSName" VARCHAR NOT NULL,
	  "DSType" VARCHAR NOT NULL,
	  "ControlID" INTEGER NOT NULL,
	  "Description" VARCHAR,
	  "ProcedureEndpoint" VARCHAR,
	  "SparqlEndpointAddress" VARCHAR,
	  PRIMARY KEY ("DSInstance")
);

create table EventFrameworkUsers
(
	  "UserID" INTEGER IDENTITY,
	  "Name" VARCHAR NOT NULL,
	  "Pass" VARCHAR NOT NULL,
	  "Created" DATETIME NOT NULL,
	  "LastLogIn" DATETIME,
	  "SessionNr" INTEGER,
	  "Datasources" VARCHAR,
	  "UserAccRight" SMALLINT NOT NULL DEFAULT 0,
	  "ECADefRight" SMALLINT NOT NULL DEFAULT 0,
	  PRIMARY KEY ("UserID")
);

create table EventFrameworkTriggers
(
	  "TriggerID" INTEGER IDENTITY,
	  "TriggerType" VARCHAR(6) NOT NULL,	--INSERT; UPDATE; DELETE; OTHER
	  "TriggerName" VARCHAR NOT NULL UNIQUE,
	  "AlternativeName" VARCHAR,
	  "DSInstance" INTEGER,     --EventFrameworkDataSources
	  "InternalSource" VARCHAR, --e.g.DB-Table
	  "Values" VARCHAR,   --Values which will automatically be passed by this trigger
	  "Created" DATETIME NOT NULL,
	  "CreatedBy" INTEGER NOT NULL, --username
	  "Description" VARCHAR,
	  "Statement" LONG VARCHAR,
	  PRIMARY KEY ("TriggerID")
	  --FOREIGN KEY ("DSInstance") REFERENCES EventFrameworkDataSources,
	  --FOREIGN KEY ("CreatedBy") REFERENCES EventFrameworkUsers
);

create table EventFrameworkConstants
(
	  "Key" VARCHAR,
	  "Value" ANY NOT NULL,
	  PRIMARY KEY ("Key")
);

create table EventFrameworkActions
(
	  "ActionID" INTEGER IDENTITY,
	  "Condition" SMALLINT NOT NULL,
	  "CreatedBy" INTEGER NOT NULL,
	  "WsdlAddress" VARCHAR NOT NULL,
	  "EndpointAddress" VARCHAR NOT NULL,
	  "MethodeName" VARCHAR NOT NULL,   -- aka. soap action
	  "Description" VARCHAR,
	  "InternalQuery" VARCHAR,
	  "ParamTypes" VARCHAR,
	  "ParamDescr" VARCHAR,
	  "ReturnType" VARCHAR,
	  "ReturnDescr" VARCHAR,
	  "SparqlQuery" LONG VARCHAR,
	  "UserName" VARCHAR,
	  "Password" VARCHAR,
	  "X509Certificate" LONG VARCHAR,
	  "X509Password" VARCHAR,
	  "DSInstance" INTEGER DEFAULT -1,
	  PRIMARY KEY ("ActionID")
	  --FOREIGN KEY ("CreatedBy") REFERENCES EventFrameworkUsers
);

create Table EventFrameworkComplexEvents
(
	"CEID" INTEGER IDENTITY,
	"Name" VARCHAR NOT NULL UNIQUE,
	"InitialStage" VARCHAR NOT NULL,
	"CreatedBy" INTEGER NOT NULL,
	"Recurrences" INTEGER NOT NULL,
	"Period" VARCHAR,
	"InitializeAt" DATETIME,
	"Description" VARCHAR,
	"IsActive" SMALLINT NOT NULL DEFAULT 0,
	"IsOverlapping" SMALLINT NOT NULL DEFAULT 0,
	PRIMARY KEY ("CEID")
	--FOREIGN KEY ("CreatedBy") REFERENCES EventFrameworkUsers
);

create table EventFrameworkComplexEventInstances
(
	  "EventID" INTEGER IDENTITY,
	  "CEID" INTEGER NOT NULL,
	  "EventUri" VARCHAR NOT NULL,
	  "FirstStageUri" VARCHAR NOT NULL,
	  "CurrentStage" INTEGER NOT NULL,
	  "Started" DATETIME NOT NULL,
	  "Finished" DATETIME,
	  PRIMARY KEY ("EventID"),
	  FOREIGN KEY ("Ceid") REFERENCES EventFrameworkComplexEvents ON DELETE CASCADE
);

create table EventFrameworkEvents
(
	  "EventID" INTEGER IDENTITY,
	  "TriggerID" INTEGER,
	  "CEID" INTEGER,
	  "CeInstance" INTEGER,
	  "Occurence" DATETIME NOT NULL,
	  "DSInstance" INTEGER,
	  "InternalSource" VARCHAR, --e.g. Tablename, graph
	  "Row" ANY,
	  PRIMARY KEY ("EventID"),
	  CHECK ( TriggerID IS NOT NULL OR CEID IS NOT NULL)
	  --FOREIGN KEY ("TriggerID") REFERENCES EventFrameworkTriggers,
	  --FOREIGN KEY ("Ceid") REFERENCES EventFrameworkComplexEvents,
	  --FOREIGN KEY ("DSInstance") REFERENCES EventFrameworkDataSources
);

create table EventFrameworkAwaitingEvent
(
	"CEID" INTEGER,		--complex e
	"TriggerID" INTEGER,	--atomic  e
	"SourceCE" INTEGER NOT NULL,  	--ce awaiting event
	"Recurrences" INTEGER DEFAULT 0,
	"Started" DATETIME NOT NULL,
	"Until" DATETIME,
	CHECK ( TriggerID IS NOT NULL OR CEID IS NOT NULL),
	FOREIGN KEY ("CEID") REFERENCES EventFrameworkComplexEvents ON DELETE CASCADE,
	FOREIGN KEY ("SourceCE") REFERENCES EventFrameworkComplexEventInstances ON DELETE CASCADE,
	FOREIGN KEY ("TriggerID") REFERENCES EventFrameworkTriggers ON DELETE CASCADE
);

create table EventFrameworkParameterMappings
(
	"CEID" INTEGER NOT NULL,	
	"StageNr" INTEGER NOT NULL,
	"ActionID" INTEGER NOT NULL,
	"ActionNr" INTEGER NOT NULL,
	"ParamNr" INTEGER NOT NULL,
	"Description" VARCHAR,  	
	"StaticValue" VARCHAR,
	"StaticValueType" VARCHAR,
	"ConditionQuery" INTEGER,
	"EventValueMap" VARCHAR,
	FOREIGN KEY ("ActionID") REFERENCES EventFrameworkActions ON DELETE CASCADE,
	FOREIGN KEY ("CEID") REFERENCES EventFrameworkComplexEvents ON DELETE CASCADE
);

create table EventFrameworkEventToInstanceMapping
(
	"EventID" INTEGER NOT NULL,
	"InstanceID" INTEGER NOT NULL,
	"StageNr" INTEGER NOT NULL,
	"CEID" INTEGER NOT NULL,
	"Occurence" DATETIME NOT NULL,
	FOREIGN KEY ("EventID") REFERENCES EventFrameworkEvents ON DELETE CASCADE,
	FOREIGN KEY ("InstanceID") REFERENCES EventFrameworkComplexEventInstances ON DELETE CASCADE,
	FOREIGN KEY ("CEID") REFERENCES EventFrameworkComplexEvents ON DELETE CASCADE
);

--client table! logs all event-deliver-attempts
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

commit work;

--Linked Data View definition

SPARQL
prefix EF: <http://EventFramework/Schema/> 

drop quad map EF:qm-eventframeworkactions.
drop quad map EF:qm-eventframeworkaconditions .
drop quad map EF:qm-eventframeworkdatasources .
drop quad map EF:qm-EventFrameworkEvents .
drop quad map EF:qm-eventframeworkcomplexevents .
drop quad map EF:qm-eventframeworktriggers .
drop quad map EF:qm-eventframeworkusers .

drop iri class EF:Action.
drop iri class EF:ConditionQuery.
drop iri class EF:DataSource .
drop iri class EF:Event.
drop iri class EF:AtomicEvent .
drop iri class EF:User .
drop iri class EF:ComplexEvent .
;

SPARQL CLEAR GRAPH  <http://EventFramework/Schema>; 
SPARQL CLEAR GRAPH  <http://EventFramework/LinkedData>; 
SPARQL CLEAR GRAPH  <http://EventFramework/Stages>; 
commit work;

SPARQL Create GRAPH  <http://EventFramework/Schema>; 
SPARQL Create GRAPH  <http://EventFramework/LinkedData>; 
SPARQL Create GRAPH  <http://EventFramework/Stages>; 

grant select on "DB"."DBA"."EventFrameworkActions" to SPARQL_SELECT;
grant select on "DB"."DBA"."EventFrameworkEvents" to SPARQL_SELECT;
grant select on "DB"."DBA"."EventFrameworkComplexEvents" to SPARQL_SELECT;
grant select on "DB"."DBA"."EventFrameworkDataSources" to SPARQL_SELECT;
grant select on "DB"."DBA"."EventFrameworkTriggers" to SPARQL_SELECT;
grant select on "DB"."DBA"."EventFrameworkUsers" to SPARQL_SELECT;
grant select on "DB"."DBA"."EventFrameworkComplexEventInstances" to SPARQL_SELECT;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:Action "http://EventFramework/LinkedData/Action%d" (in _ActionID integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:ConditionQuery "http://EventFramework/LinkedData/ConditionQuery%d" (in _ActionID integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:DataSource "http://EventFramework/LinkedData/DataSource%d" (in _DSInstance integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:Event "http://EventFramework/LinkedData/Event%d" (in _EventID integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:AtomicEvent "http://EventFramework/LinkedData/AtomicEvent%d" (in _TriggerID integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:User "http://EventFramework/LinkedData/User%d" (in _UserID integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:ComplexEvent "http://EventFramework/LinkedData/ComplexEvent%d" (in _CEID integer not null) . ;
SPARQL
prefix EF: <http://EventFramework/Schema/> 
create iri class EF:ComplexEventInstance "http://EventFramework/LinkedData/ComplexEventInstance%d" (in _EventID integer not null) . ;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix ef-stat: <http://EventFramework/Stages/stat#> 
prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
prefix void: <http://rdfs.org/ns/void#> 
prefix scovo: <http://purl.org/NET/scovo#> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkActions" as eventframeworkactions_a
 from "DB"."DBA"."EventFrameworkUsers" as eventframeworkusers_a
 where (^{eventframeworkactions_a.}^."CreatedBy" = ^{eventframeworkusers_a.}^."UserID") 
 where (^{eventframeworkactions_a.}^."Condition" = 0)
 { 
   create EF:qm-eventframeworkactions as graph iri ("http://EventFramework/LinkedData")  
    { 
      # Maps from columns of "DB.DBA.EventFrameworkActions"
      EF:Action (eventframeworkactions_a."ActionID")  a EF:Action ;
      EF:actionid eventframeworkactions_a."ActionID" as EF:dba-eventframeworkactions-actionid ;
      #EF:createdby eventframeworkactions_a."CreatedBy" as EF:dba-eventframeworkactions-createdby ;
      EF:wsdladdress eventframeworkactions_a."WsdlAddress" as EF:dba-eventframeworkactions-wsdladdress ;
      EF:endpointaddress eventframeworkactions_a."EndpointAddress" as EF:dba-eventframeworkactions-endpointaddress ;
      EF:methodename eventframeworkactions_a."MethodeName" as EF:dba-eventframeworkactions-methodename ;
      EF:description eventframeworkactions_a."Description" as EF:dba-eventframeworkactions-description ;
      EF:paramtypes eventframeworkactions_a."ParamTypes" as EF:dba-eventframeworkactions-paramtypes ;
      EF:paramdescr eventframeworkactions_a."ParamDescr" as EF:dba-eventframeworkactions-paramdescr ;
      EF:returntype eventframeworkactions_a."ReturnType" as EF:dba-eventframeworkactions-returntype ;
      EF:returndescr eventframeworkactions_a."ReturnDescr" as EF:dba-eventframeworkactions-returndescr ;
      # Maps from foreign-key relations of "DB.DBA.EventFrameworkActions"
      EF:createdby EF:User (eventframeworkusers_a."UserID")  as EF:eventframeworkactions_has_eventframeworkusers .
    }
 }
;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix ef-stat: <http://EventFramework/Stages/stat#> 
prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
prefix void: <http://rdfs.org/ns/void#> 
prefix scovo: <http://purl.org/NET/scovo#> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkActions" as eventframeworkactions_c
 from "DB"."DBA"."EventFrameworkUsers" as eventframeworkusers_c
 where (^{eventframeworkactions_c.}^."CreatedBy" = ^{eventframeworkusers_c.}^."UserID") 
 where (^{eventframeworkactions_c.}^."Condition" = 1)
 { 
   create EF:qm-eventframeworkaconditions as graph iri ("http://EventFramework/LinkedData")  
    { 
      # Maps from columns of "DB.DBA.EventFrameworkActions"
      EF:ConditionQuery (eventframeworkactions_c."ActionID")  a EF:ConditionQuery ;
      EF:actionid eventframeworkactions_c."ActionID" as EF:dba-eventframeworkconditions-actionid ;
      #EF:createdby eventframeworkactions_c."CreatedBy" as EF:dba-eventframeworkconditions-createdby ;
      EF:wsdladdress eventframeworkactions_c."WsdlAddress" as EF:dba-eventframeworkconditions-wsdladdress ;
      EF:endpointaddress eventframeworkactions_c."EndpointAddress" as EF:dba-eventframeworkconditions-endpointaddress ;
      EF:methodename eventframeworkactions_c."MethodeName" as EF:dba-eventframeworkconditions-methodename ;
      EF:description eventframeworkactions_c."Description" as EF:dba-eventframeworkconditions-description ;
      EF:paramtypes eventframeworkactions_c."ParamTypes" as EF:dba-eventframeworkconditions-paramtypes ;
      EF:paramdescr eventframeworkactions_c."ParamDescr" as EF:dba-eventframeworkconditions-paramdescr ;
      EF:returntype eventframeworkactions_c."ReturnType" as EF:dba-eventframeworkconditions-returntype ;
      EF:returndescr eventframeworkactions_c."ReturnDescr" as EF:dba-eventframeworkconditions-returndescr ;
      # Maps from foreign-key relations of "DB.DBA.EventFrameworkActions"
      EF:createdby EF:User (eventframeworkusers_c."UserID")  as EF:eventframeworkconditions_has_eventframeworkusers .
    }
 }
;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix ef-stat: <http://EventFramework/Stages/stat#> 
prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
prefix void: <http://rdfs.org/ns/void#> 
prefix scovo: <http://purl.org/NET/scovo#> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkDataSources" as eventframeworkdatasources_s
 from "DB"."DBA"."EventFrameworkEvents" as EventFrameworkEvents_s
 where (^{EventFrameworkEvents_s.}^."DSInstance" = ^{eventframeworkdatasources_s.}^."DSInstance") 
 from "DB"."DBA"."EventFrameworkTriggers" as eventframeworktriggers_s
 where (^{eventframeworktriggers_s.}^."DSInstance" = ^{eventframeworkdatasources_s.}^."DSInstance") 
 { 
   create EF:qm-eventframeworkdatasources as graph iri ("http://EventFramework/LinkedData")  
    { 
      # Maps from columns of "DB.DBA.EventFrameworkDataSources"
      EF:DataSource (eventframeworkdatasources_s."DSInstance")  a EF:DataSource ;
      EF:instanceID eventframeworkdatasources_s."DSInstance" as EF:dba-eventframeworkdatasources-dsinstance ;
      EF:dsname eventframeworkdatasources_s."DSName" as EF:dba-eventframeworkdatasources-dsname ;
      EF:dstype eventframeworkdatasources_s."DSType" as EF:dba-eventframeworkdatasources-dstype ;
      EF:description eventframeworkdatasources_s."Description" as EF:dba-eventframeworkdatasources-description ;
      EF:sparqlendpointaddress eventframeworkdatasources_s."SparqlEndpointAddress" as EF:dba-eventframeworkdatasources-sparqlendpointaddress .
      #EF:rdfgraphs eventframeworkdatasources_s."RdfGraphs" as EF:dba-eventframeworkdatasources-rdfgraphs .

    }
 }
;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix ef-stat: <http://EventFramework/Stages/stat#> 
prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
prefix void: <http://rdfs.org/ns/void#> 
prefix scovo: <http://purl.org/NET/scovo#> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkEvents" as EventFrameworkEvents_s
 from "DB"."DBA"."EventFrameworkDataSources" as eventframeworkdatasources_s
 where (^{EventFrameworkEvents_s.}^."DSInstance" = ^{eventframeworkdatasources_s.}^."DSInstance") 
 from "DB"."DBA"."EventFrameworkTriggers" as eventframeworktriggers_s
 where (^{EventFrameworkEvents_s.}^."TriggerID" = ^{eventframeworktriggers_s.}^."TriggerID") 
 { 
   create EF:qm-EventFrameworkEvents as graph iri ("http://EventFramework/LinkedData")  
    { 
      # Maps from columns of "DB.DBA.EventFrameworkEvents"
      EF:Event (EventFrameworkEvents_s."EventID")  a EF:Event ;
      EF:eventid EventFrameworkEvents_s."EventID" as EF:dba-EventFrameworkEvents-eventid ;
      #EF:triggerid EventFrameworkEvents_s."TriggerID" as EF:dba-EventFrameworkEvents-triggerid ;
      EF:occurence EventFrameworkEvents_s."Occurence" as EF:dba-EventFrameworkEvents-occurence ;
      #EF:dsinstance EventFrameworkEvents_s."DSInstance" as EF:dba-EventFrameworkEvents-dsinstance ;
      EF:internalsource EventFrameworkEvents_s."InternalSource" as EF:dba-EventFrameworkEvents-internalsource ;
      # EF:row EventFrameworkEvents_s."Row" as EF:dba-EventFrameworkEvents-row ;
      # Maps from foreign-key relations of "DB.DBA.EventFrameworkEvents"
      EF:dsinstance EF:DataSource (eventframeworkdatasources_s."DSInstance")  as EF:EventFrameworkEvents_has_eventframeworkdatasources ;
      EF:trigger EF:AtomicEvent (eventframeworktriggers_s."TriggerID")  as EF:EventFrameworkEvents_has_eventframeworktriggers .
	  }
 }
;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkComplexEvents" as eventframeworkcomplexevents_s
 from "DB"."DBA"."EventFrameworkUsers" as eventframeworkusers_s
 where (^{eventframeworkcomplexevents_s.}^."CreatedBy" = ^{eventframeworkusers_s.}^."UserID") 
 { 
   create EF:qm-eventframeworkcomplexevents as graph iri ("http://EventFramework/LinkedData")  
    { 
      # Maps from columns of "DB.DBA.EventFrameworkComplexEvents"
      EF:ComplexEvent (eventframeworkcomplexevents_s."CEID")  a EF:ComplexEvent ;
      EF:ceID eventframeworkcomplexevents_s."CEID" as EF:dba-eventframeworkcomplexevents-ceid ;
	  EF:ceName eventframeworkcomplexevents_s."Name" as EF:dba-eventframeworkcomplexevents-name ;
      EF:initialStage eventframeworkcomplexevents_s."InitialStage" as EF:dba-eventframeworkcomplexevents-initialstageid ;
      EF:isActive eventframeworkcomplexevents_s."IsActive" as EF:dba-eventframeworkcomplexevents-isactive ;
      EF:overlappingEvent eventframeworkcomplexevents_s."IsOverlapping" as EF:dba-eventframeworkcomplexevents-isoverlapping ;
      EF:description eventframeworkcomplexevents_s."Description" as EF:dba-eventframeworkcomplexevents-description ;
	  EF:recurrences eventframeworkcomplexevents_s."Recurrences" as EF:dba-eventframeworkcomplexevents-recurrence ;
	  EF:period eventframeworkcomplexevents_s."Period" as EF:dba-eventframeworkcomplexevents-period ;
	  EF:initializeAt eventframeworkcomplexevents_s."InitializeAt" as EF:dba-eventframeworkcomplexevents-initializeAt ;
      # Maps from foreign-key relations of "DB.DBA.EventFrameworkComplexEvents"
      EF:createdby EF:User (eventframeworkusers_s."UserID")  as EF:eventframeworkcomplexevents_has_eventframeworkusers .

    }
 }

;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix ef-stat: <http://EventFramework/Stages/stat#> 
prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
prefix void: <http://rdfs.org/ns/void#> 
prefix scovo: <http://purl.org/NET/scovo#> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkTriggers" as eventframeworktriggers_s
 from "DB"."DBA"."EventFrameworkEvents" as EventFrameworkEvents_s
 where (^{EventFrameworkEvents_s.}^."TriggerID" = ^{eventframeworktriggers_s.}^."TriggerID") 
 from "DB"."DBA"."EventFrameworkDataSources" as eventframeworkdatasources_s
 where (^{eventframeworktriggers_s.}^."DSInstance" = ^{eventframeworkdatasources_s.}^."DSInstance") 
 from "DB"."DBA"."EventFrameworkUsers" as eventframeworkusers_s
 where (^{eventframeworktriggers_s.}^."CreatedBy" = ^{eventframeworkusers_s.}^."UserID") 
 { 
   create EF:qm-eventframeworktriggers as graph iri ("http://EventFramework/LinkedData")  
    { 
      # Maps from columns of "DB.DBA.EventFrameworkTriggers"
      EF:AtomicEvent (eventframeworktriggers_s."TriggerID")  a EF:AtomicEvent ;
      EF:triggerid eventframeworktriggers_s."TriggerID" as EF:dba-eventframeworktriggers-triggerid ;
      EF:triggertype eventframeworktriggers_s."TriggerType" as EF:dba-eventframeworktriggers-triggertype ;
      EF:triggername eventframeworktriggers_s."TriggerName" as EF:dba-eventframeworktriggers-triggername ;
      EF:alternativename eventframeworktriggers_s."AlternativeName" as EF:dba-eventframeworktriggers-alternativename ;
      #EF:dsinstance eventframeworktriggers_s."DSInstance" as EF:dba-eventframeworktriggers-dsinstance ;
      EF:internalsource eventframeworktriggers_s."InternalSource" as EF:dba-eventframeworktriggers-internalsource ;
      EF:values eventframeworktriggers_s."Values" as EF:dba-eventframeworktriggers-values ;
      EF:created eventframeworktriggers_s."Created" as EF:dba-eventframeworktriggers-created ;
      #EF:createdby eventframeworktriggers_s."CreatedBy" as EF:dba-eventframeworktriggers-createdby ;
      EF:description eventframeworktriggers_s."Description" as EF:dba-eventframeworktriggers-description ;
      # Maps from foreign-key relations of "DB.DBA.EventFrameworkTriggers"
      EF:dsinstance EF:DataSource (eventframeworkdatasources_s."DSInstance")  as EF:eventframeworktriggers_has_eventframeworkdatasources ;
      EF:createdby EF:User (eventframeworkusers_s."UserID")  as EF:eventframeworktriggers_has_eventframeworkusers .
      
    }
 }

;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix ef-stat: <http://EventFramework/Stages/stat#> 
prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> 
prefix void: <http://rdfs.org/ns/void#> 
prefix scovo: <http://purl.org/NET/scovo#> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkUsers" as eventframeworkusers_s
 from "DB"."DBA"."EventFrameworkActions" as eventframeworkactions_s
 where (^{eventframeworkactions_s.}^."CreatedBy" = ^{eventframeworkusers_s.}^."UserID") 
 from "DB"."DBA"."EventFrameworkComplexEvents" as eventframeworkcomplexevents_s
 where (^{eventframeworkcomplexevents_s.}^."CreatedBy" = ^{eventframeworkusers_s.}^."UserID") 
 from "DB"."DBA"."EventFrameworkTriggers" as eventframeworktriggers_s
 where (^{eventframeworktriggers_s.}^."CreatedBy" = ^{eventframeworkusers_s.}^."UserID") 
 { 
   create EF:qm-eventframeworkusers as graph iri ("http://EventFramework/LinkedData") option (exclusive) 
    { 
      # Maps from columns of "DB.DBA.EventFrameworkUsers"
      EF:User (eventframeworkusers_s."UserID")  a EF:User ;
      EF:userid eventframeworkusers_s."UserID" as EF:dba-eventframeworkusers-userid ;
      EF:name eventframeworkusers_s."Name" as EF:dba-eventframeworkusers-name ;
      EF:created eventframeworkusers_s."Created" as EF:dba-eventframeworkusers-created ;
      EF:lastlogin eventframeworkusers_s."LastLogIn" as EF:dba-eventframeworkusers-lastlogin .
    }
 }
;

SPARQL
prefix EF: <http://EventFramework/Schema/> 
prefix aowl: <http://bblfish.net/work/atom-owl/2006-06-06/> 
alter quad storage virtrdf:DefaultQuadStorage 
 from "DB"."DBA"."EventFrameworkComplexEventInstances" as eventframeworkcomplexeventinstances_s
 { 
   create EF:qm-eventframeworkcomplexeventinstances as graph iri ("http://EventFramework/LinkedData") option (exclusive) 
    { 
      # Maps from columns of "DB.DBA.EventFrameworkComplexEventInstances"
      EF:ComplexEventInstance (eventframeworkcomplexeventinstances_s."EventID")  a EF:ComplexEventInstance ;
      EF:cEventId eventframeworkcomplexeventinstances_s."EventID" as EF:dba-eventframeworkcomplexeventinstances-eventid ;
      #EF:ceid eventframeworkcomplexeventinstances_s."CEID" as EF:dba-eventframeworkcomplexeventinstances-ceid ;
      EF:eventDef eventframeworkcomplexeventinstances_s."EventUri" as EF:dba-eventframeworkcomplexeventinstances-eventuri ;
      EF:firstStage eventframeworkcomplexeventinstances_s."FirstStageUri" as EF:dba-eventframeworkcomplexeventinstances-firststageuri ;
      EF:started eventframeworkcomplexeventinstances_s."Started" as EF:dba-eventframeworkcomplexeventinstances-started ;
      EF:finished eventframeworkcomplexeventinstances_s."Finished" as EF:dba-eventframeworkcomplexeventinstances-finished .

    }
 }
;

-- Virtual directories for instance data
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_rule2',
1,
'(/[^#]*)',
vector('path'),
1,
'/sparql?query=DESCRIBE+%%3Chttp%%3A//EventFramework%U%%23this%%3E+FROM+%%3Chttp%%3A//EventFramework/LinkedData%%23%%3E&format=%U',
vector('path', '*accept*'),
null,
'(text/rdf.n3)|(application/rdf.xml)|(text/n3)|(application/json)',
2,
null
);
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_rule4',
1,
'/Stages/stat([^#]*)',
vector('path'),
1,
'/sparql?query=DESCRIBE+%%3Chttp%%3A//EventFramework/Stages/stat%%23%%3E+%%3Fo+FROM+%%3Chttp%%3A//EventFramework/LinkedData%%23%%3E+WHERE+{+%%3Chttp%%3A//EventFramework/Stages/stat%%23%%3E+%%3Fp+%%3Fo+}&format=%U',
vector('*accept*'),
null,
'(text/rdf.n3)|(application/rdf.xml)|(text/n3)|(application/json)',
2,
null
);
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_rule6',
1,
'/Stages/objects/([^#]*)',
vector('path'),
1,
'/sparql?query=DESCRIBE+%%3Chttp%%3A//EventFramework/Stages/objects/%U%%3E+FROM+%%3Chttp%%3A//EventFramework/LinkedData%%23%%3E&format=%U',
vector('path', '*accept*'),
null,
'(text/rdf.n3)|(application/rdf.xml)|(text/n3)|(application/json)',
2,
null
);
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_rule1',
1,
'([^#]*)',
vector('path'),
1,
'/about/html/http://EventFramework%s',
vector('path'),
null,
null,
2,
303
);
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_rule7',
1,
'/Stages/stat([^#]*)',
vector('path'),
1,
'/about/html/http://EventFramework/Stages/stat%%01',
vector('path'),
null,
null,
2,
303
);
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_rule5',
1,
'/Stages/objects/(.*)',
vector('path'),
1,
'/services/rdf/object.binary?path=%%2FEF%%2Fobjects%%2F%U&accept=%U',
vector('path', '*accept*'),
null,
null,
2,
null
);
DB.DBA.URLREWRITE_CREATE_RULELIST ( 'ef_rule_list1', 1, vector ( 'ef_rule1', 'ef_rule7', 'ef_rule5', 'ef_rule2', 'ef_rule4', 'ef_rule6'));
DB.DBA.VHOST_REMOVE (lpath=>'/LinkedData');
DB.DBA.VHOST_DEFINE (lpath=>'/LinkedData', ppath=>'/', vsp_user=>'dba', is_dav=>0,
is_brws=>0, opts=>vector ('url_rewrite', 'ef_rule_list1')
);

-- Virtual directories for ontology
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_owl_rule2',
1,
'(/[^#]*)',
vector('path'),
1,
'/sparql?query=DESCRIBE+%%3Chttp%%3A//EventFramework%U%%3E+FROM+%%3Chttp%%3A//EventFramework/Schema%%23%%3E&format=%U',
vector('path', '*accept*'),
null,
'(text/rdf.n3)|(application/rdf.xml)|(text/n3)|(application/json)',
2,
null
);
DB.DBA.URLREWRITE_CREATE_REGEX_RULE (
'ef_owl_rule1',
1,
'([^#]*)',
vector('path'),
1,
'/about/html/http://EventFramework%s',
vector('path'),
null,
null,
2,
303
);
DB.DBA.URLREWRITE_CREATE_RULELIST ( 'ef_owl_rule_list1', 1, vector ( 'ef_owl_rule1', 'ef_owl_rule2'));
DB.DBA.VHOST_REMOVE (lpath=>'/Schema');
DB.DBA.VHOST_DEFINE (lpath=>'/Schema', ppath=>'/', vsp_user=>'dba', is_dav=>0,
is_brws=>0, opts=>vector ('url_rewrite', 'ef_owl_rule_list1')
);

commit work;

-- insert mapping graph

TTLP_MT 
('
@prefix rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#> .
@prefix rdfs: <http://www.w3.org/2000/01/rdf-schema#> .
@prefix owl: <http://www.w3.org/2002/07/owl#> .
@prefix owl2: <http://www.w3.org/2006/12/owl2#> .
@prefix xsd: <http://www.w3.org/2001/XMLSchema#> .
@prefix virtrdf: <http://www.openlinksw.com/schemas/virtrdf#> .
@prefix EF: <http://EventFramework/Schema/> .

EF: a owl:Ontology .


EF:ComplexEvent			a					owl:Class.
EF:ComplexEvent			rdfs:isDefinedBy 	EF: .
EF:ComplexEvent			rdfs:subClassOf 	_:x18.
_:x18					a					owl:Class;
						owl:intersectionOf	(_:x16 _:x17 _:x5 _:x13 _:x31).		
_:x17					a					owl:Restriction;
						owl:onProperty		EF:isActive;
						owl:cardinality		"1"^^xsd:integer.
_:x16					a 					owl:Restriction;
						owl:onProperty		EF:initialEvents;
						owl:cardinality		"1"^^xsd:integer.	
_:x5					a					owl:Restriction;
						owl:onProperty		EF:initializeAt;
						owl:cardinality		"1"^^xsd:integer.
_:x13					a 					owl:Restriction;
						owl:onProperty		EF:period;
						owl:cardinality		"1"^^xsd:integer.
_:x31					a 					owl:Restriction;
						owl:onProperty		EF:recurrences;
						owl:cardinality		"1"^^xsd:integer.						

EF:InitialEventSet		a					owl:Class.
EF:InitialEventSet		rdfs:isDefinedBy 	EF: ;
						rdfs:subClassOf		_:x39.
_:x39					a					owl:Restriction;
						owl:onProperty		EF:setID;
						owl:cardinality		"1"^^xsd:integer.	

EF:EventSet				a					owl:class.
EF:EventSet				rdfs:subClassOf		_:x40.
EF:EventSet				rdfs:isDefinedBy 	EF: .
_:x40					a					owl:Class;
						owl:intersectionOf	(_:x15 _:x41).
_:x15 					a 					owl:Restriction ;
    					owl:onProperty 		EF:operands ;
    					owl:cardinality 	"1"^^xsd:integer.
_:x41					a					owl:Restriction;
						owl:onProperty		EF:operator;
						owl:cardinality		"1"^^xsd:integer.

EF:Time 				a					owl:Class.
EF:Time 				rdfs:isDefinedBy 	EF: .
EF:Time					rdfs:subClassOf 	_:x23.
_:x23					a					owl:Class;
						owl:intersectionOf	(_:x24 _:x25).		
_:x24					a					owl:Restriction;
						owl:onProperty		EF:waitTillEnd;
						owl:cardinality		"1"^^xsd:integer.
_:x25					a 					owl:Restriction;
						owl:onProperty		EF:timeDuration;
						owl:cardinality		"1"^^xsd:integer.		

EF:InitialStage 		a					owl:Class.
EF:InitialStage			rdfs:isDefinedBy 	EF: .
EF:InitialStage			rdfs:subClassOf 	_:x19.
_:x19					a					owl:Class;
						owl:intersectionOf	(_:x20 _:x21).		
_:x20					a					owl:Restriction;
						owl:onProperty		EF:eventSet;
						owl:cardinality		"1"^^xsd:integer.
_:x21					a 					owl:Restriction;
						owl:onProperty		EF:transition;
						owl:maxCardinality	"1"^^xsd:integer.						

EF:Stage  				rdfs:subClassOf		EF:InitialStage .
EF:Stage				rdfs:isDefinedBy 	EF: .
EF:Stage				rdfs:subClassOf 	_:x23.
_:x23					a					owl:Class;
						owl:intersectionOf	(_:x22 _:x24).	
_:x22					a 					owl:Restriction;
						owl:onProperty		EF:timeRestriction;
						owl:maxCardinality	"1"^^xsd:integer.	
_:x24					a					owl:Restriction;
						owl:onProperty		EF:eventSet;
						owl:allValuesFrom	EF:EventSet.
						
EF:MultiEventSet		rdfs:subClassOf		EF:EventSet;
						rdfs:isDefinedBy 	EF: ;
						rdfs:subClassOf		_:x32.
_:x32					a					owl:Class;
						owl:intersectionOf	(_:x2 _:x3 _:x4).						

_:x2					a 					owl:Restriction;
						owl:onProperty		EF:operands;
						owl:cardinality		"1"^^xsd:integer.
_:x3					a 					owl:Restriction;
						owl:onProperty		EF:minRecurrence;
						owl:cardinality		"1"^^xsd:integer.
_:x4					a 					owl:Restriction;
						owl:onProperty		EF:maxRecurrence;
						owl:cardinality		"1"^^xsd:integer.						
						

EF:minRecurrence		a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF: ;
						rdfs:domain			EF:MultiEventSet;
						rdfs:range			xsd:int;
						rdfs:label			"event must recur at least.. ".		
						
EF:maxRecurrence		a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF: ;
						rdfs:domain			EF:MultiEventSet;
						rdfs:range			xsd:int;
						rdfs:label			"event must not recur more than... ".							
						
EF:ComplexEventInstance a                   owl:Class;
						rdfs:isDefinedBy 	EF: ;
						rdfs:subClassOf		_:x33.
_:x33					a					owl:Class;
						owl:intersectionOf	(_:x34 _:x35 _:x36 _:x37 _:x38).						

_:x34					a 					owl:Restriction;
						owl:onProperty		EF:cEventId;
						owl:cardinality		"1"^^xsd:integer.
_:x35					a 					owl:Restriction;
						owl:onProperty		EF:eventDef;
						owl:cardinality		"1"^^xsd:integer.
_:x36					a 					owl:Restriction;
						owl:onProperty		EF:firstStage;
						owl:cardinality		"1"^^xsd:integer.
_:x37					a 					owl:Restriction;
						owl:onProperty		EF:started;
						owl:cardinality		"1"^^xsd:integer.
_:x38					a 					owl:Restriction;
						owl:onProperty		EF:finished;
						owl:cardinality		"1"^^xsd:integer.

EF:cEventId				a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF: ;
						rdfs:domain			EF:ComplexEventInstance;
						rdfs:range			xsd:int;
						rdfs:label			"ID of complex event".
						
EF:eventDef				a					owl:ObjectProperty;
						rdfs:range			EF:ComplexEvent;	
						rdfs:domain			EF:ComplexEventInstance;
						rdfs:isDefinedBy 	EF: ;
						rdfs:label			"points to definition".
						
EF:firstStage			a					owl:ObjectProperty;
						rdfs:isDefinedBy 	EF: ;
						rdfs:domain			EF:ComplexEventInstance;
						rdfs:range			EF:InitialStageInstance;
						rdfs:label			"the first stage of this event".
						
EF:started				a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF: ;
						rdfs:domain			EF:ComplexEventInstance;
						rdfs:range			xsd:dateTime;
						rdfs:label			"date of instantiation".	

EF:finished				a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF: ;
						rdfs:domain			EF:ComplexEventInstance;
						rdfs:range			xsd:dateTime;
						rdfs:label			"date of completion".
						
EF:timeRestriction		a					owl:ObjectProperty.
EF:timeRestriction		rdfs:domain 		EF:InitialStage .
EF:timeRestriction 		rdfs:range          EF:Time .
EF:timeRestriction      rdfs:label 			"time restriction" .
EF:timeRestriction 		rdfs:isDefinedBy 	EF: .

EF:timeDuration 		a					owl:DatatypeProperty.
EF:timeDuration 		rdfs:isDefinedBy 	EF: .
EF:timeDuration 		rdfs:label 			"time-duration" .
EF:timeDuration			 rdfs:range 		xsd:dayTimeDuration .
EF:timeDuration			 rdfs:domain 		EF:Time  .

EF:waitTillEnd			a					owl:DatatypeProperty.
EF:waitTillEnd			rdfs:isDefinedBy 	EF: .
EF:waitTillEnd	 		rdfs:label 			"This stage can only be cleared if the whole duration is elapsed, otherwise the stage will be cleared after all positive events occured" .
EF:waitTillEnd			rdfs:domain			EF:Time.
EF:waitTillEnd			rdfs:range 			xsd:boolean.

EF:hasCondition			a					owl:ObjectProperty.
EF:hasCondition 		rdfs:isDefinedBy 	EF: .
EF:hasCondition 		rdfs:label 			"stage has a condition" .
EF:hasCondition			rdfs:range 			EF:ConditionOrQuery .
EF:hasCondition			rdfs:domain 		EF:InitialStage .

EF:stageID				a					owl:DatatypeProperty.
EF:stageID		 		rdfs:isDefinedBy 	EF: .
EF:stageID		 		rdfs:label 			"ID of this stage" .
EF:stageID				rdfs:range 			EF:int .
EF:stageID				rdfs:domain 		EF:InitialStage .

EF:takeAction			a					owl:ObjectProperty.
EF:takeAction			rdfs:isDefinedBy 	EF: .
EF:takeAction 			rdfs:label 			"the completion of this stage is followed by this action" .
EF:takeAction			rdfs:range 			EF:Action .
EF:takeAction			rdfs:domain 		EF:InitialStage .
#######!!!!!
EF:takeAction			owl2:disjointObjectProperties 	EF:transition.
#######!!!!!

EF:transition			a					owl:ObjectProperty.
EF:transition			rdfs:isDefinedBy 	EF: .
EF:transition 			rdfs:label 			"the completion of this stage is followed by this stage" .
EF:transition			rdfs:range 			EF:Stage .
EF:transition			rdfs:domain 		EF:InitialStage .

EF:ceID					a					owl:DatatypeProperty.
EF:ceID					rdfs:isDefinedBy 	EF: .
EF:ceID	 				rdfs:label 			"complex-event ID" .
EF:ceID					rdfs:domain 		EF:ComplexEvent.
EF:ceID					rdfs:range 			xsd:int .
						
EF:isActive				a					owl:DatatypeProperty.
EF:isActive				rdfs:isDefinedBy 	EF: .
EF:isActive				rdfs:label 			"determines wether this event will be recorded" .
EF:isActive				rdfs:domain 		EF:ComplexEvent .
EF:isActive				rdfs:range 			xsd:int .

EF:recurrences			a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF:;
						rdfs:label 			"event can be repeated x times";
						rdfs:domain 		EF:ComplexEvent;
						rdfs:range 			xsd:int .
						
EF:initializeAt			a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF:;
						rdfs:label 			"the next time this event is to be initialized";
						rdfs:domain 		EF:ComplexEvent;
						rdfs:range 			xsd:dateTime .		
						
EF:period				a					owl:DatatypeProperty;
						rdfs:isDefinedBy 	EF:;
						rdfs:label 			"time-span between two instances of this event";
						rdfs:domain 		EF:ComplexEvent;
						rdfs:range 			xsd:dayTimeDuration .	

EF:initialStage			a					owl:ObjectProperty.
EF:initialStage			rdfs:isDefinedBy 	EF: .
EF:initialStage			rdfs:label 			"the initialStage of this evenet" .
EF:initialStage			rdfs:domain 		EF:ComplexEvent.
EF:initialStage			rdfs:range 			EF:InitialStage .

EF:ceName				a					owl:DatatypeProperty.
EF:ceName				rdfs:isDefinedBy 	EF: .
EF:ceName				rdfs:label 			"name of this event" .
EF:ceName				rdfs:domain 		EF:ComplexEvent .
EF:ceName				rdfs:range 			xsd:string .

EF:overlappingEvent		a					owl:DatatypeProperty.
EF:overlappingEvent		rdfs:isDefinedBy 	EF: .
EF:overlappingEvent		rdfs:label 			"event can be initialized (if true) while an other instance is in progress" .
EF:overlappingEvent		rdfs:domain 		EF:ComplexEvent .
EF:overlappingEvent		rdfs:range 			xsd:int .

EF:recurrences			a					owl:DatatypeProperty.
EF:recurrences			rdfs:isDefinedBy 	EF: .
EF:recurrences			rdfs:label 			"how often this event is will occure" .
EF:recurrences			rdfs:domain 		EF:ComplexEvent .
EF:recurrences			rdfs:range 			xsd:int .

EF:periode				a					owl:DatatypeProperty.
EF:periode				rdfs:isDefinedBy 	EF: .
EF:periode				rdfs:label 			"event will restart in this periode" .
EF:periode				rdfs:domain 		EF:ComplexEvent .
EF:periode				rdfs:range 			xsd:dayTimeDuration .

EF:description 			rdfs:subPropertyOf 	virtrdf:label . 
EF:description 			rdfs:range 			xsd:string .
EF:description 			rdfs:domain 		_:x28 .
EF:description 			rdfs:isDefinedBy 	EF: .
EF:description 			rdfs:label 			"Description" .
_:x28					a					owl:Class;
						owl:unionOf			(EF:ComplexEvent EF:AtomicEvent EF:Action EF:ConditionQuery EF:DataSource).

EF:createdby  			a 					owl:ObjectProperty .
EF:createdby  			rdfs:domain 		_:x29 .
EF:createdby  			rdfs:range 			EF:Users .
EF:createdby  			rdfs:label 			"created by this user" .
EF:createdby  			rdfs:isDefinedBy 	EF: .
_:x29					a					owl:Class;
						owl:unionOf			(EF:ComplexEvent EF:AtomicEvent EF:Action EF:ConditionQuery) .
						
EF:dsinstance 			a 					owl:ObjectProperty .
EF:dsinstance 			 rdfs:domain 		_:x30 .
EF:dsinstance 			 rdfs:range 		EF:DataSource .
EF:dsinstance 			 rdfs:label 		"Data Source of this instance" .
EF:dsinstance 			 rdfs:isDefinedBy 	EF: .	
_:x30					a					owl:Class;
						owl:unionOf			(EF:AtomicEvent EF:Event).

EF:eventSet				a					owl:ObjectProperty.
EF:eventSet				rdfs:isDefinedBy 	EF: .
EF:eventSet				rdfs:label 			"set of events which have to occure (or not) to complete this stage" .
EF:eventSet				rdfs:range 			EF:EventSet .
EF:eventSet				rdfs:domain 		EF:Stage .

EF:initialEventSet		a					owl:ObjectProperty.
EF:initialEventSet		rdfs:isDefinedBy 	EF: .
EF:initialEventSet		rdfs:label 			"initial set of events which have to occure to complete the first stage" .
EF:initialEventSet		rdfs:range 			EF:InitialEventSet .
EF:initialEventSet		rdfs:domain 		EF:InitialStage .
#######!!!!!
EF:initialEventSet		owl2:disjointObjectProperties 	EF:eventSet.
#######!!!!!


# Operators
EF:Operator				a					owl:Class .
EF:Operator				rdfs:label 			"all operators" .
EF:Operator				rdfs:isDefinedBy 	EF: .
EF:Operator				owl:oneOf			(EF:And EF:Or EF:Xor EF:Not).

EF:And					a					EF:Operator.
EF:Or					a					EF:Operator.
EF:Xor				    a					EF:Operator.
EF:Not					a					EF:Operator.
_:x0					a					owl:AllDifferent;
    					owl:distinctMembers (EF:And EF:Or EF:Xor EF:Not).
    					
EF:initialOr			a					owl:ObjectProperty.
EF:initialOr			rdfs:isDefinedBy 	EF: .
EF:initialOr 			rdfs:label 			"one of these event(triggers) will initialize the complex event" .
EF:initialOr			rdfs:range 			 _:x14 .
EF:initialOr			rdfs:domain 		EF:InitialEventSet .
_:x14					a					owl:Class;
						owl:unionOf			(EF:AtomicEvent EF:ComplexEvent EF:EventSet). 

EF:operator				a					owl:ObjectProperty.
EF:operator				rdfs:isDefinedBy 	EF: .
EF:operator 			rdfs:label 			"defines an operator" .
EF:operator				rdfs:range 			EF:Operator.
EF:operator				rdfs:domain 		EF:EventSet .

EF:operands				a					owl:ObjectProperty.
EF:operands				rdfs:isDefinedBy 	EF: .
EF:operands				rdfs:label 			"points to an EventSet which contains all operands pertaining to the chosen operator" .
EF:operands				rdfs:domain			EF:EventSet .
EF:operands				rdfs:range			EF:InitialEventSet.


EF:setID				a					owl:DatatypeProperty.
EF:setID				rdfs:isDefinedBy 	EF: .
EF:setID 				rdfs:label 			"Id of this EventSet" .
EF:setID				rdfs:range 			xsd:int .
EF:setID				rdfs:domain 		EF:InitialEventSet .

# Action
EF:Action 				a 					owl:Class .
EF:Action 				rdfs:isDefinedBy 	EF: .
EF:Action 				rdfs:label 			"Action" .
      					
#Conditions & Queries

EF:ConditionQuery 		rdfs:subClassOf	 	EF:Action.
EF:ConditionQuery 		rdfs:isDefinedBy 	EF: .
EF:ConditionQuery 		rdfs:label 			"condition or query" .

_:x3					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:actionid 			a 					owl:DatatypeProperty .
EF:actionid 			rdfs:range 			xsd:int .
EF:actionid 			rdfs:domain 		_:x3.
EF:actionid 			rdfs:isDefinedBy 	EF: .
EF:actionid 			rdfs:label 			"ActionID" .

_:x6					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:wsdladdress 			a 					owl:DatatypeProperty .
EF:wsdladdress 			rdfs:range 			xsd:string .
EF:wsdladdress 			rdfs:domain 		_:x6.
EF:wsdladdress 			rdfs:isDefinedBy 	EF: .
EF:wsdladdress 			rdfs:label 			"WsdlAddress" .

_:x7					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:endpointaddress 		a 					owl:DatatypeProperty .
EF:endpointaddress 		rdfs:range 			xsd:string .
EF:endpointaddress 		rdfs:domain 		_:x7.
EF:endpointaddress 		rdfs:isDefinedBy 	EF: .
EF:endpointaddress 		rdfs:label 			"EndpointAddress" .

_:x8					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:methodename 			a 					owl:DatatypeProperty .
EF:methodename 			rdfs:range 			xsd:string .
EF:methodename 			rdfs:domain 		_:x8.
EF:methodename 			rdfs:isDefinedBy 	EF: .
EF:methodename 			rdfs:label 			"MethodeName" .

_:x9					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:paramtypes 			a 					owl:DatatypeProperty .
EF:paramtypes 			rdfs:range 			xsd:string .
EF:paramtypes 			rdfs:domain 		_:x9.
EF:paramtypes 			rdfs:isDefinedBy 	EF: .
EF:paramtypes 			rdfs:label 			"ParamTypes" .

_:x10					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:paramdescr 			a 					owl:DatatypeProperty .
EF:paramdescr 			rdfs:range 			xsd:string .
EF:paramdescr 			rdfs:domain 		_:x10.
EF:paramdescr 			rdfs:isDefinedBy 	EF: .
EF:paramdescr 			rdfs:label 			"ParamDescr" .

_:x11					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:returntype 			a 					owl:DatatypeProperty .
EF:returntype 			rdfs:range 			xsd:string .
EF:returntype 			rdfs:domain 		_:x11.
EF:returntype 			rdfs:isDefinedBy 	EF: .
EF:returntype 			rdfs:label 			"ReturnType" .

_:x12					a					owl:Class;
						owl:unionOf			(EF:Action EF:ConditionQuery) .
EF:returndescr 			a 					owl:DatatypeProperty .
EF:returndescr 			rdfs:range 			xsd:string .
EF:returndescr 			rdfs:domain 		_:x12.
EF:returndescr 			rdfs:isDefinedBy 	EF: .
EF:returndescr 			rdfs:label 			"ReturnDescr" .

# Event
EF:Event 				a 					owl:Class .
EF:Event 				rdfs:isDefinedBy 	EF: .
EF:Event 				rdfs:label 			"atomic event" .

EF:eventid 				a 					owl:DatatypeProperty .
EF:eventid 				rdfs:range 			xsd:int .
EF:eventid 				rdfs:domain 		EF:Event .
EF:eventid 				rdfs:isDefinedBy 	EF: .
EF:eventid 				rdfs:label 			"EventID" .

EF:occurence 			a 					owl:DatatypeProperty .
EF:occurence 			rdfs:range 			xsd:dateTime .
EF:occurence 			rdfs:domain 		EF:Event .
EF:occurence 			rdfs:isDefinedBy 	EF: .
EF:occurence 			rdfs:label 			"Occurence" .

EF:row 					a 					owl:DatatypeProperty .
EF:row 					rdfs:range 			xsd:string .
EF:row 					rdfs:domain 		EF:Event .
EF:row 					rdfs:isDefinedBy 	EF: .
EF:row 					rdfs:label 			"Row" .

EF:trigger 				a 					owl:ObjectProperty .
EF:trigger 				rdfs:domain 		EF:Event .
EF:trigger 				rdfs:range 			EF:AtomicEvent .
EF:trigger 				rdfs:label 			"the trigger which caused this event" .
EF:trigger				rdfs:isDefinedBy 	EF: .

# User
EF:User 				a 					owl:Class .
EF:User 				rdfs:isDefinedBy 	EF: .
EF:User 				rdfs:label 			"User" .

EF:userid 				a 					owl:DatatypeProperty .
EF:userid 				rdfs:range 			xsd:int .
EF:userid 				rdfs:domain 		EF:User .
EF:userid 				rdfs:isDefinedBy 	EF: .
EF:userid 				rdfs:label 			"UserID" .

EF:name 				a 					owl:DatatypeProperty .
EF:name 				rdfs:range 			xsd:string .
EF:name 				rdfs:domain 		EF:User .
EF:name 				rdfs:isDefinedBy 	EF: .
EF:name 				rdfs:label 			"Name" .

EF:created 				a 					owl:DatatypeProperty .
EF:created 				rdfs:range 			xsd:dateTime .
EF:created 				rdfs:domain 		EF:User .
EF:created 				rdfs:isDefinedBy 	EF: .
EF:created 				rdfs:label 			"Created" .

EF:lastlogin 			a 					owl:DatatypeProperty .
EF:lastlogin 			rdfs:range 			xsd:dateTime .
EF:lastlogin 			rdfs:domain 		EF:User .
EF:lastlogin 			rdfs:isDefinedBy 	EF: .
EF:lastlogin 			rdfs:label 			"LastLogIn" .

#DataSource
EF:DataSource 			a 					owl:Class .
EF:DataSource 			rdfs:isDefinedBy 	EF: .
EF:DataSource 			rdfs:label 			"DataSource" .

EF:instanceID			a 					owl:DatatypeProperty .
EF:instanceID 			rdfs:range 			xsd:int .
EF:instanceID			rdfs:domain 		EF:DataSource .
EF:instanceID			rdfs:isDefinedBy 	EF: .
EF:instanceID			rdfs:label 			"DSInstance" .

EF:dsname 				a 					owl:DatatypeProperty .
EF:dsname 				rdfs:range 			xsd:string .
EF:dsname 				rdfs:domain 		EF:DataSource .
EF:dsname 				rdfs:isDefinedBy 	EF: .
EF:dsname 				rdfs:label 			"DSName" .

EF:dstype 				a 					owl:DatatypeProperty .
EF:dstype 				rdfs:range 			xsd:string .
EF:dstype 				rdfs:domain 		EF:DataSource .
EF:dstype 				rdfs:isDefinedBy 	EF: .
EF:dstype 				rdfs:label 			"DSType" .

EF:sparqlendpointaddress a 					owl:DatatypeProperty .
EF:sparqlendpointaddress rdfs:range			 xsd:string .
EF:sparqlendpointaddress rdfs:domain 		EF:DataSource .
EF:sparqlendpointaddress rdfs:isDefinedBy 	EF: .
EF:sparqlendpointaddress rdfs:label 		"SparqlEndpointAddress" .

EF:rdfgraphs 			a 					owl:DatatypeProperty .
EF:rdfgraphs 			rdfs:range 			xsd:string .
EF:rdfgraphs 			rdfs:domain 		EF:DataSource .
EF:rdfgraphs 			rdfs:isDefinedBy 	EF: .
EF:rdfgraphs 			rdfs:label 			"RdfGraphs" .

#AtomicEvent
EF:AtomicEvent 			a 					owl:Class .
EF:AtomicEvent 			rdfs:isDefinedBy 	EF: .
EF:AtomicEvent 			rdfs:label 			"Atomic Event" .

EF:triggerid 			a 					owl:DatatypeProperty .
EF:triggerid 			rdfs:range 			xsd:int .
EF:triggerid 			rdfs:domain 		EF:AtomicEvent .
EF:triggerid 			rdfs:isDefinedBy 	EF: .
EF:triggerid 			rdfs:label 			"TriggerID" .

EF:triggertype 			a 					owl:DatatypeProperty .
EF:triggertype 			rdfs:range 			xsd:string .
EF:triggertype 			rdfs:domain 		EF:AtomicEvent .
EF:triggertype 			rdfs:isDefinedBy 	EF: .
EF:triggertype 			rdfs:label 			"TriggerType" .

EF:triggername 			a 					owl:DatatypeProperty .
EF:triggername 			rdfs:range 			xsd:string .
EF:triggername 			rdfs:domain 		EF:AtomicEvent .
EF:triggername 			rdfs:isDefinedBy 	EF: .
EF:triggername 			rdfs:label 			"TriggerName" .

EF:alternativename 		a 					owl:DatatypeProperty .
EF:alternativename 		rdfs:range 			xsd:string .
EF:alternativename 		rdfs:domain 		EF:AtomicEvent .
EF:alternativename 		rdfs:isDefinedBy 	EF: .
EF:alternativename 		rdfs:label 			"AlternativeName" .

EF:internalsource 		a 					owl:DatatypeProperty .
EF:internalsource 		rdfs:range 			xsd:string .
EF:internalsource 		rdfs:domain 		_:x27.
EF:internalsource 		rdfs:isDefinedBy 	EF: .
EF:internalsource 		rdfs:label 			"Internal Source" .
_:x27					a					owl:Class;
						owl:unionOf			(EF:AtomicEvent EF:Event).

EF:values 				a 					owl:DatatypeProperty .
EF:values 				rdfs:range 			xsd:string .
EF:values 				rdfs:domain 		EF:AtomicEvent .
EF:values 				rdfs:isDefinedBy 	EF: .
EF:values 				rdfs:label 			"Values" .

EF:created 				a 					owl:DatatypeProperty .
EF:created 				rdfs:range 			xsd:dateTime .
EF:created 				rdfs:domain 		EF:AtomicEvent .
EF:created 				rdfs:isDefinedBy 	EF: .
EF:created 				rdfs:label 			"Created" .


', '', 'http://EventFramework/Schema');

commit work;

SELECT 'please continue the installation of the event-framework by executing \'CentralDB - Procedures.sql\'';