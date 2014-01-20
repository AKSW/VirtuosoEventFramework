
--all procedures & tables needed at a client-DB

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
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_CHECK_TRIGGER_EXISTS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_TRIGGERS_OF_TABLE: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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
{  
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_TRIGGER_SYNTAX: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
return 0;
};
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
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_SET_NEW_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 'False';
  };

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
 declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_SCHEMA_TABLES: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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

create procedure EVENT_FRAMEWORK_DROP_TRIGGER(
in controlID integer,
in triggername varchar
)
RETURNS VARCHAR
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_DROP_TRIGGER: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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

create procedure
EVENT_FRAMEWORK_INSERT_CONSTANT
( 
in controlID INTEGER,
in keyV VARCHAR,
in valueV ANY
)
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_CONSTANT: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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

create procedure
EVENT_FRAMEWORK_TEST_SQL_CONDITION
( 
in executeQuerry VARCHAR
)
RETURNS ANY ARRAY ARRAY
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_TEST_SQL_CONDITION: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
DECLARE state, msg, descs, rows, exarray any;
EXEC(executeQuerry, state, msg, vector(), 100, descs, rows);

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
};

create procedure EVENT_FRAMEWORK_GET_GRAPHS
(in controlID INTEGER)
RETURNS VARCHAR ARRAY
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_GET_GRAPHS: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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

create procedure EVENT_FRAMEWORK_TYPE_TEST (in DTP integer)
RETURNS VARCHAR ARRAY
{
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_TYPE_TEST: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
declare TypeName varchar;
declare i, TypeNr integer;

TypeName:= internal_type_name(DTP);
if(TypeName <> 'UNK_DV_TYPE' AND not strcontains(TypeName, 'ARRAY') AND DTP NOT IN(180,181,196,197,198,199,200,204,207,234,242,246,254, 126, 133,134,135,238,121,122,123))
{
return 1;
}
else
{
return 0;
}
} ;

--executes sparql querry only for use as internal sparql 'endpoint'
create procedure
EVENT_FRAMEWORK_INTERNAL_SPARQL
( 
in controlID INTEGER,
in executeQuerry VARCHAR
)
RETURNS ANY
{
  declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INTERNAL_SPARQL: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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
declare exit handler for sqlstate '*'
  {
    declare debug integer;
    debug := (SELECT "Value" FROM EventFrameworkConstants WHERE "Key" = 'debugMode');
    if(debug)
      dbg_printf ('EventFramework: EVENT_FRAMEWORK_INSERT_TTL_DATA: \nSQLSTATE: %s, \nSQLMESSAGE: %s', __SQL_STATE, __SQL_MESSAGE);
  return 0;
  };
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
};

