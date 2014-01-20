--all procedures needed for the central DB

SELECT 'Scroll to the end, to see installation-summary!';

--returns utc time of given date - UTC_TIME() returns utc now()
create procedure UTC_TIME
(
in datum DATETIME := null
)
{
if(datum IS NULL)
	datum := now();
	
return DATEADD('minute', timezone(datum)*(-1), datum);
};

create procedure nullableDateString(in datum DATETIME)
RETURNS VARCHAR
{
if(datum is NULL)
return '';
else
return datestring(datum);
};

create procedure DUMP_VEC_IMPL (inout _vec any, inout _ses any)
{
  declare _len, _ctr integer;
  if (193 <> __tag (_vec))
    {
      if (isstring (_vec))
        http (WS.WS.STR_SQL_APOS (_vec), _ses);
      else
        http (cast (_vec as varchar), _ses);
      return;
    }
  _len := length (_vec);
  _ctr := 0;
  http ('\nvector (', _ses);
  while (_ctr < _len)
    {
      if (_ctr > 0)
        http (', ', _ses);
      DUMP_VEC_IMPL (_vec[_ctr], _ses);
      _ctr := _ctr+1;
    }
  http (')', _ses);
};
--return string-copy of vector
create function DUMP_VEC (in _vec any)
{
  declare _ses any;
  _ses := string_output();
  DUMP_VEC_IMPL (_vec, _ses);
  return string_output_string (_ses);
}; 

--soap procedures
create procedure
SOAP_CLIENT (
    in url varchar,
    in operation varchar,
    in target_namespace varchar := null,
    in parameters any := null,
    in headers any := null,
    in soap_action varchar := '',
    in attachments any := null,
    in ticket any := null,
    in passwd varchar := null,
    in user_name varchar := null,
    in user_password varchar := null,
    in auth_type varchar := 'none',
    in security_type varchar := 'sign',
    in debug integer := 0,
    in template varchar := null,
    in style integer := 0,
    in version integer := 11,
    in direction integer := 0,
    in http_header any := null,
    in security_schema any := null,
    in time_out int := 100)
{
  declare conn, ret any;
  conn := null;
  ret := SOAP_ASYNC_CLIENT (
		url,
		operation,
		target_namespace,
		parameters,
		headers,
		soap_action,
		attachments,
		ticket,
		passwd,
		user_name,
		user_password,
		auth_type,
		security_type,
		debug,
		template,
		style,
		version,
		direction,
		http_header,
		security_schema,
		time_out,
		conn
		);
   return ret;
}
;

create procedure
SOAP_ASYNC_CLIENT
    (
    in url varchar,
    in operation varchar,
    in target_namespace varchar := null,
    in parameters any := null,
    in headers any := null,
    in soap_action varchar := '',
    in attachments any := null,
    in ticket any := null,
    in passwd varchar := null,
    in user_name varchar := null,
    in user_password varchar := null,
    in auth_type varchar := 'none',
    in security_type varchar := 'sign',
    in debug integer := 0,
    in template varchar := null,
    in style integer := 0,
    in version integer := 11,
    in direction integer := 0,
    in http_header any := null,
    in security_schema any := null,
    in time_out int := 100,
    inout conn any
    )
{
  declare host, path varchar;
  declare hinfo, resp, ver, skeys any;
  declare security_tp int;

  hinfo := rfc1808_parse_uri (url);
  host := hinfo [1];
  if (lower (hinfo[0]) = 'https' and ticket is null)
    ticket := '1';
  path := vspx_uri_compose (vector ('','', hinfo [2], hinfo[3], hinfo[4], hinfo[5]));
  if (parameters is null)
    parameters := vector ();

  if (auth_type = 'x509' or auth_type = 'kerberos' or auth_type = 'key')
    security_tp := case security_type when 'sign' then 1 else 2 end;
  else
    {
      security_tp := 0;
      if (lower (hinfo[0]) <> 'https')
        ticket := null;
    }

  if (debug)
    ver := -1 * version;
  else
    ver := version;

  soap_action := '"' || trim (soap_action, '"') || '"';

  if (lower (hinfo[0]) = 'https' and strchr (host, ':') is null)
    host := host || ':443';

  skeys := null;

  if (connection_get ('wssc-keys') is not null)
    connection_set ('wssc-keys', null);

  resp := soap_call_new (host, path, target_namespace, operation, parameters,
      ver, ticket, passwd, soap_action, style, -- rpc/doclit
      user_name, user_password, security_tp, ticket, template, headers, http_header,
      direction, security_schema, conn, time_out, skeys);

  if (skeys is not null)
    connection_set ('wssc-keys', skeys);

  return resp;
}
;


--executes simple SELECT FROM WHERE querry to test conditions proposed by event creator
create procedure
EVENT_FRAMEWORK_TEST_SQL_CONDITION
( 
in executeQuerry VARCHAR
)
RETURNS ANY ARRAY ARRAY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_TEST_SQL_CONDITION: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;};*/
DECLARE state, msg, descs, rows, exarray any;
EXEC(executeQuerry, state, msg, vector(), 100, descs, rows);
--SIGNAL(DUMP_VEC(rows), DUMP_VEC(descs));
if(isarray(rows))
{
exarray := make_array(LENGTH(rows)+1, 'any');
exarray[0] := make_array(LENGTH(descs[0]), 'any');

declare i,j integer;
for(i:=0;i<LENGTH(descs[0]);i:=i+1){
exarray[0][i] := descs[0][i][0];
}

for(i:=0;i<LENGTH(rows);i:=i+1){
exarray[i+1] :=make_array(LENGTH(rows[0]), 'any');
for(j:=0;j<LENGTH(rows[0]);J:=j+1){

exarray[i+1][j] := CAST(rows[i][j] as varchar);
}
}
return exarray;
}
return NULL;
} ;

--check for existing trigger on a table
create procedure
EVENT_FRAMEWORK_CHECK_TRIGGER_EXISTS
(
in controlID INTEGER,
in tablename VARCHAR,
in triggername VARCHAR
)
RETURNS VARCHAR
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_CHECK_TRIGGER_EXISTS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows any;
exec(sprintf('
SELECT CASE WHEN EXISTS(SELECT * FROM SYS_TRIGGERS WHERE strcontains(T_NAME, \'%s\') AND strcontains(T_TABLE, \'%s\')) THEN \'True\' ELSE \'False\' END', triggername, tablename), state, msg, vector (), 1, descs, rows);

RETURN CAST(rows[0][0] AS VARCHAR);
}
signal('wrongControlID', 'provide the correct control-ID');
return;
}
;

--returns all triggers of a table
create procedure
EVENT_FRAMEWORK_GET_TRIGGERS_OF_TABLE
(
in controlID INTEGER,
in tablename VARCHAR
)
RETURNS VARCHAR ARRAY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_TRIGGERS_OF_TABLE: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
  declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows any;
exec(sprintf('
SELECT T_NAME FROM SYS_TRIGGERS WHERE strcontains(T_TABLE, \'%s\')', tablename), state, msg, vector (), 0, descs, rows);

declare exarray VARCHAR ARRAY;
exarray := make_array(LENGTH (rows), 'any' );

declare i INTEGER;
for(i:=0;i<LENGTH(rows);i:=i+1){
exarray[i] := rows[i][0];
}

return exarray;
}
signal('wrongControlID', 'provide the correct control-ID');
return;
};

--returns the syntax of a trigger
create procedure
EVENT_FRAMEWORK_GET_TRIGGER_SYNTAX
(
in controlID INTEGER,
in tablename VARCHAR,
in triggername VARCHAR
)
RETURNS VARCHAR
{  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_TRIGGER_SYNTAX: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows any;
exec(sprintf('SELECT T_TEXT FROM  SYS_TRIGGERS 
WHERE strcontains(T_TABLE, \'%s\') AND strcontains(T_NAME, \'%s\')', tablename, triggername), state, msg, vector (), 1, descs, rows);

RETURN CAST(rows[0][0] AS VARCHAR);
}
signal('wrongControlID', 'provide the correct control-ID');
return;
};

create function EVENT_FRAMEWORK_GET_SESSION_NR()
{
declare session, rowcount INTEGER;
  session := 0;
  rowcount := 1;
  declare state, msg, descs, rows any;
  declare statement VARCHAR;
  statement := 'SELECT COUNT(*) FROM EventFrameworkUsers WHERE SessionNr = ?';
 
WHILE(session = 0 OR rowcount > 0)
{
session := rnd(1000000000) +1;
EXEC(statement ,state ,msg , vector(session), 1, descs, rows);
rowcount := CAST(rows[0][0] AS INTEGER);
}
return session;
};


---this procedure is called from client-databases to register new events
--change parameters of soap call!
create procedure
EVENT_FRAMEWORK_INSERT_NEW_EVENT( 
in dsInstance INTEGER,
in internalSource VARCHAR,
in name VARCHAR := '',
in occ DATETIME,
in rowVector any,
in ceInstance INTEGER := 0,
in trigg INTEGER :=0
)
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_NEW_EVENT: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
  
if(occ IS NULL)
occ := UTC_TIME();

declare id, ceid, active integer;
active := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'active');
if(not active)
return 0;

if(trigg = 0 and ceInstance = 0)
    trigg := (SELECT TriggerID FROM EventFrameworkTriggers WHERE TriggerName = name OR AlternativeName = name);

if(trigg IS NULL OR trigg = 0 )
{
if(ceInstance =0)
	return 0;
}

if(trigg IS NOT NULL AND trigg > 0)
{

INSERT INTO EventFrameworkEvents("DSInstance", "InternalSource", "TriggerID", "Occurence", "Row")
	VALUES(dsInstance, internalSource, trigg, occ, rowVector);
id := identity_value();
EVENT_FRAMEWORK_UPDATE_EVENTS(0, trigg, id, occ);
}
else if(ceInstance IS NOT NULL AND ceInstance > 0)
{
--signal('hfdshsd','shdz');
ceid := (Select CEID FROM EventFrameworkComplexEventInstances WHERE EventID = ceInstance);
INSERT INTO EventFrameworkEvents("InternalSource", "CEID", "CeInstance", "Occurence", "Row")
	VALUES(internalSource, ceid, ceInstance, occ, rowVector);
id := identity_value();
EVENT_FRAMEWORK_UPDATE_EVENTS(1, ceid, id, occ);
}
else
    return 0;

commit work;
return 1;
} ;

