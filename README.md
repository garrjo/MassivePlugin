# MassivePlugin
Massive plugin is a jQuery plugin that is used in unison with Massive.  It allows a user to build complex applications with html.  At present, this plugin is built with an asmx/class.  We have hopes to switch to WCF in the future.  

At present, you can do the following:
1. Select, Insert, Update and Delete.
2. Query, Execute and Etc.


Usage Examples:
The HTML Tags that create and connect to the data.

data-where:
			This is used in the Massive's "where" clause.  This tag is a mandatory tag.
			Usage: <input id="inputID" data-where="Column = @Value" />
data-query:
			This is used with Massive's "tbl.Query()" method.  This tag can contain an entire SQL query.		
			Usage: <input id="inputID" data-query="SELECT (cols) FROM dbo.Table WHERE Col = Val" />
				- NOTE: "WHERE" in the query string is not mandatory.

data-columns:
			This is used with Massive's "Columns" clause.  The default for this tag is "*"; however, you can limit the columns you want returned from SQL. This is not a mandatory column as it has a default value.
      Usage: <input id="inputID" data-columns="Column1, Column2, Column3" /> OR	
				   <input id="inputID" data-columns="*" /> 
data-orderby:
			This is used with Massive's "orderby" clause.  This is not a mandatory field but it will tell SQL to order by whatever field is listed in the data tag.
			Usage: <input id="inputID" data-orderby="Column1 DESC/ASC" />
				   
data-method: 
			This is used to let the Massive Web Service know which method to run i.e. (MassiveInsert, MassiveUpdate, MassiveDelete, MassiveSelect, MassiveAll).
			Usage: <input id="inputID" data-method="MassiveAll" />
      
data-table:
			This is used to let Massive know which table to pull from when making a connection to SQL.
			Usage: <input id="inputID" data-table="YourTable" />
			
data-placement:
			This is used to tell the plugin where to place the returned data from the Web Service. The placement can be any type of data handling tag in html.		
			Usage: <input id="inputID" data-placement="DivTagID/TableTagID/SelectTagID/ULTagID/FieldSetTagID" />		
			
data-datatable:
			This is a tag that will be parsed to let the plugin know if the table you are wanting to use will implement datatables. 			
			Usage: <input id="inputID" data-datatables="true/false" />
			
data-fields:
			This is used when inserting/updating.  This attribute is usally listed on the button or <a> tag you are using. The fields that are listed are the input values that are on the page.  You will need to set the ID to the name of the column in the table. The plugin sets the value equal to the id name and can be directly inserted into SQL.		
			Usage: <a href="#" id="btnID" data-fields="field1,field2,etc."></a>

data-formfill:
			This is used to fill-out the pages input values.  If you have 3 or 4 input values you can use a single <span> tag and us the data-formfill tag and populate all 3 or 4 inputs.  You will need to name the input ids the same name as the columns in the table you are using.  You can then say you want to form fill and the plugin will look for matching names and populate the data.		
			Usage: <span id="spanID" formfill="true/false" />

data-hidden-cols:
			This is used when you have data returned upon the initial web service call to massive and you want to pull columns but hide them in the table.		
			Usage: <input id="inputID"  data-hidden-cols="col_1,col_2" />

data-input-field:
			This tag is added to a button or span that searches based on a page control.  If you have a text-box or dropdown or any other control, you can add this tag and it will run a search after pressing enter or button.  This requires the page controls ID in the tag.	
			Usage: <a href="#" id="buttonID" data-input-field="dropdownID/selectID/ID" />
		
Working with a dropdown <select>:
data-text: 
			This is used to fill the select control with the data that will show.  
data-value:
			This is used to set the data-text name with the value.

Example: If you have a table that contains an Id column and a name column. If you want the Name to be shown but you want to use the id when the name is selected, then the data-text tag will need to be something like the following: data-text="Col_Name". The data-value tag will need to look like the following: data-value="Col_ID".
		
