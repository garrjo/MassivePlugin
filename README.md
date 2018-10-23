<h2><strong>Massive Plugin </strong></h2>
<h3>Documentation/Usage</h3>

<pre>
Massive plugin is a jquery plugin that is used in unison with Massive ORM.  It allows a user to build <span style="white-space:pre"> </span>complex applications with html.</pre>

<div>&nbsp;</div>

<div><strong><span class="marker">Available Html Data Tags</span></strong></div>

<ol>
	<li>data-where:
	<ul>
		<li>This is used in the Massive&#39;s &quot;where&quot; clause.&nbsp; This tag is a mandatory tag.</li>
		<li>Usage: &lt;input id=&quot;inputID&quot; data-where=&quot;Column = @Value&quot; /&gt;</li>
	</ul>
	</li>
	<li>data-query:
	<ul>
		<li>This is used with Massive&#39;s &quot;tbl.Query()&quot; method.&nbsp; This tag can contain an entire SQL query.</li>
		<li>Usage: &lt;input id=&quot;inputID&quot; data-query=&quot;SELECT (cols) FROM dbo.Table WHERE Col = Val&quot; /&gt;</li>
		<li>&quot;WHERE&quot; in the query string is not mandatory.</li>
	</ul>
	</li>
	<li>data-columns:
	<ul>
		<li>This is used with Massive&#39;s &quot;Columns&quot; clause.&nbsp; The default for this tag is &quot;*&quot;; however, you can limit the columns you want returned from SQL. This is not a mandatory column as it has a default value.</li>
		<li>Usage: &lt;input id=&quot;inputID&quot; data-columns=&quot;Column1, Column2, Column3&quot; /&gt; OR&nbsp;<span style="white-space:pre">&lt;input id=&quot;inputID&quot; data-columns=&quot;*&quot; /&gt;&nbsp;</span></li>
	</ul>
	</li>
	<li><span style="white-space:pre">data-orderby: &nbsp;</span>
	<ul>
		<li><span style="white-space:pre">This is used with Massive&#39;s &quot;orderby&quot; clause.&nbsp; This is not a mandatory field but it will tell SQL to order by whatever field is listed in the data tag.</span></li>
		<li><span style="white-space:pre">Usage: &lt;input id=&quot;inputID&quot; data-orderby=&quot;Column1 DESC/ASC&quot; /&gt;</span></li>
	</ul>
	</li>
	<li><span style="white-space:pre">data-method:&nbsp;</span>
	<ul>
		<li><span style="white-space:pre">This is used to let the Massive Web Service know which method to run i.e. (MassiveInsert, MassiveUpdate, MassiveDelete, MassiveSelect, MassiveAll).</span></li>
		<li><span style="white-space:pre">Usage: &lt;input id=&quot;inputID&quot; data-method=&quot;MassiveAll&quot; /&gt;</span></li>
	</ul>
	</li>
	<li><span style="white-space:pre">data-table:</span>
	<ul>
		<li><span style="white-space:pre">This is used to let Massive know which table to pull from when making a connection to SQL.</span></li>
		<li><span style="white-space:pre">Usage: &lt;input id=&quot;inputID&quot; data-table=&quot;YourTable&quot; /&gt;</span></li>
	</ul>
	</li>
	<li><span style="white-space:pre">data-placement:</span>
	<ul>
		<li><span style="white-space:pre">This is used to tell the plugin where to place the returned data from the Web Service. The placement can be any type of data handling tag in html.</span></li>
		<li><span style="white-space:pre">Usage: &lt;input id=&quot;inputID&quot; data-placement=&quot;DivTagID/TableTagID/SelectTagID/ULTagID/FieldSetTagID&quot; /&gt;<span style="white-space:pre"> </span></span></li>
	</ul>
	</li>
	<li><span style="white-space:pre"><span style="white-space:pre">data-datatable:</span></span>
	<ul>
		<li><span style="white-space:pre"><span style="white-space:pre">This is a tag that will be parsed to let the plugin know if the table you are wanting to use will implement datatables.&nbsp;</span></span></li>
		<li><span style="white-space:pre"><span style="white-space:pre">Usage: &lt;input id=&quot;inputID&quot; data-datatables=&quot;true/false&quot; /&gt;</span></span></li>
	</ul>
	</li>
	<li><span style="white-space:pre"><span style="white-space:pre">data-fields:</span></span>
	<ul>
		<li><span style="white-space:pre"><span style="white-space:pre">This is used when inserting/updating.&nbsp; This attribute is usally listed on the button or &lt;a&gt; tag you are using. The fields that are listed are the input values that are on the page.&nbsp; You will need to set the ID to the name of the column in the table. The plugin sets the value equal to the id name and can be directly inserted into SQL.</span></span></li>
		<li><span style="white-space:pre"><span style="white-space:pre">Usage: &lt;a href=&quot;#&quot; id=&quot;btnID&quot; data-fields=&quot;field1,field2,etc.&quot;&gt;&lt;/a&gt;</span></span></li>
	</ul>
	</li>
	<li><span style="white-space:pre"><span style="white-space:pre">data-formfill:</span></span>
	<ul>
		<li><span style="white-space:pre"><span style="white-space:pre">This is used to fill-out the pages input values.&nbsp; If you have 3 or 4 input values you can use a single &lt;span&gt; tag and us the data-formfill tag and populate all 3 or 4 inputs.&nbsp; </span></span></li>
		<li><span style="white-space:pre"><span style="white-space:pre">Notes: </span></span>
		<ul>
			<li><span style="white-space:pre"><span style="white-space:pre">You will need to name the input ids the same name as the columns in the table you are using.&nbsp; </span></span></li>
			<li><span style="white-space:pre"><span style="white-space:pre">You can then say you want to form fill and the plugin will look for matching names and populate the data</span></span></li>
		</ul>
		</li>
		<li><span style="white-space:pre"><span style="white-space:pre">U</span></span>sage: &lt;span id=&quot;spanID&quot; formfill=&quot;true/false&quot; /&gt;</li>
	</ul>
	</li>
	<li>data-hidden-cols:
	<ul>
		<li>This is used when you have data returned upon the initial web service call to massive and you want to pull columns but hide them in the table.</li>
		<li>Usage: &lt;input id=&quot;inputID&quot;&nbsp; data-hidden-cols=&quot;col_1,col_2&quot; /&gt;</li>
	</ul>
	</li>
	<li>data-input-field:
	<ol>
		<li>This tag is added to a button or span that searches based on a page control.&nbsp; If you have a text-box or dropdown or any other control, you can add this tag and it will run a search after pressing enter or button.&nbsp; This requires the page controls ID in the tag.</li>
		<li>Usage: &lt;a href=&quot;#&quot; id=&quot;buttonID&quot; data-input-field=&quot;dropdownID/selectID/ID&quot; /&gt;</li>
	</ol>
	</li>