create procedure EVENT_FRAMEWORK_DROP_TRIGGER(
in controlID integer,
in triggername varchar
)
RETURNS VARCHAR
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_DROP_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
EXEC(sprintf('drop trigger %s', triggername));
commit work;
return 'True';
}

signal('wrongControlID', 'provide the correct control-ID');
return 'False';

cont: 
return 'False';
};

--calls a remote procedure/function of a soap service (uses SOAP 1.0)
create procedure EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP (
in parametersVector any,
in endpointUrl varchar,
in operationName varchar,
in targetNamespace varchar,
in soapAction varchar
)
{


return SOAP_CLIENT(
URL=>endpointUrl,
operation=>operationName,
parameters=>parametersVector,
target_namespace=>targetNamespace,
soap_action=>soapAction,
direction=>1,
version=>01);
};

--calls a remote procedure/function of a soap service (uses SOAP 1.1)
create procedure EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP_11 (
in parametersVector any,
in endpointUrl varchar,
in operationName varchar,
in targetNamespace varchar,
in soapAction varchar
)
returns varchar
{
declare continue handler for SQLSTATE 'HTCLI' {return;};
declare resp varchar;
resp := SOAP_CLIENT(
URL=>endpointUrl,
operation=>operationName,
parameters=>parametersVector,
target_namespace=>targetNamespace,
soap_action=>soapAction,
version=>11);

return resp;
};

--establishes new trigger an a given table
   create procedure
EVENT_FRAMEWORK_SET_NEW_TRIGGER(
in controlID INTEGER,
in triggerStatement VARCHAR,
in tableName VARCHAR,
in triggerName VARCHAR,
in triggerType VARCHAR,
in condition VARCHAR,
in paramArray ANY := null
)
RETURNS VARCHAR
{
/* declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_SET_NEW_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 'False';
  }; */

declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
--insert new trigger
Insert Replacing EventFrameworkTriggerConditions(TriggerName,TriggerType,TableName,Condition,ParamArray) VALUES(triggerName, triggerType, tableName, condition, paramArray);

if(triggerStatement IS NOT NULL  AND LENGTH(triggerStatement)>10)
{
EXEC(triggerStatement);
commit work;
}
RETURN 'True';
}
signal('wrongControlID', 'provide the correct control-ID');
return 'False';
};	

--returns all tables of a given DB-schema
 create procedure
EVENT_FRAMEWORK_GET_SCHEMA_TABLES
(in controlID INTEGER)
RETURNS VARCHAR ARRAY
{
/*  declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_SCHEMA_TABLES: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  }; */
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows any;
EXEC('SELECT inn.T_NAME FROM XDDL_TABLES as inn
WHERE strcontains(T_NAME, \'EventFrameworkConstants\')', state, msg, vector(), 3000, descs, rows);

declare TABLE_NAMES,SCHEMAA VARCHAR;

SCHEMAA := rows[0][0];
SCHEMAA := SUBSTRING(SCHEMAA,1,LENGTH(SCHEMAA)-LENGTH('EventFrameworkConstants'));

EXEC(sprintf('SELECT inn.T_NAME FROM XDDL_TABLES as inn
WHERE strcontains(T_NAME, \'%s\')', SCHEMAA), state, msg, vector(), 3000, descs, rows);

declare exarray VARCHAR ARRAY;
exarray := make_array(LENGTH (rows), 'any' );

declare i INTEGER;
for(i:=0;i<LENGTH(rows);i:=i+1){
exarray[i] := rows[i][0];
};

return exarray;
}
signal('wrongControlID', 'provide the correct control-ID');
return;
};

create procedure
EVENT_FRAMEWORK_INSERT_CONSTANT
( 
in controlID INTEGER,
in keyV VARCHAR,
in valueV ANY
)
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_CONSTANT: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID IS NULL OR currentID = controlID)
{
INSERT REPLACING EventFrameworkConstants VALUES(keyV, valueV);
return;
}
signal('wrongControlID', 'provide the correct control-ID');
return;
};

create procedure EVENT_FRAMEWORK_REGISTER_DB
( 
in controlID INTEGER,
in dbInstance INTEGER,
in endpointAdd VARCHAR,
in thisEndpoint VARCHAR
)
RETURNS VARCHAR
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_CONSTANT: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 'False';
  };
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID IS NULL OR currentID = controlID)
{
INSERT INTO EventFrameworkConstants VALUES('endpointAddress', endpointAdd);
INSERT INTO EventFrameworkConstants VALUES('dbInstance', dbInstance);
INSERT INTO EventFrameworkConstants VALUES('controlID', controlID);
INSERT INTO EventFrameworkConstants VALUES('thisEndpointAddress', thisEndpoint);
INSERT INTO EventFrameworkConstants VALUES('active', 1);
return 'True'; 
}
signal('wrongControlID', 'provide the correct control-ID');
return;
} ;

create procedure EVENT_FRAMEWORK_REGISTER_REMOTE_DB
(
in dsName VARCHAR,
in dsType VARCHAR,
in description VARCHAR,
in remoteSoapEndpoint VARCHAR,
in remoteSparqlEndpoint VARCHAR
)
RETURNS INTEGER
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_REGISTER_REMOTE_DB: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  goto ex;
  };
whenever sqlstate 'HTCLI' goto ex;
whenever sqlstate 'SP027' goto ex;

INSERT INTO EventFrameworkDataSources (DSName, DSType, Description, ControlID, ProcedureEndpoint, SparqlEndpointAddress) 
VALUES (dsName, dsType, description, rnd(2000000000), remoteSoapEndpoint, remoteSparqlEndpoint);
commit work;
declare dbInstance, controlID, thisEndpoint, resp any;
dbInstance := (SELECT MAX(DSInstance) FROM EventFrameworkDataSources);
controlID := (SELECT ControlID FROM EventFrameworkDataSources WHERE ProcedureEndpoint = remoteSoapEndpoint);
thisEndpoint := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'endpointAddress');

if(LENGTH(remoteSoapEndpoint) > 10)
{
resp := EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP(vector('thisEndpoint', remoteSoapEndpoint, 'controlID', controlID ,'dbInstance', dbInstance, 'endpointAdd', thisEndpoint), remoteSoapEndpoint, 'EVENT_FRAMEWORK_REGISTER_DB', 'services.wsdl', '"http://openlinksw.com/virtuoso/soap/schema#EVENT_FRAMEWORK_REGISTER_DB"');

if(isarray(resp) AND LENGTH(resp) > 1 AND CAST(resp[1][1] as VARCHAR) = 'True')
{
commit work;
RETURN CAST(dbInstance as integer);
}
else
goto ex;
}
RETURN CAST(dbInstance as integer);

ex:
rollback work;
if(dbInstance IS NOT NULL)
DELETE FROM EventFrameworkDataSources WHERE DSInstance = dbInstance;
commit work;
return 0;
};


create procedure
EVENT_FRAMEWORK_GET_COLUMNS_OF_TABLE
( in controlID INTEGER,
in tableName VARCHAR )
RETURNS VARCHAR ARRAY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_COLUMNS_OF_TABLE: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows any;
EXEC(sprintf('SELECT COL_DTP, "COLUMN" FROM  "TABLE_COLS"
WHERE subseq("TABLE", strrchr("TABLE", \'.\')+1) = \'%s\' ORDER BY "COL_ID"',tableName ), state, msg, vector(), 3000, descs, rows);

if(isarray(rows))
{
declare exarray VARCHAR ARRAY;
exarray := make_array(LENGTH (rows), 'any' );
declare i INTEGER;
for(i:=0;i<LENGTH(rows);i:=i+1){
if(EVENT_FRAMEWORK_TYPE_TEST(CAST(rows[i][0] AS INTEGER)) = 1)
{
exarray[i] := rows[i][1];
}
}
return exarray;
}
return NULL;
}
signal('wrongControlID', 'provide the correct control-ID');
return NULL;
};

create procedure EVENT_FRAMEWORK_GET_GRAPHS
(in controlID INTEGER)
RETURNS VARCHAR ARRAY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_GRAPHS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows any;
EXEC('SELECT GRAPH_IRI FROM SPARQL_SELECT_KNOWN_GRAPHS_T
WHERE not strcontains(GRAPH_IRI , \'localhost\') AND NOT strcontains(GRAPH_IRI , \'w3\') AND NOT strcontains(GRAPH_IRI , \'openlink\') ', state, msg, vector(), 3000, descs, rows);

declare exarray VARCHAR ARRAY;
exarray := make_array(LENGTH (rows), 'any' );

declare i INTEGER;
for(i:=0;i<LENGTH(rows);i:=i+1){
exarray[i] := rows[i][0];
}
return exarray;
}
signal('wrongControlID', 'provide the correct control-ID');
return;
};

 create procedure EVENT_FRAMEWORK_SPARQL_TO_SQL
(
in syntax VARCHAR
)
RETURNS VARCHAR
{
return CAST(sparql_to_sql_text(syntax) AS VARCHAR);
};

create procedure EVENT_FRAMEWORK_GET_NEXT_ID(
in tablename varchar,
in idColumn varchar
)
RETURNS INTEGER
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_NEXT_ID: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
DECLARE state, msg, rows any;
DECLARE descs ANY ARRAY;

EXEC(sprintf('SELECT CASE WHEN MIN(aa.%s) > 0 THEN 0 ELSE (SELECT TOP 1 bb.%s+1 FROM %s as bb WHERE NOT EXISTS(SELECT * FROM %s WHERE %s = bb.%s+1) ORDER BY %s) END FROM %s as aa', idColumn, idColumn, tablename, tablename, idColumn, idColumn, idColumn, tablename), state, msg, vector(), 1, descs, rows);
return CAST(rows[0][0] AS INTEGER);
};

create procedure EVENT_FRAMEWORK_TYPE_TEST (in DTP integer)
RETURNS VARCHAR ARRAY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_TYPE_TEST: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare TypeName varchar;
declare i, TypeNr integer;

TypeName:= internal_type_name(DTP);
if(TypeName <> 'UNK_DV_TYPE' AND not strcontains(TypeName, 'ARRAY') AND DTP NOT IN(180,181,196,197,198,199,200,204,207,234,246,254, 126, 133,134,135,238,121,122,123))
{
return 1;
}
else
{
return 0;
}
} ;