CSS Required Classes
--------------------------------------
For the data tags to work you will need a couple of classes in you html tags.
		1.) "massive-config" - This will tell the plugin that you are wanting to use its functionality for the	desired html tag you are building.	
		2.) "loadcontrol" - This will tell the plugin that you are wanting it to read the values in the data tags and use them to load data into a table/fieldset/etc.  You CAN load multiple controls on a page you will just need to add this class to each control you wish to load.	
		3.) "searchable_select" - This is a class that can be added to a select control in html.  This will allow you to type into a select control and narrow results based on what you are typing.  Kind of has the same features as a radComboBox but not as extensive.
		4.) "preventDefault" - This can be added to a button when you don't want the page to recognize an enter press.
	
IMPORTANT NOTES:
--------------------------------------
  1.) The plugin requires the use of SQL Column names as ID's.  This will give the inputs unique ID's and the plugin can parse the values and create dictionaries.  This is important as you can insert a dictionary directly into SQL using the Massive Extensions Class.
  2.) Results are limited to the following: Div's, Table's, UL's, FieldSet's, and DropDown's
	
CODE EXAMPLES:
--------------------------------------
LOADED TABLE:
			<div id="divID" class="massive-config loadcontrol" data-results="results" data-datatables="false" data-method="MassiveAll" data-where="" data-columns="Col,Col2,Col3" data-table="YourTable" data-orderby="Column2">
				<h4>GENERIC HEADER</h4>
				<div>
					<table class="table table-striped table-bordered table-hover" id="results">
					</table>
				</div>
			</div>
--------------------------------------
NOTE: To enable datatables just set the data-datatables="true".

ACTION BUTTON:
--------------------------------------
		<a href="#" id="btnID" data-method="MassiveInsert" onclick="ActionHandler('btnID')" data-table="YourTable" data-fields="inputID1,inputID2,inputID3" data-where="" class="actionhandler preventDefault"><i class="iconClass"></i>&nbsp;BtnName</a>

NOTE: onclick attribute is needed, but you use the ActionHandler method and give it the button id so the plugin can parse the correct button and insert/update the data accordingly.  Also to set the button to the update/delete you would change the data-method="MassiveUpdate/MassiveDelete" and set your where condition to the primary key field i.e. (data-where="PKey = @inputID").

SEARCH WITH SELECT(PRE-LOADED CONTROLS):
--------------------------------------
<select id="DropDownID" class="searchable_select loadcontrol" data-text="SQL_Col_1,SQL_Col_2" data-value="SQL_Col_3" data-results="DropDownID" data-where="whereStatement" data-method="MassiveAll" data-columns="SQL_Col_1,SQL_Col_2,SQL_Col_3" data-table="YourTable" data-orderby="SQL_Col_1,SQL_Col_2 ASC">
				<option>Please Select a Value</option>
	</select>
	<div class="row-fluid">
			<table id="resultsID" class="table table-striped table-bordered table-hover">
			</table>
	</div>
		
  <span id="spanID" class="massive-config" data-results="resultsID" data-input-field="DropDownID" data-hidden-cols="SQL_Col_3" data-validation-enabled="false" data-datatables="false" data-method="MassiveAll" data-where="SQL_Col_1=@Val_1" data-columns="SQL_Col_1,SQL_Col_2" data-table="YourTable" data-orderby="SQL_Col_1,SQL_Col_2" />

SEARCH WITH INPUT:
--------------------------------------
		<input id="inputID" class="input-sm" value="" />		
		<div id="resultsID"></div>
		<a href="#" class="massive-config" data-results="resultsID" data-input-field="inputID" data-where="SQL_Col_1 = @Val_1"

FORM FILL PAGE:
--------------------------------------
		<input id="SQL_Col_1" class="input-sm" />
		<input id="SQL_Col_2" class="input-sm" />
		<input id="SQL_Col_3" class="input-sm" />
		
		<span id="form-loader" class="hidden loadcontrol" data-formfill="true" data-method="MassiveAll" data-table="YourTable" data-columns="*" data-where="SQL_Col_1 = @val" />
