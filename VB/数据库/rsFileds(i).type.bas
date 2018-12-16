'----------------------------------------------------------------------------   
' 函数原型： SQLDataTypeName(dtIndex)   
' 功能说明： 返回SQL   Server数据类型名称   
' 参数类型： dtIndex 数据类型   
' 返   回   值： 数据类型名称   
' 创建日期： 2004年6月24日   
' 最后修改： 2004年6月24日   
'----------------------------------------------------------------------------   
Public   Function   SQLDataTypeName(dtIndex)   
	  Select   Case   dtIndex   
			  Case   adEmpty   :   SQLDataTypeName   =   "Empty"   
			  Case   adTinyInt   :   SQLDataTypeName   =   "TinyInt"   
			  Case   adSmallInt   :   SQLDataTypeName   =   "SmallInt"   
			  Case   adInteger   :   SQLDataTypeName   =   "Integer"   
			  Case   adBigInt   :   SQLDataTypeName   =   "BigInt"   
			  Case   adUnsignedTinyInt   :   SQLDataTypeName   =   "UnsignedTinyInt"   
			  Case   adUnsignedSmallInt   :   SQLDataTypeName   =   "UnsignedSmallInt"   
			  Case   adUnsignedInt   :   SQLDataTypeName   =   "UnsignedInt"   
			  Case   adUnsignedBigInt   :   SQLDataTypeName   =   "UnsignedBigInt"   
			  Case   adSingle   :   SQLDataTypeName   =   "Single"   
			  Case   adDouble   :   SQLDataTypeName   =   "Double"   
			  Case   adCurrency   :   SQLDataTypeName   =   "Currency"   
			  Case   adDecimal   :   SQLDataTypeName   =   "Decimal"   
			  Case   adNumeric   :   SQLDataTypeName   =   "Numeric"   
			  Case   adBoolean   :   SQLDataTypeName   =   "Boolean"   
			  Case   adError   :   SQLDataTypeName   =   "Error"   
			  Case   adUserDefined   :   SQLDataTypeName   =   "UserDefined"   
			  Case   adVariant   :   SQLDataTypeName   =   "Variant"   
			  Case   adIDispatch   :   SQLDataTypeName   =   "IDispatch"   
			  Case   adIUnknown   :   SQLDataTypeName   =   "IUnknow"   
			  Case   adGUID   :   SQLDataTypeName   =   "GUID"   
			  Case   adDate   :   SQLDataTypeName   =   "Date"   
			  Case   adDBDate   :   SQLDataTypeName   =   "DBDate"   
			  Case   adDBTime   :   SQLDataTypeName   =   "DBTime"   
			  Case   adDBTimeStamp   :   SQLDataTypeName   =   "DBTimeStamp"   
			  Case   adBSTR   :   SQLDataTypeName   =   "BSTR"   
			  Case   adChar   :   SQLDataTypeName   =   "Char"   
			  Case   adVarChar   :   SQLDataTypeName   =   "VarChar"   
			  Case   adLongVarChar   :   SQLDataTypeName   =   "LongVarChar"   
			  Case   adWChar   :   SQLDataTypeName   =   "WChar"   
			  Case   adVarWChar   :   SQLDataTypeName   =   "VarWChar"   
			  Case   adLongVarWChar   :   SQLDataTypeName   =   "LongVarWChar"   
			  Case   adBinary   :   SQLDataTypeName   =   "Binary"   
			  Case   adVarBinary   :   SQLDataTypeName   =   "VarBinary"   
			  Case   adLongVarBinary   :   SQLDataTypeName   =   "LongVarBinary"   
			  Case   adChapter   :   SQLDataTypeName   =   "Chapter"   
			  Case   adFileTime   :   SQLDataTypeName   =   "FileTime"   
			  Case   adPropVariant   :   SQLDataTypeName   =   "PropVariant"   
			  Case   adVarNumeric   :   SQLDataTypeName   =   "VarNumeric"   
			  Case   adArray   :   SQLDataTypeName   =   "Array"   
	  End   Select   
End   Function