--sends soap request for conditon or action to the central-service and converts returned soap-type to DB-type
create procedure EVENT_FRAMEWORK_CALL_SERVICE_METHODE
(
in methodname varchar,
in parameters any
)
returns any
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_CALL_SERVICE_METHODE: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/

declare outchar, type, value, endpoint varchar;
endpoint := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'virtuosoExtentionPoint');
outchar := EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP_11(parameters, endpoint, methodname, 'http://tempuri.org/', '"http://tempuri.org/IVirtuosoExtentionService/' || methodname || '"');

type := CAST(xpath_eval('string(//*[ends-with(name(),  \'Result\')]/@xsi:type)', xml_tree_doc(outchar),0)[0] AS VARCHAR);
type := subseq(type, strstr(type, 'XMLSchema:')+10);
value := CAST(xpath_eval('//*[ends-with(name(),  \'Result\')]', xml_tree_doc(outchar), 0)[0] AS VARCHAR);

if(type in ('bool', 'boolean'))
{
if(trim(lower(value)) = 'false')
{
return CAST(1 as SMALLINT);
}
else if(trim(lower(value)) = 'false')
{
return CAST(0 as SMALLINT);
}
}
if(type in ('short'))
{
return CAST(value as SMALLINT);
}
if(type in ('Double', 'double', 'Long', 'long', 'Float', 'float', 'decimal'))
{
return CAST(value as decimal);
}
if(type in ('Integer', 'int'))
{
return cast(value as integer);
}
if(type in ('timeInstant', 'date', 'dateTime'))
{
return cast(value as DATETIME);
}
return value;

cont:
return NULL;
};


create procedure EVENT_FRAMEWORK_ADD_DURATION(
in datum DATETIME,	
in dayTimeDuration VARCHAR
)
RETURNS DATETIME
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_ADD_DURATION: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
  
declare days, hours, minutes, seconds, sign integer;
dayTimeDuration :=trim(dayTimeDuration);
sign:=1;
if(dayTimeDuration[0] = '-'[0])
{
sign:=-1;
}

days := CAST(COALESCE(regexp_match('(?<=P)[[:digit:]]+(?=D)', dayTimeDuration), 0) AS INTEGER)*sign;
hours := CAST(COALESCE(regexp_match('(?<=T)[[:digit:]]+(?=H)', dayTimeDuration),0) AS INTEGER)*sign;
minutes := CAST(COALESCE(regexp_match('(?<=[[:alpha:]])[[:digit:]]+(?=M)', dayTimeDuration),0) AS INTEGER)*sign;
seconds := CAST(COALESCE(regexp_match('(?<=[[:alpha:]])[[:digit:]]+((?=S)|(?=\\.))', dayTimeDuration),0) AS INTEGER)*sign;

datum:= dateadd('day', days, datum);
datum:= dateadd('hour', hours, datum);
datum:= dateadd('minute', minutes, datum);
datum:= dateadd('second', seconds, datum);

return datum;
} ;

create procedure EVENT_FRAMEWORK_GET_DURATION_SECONDS(
in dayTimeDuration VARCHAR
)
RETURNS INTEGER
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_DURATION_SECONDS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
  
declare days, hours, minutes, seconds, sign, ret integer;
dayTimeDuration :=trim(dayTimeDuration);
sign:=1;
if(dayTimeDuration[0] = '-'[0])
{
sign:=-1;
}

days := CAST(COALESCE(regexp_match('(?<=P)[[:digit:]]+(?=D)', dayTimeDuration), 0) AS INTEGER)*sign;
hours := CAST(COALESCE(regexp_match('(?<=T)[[:digit:]]+(?=H)', dayTimeDuration),0) AS INTEGER)*sign;
minutes := CAST(COALESCE(regexp_match('(?<=[[:alpha:]])[[:digit:]]+(?=M)', dayTimeDuration),0) AS INTEGER)*sign;
seconds := CAST(COALESCE(regexp_match('(?<=[[:alpha:]])[[:digit:]]+((?=S)|(?=\\.))', dayTimeDuration),0) AS INTEGER)*sign;

ret := days * 24 * 60 *60;
ret := ret + (hours * 60 *60);
ret := ret + seconds + (minutes * 60);

return ret;
} ;

--converts from sql datetime to rdf-datetime-literal and back (without timezone!)
 create procedure
EVENT_FRAMEWORK_CONVERT_DATETIME(
in datum any
)
RETURNS ANY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_CONVERT_DATETIME: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
  
if(datum IS NULL)
{
signal('wrongInput', 'please provide a parameter');
return 0;
}
if(internal_type_name(internal_type(datum)) = 'VARCHAR')
{
datum:= stringdate(regexp_match('(?<=")[^"]*(?=")', datum));
return datum;
}
else if(internal_type_name(internal_type(datum)) = 'DATETIME')
{
datum := '"' || regexp_replace(regexp_replace(CAST(DATEADD('minute', timezone(datum)*(-1), datum) AS VARCHAR), ' ', 'T'), '\\.[[:digit:]]{5,}', 'Z') || '"^^xsd:dateTime';
return datum;
}
else
{
return null;
}
};


--executes sparql querry only for use as internal sparql 'endpoint'
create procedure
EVENT_FRAMEWORK_INTERNAL_SPARQL
( 
in controlID INTEGER,
in executeQuerry VARCHAR
)
RETURNS ANY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INTERNAL_SPARQL: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
DECLARE state, msg, descs, rows, exarray any;
EXEC(executeQuerry, state, msg, vector(), 100, descs, rows);

if (internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows)>0)
{
exarray := make_array(LENGTH(rows)+1, 'any');
exarray[0] := make_array(LENGTH(descs[0]), 'any');

declare i,j integer;
for(i:=0;i<LENGTH(descs[0]);i:=i+1){
exarray[0][i] := descs[0][i][0];
}
for(i:=0;i<LENGTH(rows);i:=i+1){
exarray[i+1] :=make_array(LENGTH(rows[0]), 'any');
for(j:=0;j<LENGTH(rows[0]);J:=j+1){
RESULT(rows[i]);
exarray[i+1][j] := CAST(rows[i][j] as varchar);
}
}
return exarray;
}
}
signal('wrongControlID', 'provide the correct control-ID');
cont: 
return 0;
};

create procedure EVENT_FRAMEWORK_GET_NEXT_PROPERTY_ID
(
in property VARCHAR,
in offset INTEGER := 0
)
RETURNS INTEGER
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_NEXT_PROPERTY_ID: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
whenever sqlstate '22003' goto cont;