</ol>

<p><strong>Special Data Tags</strong> Drop Down Lists</p>

<ol>
	<li>data-text
	<ol>
		<li>used to fill the select control with the data that will show.&nbsp;&nbsp;</li>
	</ol>
	</li>
	<li>data-value:
	<ol>
		<li>used to set the data-text name with the value.</li>
	</ol>
	</li>
</ol>

<fieldset><legend>Example:</legend>

<p>If you have a table that contains an Id column and a name column. If you want the Name to be shown but you want to use the id when the name is selected, then the data-text tag will need to be something like the following: data-text=&quot;Col_Name&quot;. The data-value tag will need to look like the following: data-value=&quot;Col_ID&quot;.</p>
</fieldset>

<div>&nbsp;</div>

<div><strong>Required CSS Classes</strong></div>

<div>For the data tags to work you will need a couple of classes in you html tags.</div>

<ol>
	<li>&nbsp;&quot;massive-config&quot; - This will tell the plugin that you are wanting to use its functionality for the<span style="white-space:pre"> </span>desired html tag you are building.</li>
	<li>&quot;loadcontrol&quot; - This will tell the plugin that you are wanting it to read the values in the data tags and use them to load data into a table/fieldset/etc.&nbsp; You CAN load multiple controls on a page you will just need to add this class to each control you wish to load.</li>
	<li>&quot;searchable_select&quot; - This is a class that can be added to a select control in html.&nbsp; This will allow you to type into a select control and narrow results based on what you are typing.&nbsp; Kind of has the same features as a radComboBox but not as extensive.</li>
	<li>&quot;preventDefault&quot; - This can be added to a button when you don&#39;t want the page to recognize an enter press.</li>
</ol>

<div><strong>Important Notes:</strong></div>

<ol>
	<li>The plugin requires the use of SQL Column names as ID&#39;s.&nbsp; This will give the inputs unique ID&#39;s and the plugin can parse the values and create dictionaries.&nbsp; This is important as you can insert a dictionary directly into SQL using the Massive Extensions Class.</li>
	<li>Results are limited to the following (presently): Div&#39;s, Table&#39;s, UL&#39;s, FieldSet&#39;s, and DropDown&#39;s
	<ul>
		<li>This can be extended to support any selector by defining it in the massive plugin code.</li>
	</ul>
	</li>
</ol>

<h4><strong>Code Examples:</strong></h4>

<h5><em>LOADED TABLE:</em></h5>

<div>&lt;div id=&quot;divID&quot; class=&quot;massive-config loadcontrol&quot; data-results=&quot;results&quot; data-datatables=&quot;false&quot; data-method=&quot;MassiveAll&quot; data-where=&quot;&quot; data-columns=&quot;Col,Col2,Col3&quot; data-table=&quot;YourTable&quot; data-orderby=&quot;Column2&quot;&gt;</div>

<div>&lt;h4&gt;GENERIC HEADER&lt;/h4&gt;</div>