--quad trigger
create trigger EVENT_FRAMEWORK_TRIPLE_INSERT_TRIGGER AFTER INSERT on RDF_QUAD REFERENCING NEW AS N
{
	declare aq, res, params, i any; 
	declare state, msg, descs, rows, trigg any;
	EXEC('SELECT TriggerName, Condition FROM EventFrameworkTriggerConditions WHERE TableName = \'VirtuosoTripleStore\' AND TriggerType = \'INSERT\'' , state, msg, vector(), 10000, descs, trigg);	
	params := vector(CAST(ID_TO_IRI(N.G) AS VARCHAR), CAST(ID_TO_IRI(N.S) AS VARCHAR), CAST(ID_TO_IRI(N.P) AS VARCHAR), CAST(__ro2sq(N.O) AS VARCHAR)); 
		
	if (internal_type_name(internal_type(trigg)) <> 'INTEGER' AND LENGTH(trigg)>0)
	{
		aq := async_queue (20); 
		for(i:=0;i<LENGTH(trigg);i:=i+1)
		{
			aq_request (aq, 'EVENT_FRAMEWORK_CHECK_TRIGGER_CONDITION', vector ('VirtuosoTripleStore', UTC_TIME(), trigg[i][0], trigg[i][1], params, params, 0)); 
		}
	}
} ;
	
 create trigger EVENT_FRAMEWORK_TRIPLE_DELETE_TRIGGER BEFORE DELETE on RDF_QUAD REFERENCING OLD AS N
{
	declare aq, res, params, i any; 
	declare state, msg, descs, rows, trigg any;
	EXEC('SELECT TriggerName, Condition FROM EventFrameworkTriggerConditions WHERE TableName = \'VirtuosoTripleStore\' AND TriggerType = \'DELETE\'' , state, msg, vector(), 10000, descs, trigg);	
	params := vector(CAST(ID_TO_IRI(N.G) AS VARCHAR), CAST(ID_TO_IRI(N.S) AS VARCHAR), CAST(ID_TO_IRI(N.P) AS VARCHAR), CAST(__ro2sq(N.O) AS VARCHAR));  
		
	if (internal_type_name(internal_type(trigg)) <> 'INTEGER' AND LENGTH(trigg)>0)
	{
		aq := async_queue (20); 
		for(i:=0;i<LENGTH(trigg);i:=i+1)
		{
			aq_request (aq, 'EVENT_FRAMEWORK_CHECK_TRIGGER_CONDITION', vector ('VirtuosoTripleStore', UTC_TIME(), trigg[i][0], trigg[i][1], params, params, 0)); 
		}
	}
};

--grant execute rights on all procedures to publish
grant execute on EVENT_FRAMEWORK_GET_SCHEMA_TABLES to DBA;
grant execute on EVENT_FRAMEWORK_SET_NEW_TRIGGER to DBA;
grant execute on EVENT_FRAMEWORK_GET_TRIGGER_SYNTAX to DBA;
grant execute on EVENT_FRAMEWORK_GET_TRIGGERS_OF_TABLE to DBA;
grant execute on EVENT_FRAMEWORK_CHECK_TRIGGER_EXISTS to DBA;
grant execute on EVENT_FRAMEWORK_INSERT_CONSTANT to DBA;
grant execute on EVENT_FRAMEWORK_REGISTER_DB to DBA;
grant execute on EVENT_FRAMEWORK_GET_COLUMNS_OF_TABLE to DBA;
grant execute on EVENT_FRAMEWORK_TEST_SQL_CONDITION to DBA;
grant execute on EVENT_FRAMEWORK_DROP_TRIGGER to DBA;
grant execute on EVENT_FRAMEWORK_GET_GRAPHS to DBA;
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
	 soap_opts=>vector ('ServiceName', 'EventFrameworkProcedures', 'MethodInSoapAction', 'yes', 'CR-escape', 'yes', 'elementFormDefault', 'unqualified', 'DIME-ENC', 'no', 'WS-SEC', 'no', 'WSS-Validate-Signature', '0', 'WS-RP', 'no', 'Use', 'literal', 'XML-RPC', 'no'),
	 is_default_host=>0
);

DELETE FROM EventFrameworkConstants;
SELECT (CASE WHEN tcpip_gethostbyaddr(tcpip_gethostbyname('localhost')) <> 'localhost' THEN ('Copy the following url to register this database in the Event-Framework-Control:  http://'|| 
	tcpip_gethostbyaddr(tcpip_gethostbyname('localhost')) || ':' || (select cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort')) || '/EventFrameworkProcedures') ELSE '' END);
SELECT CASE WHEN tcpip_gethostbyaddr(tcpip_gethostbyname('localhost')) = 'localhost' THEN  ('Copy the following url to register this database in the Event-Framework-Control:  http://--replace with hostname!--:' || 
	CAST((select cfg_item_value (virtuoso_ini_path (), 'HTTPServer','ServerPort')) AS VARCHAR)|| '/EventFrameworkProcedures') ELSE '' END;
	
SELECT 'Make sure that this endpoint is accessable via hostname from the central-database.';