DECLARE state, msg, descs, rows, exarray any;
EXEC(sprintf(
'SELECT CASE WHEN "zz"  IS NULL THEN 1 ELSE "zz" END
FROM(
SELECT TOP %s,1 "s_19_2"."zz" AS "zz"
FROM ( 
	SELECT ( CAST ( "s_19_1_t0"."O" AS INTEGER) + 1) AS "zz" 
	FROM DB.DBA.RDF_QUAD AS "s_19_1_t0" 
	WHERE "s_19_1_t0"."G" = __i2idn ( __bft( \'http://EventFramework/Stages\' , 1)) 
		AND "s_19_1_t0"."P" = __i2idn ( __bft(\'http://EventFramework/Schema/%s\', 1)) 
		OPTION (QUIETCAST) ) AS "s_19_2" 
WHERE "s_19_2".zz not in (SELECT * FROM( 
		SELECT CAST ( "s_29_4_t1"."O" AS INTEGER) AS "zz" 
		FROM DB.DBA.RDF_QUAD AS "s_29_4_t1" 
		WHERE "s_29_4_t1"."G" = __i2idn ( __bft( \'http://EventFramework/Stages\' , 1)) 
			AND "s_29_4_t1"."P" = __i2idn ( __bft(\'http://EventFramework/Schema/%s\', 1)) 
			OPTION (QUIETCAST) ) AS "s_29_5" )
ORDER BY "zz"
	OPTION (QUIETCAST)
) AS jj', CAST(offset AS VARCHAR), property, property), state, msg, vector(), 1, descs, rows);

return CAST(rows[0][0] as INTEGER);
cont:
return null;
};

create procedure EVENT_FRAMEWORK_GET_NEXT_IRICLASS_ID
(
in class VARCHAR,
in offset INTEGER := 0
)
RETURNS INTEGER
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_NEXT_IRICLASS_ID: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
whenever sqlstate '22003' goto cont;

DECLARE state, msg, descs, rows, exarray any;
EXEC(sprintf(
'SELECT newID
FROM(
SELECT TOP %s,1 "zz" AS newID
FROM(
SELECT "s_17_3"."zz" AS "zz" 
FROM ( 
	SELECT ( CAST ( __ro2sq ( "s_17_2_t1"."O" ) AS INTEGER) + 1) AS "zz" 
	FROM DB.DBA.RDF_QUAD AS "s_17_2_t0" INNER JOIN DB.DBA.RDF_QUAD AS "s_17_2_t1" ON ( "s_17_2_t0"."S" = "s_17_2_t1"."S" ) 
	WHERE "s_17_2_t0"."G" = __i2idn ( __bft( \'http://EventFramework/Stages\' , 1)) 
		AND "s_17_2_t0"."P" = __i2idn ( __bft( \'http://www.w3.org/1999/02/22-rdf-syntax-ns#type\' , 1)) 
		AND "s_17_2_t0"."O" = __i2idn ( __bft( \'http://EventFramework/Schema/%s\' , 1)) 
		AND "s_17_2_t1"."G" = __i2idn ( __bft( \'http://EventFramework/Stages\' , 1)) 
		AND strcontains ( lower ( __bft (__id2in ( "s_17_2_t1"."P" ), 2)), \'id\' ) 
	OPTION (QUIETCAST) ) AS "s_17_3" 

WHERE "s_17_3"."zz" not in ( 
	SELECT "s_27_7"."zz" AS "zz"
	FROM ( 
		SELECT CAST ( __ro2sq ( "s_27_6_t3"."O" ) AS INTEGER) AS "zz" 
		FROM DB.DBA.RDF_QUAD AS "s_27_6_t2" INNER JOIN DB.DBA.RDF_QUAD AS "s_27_6_t3" ON ( "s_27_6_t2"."S" = "s_27_6_t3"."S" ) 
		WHERE "s_27_6_t2"."G" = __i2idn ( __bft( \'http://EventFramework/Stages\' , 1)) 
			AND "s_27_6_t2"."P" = __i2idn ( __bft( \'http://www.w3.org/1999/02/22-rdf-syntax-ns#type\' , 1)) 
			AND "s_27_6_t2"."O" = __i2idn ( __bft( \'http://EventFramework/Schema/%s\' , 1)) 
			AND "s_27_6_t3"."G" = __i2idn ( __bft( \'http://EventFramework/Stages\' , 1)) 
			AND strcontains ( lower ( __bft (__id2in ( "s_27_6_t3"."P" ), 2)), \'id\' ) 
		OPTION (QUIETCAST) ) AS "s_27_7" 
	OPTION (QUIETCAST) ) 
ORDER BY "zz"
OPTION (QUIETCAST)) as jj) as dd', CAST(offset AS VARCHAR), class, class), state, msg, vector(), 1, descs, rows);
return CAST(rows[0][0] as INTEGER);
cont:
return 1;
};

create procedure EVENT_FRAMEWORK_INSERT_NEXT_INSTANCE
(
in instanceName VARCHAR,
in idProperty VARCHAR
)
RETURNS INTEGER --the id
{
DECLARE state, msg, descs, rows, properties, classes any;

EXEC('sparql
SELECT DISTINCT (bif:subseq(?s, bif:strrchr(?s, \'/\')+1) as ?Class)
FROM <http://EventFramework/Schema>
WHERE {?s ?p ?o. ?s a owl:Class
FILTER(bif:strcontains(?s, \'http\'))
}', state, msg, vector(), 1000, descs, rows);

classes := make_array(LENGTH(rows), 'any');
declare i, ind integer;
for(i:=0;i < LENGTH(rows);i:=i+1)
{
classes[i] := CAST(rows[i][0] AS VARCHAR);
}

EXEC('sparql
SELECT DISTINCT (bif:subseq(?s, bif:strrchr(?s, \'/\')+1) as ?Prop)
FROM <http://EventFramework/Schema>
WHERE {?s ?p ?o. ?s a owl:DatatypeProperty
FILTER(bif:strcontains(?s, \'http\'))
}', state, msg, vector(), 1000, descs, rows);

properties := make_array(LENGTH(rows), 'any');
declare i, ind integer;
for(i:=0;i < LENGTH(rows);i:=i+1)
{
properties[i] := CAST(rows[i][0] AS VARCHAR);
}

if(position(instanceName, classes) IS NOT NULL AND position(idProperty, properties) IS NOT NULL)
{
ind := EVENT_FRAMEWORK_GET_NEXT_IRICLASS_ID(instanceName);
EXEC(sprintf('sparql
PREFIX shma: <http://EventFramework/Schema/>
PREFIX link: <http://EventFramework/LinkedData/>
PREFIX : <http://EventFramework/Stages/>

INSERT INTO <http://EventFramework/Stages>
{
:%s%s     a      shma:%s;
shma:%s   "%s"^^xsd:integer.
}', instanceName, CAST(ind AS VARCHAR), instanceName, idProperty, CAST(ind AS VARCHAR) ), state, msg, vector(), 1000, descs, rows);

return ind;
if(strcontains(CAST(rows[0][0] as VARCHAR), 'triples -- done'))
{
return ind;
}
return null;
}
signal('wrongIriClass', 'no such IriClass');

};

create procedure EVENT_FRAMEWORK_EVALUATE_SET 
(
in instanceId INTEGER,	
in setUri VARCHAR,
in started DATETIME,
in ended DATETIME := null
)
RETURNS INTEGER
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_EVALUATE_SET: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
DECLARE operat, inId VARCHAR;
DECLARE state, msg, descs, rows, query any;

--if(ended IS NULL)
--ended := UTC_TIME();

EXEC(sprintf('sparql
PREFIX shma: <http://EventFramework/Schema/>
PREFIX link: <http://EventFramework/LinkedData/>
PREFIX : <http://EventFramework/Stages/>

Select DISTINCT (COALESCE(?dist, 1) as ?Distance) (COALESCE(?ur, ?set) as ?ParenSet) 
(?ev as ?Event) (COALESCE(?op, <http://EventFramework/Schema/Or>) AS ?Operator) (?min as ?minRecurrence) (?max as ?maxRecurrence)
FROM <http://EventFramework/Stages>
WHERE
{
{
SELECT DISTINCT  (Max(?distance) as ?dist)  ?path ?ur ?ev ?op ?set ?min ?max
FROM <http://EventFramework/Stages>
WHERE {
{
{ SELECT  ?set ((?set) as ?ur) ?op ?ev ?event ?min ?max

	WHERE { {?set (shma:operands/shma:initialOr) ?event.
                 ?set (shma:operands/shma:initialOr) ?ev.

}
OPTIONAL
{
?set shma:operator ?op.
}
OPTIONAL
{
?set shma:minRecurrence ?min.
?set shma:maxRecurrence  ?max.
}
} 
}
	OPTION ( TRANSITIVE, T_DIRECTION 3, t_in(?set), t_out(?event), t_min (1), t_max (1), 
t_step (\'step_no\') as ?distance, t_step (\'path_id\') as ?path).  
}
UNION
{
?set shma:initialOr ?ev.
}
FILTER(?set = <%s>)
}
}

}
', setUri), state, msg, vector (), 1000, descs, rows);

--signal(cast(setUri as varchar), cast(rows[0][3] as varchar));
if (internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows)>0)
{
declare i,summe integer;
summe :=0;
i :=0;
operat:= subseq(rows[i][3], strrchr(rows[i][3], 'Schema')+7);

for(i:=0;i<LENGTH(rows);i:=i+1)
{
query := null;

--if(strcontains(rows[i][1], 'MultiEventSet'))
if(rows[i][4] IS NOT NULL AND rows[i][5] IS NOT NULL)  --MultiEventSet -> max and min Recurrence!
{
declare minRec, maxRec integer;
minRec := CAST(rows[i][4] as INTEGER);
maxRec := CAST(rows[i][5] as INTEGER);

if(strcontains(rows[i][2], 'ComplexEvent'))
{
	inID := subseq(rows[i][2], strrchr(rows[i][2], 'ComplexEvent')+12);
	EXEC(sprintf('SELECT Recurrences FROM EventFrameworkAwaitingEvent WHERE SourceCE = %d AND CEID = %s', 
		instanceId, inID), state, msg, vector (), 1, descs, query);
}
else if(strcontains(rows[i][2], 'AtomicEvent'))
{
	inID := subseq(rows[i][2], strrchr(rows[i][2], 'AtomicEvent')+11);
	EXEC(sprintf('SELECT Recurrences FROM EventFrameworkAwaitingEvent WHERE SourceCE = %d AND TriggerID = %s', 
		instanceId, inID), state, msg, vector (), 1, descs, query);
}
--signal(cast(inID as varchar), cast(started as varchar));
	if(internal_type_name(internal_type(query)) <> 'INTEGER' AND LENGTH(query)>0)
	{
		if(CAST(query[0][0] as INTEGER) >= minRec AND CAST(query[0][0] as INTEGER) <= maxRec)
			summe := summe +1;
	}
}
else
{
if(strcontains(rows[i][2], 'ComplexEvent'))
{
	inID := subseq(rows[i][2], strrchr(rows[i][2], 'ComplexEvent')+12);
	EXEC(sprintf('SELECT Recurrences FROM EventFrameworkAwaitingEvent WHERE SourceCE = %d AND CEID = %s', 
		instanceId, inID), state, msg, vector (), 1, descs, query);
	if(internal_type_name(internal_type(query)) <> 'INTEGER' AND LENGTH(query)>0)
	{
	if(CAST(query[0][0] as INTEGER) > 0)
		summe := summe +1;
	}
}
else if(strcontains(rows[i][2], 'AtomicEvent'))
{
	inID := subseq(rows[i][2], strrchr(rows[i][2], 'AtomicEvent')+11);
	EXEC(sprintf('SELECT Recurrences FROM EventFrameworkAwaitingEvent WHERE SourceCE = %d AND TriggerID = %s', 
		instanceId, inID), state, msg, vector (), 1, descs, query);
--signal(cast(instanceId as varchar), cast(inID as varchar));
	if(internal_type_name(internal_type(query)) <> 'INTEGER' AND LENGTH(query)>0)
	{

	if(CAST(query[0][0] as INTEGER) > 0)
		summe := summe +1;
	}
}
else -- if(strcontains(rows[i][2], 'EventSet') OR strcontains(rows[i][2], 'MultiEventSet'))
	{
	summe := summe + EVENT_FRAMEWORK_EVALUATE_SET(instanceId, rows[i][2], started);
	}
}
}

if(operat = 'And')
{
	if(summe = Length(rows))
		return 1;
	else
		return 0;
}
else if(operat = 'Or')
{
	if(summe > 0)
		return 1;
	else
		return 0;
}
else if(operat = 'Xor')
{
	if(summe = 1)
		return 1;
	else
		return 0;
}
else if(operat = 'Not')
{
	if(summe = 0)
		return 1;
	else
		return 0;
}
}
else
    return 1;  --blank set!
	
err:
signal('evaluateError', 'set could not be evaluated');
};

create procedure EVENT_FRAMEWORK_GET_EVENTS_FROM_STAGE
(
in stageUri VARCHAR,
in minDist INTEGER := 1,
in maxDist INTEGER := 0,  --transitive distance
in showResultSet SMALLINT :=0
)
returns ANY ARRAY
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_EVENTS_FROM_STAGE: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
DECLARE state, msg, descs, rows, minn, maxx, initialSet, exarray any;
if(maxDist < 1)
maxx := '';
else
maxx := ' t_max (' || CAST(maxDist as varchar) || '),';

if(minDist < 1)
minn := '1';
else
minn := CAST(minDist as varchar);

initialSet := (SELECT seet FROM (sparql
PREFIX shma: <http://EventFramework/Schema/>
PREFIX link: <http://EventFramework/LinkedData/>
PREFIX : <http://EventFramework/Stages/>

SELECT DISTINCT  ?stage ?seet
FROM <http://EventFramework/Stages>
WHERE {
?stage shma:initialEventSet ?seet

FILTER(str(?stage) = ?:stageUri)
})as dd);


EXEC(sprintf('
sparql
PREFIX shma: <http://EventFramework/Schema/>
PREFIX link: <http://EventFramework/LinkedData/>
PREFIX : <http://EventFramework/Stages/>

Select DISTINCT (COALESCE(?dist, 1) as ?Distance) (COALESCE(?ur, ?set) as ?ParentSet) (?ev as ?Event) (COALESCE(?op, <http://EventFramework/Schema/Or>) AS ?Operator) (?min as ?minRecurrence) (?max as ?maxRecurrence) (?init as ?initialEventSet) 
FROM <http://EventFramework/Stages>
WHERE
{
{
SELECT DISTINCT  (Max(?distance) as ?dist)  ?path ?ur ?ev ?op ?min ?max ?set ?init
FROM <http://EventFramework/Stages>
WHERE {
{
{ SELECT  ?set ((?set) as ?ur) ?op ?ev ?event ?min ?max ?init

	WHERE { {?set (shma:operands/shma:initialOr) ?event.
                 ?set shma:operands ?init.
                 ?init shma:initialOr ?ev.

}
OPTIONAL
{
?set shma:operator ?op.
}
OPTIONAL
{
?set shma:minRecurrence ?min.
?set shma:maxRecurrence  ?max.
}
} 
} OPTION ( TRANSITIVE, T_NO_CYCLES, t_in(?set), t_out(?event), t_min (%s), %s t_step (\'step_no\') as ?distance, t_step (\'path_id\') as ?path). 
}
UNION
{
?set shma:initialOr ?ev.
}

FILTER(?set = <%s>)
}
}
#FILTER NOT EXISTS{?ev a shma:EventSet}
}
ORDER BY ?dist ?ur
', CAST(minn AS VARCHAR), CAST(maxx AS VARCHAR), initialSet), state, msg, vector (), 1000, descs, rows);


if(internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows)>0)
{


declare dist, parent, initialEventSet, event, operator, minRec, maxRec VARCHAR;

if(showResultSet)
EXEC_RESULT_NAMES(descs[0]);--dist, parent, event, operator, minRec, maxRec, initialEventSet);

exarray := make_array(LENGTH(rows), 'any');
declare i,j integer;
for(i:=0;i<LENGTH(rows);i:=i+1)
{
exarray[i] :=make_array(LENGTH(rows[0]), 'any');

for(j:=0;j<LENGTH(rows[0]);j:=j+1){
exarray[i][j] := CAST(rows[i][j] as varchar);
}
if(showResultSet)
EXEC_RESULT(exarray[i]);--[0], exarray[i][1], exarray[i][2], exarray[i][3], exarray[i][4], exarray[i][5], exarray[i][6]);
}
return exarray;	
}
return null;
};

create procedure EVENT_FRAMEWORK_GET_SETS_OF_STAGE
(
in initialStage varchar,
in showResultSet SMALLINT := 0
)
{
DECLARE state, msg, descs, rows any;
EXEC(sprintf('
sparql 
PREFIX shma: <http://EventFramework/Schema/>
PREFIX : <http://EventFramework/Stages/>

SELECT DISTINCT (MAX(?distance) as ?dist) ?stage (?set as ?initialSet)
FROM <http://EventFramework/Stages> 
WHERE{ 
{SELECT ?start ?stage 
WHERE {?start shma:transition ?stage.
}
} OPTION ( TRANSITIVE, t_in(?start), T_NO_CYCLES, t_out(?stage), t_min (0), t_step (\'step_no\') as ?distance, t_step (\'path_id\') as ?path).
OPTIONAL
{
?stage shma:initialEventSet ?set.
}
FILTER(?start = <%s>)}
GROUP BY ?stage ?set
ORDER BY DESC(?dist)
', initialStage), state, msg, vector (), 1000, descs, rows);

if(internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows)>0)
{
if(showResultSet)
{
declare dist, stage, initialSet VARCHAR;
EXEC_RESULT_NAMES(descs[0]);--(dist, stage, initialSet);
for(msg:=0;msg < LENGTH(rows);msg:=msg+1)
{
EXEC_RESULT(rows[msg]);--CAST(rows[msg][0] as VARCHAR), CAST(rows[msg][1] as VARCHAR), CAST(rows[msg][2] as VARCHAR));
}
}
return rows;
}
return NULL;
};

create procedure EVENT_FRAMEWORK_GET_DURATION_OF_STAGE
(
in initialStage VARCHAR,
in stageNr INTEGER, --0 = initialStage
in showResultSet SMALLINT := 0,
in getMaxStage SMALLINT := 0 --if 1 -> returns always value of max. stage nr!
)
returns ANY ARRAY
{
DECLARE state, msg, descs, rows, maxStage any;

rows := EVENT_FRAMEWORK_GET_SETS_OF_STAGE(initialStage);
if(rows IS NOT NULL)
{
maxStage:= CAST(rows[0][0] as INTEGER);
if(maxStage >= 0)
{
	if(stageNr > maxStage)
	{
		if(getMaxStage)
			stageNr := maxStage;
		else
			return null;
	}
		

EXEC(sprintf('
sparql PREFIX shma: <http://EventFramework/Schema/>
	PREFIX : <http://EventFramework/Stages/>
SELECT DISTINCT ?stage ?set (COALESCE(?end, 0) as ?wait) (COALESCE(?duration, 0) as ?duration) 
FROM <http://EventFramework/Stages> 
WHERE{ 

{SELECT ?start ?stage
WHERE {?start shma:transition ?stage.}
} OPTION ( TRANSITIVE, T_DIRECTION 3, t_in(?start), t_out(?stage), t_min (%d), t_max (%d), t_step (\'step_no\') as ?distance, t_step (\'path_id\') as ?path).
?stage shma:initialEventSet ?set.
OPTIONAL
{
?stage shma:timeRestriction / shma:waitTillEnd ?end.
?stage shma:timeRestriction / shma:timeDuration ?duration
}
FILTER(?start = <%s>)}
', stageNr, stageNr, initialStage), state, msg, vector (), 1, descs, rows);
if(showResultSet)
{
if(internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows)>0)
{
declare stage, initialSet, waitTillEnd, duration VARCHAR;
EXEC_RESULT_NAMES(descs[0]);--stage, initialSet, waitTillEnd, duration);
for(msg:=0;msg < LENGTH(rows);msg:=msg+1)
{
EXEC_RESULT(rows[msg]);
--RESULT(CAST(rows[msg][0] as VARCHAR), CAST(rows[msg][1] as VARCHAR), CAST(rows[msg][2] as VARCHAR), CAST(rows[msg][3] as VARCHAR));
}
}
}
return rows;
}
}
return null;
};

 create procedure EVENT_FRAMEWORK_UPDATE_STAGE
(
in instanceId INTEGER,		--ce event instance
in started DATETIME			--stage was entered at
)
{
--pl_debug+
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_UPDATE_STAGE: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
DECLARE state, msg, descs, rows, query, res, aq, checker any;
EXEC(sprintf('SELECT FirstStageUri, CurrentStage FROM EventFrameworkComplexEventInstances WHERE EventID = %s', CAST(instanceId AS VARCHAR)), state, msg, vector (), 1, descs, query);

if (internal_type_name(internal_type(query)) = 'INTEGER' OR LENGTH(query)<1)
{
	goto err;
}

rows:=EVENT_FRAMEWORK_GET_DURATION_OF_STAGE(CAST(query[0][0] as VARCHAR), CAST(query[0][1] as INTEGER));

if(internal_type_name(internal_type(rows)) = 'INTEGER' OR LENGTH(rows) < 1)
goto err;

checker := EVENT_FRAMEWORK_EVALUATE_SET (instanceId, CAST(rows[0][1] AS VARCHAR), started);
--signal(CAST(rows[0][1] AS VARCHAR), cast(started as varchar));
if(checker = 1)
{ 
checker := EVENT_FRAMEWORK_CHECK_CONDITIONS(instanceId, CAST(query[0][1] as INTEGER));
}
if(checker = 1)
{
if(not CAST(rows[0][2] AS INTEGER) 	OR CAST(rows[0][3] AS INTEGER) = 0  --waitTillEnd ?  OR no time restriction?
OR DATEDIFF(started, UTC_TIME()) > CAST(rows[0][3] AS INTEGER)) 				--or duration of stage < elapsed time 
{
--signal(CAST(query[0][1] as VARCHAR), CAST(query[0][0] as VARCHAR), CAST(query[0][1] as VARCHAR));
EXEC(sprintf('DELETE FROM EventFrameworkAwaitingEvent WHERE SourceCE = %d', instanceId));
rows:=EVENT_FRAMEWORK_GET_DURATION_OF_STAGE(CAST(query[0][0] as VARCHAR), CAST(query[0][1] as INTEGER)+1);


if(rows IS NOT NULL AND internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows) > 0)  -- next stage is initialized
{

EXEC(sprintf('UPDATE EventFrameworkComplexEventInstances SET CurrentStage = %d WHERE EventID = %d', CAST(query[0][1] as INTEGER)+1, instanceId));
EVENT_FRAMEWORK_INSERT_AWAITING_EVENTS(instanceId, CAST(query[0][0] as varchar), CAST(query[0][1] as INTEGER)+1);
}

if(rows IS NULL)  -- last stage finish event
{
--signal(DUMP_VEC(query), 'hudfugd');
EVENT_FRAMEWORK_INSERT_NEW_EVENT( 1, 'at stage :' || CAST(query[0][1] as VARCHAR), '', UTC_TIME(), '', instanceId);
UPDATE EventFrameworkComplexEventInstances SET Finished = UTC_TIME() WHERE EventID = instanceId;
EVENT_FRAMEWORK_TAKE_ACTIONS(instanceId);
}
}
}
return;
err:
signal('undefinedError', 'an error occured');
};

create procedure EVENT_FRAMEWORK_UPDATE_EVENTS
(
in eventType SMALLINT,  --0 = atomic, 1 = complex	
in id INTEGER,
in eventID INTEGER,
in occurence DATETIME
)
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_UPDATE_EVENTS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
DECLARE state, msg, descs, rows, query any;
if(not eventType)
{
/* EXEC(sprintf('SELECT * FROM EventFrameworkTriggers WHERE TriggerID = %d', id), state, msg, vector (), 1, descs, query);
if(internal_type_name(internal_type(query)) = 'INTEGER' OR LENGTH(query) < 1)
return; */

EXEC(sprintf('SELECT DISTINCT (SELECT CEID FROM EventFrameworkComplexEventInstances WHERE EventID = inn.SourceCe) AS CEinst, SourceCe, Started ,
	(SELECT CurrentStage FROM EventFrameworkComplexEventInstances WHERE EventID = inn.SourceCe) as StageNr
	FROM EventFrameworkAwaitingEvent as inn WHERE TriggerID = %d AND CASE WHEN Until IS NULL THEN 1 WHEN 0 < DATEDIFF(\'second\', stringdate(\'%s\'), Until) THEN 1 ELSE 0 END', 
	id, CAST(occurence AS VARCHAR)), state, msg, vector(), 1000, descs, rows);

--SIGNAL(CAST(msg as VARCHAR), DUMP_VEC(rows));
}

if(eventType)
{
/* EXEC(sprintf('SELECT * FROM EventFrameworkComplexEvents WHERE CEID = %d', id), state, msg, vector (), 1, descs, query);
if(internal_type_name(internal_type(query)) = 'INTEGER' OR LENGTH(query) < 1)
    return ; */
	
EXEC(sprintf('SELECT DISTINCT (SELECT CEID FROM EventFrameworkComplexEventInstances WHERE EventID = inn.SourceCe) AS CEinst, SourceCe, Started ,
	(SELECT CurrentStage FROM EventFrameworkComplexEventInstances WHERE EventID = inn.SourceCe) as StageNr
	FROM EventFrameworkAwaitingEvent as inn WHERE CEID = %d AND CASE WHEN Until IS NULL THEN 1 WHEN 0 < DATEDIFF(\'second\', stringdate(\'%s\'), Until) THEN 1 ELSE 0 END', 
	id, CAST(occurence AS VARCHAR)), state, msg, vector (), 1000, descs, rows);
}

if(not eventType)
{
EXEC(sprintf('UPDATE EventFrameworkAwaitingEvent SET "Recurrences" = ("Recurrences" +1) WHERE TriggerID = %d
	AND CASE WHEN Started IS NULL THEN 0 WHEN 0 > DATEDIFF(\'second\', UTC_TIME(), Started) THEN 1 ELSE 0 END', id));
}
if(eventType)
{
EXEC(sprintf('UPDATE EventFrameworkAwaitingEvent SET "Recurrences" = ("Recurrences" +1) WHERE CEID = %d
	AND CASE WHEN Started IS NULL THEN 0 WHEN 0 > DATEDIFF(\'second\', UTC_TIME(), Started) THEN 1 ELSE 0 END', id));
}
if(internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows) > 0)
{
	declare i integer;
	for(i:=0;i<LENGTH(rows);i:=i+1)
	{
		if(0 > DATEDIFF('second', UTC_TIME(), CAST(rows[i][2] as DATETIME)))
		{
			INSERT INTO EventFrameworkEventToInstanceMapping VALUES(eventID, CAST(rows[i][1] AS INTEGER), CAST(rows[i][3] as INTEGER), CAST(rows[i][0] AS INTEGER), occurence);
--SIGNAL(CAST(rows[i][0] as VARCHAR), DUMP_VEC(rows[i][1]), DUMP_VEC(LENGTH(rows)));
			EVENT_FRAMEWORK_UPDATE_STAGE(rows[i][1], rows[i][2]);	
		}
	}
}
commit work;
return;
err:
rollback work;
signal('unknownError', 'an error occured');
};


create procedure EVENT_FRAMEWORK_INSERT_AWAITING_EVENTS
(
in sourceEventId  INTEGER,
in stageUri varchar,
in currentStage INTEGER,
in startsAt DATETIME := null
)
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_AWAITING_EVENTS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
DECLARE state, msg, descs, rows, query, res any;
rows:= EVENT_FRAMEWORK_GET_DURATION_OF_STAGE(stageUri, currentStage);

if(startsAt IS NULL)
startsAt := UTC_TIME();

if(internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows) > 0)
{
res := EVENT_FRAMEWORK_GET_EVENTS_FROM_STAGE(CAST(rows[0][0] as varchar), 1);

if(internal_type_name(internal_type(res)) <> 'INTEGER' AND LENGTH(res) > 0)
{
declare i, sec integer;
if(strcontains(CAST(rows[0][3] as varchar), 'T'))
{
sec := EVENT_FRAMEWORK_GET_DURATION_SECONDS(rows[0][3]);
}
else if(CAST(rows[0][3] as varchar) = '0')
sec := 0;
else
sec := CAST(rows[0][3] as integer);

for(i:=0;i<LENGTH(res);i:=i+1)
{
	if(strcontains(res[i][2], 'ComplexEvent'))
	{
	INSERT INTO EventFrameworkAwaitingEvent ("CEID","SourceCE","Started","Until")
	VALUES(CAST(subseq(res[i][2], strrchr(res[i][2], 'ComplexEvent') +12 ) AS INTEGER), sourceEventId, startsAt, CASE WHEN sec = 0 THEN null ELSE dateadd('second', sec, UTC_TIME()) END);
	}
	else if(strcontains(res[i][2], 'AtomicEvent'))
	{
	INSERT INTO EventFrameworkAwaitingEvent ("TriggerID","SourceCE","Started","Until")
	VALUES(CAST(subseq(res[i][2], strrchr(res[i][2], 'AtomicEvent') +11 ) AS INTEGER), sourceEventId, startsAt, CASE WHEN sec = 0 THEN null ELSE dateadd('second', sec, UTC_TIME()) END);
	}
}
commit work;
}
}
};

 create procedure EVENT_FRAMEWORK_TAKE_ACTION
(
in eventInstanceID INTEGER,
in actionID INTEGER
)
{
declare zw, state, msg, descs, params, exarray, j, controlID, endpoint any;
zw:=(SELECT Condition FROM EventFrameworkActions WHERE "ActionID" = actionID);
if(zw = 0)
{
EXEC(sprintf('SELECT DISTINCT ParamNr,StaticValueType,StaticValue,ConditionQuery,EventValueMap FROM EventFrameworkParameterMappings WHERE StageNr = (SELECT MAX(StageNr) 
	FROM EventFrameworkParameterMappings WHERE CEID = (SELECT CEID From EventFrameworkComplexEventInstances WHERE EventID = %d)) AND CEID = (SELECT CEID From EventFrameworkComplexEventInstances 
	WHERE EventID = %d AND ActionID = %d) ORDER BY ParamNr', eventInstanceID, eventInstanceID, actionID), state, msg, vector (), 1000, descs, params);
	
--SIGNAL(DUMP_VEC(params), 'hhhh');

if(internal_type_name(internal_type(params)) <> 'INTEGER' AND LENGTH(params) > 0)
{
exarray := make_array(LENGTH(params)*2, 'any');
for(j:=0;j<LENGTH(params);j:=j+1)
{
if(params[j][3] > 0)
{
exarray[j*2] := 'query';
exarray[j*2+1]:=Cast(params[j][3] as varchar);
}
else if(params[j][4] IS NOT NULL)
{
exarray[j*2] := 'valueMap';
	exarray[j*2+1] := cast(params[j][4] as varchar);
}
else
{
	exarray[j*2] := cast(params[j][1] as varchar);
	exarray[j*2+1] := cast(params[j][2] as varchar);
}
}
}
controlID := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'controlID');
endpoint := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'virtuosoExtentionPoint');
EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP_11(vector('controlID', controlID,'actionID', actionID, 'eventInstance', eventInstanceID, 'parameters', exarray ), endpoint, 'CallForAction', 'http://tempuri.org/', '"http://tempuri.org/IVirtuosoExtentionService/CallForAction"');
}
};

create procedure EVENT_FRAMEWORK_TAKE_ACTIONS
(
in eventInstanceID INTEGER
)
{
DECLARE state, msg, descs, rows, query, params, exarray, zw, err, res, aq any;
DECLARE eventUri, firstStage VARCHAR;
EXEC(sprintf('SELECT DISTINCT ActionNr, ActionID FROM EventFrameworkParameterMappings WHERE StageNr = (SELECT MAX(StageNr) FROM EventFrameworkParameterMappings WHERE 
	CEID = (SELECT CEID From EventFrameworkComplexEventInstances WHERE EventID = %d)) AND CEID = (SELECT CEID From EventFrameworkComplexEventInstances WHERE EventID = %d) 
	ORDER BY ActionNr', eventInstanceID, eventInstanceID), state, msg, vector (), 1000, descs, query);

if(internal_type_name(internal_type(query)) <> 'INTEGER' AND LENGTH(query) > 0)
{
declare i,j,actionID,controlID integer;
aq := async_queue (20);
for(i:=0;i<LENGTH(query);i:=i+1)
{
aq_request (aq, 'EVENT_FRAMEWORK_TAKE_ACTION', vector (eventInstanceID, CAST(query[i][1] as INTEGER)));
}
}} ;

 create procedure EVENT_FRAMEWORK_CHECK_CONDITIONS
(
in eventInstanceID INTEGER,
in stage INTEGER,
in assumeTruth SMALLINT :=1	--1 = if condition-check return NULL (condition could not be checked...) we assume truth
)
RETURNS INTEGER
{
DECLARE state, msg, descs, query, res, err, results, aq any;
DECLARE eventUri, firstStage VARCHAR;
declare i,j,actionID,controlID, summ, zw integer;
summ :=0;

EXEC(sprintf('SELECT DISTINCT ActionNr, ActionID FROM EventFrameworkParameterMappings WHERE StageNr = %d AND CEID = (SELECT CEID From EventFrameworkComplexEventInstances WHERE EventID = %d) ORDER BY ActionNr',stage, eventInstanceID), state, msg, vector (), 1000, descs, query);

if(internal_type_name(internal_type(query)) <> 'INTEGER' AND LENGTH(query) > 0)
{
results := make_array(LENGTH(query), 'any');
aq := async_queue (20);

for(i:=0;i<LENGTH(query);i:=i+1)
{
results[i] := aq_request (aq, 'EVENT_FRAMEWORK_CHECK_INNER_CONDITIONS', vector (eventInstanceID, CAST(query[i][1] as INTEGER), assumeTruth));
}
commit work;
for(i:=0;i<LENGTH(query);i:=i+1)
{
summ := summ + aq_wait (aq, results[i], 1, err);
}

if(summ - LENGTH(query) +1)
	return 1;
else
	return 0;
}
else
	return 1;
};

 create procedure EVENT_FRAMEWORK_CHECK_INNER_CONDITIONS
(
in eventInstanceID INTEGER,
in actionID INTEGER,
in assumeTruth SMALLINT :=1	--1 = if condition-check return NULL (condition could not be checked...) we assume truth
)
RETURNS INTEGER
{
	declare zw, exarray, state, msg, descs, query, params, controlID, endpoint, j any;
zw:=(SELECT Condition FROM EventFrameworkActions WHERE "ActionID" = actionID);
if(zw)
{
EXEC(sprintf('SELECT ParamNr,StaticValueType,StaticValue,ConditionQuery,EventValueMap FROM EventFrameworkParameterMappings 
	WHERE CEID = (SELECT CEID From EventFrameworkComplexEventInstances WHERE EventID = %d) 
AND ActionID = %d
	ORDER BY ParamNr', eventInstanceID, actionID), state, msg, vector (), 1000, descs, params);

if(internal_type_name(internal_type(params)) <> 'INTEGER' AND LENGTH(params) > 0)
{
	exarray := make_array(LENGTH(params)*2, 'any');
	for(j:=0;j<LENGTH(params);j:=j+1)
	{
		if(params[j][3] > 0)
		{
			exarray[j*2] := 'query';
			exarray[j*2+1]:=Cast(params[j][3] as varchar);
		}
		else if(params[j][4] IS NOT NULL)
		{
			exarray[j*2] := 'valueMap';
			exarray[j*2+1] := cast(params[j][4] as varchar);
		}
		else
		{
			exarray[j*2] := cast(params[j][1] as varchar);
			exarray[j*2+1] := cast(params[j][2] as varchar);
		}
	}
	controlID := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'controlID');
	endpoint := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'virtuosoExtentionPoint');
	zw :=  EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP_11(vector('controlID', controlID,'actionID', actionID, 'eventInstance', eventInstanceID, 'parameters', exarray ), endpoint, 'CheckCondition', 'http://tempuri.org/', '"http://tempuri.org/IVirtuosoExtentionService/CheckCondition"');
	if(CAST(zw[1][1] as INTEGER) < 0)
	{
	if(assumeTruth)
		return 1;
	else
		return 0;
	}
	else
		return CAST(zw[1][1] as INTEGER);
}
}
else
	return 1;
};

create procedure EVENT_FRAMEWORK_INSERT_EVENT_INSTANCE
(
in initStage VARCHAR := null,
in ceide INTEGER :=0,
in startAtStage INTEGER := 0,
in startWhen DATETIME := null,
in replace INTEGER := 0  --delete any instance with the same ceid
)
{
if(startWhen IS NULL)
startWhen := UTC_TIME();

if(ceide = 0 AND initStage IS NULL)
signal('NeedValuesToInsert', 'Neither CEID or InitialStage was provided!');

declare state, msg, descs, rows any;
if(ceide = 0)
ceide := (SELECT CEID FROM EventFrameworkComplexEvents WHERE InitialStage = initStage);

if(initStage IS NULL AND ceide IS NOT NULL)
initStage := (SELECT InitialStage FROM EventFrameworkComplexEvents WHERE CEID = ceide);

if(initStage IS NULL OR ceide IS NULL)
signal('wrongInput', 'complex event was not found');

if(replace)
DELETE FROM EventFrameworkComplexEventInstances WHERE CEID = ceide;

INSERT INTO EventFrameworkComplexEventInstances
( "CEID", "EventUri","FirstStageUri","CurrentStage","Started") 
VALUES
(
ceide,
'http://EventFramework/LinkedData/ComplexEvent' || CAST(ceide AS VARCHAR),
initStage,
startAtStage,
startWhen
);
};

  create procedure EVENT_FRAMEWORK_CHECK_TRIGGER_CONDITION
(
in tableNa VARCHAR ,
in occ DATETIME,
in triggerNa VARCHAR,
in triggerCondition VARCHAR,
in params ANY := null,
in rowVector ANY := null,
in onlyNewRows SMALLINT := 1 --1 = if row already exists do not fire, 0 = fire on every try
)
{

declare state, msg, descs, rows, endpoint, active, paramPos, paraTemp any;
active := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'active');
if(not active)
{
set triggers off;
return 0;
}

if(params IS NULL)
params := vector();	
if(regexp_match('[[:alpha:]]+', params) IS NULL)
{
paraTemp := params;
params := vector();	
paramPos := 0;
while(regexp_instr(paraTemp, '[[:digit:]]+',paramPos+1) IS NOT NULL)
{
declare paramNr, zw varchar;
paramNr := regexp_substr('[[:digit:]]+', subseq(paraTemp, paramPos),0);
if (internal_type_name(internal_type(rowVector[CAST(paramNr as INTEGER)])) = 'VARCHAR')
params := vector_concat(params, vector('\\\'' || CAST(rowVector[CAST(paramNr as INTEGER)] AS VARCHAR) || '\\\''));

else
params := vector_concat(params, vector(rowVector[CAST(paramNr as INTEGER)]));
paramPos := paramPos + LENGTH(paramNr) +1;
}
}
declare instance integer;

instance := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'dbInstance');	
endpoint := (SELECT Value FROM EventFrameworkConstants WHERE "Key" = 'endpointAddress');

EXEC(triggerCondition , state, msg, params, 1, descs, rows);	
if (internal_type_name(internal_type(rows)) <> 'INTEGER' AND LENGTH(rows)>0)
{
	if(internal_type_name(internal_type(rows[0][0])) = 'INTEGER' AND rows[0][0])
	{
		--declare recurrenceTimeout integer;
		--recurrenceTimeout := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'recurrenceTimeout');
/* 					 if(onlyNewRows AND (0 = (SELECT COUNT(*) FROM EventFrameworkAtomicEvents WHERE TriggerName = t[0] AND Row[0] = rowVector[0] AND recurrenceTimeout >= DATEDIFF('second', "Occurence", occ))) 
					OR not onlyNewRows AND (0 = (SELECT COUNT(*) FROM EventFrameworkAtomicEvents WHERE TriggerName = t[0] AND Row[0] = rowVector[0])))
					{ */
		descs := EVENT_FRAMEWORK_SEND_EVENT_AS_SOAP(vector('dsInstance', instance, 'internalSource', tableNa, 'name', triggerNa, 'occ', occ, 'rowVector', rowVector), 
		endpoint, 'EVENT_FRAMEWORK_INSERT_NEW_EVENT', 'services.wsdl', '"services.wsdl/EVENT_FRAMEWORK_INSERT_NEW_EVENT"');
		if(isarray(descs) AND LENGTH(descs) > 1 AND CAST(descs[1][1] as INTEGER) = 1)
		{
			INSERT INTO EventFrameworkAtomicEvents VALUES(1, occ, triggerNa, tableNa, rowVector);
			return 1;
		}	 
		else
			INSERT INTO EventFrameworkAtomicEvents VALUES(0, occ, triggerNa, tableNa, rowVector);		
	--}
	}
}		
return 0;
} ;

 create procedure EVENT_FRAMEWORK_INSERT_TTL_DATA
(
in controlID INTEGER,
in ttl VARCHAR := null,
in graph VARCHAR := null,
in filePath VARCHAR := null,
in filePathFrom INTEGER := 0,
in filePathTo INTEGER := null
)
{
/*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_TTL_DATA: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare currentID INTEGER;
currentID:= (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'controlID');
if(currentID = controlID)
{
if(filePath IS NOT NULL)
{
ttl := file_to_string_output (filePath, filePathFrom, filePathTo);
}
TTLP_MT(ttl, '', graph);
}};

create procedure EVENT_FRAMEWORK_DELETE_STAGES_EVENTS
(
in subject VARCHAR
)
returns INTEGER
{
/*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_DELETE_STAGES_EVENTS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;*/
declare i, state, msg, descs, rows any;
EXEC(sprintf('sparql prefix shma: <http://EventFramework/Schema/> prefix link: <http://EventFramework/LinkedData/> prefix : <http://EventFramework/Stages/> 
	WITH <http://EventFramework/Stages> DELETE {?s ?p ?q. ?time ?r ?u} WHERE {{?s ?p ?q. }OPTIONAL{?time ?r ?u. ?s shma:timeRestriction ?time.} 
	FILTER (?s = <%s>)}',subject),state, msg, vector (), 1000, descs, rows);

};

create procedure EVENT_FRAMEWORK_FINISH_INSTALLATION
(
	in hostname VARCHAR := 'localhost'
)
RETURNS VARCHAR
{
if( NOT EXISTS (SELECT * FROM EventFrameworkDataSources WHERE DSName = 'CentralDB'))
{
if(hostname = 'localhost' AND tcpip_gethostbyaddr(tcpip_gethostbyname(hostname)) = 'localhost')
{
SIGNAL('hostname not detected', 'The hostname of this machine was not detected automatically. To finish this installation, please execute the following command and insert the hostname as parameter: SELECT EVENT_FRAMEWORK_FINISH_INSTALLATION(\'--replace with hostname--\')');
}
if(hostname = 'localhost')
{
INSERT INTO EventFrameworkDataSources (DSName, DSType, Description, ControlID, ProcedureEndpoint, SparqlEndpointAddress)
VALUES ('CentralDB', 'Virtuoso', 'central event-database', rnd(2000000000), 'http://' || tcpip_gethostbyaddr(tcpip_gethostbyname('localhost')) 
	|| ':' || cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort') || '/EventFrameworkProcedures', 'http://' || 
tcpip_gethostbyaddr(tcpip_gethostbyname('localhost') || ':' || cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort')) || '/sparql');
INSERT INTO EventFrameworkConstants VALUES('endpointAddress', 'http://' || tcpip_gethostbyaddr(tcpip_gethostbyname('localhost')) 
	|| ':' || cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort') || '/EventFrameworkProcedures');
}
else
{
INSERT INTO EventFrameworkDataSources (DSName, DSType, Description, ControlID, ProcedureEndpoint, SparqlEndpointAddress)
VALUES ('CentralDB', 'Virtuoso', 'central event-database', rnd(2000000000), 'http://' || hostname
	|| ':' || cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort') || '/EventFrameworkProcedures', 'http://' || 
hostname || ':' || cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort') || '/sparql');
INSERT INTO EventFrameworkConstants VALUES('endpointAddress', 'http://' || hostname
	|| ':' || cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort') || '/EventFrameworkProcedures');
}

INSERT INTO EventFrameworkUsers(Name,Pass,Created,UserAccRight,ECADefRight, Datasources) VALUES('Admin', subseq(pwd_magic_calc('Admin', 'admin'),1), UTC_TIME(), 1, 1, 'all');
INSERT INTO EventFrameworkConstants VALUES('controlID', (SELECT ControlID FROM EventFrameworkDataSources WHERE DSInstance = 1));
INSERT INTO EventFrameworkConstants VALUES('debugMode', 0);
INSERT INTO EventFrameworkConstants VALUES('active', 1);
INSERT INTO EventFrameworkConstants VALUES('dbInstance', 1);
INSERT INTO EventFrameworkConstants VALUES('recurrenceTimeout', 3);
INSERT INTO EventFrameworkConstants VALUES('virtuosoExtentionPoint', 'http://' || hostname || ':8000/VirtuosoExtentionService/VirtuosoExtentionEndpoint');
INSERT INTO EventFrameworkConstants VALUES('supportedSources', 'Virtuoso-SQL, MS Server-SQL, Oracle-SQL, Triplestore, Remoteendpoint, Other');
INSERT INTO EventFrameworkActions(Condition,CreatedBy,WsdlAddress,EndpointAddress,MethodeName,Description,ParamTypes,ParamDescr,ReturnType,ReturnDescr,DSInstance)
VALUES(0, 1, 'http://' || hostname || ':8000/VirtuosoExtentionService', 'http://' || hostname || ':8000/VirtuosoExtentionService/VirtuosoExtentionEndpoint', 'SendSmtpMail', 'send e-mail(s) as action', 'String, Int32, String, String, String, String, String, String', 'smtpHost, port, username, password, from, to, subject, body', 'Void', 'SendSmtpMailResult',1);
INSERT INTO SYS_SCHEDULED_EVENT (SE_NAME, SE_SQL, SE_START, SE_INTERVAL)
		VALUES ('EventFrameworkDeleteOldInstances', 'DELETE FROM EventFrameworkAwaitingEvent WHERE Until IS NOT NULL AND 24 < DATEDIFF(\'hour\', Until, UTC_TIME())', UTC_TIME(), 10);
INSERT INTO SYS_SCHEDULED_EVENT (SE_NAME, SE_SQL, SE_START, SE_INTERVAL)
		VALUES ('EventFrameworkDeleteDeletedInstances', 'DELETE FROM EventFrameworkComplexEventInstances as aa WHERE Finished IS NULL AND not exists (SELECT * FROM EventFrameworkAwaitingEvent as bb WHERE bb.SourceCE = aa.EventID)', UTC_TIME(), 10);	
		
if(EXISTS (SELECT * FROM EventFrameworkDataSources WHERE DSName = 'CentralDB'))
return 'The Installation was successful. Run the EventFrameworkService to activate the framework.';
else
SIGNAL('installation was unseccessful', 'An error occured. To finish this installation, \nplease execute the following command and insert \nthe hostname as parameter: \nSELECT EVENT_FRAMEWORK_FINISH_INSTALLATION(\'--replace with hostname--\')\n get your hostname with ~hostname');
}
return 'this installation is already complete!';
} ;

----------------
--client trigger
----------------


--quad trigger
create trigger EVENT_FRAMEWORK_TRIPLE_INSERT_TRIGGER AFTER INSERT on RDF_QUAD REFERENCING NEW AS N
{
	declare aq, res, params, i any; 
	declare state, msg, descs, rows, trigg any;
	EXEC('SELECT TriggerName, Condition, FROM EventFrameworkTriggerConditions WHERE TableName = \'VirtuosoTripleStore\' AND TriggerType = \'INSERT\'' , state, msg, vector(), 1000, descs, trigg);	
	params := vector(CAST(ID_TO_IRI(N.G) AS VARCHAR), CAST(ID_TO_IRI(N.S) AS VARCHAR), CAST(ID_TO_IRI(N.P) AS VARCHAR), CAST(__ro2sq(N.O) AS VARCHAR)); 
		
	if (internal_type_name(internal_type(trigg)) <> 'INTEGER' AND LENGTH(trigg)>0)
	{
		aq := async_queue (20); 
		for(i:=0;i<LENGTH(trigg);i:=i+1)
		{
			res:=aq_request (aq, 'EVENT_FRAMEWORK_CHECK_TRIGGER_CONDITION', vector ('VirtuosoTripleStore', UTC_TIME(), trigg[i][0], trigg[i][1], params, params, 0)); 
		}
	}
} ;
	
 create trigger EVENT_FRAMEWORK_TRIPLE_DELETE_TRIGGER BEFORE DELETE on RDF_QUAD REFERENCING OLD AS N
{
	declare aq, res, params, i any; 
	declare state, msg, descs, rows, trigg any;
	EXEC('SELECT TriggerName, Condition FROM EventFrameworkTriggerConditions WHERE TableName = \'VirtuosoTripleStore\' AND TriggerType = \'DELETE\'' , state, msg, vector(), 1000, descs, trigg);	
	params := vector(CAST(ID_TO_IRI(N.G) AS VARCHAR), CAST(ID_TO_IRI(N.S) AS VARCHAR), CAST(ID_TO_IRI(N.P) AS VARCHAR), CAST(__ro2sq(N.O) AS VARCHAR));  
		
	if (internal_type_name(internal_type(trigg)) <> 'INTEGER' AND LENGTH(trigg)>0)
	{
		aq := async_queue (20); 
		for(i:=0;i<LENGTH(trigg);i:=i+1)
		{ 
			res:=aq_request (aq, 'EVENT_FRAMEWORK_CHECK_TRIGGER_CONDITION', vector ('VirtuosoTripleStore', UTC_TIME(), trigg[i][0], trigg[i][1], params, params, 0)); 
		}
	}
};


----------
--central trigger
----------

create trigger EVENT_FRAMEWORK_LEAVE_FIRST_STAGE_TRIGGER AFTER UPDATE on EventFrameworkComplexEventInstances
REFERENCING OLD AS O, NEW AS N
{
  /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
  dbg_printf ('EventFramework: EVENT_FRAMEWORK_LEAVE_FIRST_STAGE_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);*/
if(O.CurrentStage =0 AND N.CurrentStage=1)
{
declare overlap,i integer;
DECLARE state, msg, descs, rows, query, res any;
overlap := (SELECT IsOverlapping FROM EventFrameworkComplexEvents WHERE CEID = O.CEID);
if(overlap IS NOT NULL AND overlap = 1)
{
EVENT_FRAMEWORK_INSERT_EVENT_INSTANCE(O.FirstStageUri, O.CEID);
}
}
};

create trigger EVENT_FRAMEWORK_START_CE_DELETE AFTER DELETE on EventFrameworkComplexEvents
REFERENCING OLD AS O
{
/*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
  dbg_printf ('EventFramework: EVENT_FRAMEWORK_LEAVE_FIRST_STAGE_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);*/
EXEC(sprintf('DELETE FROM EventFrameworkComplexEventInstances WHERE CEID = %d', O.CEID));
};

create trigger EVENT_FRAMEWORK_INSERT_INSTANCE_TRIGGER AFTER INSERT 
on EventFrameworkComplexEventInstances REFERENCING NEW AS N 
{
/*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
  dbg_printf ('EventFramework: EVENT_FRAMEWORK_LEAVE_FIRST_STAGE_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);*/
EVENT_FRAMEWORK_INSERT_AWAITING_EVENTS(N.EventID, N.FirstStageUri, N.CurrentStage);
};

create trigger EVENT_FRAMEWORK_DELETE_INSTANCE_TRIGGER AFTER DELETE 
on EventFrameworkComplexEventInstances REFERENCING OLD AS O 
{   /*declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
  dbg_printf ('EventFramework: EVENT_FRAMEWORK_LEAVE_FIRST_STAGE_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);*/
  EXEC(sprintf('DELETE FROM EventFrameworkAwaitingEvent WHERE SourceCE = %d', O.EventID)); 
  };

  
  -----------------------------------------------
--grant execute rights on all procedures to publish
grant execute on EVENT_FRAMEWORK_GET_SCHEMA_TABLES to DBA;
grant execute on EVENT_FRAMEWORK_SET_NEW_TRIGGER to DBA;
grant execute on EVENT_FRAMEWORK_INSERT_NEW_EVENT to DBA;
grant execute on EVENT_FRAMEWORK_GET_TRIGGER_SYNTAX to DBA;
grant execute on EVENT_FRAMEWORK_GET_TRIGGERS_OF_TABLE to DBA;
grant execute on EVENT_FRAMEWORK_CHECK_TRIGGER_EXISTS to DBA;
grant execute on EVENT_FRAMEWORK_INSERT_CONSTANT to DBA;
grant execute on EVENT_FRAMEWORK_GET_COLUMNS_OF_TABLE to DBA;
grant execute on EVENT_FRAMEWORK_TEST_SQL_CONDITION to DBA;
grant execute on EVENT_FRAMEWORK_DROP_TRIGGER to DBA;
grant execute on EVENT_FRAMEWORK_GET_GRAPHS to DBA;
grant execute on EVENT_FRAMEWORK_SPARQL_TO_SQL to DBA;
grant execute on EVENT_FRAMEWORK_GET_NEXT_ID to DBA;
grant execute on EVENT_FRAMEWORK_REGISTER_DB to DBA;
grant execute on EVENT_FRAMEWORK_INTERNAL_SPARQL to DBA;
grant execute on EVENT_FRAMEWORK_INSERT_TTL_DATA to DBA;

--create new SOAP endpoint 'EventFrameworkProcedures'
VHOST_REMOVE (
	 lhost=>'*ini*',
	 vhost=>'*ini*',
	 lpath=>'/EventFrameworkProcedures'
);

VHOST_DEFINE (
	 lhost=>'*ini*',
	 vhost=>'*ini*',
	 lpath=>'/EventFrameworkProcedures',
	 ppath=>'/SOAP/',
	 is_dav=>0,
	 def_page=>'index.html',
	 soap_user=>'dba',
	 ses_vars=>0,
	 soap_opts=>vector ('ServiceName', 'EventFrameworkProcedures', 'MethodInSoapAction', 'yes', 'CR-escape', 
		 'yes', 'elementFormDefault', 'unqualified', 'DIME-ENC', 'no', 'WS-SEC', 'no', 'WSS-Validate-Signature', '0', 'WS-RP', 'no', 'Use', 'literal', 'XML-RPC', 'no'),
	 is_default_host=>0
);

SELECT EVENT_FRAMEWORK_FINISH_INSTALLATION();

commit work;