<div>&lt;div&gt;</div>

<div>&lt;table class=&quot;table table-striped table-bordered table-hover&quot; id=&quot;results&quot;&gt;</div>

<div>&lt;/table&gt;</div>

<div>&lt;/div&gt;</div>

<div>&lt;/div&gt;</div>

<div>- NOTE: To enable datatables just set the data-datatables=&quot;true&quot;.</div>

<div>&nbsp;</div>

<div>&nbsp;</div>

<div><em>ACTION BUTTON:</em></div>

<div>&lt;a href=&quot;#&quot; id=&quot;btnID&quot; data-method=&quot;MassiveInsert&quot; onclick=&quot;ActionHandler(&#39;btnID&#39;)&quot; data-table=&quot;YourTable&quot; data-fields=&quot;inputID1,inputID2,inputID3&quot; data-where=&quot;&quot; class=&quot;actionhandler preventDefault&quot;&gt;&lt;i class=&quot;iconClass&quot;&gt;&lt;/i&gt;&amp;nbsp;BtnName&lt;/a&gt;</div>

<div>&nbsp;</div>

<div>- NOTE: onclick attribute is needed, but you use the ActionHandler method and give it the button id so the plugin can parse the correct button and insert/update the data accordingly.&nbsp; Also to set the button to the update/delete you would change the data-method=&quot;MassiveUpdate/MassiveDelete&quot; and set your where condition to the primary key field i.e. (data-where=&quot;PKey = @inputID&quot;).</div>

<div>&nbsp;</div>

<div>&nbsp;</div>

<div><em>SEARCH WITH SELECT(PRE-LOADED CONTROLS):</em></div>

<div>&lt;select id=&quot;DropDownID&quot; class=&quot;searchable_select loadcontrol&quot; data-text=&quot;SQL_Col_1,SQL_Col_2&quot; data-value=&quot;SQL_Col_3&quot; data-results=&quot;DropDownID&quot; data-where=&quot;whereStatement&quot; data-method=&quot;MassiveAll&quot; data-columns=&quot;SQL_Col_1,SQL_Col_2,SQL_Col_3&quot; data-table=&quot;YourTable&quot; data-orderby=&quot;SQL_Col_1,SQL_Col_2 ASC&quot;&gt;</div>

<div>&lt;option&gt;Please Select a Value&lt;/option&gt;</div>

<div>&lt;/select&gt;</div>

<div>&nbsp;</div>

<div>&lt;div class=&quot;row-fluid&quot;&gt;</div>

<div>&lt;table id=&quot;resultsID&quot; class=&quot;table table-striped table-bordered table-hover&quot;&gt;</div>

<div>&lt;/table&gt;</div>

<div>&lt;/div&gt;</div>

<div>&nbsp;</div>

<div>&lt;span id=&quot;spanID&quot; class=&quot;massive-config&quot; data-results=&quot;resultsID&quot; data-input-field=&quot;DropDownID&quot; data-hidden-cols=&quot;SQL_Col_3&quot; data-validation-enabled=&quot;false&quot; data-datatables=&quot;false&quot; data-method=&quot;MassiveAll&quot; data-where=&quot;SQL_Col_1=@Val_1&quot; data-columns=&quot;SQL_Col_1,SQL_Col_2&quot; data-table=&quot;YourTable&quot; data-orderby=&quot;SQL_Col_1,SQL_Col_2&quot; /&gt;</div>

<div>&nbsp;</div>

<div>&nbsp;</div>

<div><em>SEARCH WITH INPUT:</em></div>

<div>&lt;input id=&quot;inputID&quot; class=&quot;input-sm&quot; value=&quot;&quot; /&gt;</div>

<div>&nbsp;</div>

<div>&lt;div id=&quot;resultsID&quot;&gt;&lt;/div&gt;</div>

<div>&nbsp;</div>

<div>&lt;a href=&quot;#&quot; class=&quot;massive-config&quot; data-results=&quot;resultsID&quot; data-input-field=&quot;inputID&quot; data-where=&quot;SQL_Col_1 = @Val_1&quot;</div>

<div>&nbsp;</div>

<div>&nbsp;</div>

<div><em>FORM FILL PAGE:</em></div>

<div>&lt;input id=&quot;SQL_Col_1&quot; class=&quot;input-sm&quot; /&gt;</div>

<div>&lt;input id=&quot;SQL_Col_2&quot; class=&quot;input-sm&quot; /&gt;</div>

<div>&lt;input id=&quot;SQL_Col_3&quot; class=&quot;input-sm&quot; /&gt;</div>

<div>&nbsp;</div>

<div>&lt;span id=&quot;form-loader&quot; class=&quot;hidden loadcontrol&quot; data-formfill=&quot;true&quot; data-method=&quot;MassiveAll&quot; data-table=&quot;YourTable&quot; data-columns=&quot;*&quot; data-where=&quot;SQL_Col_1 = @val&quot; /&gt;</div>
</body>
</html>
