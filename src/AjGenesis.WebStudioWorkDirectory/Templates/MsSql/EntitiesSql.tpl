<#


'	Database Generator
'		MS SQL Server 2000

include "Templates/EntityFunctions.tpl"

#>

--
--		Project:		${Project.Name}
--		Description:	${Project.Description}
--

use master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning Create ${Technology.Database.Name}.SQL at %s ....',1,1,@dttm) with nowait

GO

if exists (select * from sysdatabases where name='${Technology.Database.Name}')
begin
  raiserror('Dropping existing ${Technology.Database.Name} database ....',0,1)
  DROP database ${Technology.Database.Name}
end
GO

CHECKPOINT
go

raiserror('Creating ${Technology.Database.Name} database....',0,1)
go

/*
   Use default size with autogrow
*/

create database ${Technology.Database.Name}
go

checkpoint

go

use ${Technology.Database.Name}

go

if db_name() <> '${Technology.Database.Name}'
   raiserror('Error in ${Technology.Database.Name}.SQL, ''USE ${Technology.Database.Name}'' failed!  Killing the SPID now.'
            ,22,127) with log

go

use ${Technology.Database.Name}
go
set ansi_nulls on
go
set quoted_identifier on
go

<#

for each Entity in Project.Model.Entities

message	"Processing SQL for ${Entity.Name}..."

EntitySqlProperties	= SqlProperties(Entity)
EntityNoIdSqlProperties	= SqlNoIdProperties(Entity)
EntityIdRefSqlProperties = SqlIdRefProperties(Entity)
EntityIdProperty = IdProperty(Entity)

#>

--
--		Entity:		${Entity.Name}
--		Description:	${Entity.Description}
--

<#
message	"Generating Create Table Command..."
#>

if exists (select name from sysobjects 
         where name = '${Technology.Database.Prefix}${Entity.SqlTable}' and type = 'U')
	drop table ${Technology.Database.Prefix}${Entity.SqlTable}
go

create table ${Technology.Database.Prefix}${Entity.SqlTable} (
		[${EntityIdProperty.Name}] [int] identity (1, 1) primary key not null,<# 
p = 0
for each Property in EntityNoIdSqlProperties
	if p then
		Print ","
	end if
#>	
		<#	print "[${Property.Name}] ${Property.SqlType}"
		if Property.Type="IdRef" and not Property.Required then
			print " null"
		end if
	p = p+1
end for
printline ""
#>
)
go

<# message	"Generating Insert Procedure..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}Insert' and type = 'P')
	drop procedure dbo.${Entity.Name}Insert
go

create procedure dbo.${Entity.Name}Insert
	(
		@${EntityIdProperty.Name} int output,<#
p = 0
for each Property in EntityNoIdSqlProperties
	if p then
		print ","
	end if
#>	
		<#	print "@${Property.Name} ${Property.SqlType}"
	p = p+1
end for
printline ""
#>
	)
as
<# for each Property in EntityIdRefSqlProperties #>
		if @${Property.Name} <= 0
		begin
			set @${Property.Name} = null
		end
<# end for #>
	Insert into ${Technology.Database.Prefix}${Entity.SqlTable}(<#
p = 0
for each Property in EntityNoIdSqlProperties
	if p then
		print ","
	end if
#>	
		<#	print "[${Property.Name}]"
	p = p+1
end for
printline ""
#>
		)
	values (<#
p = 0
for each Property in EntityNoIdSqlProperties
	if p then
		print ","
	end if
#>	
		<#	print "@${Property.Name}"
	p = p +1
end for
#>
		)
	set @${EntityIdProperty.Name} = @@identity
	return
go

<# message	"Generating Update Procedure..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}Update' and type = 'P')
	drop procedure dbo.${Entity.Name}Update
go

create procedure dbo.${Entity.Name}Update
	(
<#
p = 0
for each Property in EntitySqlProperties
	if p then
		print ","
	end if
#>	
		<# print "@${Property.Name} ${Property.SqlType}"
	p = p+1
end for
printline ""
#>
	)
as
<# for each Property in EntityIdRefSqlProperties #>
		if @${Property.Name} <= 0
		begin
			set @${Property.Name} = null
		end
<# end for #>
	Update ${Technology.Database.Prefix}${Entity.SqlTable}
		set <#
p = 0
for each Property in EntityNoIdSqlProperties
	if p then
		print ","
	end if
#>	
			<#	print "[${Property.Name}] = @${Property.Name}"
	p = p+1	
end for
printline ""
#>
	where Id = @Id
go

<# message	"Generating Delete Procedure..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}Delete' and type = 'P')
	drop procedure dbo.${Entity.Name}Delete
go

create procedure dbo.${Entity.Name}Delete
	(
		@Id int
	)
as
	Delete ${Technology.Database.Prefix}${Entity.SqlTable}
	where Id = @Id
go

<# message	"Generating Get..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}GetById' and type = 'P')
	drop procedure dbo.${Entity.Name}GetById
go

create procedure dbo.${Entity.Name}GetById
	(
		@Id  int
	)
as
	select <#
p = 0
for each Property in EntitySqlProperties
	if p then
		print ","
	end if
#>	
		<#	print "[${Property.Name}]"
	p=p+1
end for
#>

	from ${Technology.Database.Prefix}${Entity.SqlTable}
	where Id = @Id
go

<# message	"Generating GetAll..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}GetAll' and type = 'P')
	drop procedure dbo.${Entity.Name}GetAll
go

create procedure dbo.${Entity.Name}GetAll
as
	select <#
p = 0
for each Property in EntitySqlProperties
	if p then
		print ","
	end if
#>	
		<# print "[${Property.Name}]"
	p = p+1
end for
#>

	from ${Technology.Database.Prefix}${Entity.SqlTable}
	order by Id
go

<# if Entity.Relations then #>

/*		Public Relations */

<# for each Relation in Entity.Relations
	if	Relation.RelationType="Reference" then
		message	"Generating GetBy${Relation.Entity.Name}..."
#>
if exists (select name from sysobjects 
         where name = '${Entity.Name}GetBy${Relation.Entity.Name}' and type = 'P')
	drop procedure dbo.${Entity.Name}GetBy${Relation.Entity.Name}
go

create procedure dbo.${Entity.Name}GetBy${Relation.Entity.Name}
	(
		@${Relation.Property.Name}  ${Relation.Property.SqlType}
	)
as
	select <#
p=0
for each Property in EntitySqlProperties
	if p then
		print ", "
	end if
	print "[${Property.Name}]"
	p = p+1
end for
#>

	from ${Technology.Database.Prefix}${Entity.SqlTable}
	where [${Relation.Property.Name}] = @${Relation.Property.Name}
	order by Id
go

<# 
	end if

	if		Relation.Type="multiple" then
		message	"Generating Get${Relation.Name}..."
#>
if exists (select name from sysobjects 
         where name = '${Entity.Name}GetBy${Relation.Name}' and type = 'P')
	drop procedure dbo.${Entity.Name}GetBy${Relation.Name}
go

create procedure dbo.${Entity.Name}GetBy${Relation.Name}
	(
		@Id  int
	)
as
	select 
<#
p = 0
for each Property in EntitySqlProperties 
	if p then
		printline ","
	end if
	print "${Technology.Database.Prefix}${Entity.SqlTable}.[${Property.Name}]"
	p = p+1
end for
#>
	from ${Technology.Database.Prefix}${Entity.SqlTable}, ${Technology.Database.Prefix}${Entity.SqlTable}${Relation.SetName}
	where ${Technology.Database.Prefix}${Entity.SqlTable}.Id = ${Technology.Database.Prefix}${Entity.SqlTable}${Relation.SetName}.Id${Entity.Name}
	and
	${Technology.Database.Prefix}${Entity.SqlTable}${Relation.SetName}.Id${Relation.Entity} = @Id
	order by ${Technology.Database.Prefix}${Entity.SqlTable}.Id
go

if exists (select name from sysobjects 
         where name = '${Technology.Database.Prefix}${Entity.SqlTable}${Relation.SetName}' and type = 'U')
	drop table dbo.${Technology.Database.Prefix}${Entity.SqlTable}${Relation.SetName}
go

create table dbo.${Technology.Database.Prefix}${Entity.SqlTable}${Relation.SetName}
	(
		Id [int] identity (1, 1) primary key not null,
		Id${Technology.Database.Prefix}${Entity.SqlTable} int,
		Id${Relation.SetName} int
	)
go

<#
	end if
end for
end if

end for

for each Entity in Project.Model.Entities

for each Relation in Entity.Relations where Relation.RelationType = "Reference"

message "Creating Reference from " & Entity.Name & " to " & Relation.Entity.Name
#>

alter table dbo.${Technology.Database.Prefix}${Entity.SqlTable} add
	constraint [fk_${Technology.Database.Prefix}${Entity.SqlTable}_${Technology.Database.Prefix}${Relation.Entity.SqlTable}] foreign key 
	(
		[${Relation.Property.SqlColumn}]
	) references dbo.${Technology.Database.Prefix}${Relation.Entity.SqlTable} (
		Id
	) on delete cascade  on update cascade 

GO

<#
end for

end for

for each Entity in Project.Model.Entities where Entity.HasReferences
		EntitySqlProperties	= SqlProperties(Entity)
#>

<# message	"Generating GetAllEx..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}GetAllEx' and type = 'P')
	drop procedure dbo.${Entity.Name}GetAllEx
go

create procedure dbo.${Entity.Name}GetAllEx
as
	select <#
p = 0
nref = 0
for each Property in EntitySqlProperties
	if p then
		print ","
	end if
#>	
		e.<# print "[${Property.Name}]"
	p = p+1

	if Property.Reference then
		nref = nref+1
		print ", e" & nref & ".[" & Property.Reference.DescriptorProperty.Name & "] as " & Property.Reference.Name & "Description"
	end if
end for
#>

	from ${Technology.Database.Prefix}${Entity.SqlTable} e
<#
	nref = 0
	for each Property in EntitySqlProperties where Property.Reference
		nref = nref + 1
		RefIdProperty = IdProperty(Property.Reference)
#>
	left join ${Technology.Database.Prefix}${Property.Reference.SqlTable} e<# print nref #> on e.[${Property.Name}] = e<# print nref #>.[${RefIdProperty.Name}]
<#
	end for
#>
	order by e.Id
go

<#
end for

for each Entity in Project.Model.Entities where Entity.HasReferences
	EntitySqlProperties	= SqlProperties(Entity)
	for each RefProperty in Entity.Properties where RefProperty.Reference
#>

<# message	"Generating GetBy${RefProperty.Reference.Name}Ex..." #>

if exists (select name from sysobjects 
         where name = '${Entity.Name}GetBy${RefProperty.Reference.Name}Ex' and type = 'P')
	drop procedure dbo.${Entity.Name}GetBy${RefProperty.Reference.Name}Ex
go

create procedure dbo.${Entity.Name}GetBy${RefProperty.Reference.Name}Ex
	(
		@${RefProperty.Name}  ${RefProperty.SqlType}
	)
as
	select <#
p = 0
nref = 0
for each Property in EntitySqlProperties
	if p then
		print ","
	end if
#>	
		e.<# print "[${Property.Name}]"
	p = p+1

	if Property.Reference then
		nref = nref+1
		print ", e" & nref & ".[" & Property.Reference.DescriptorProperty.Name & "] as " & Property.Reference.Name & "Description"
	end if
end for
#>

	from ${Technology.Database.Prefix}${Entity.SqlTable} e
<#
	nref = 0
	for each Property in EntitySqlProperties where Property.Reference
		nref = nref + 1
		RefIdProperty = IdProperty(Property.Reference)
#>
	left join ${Technology.Database.Prefix}${Property.Reference.SqlTable} e<# print nref #> on e.[${Property.Name}] = e<# print nref #>.[${RefIdProperty.Name}]
<#
	end for
#>
	where e.[${RefProperty.Name}] = @${RefProperty.Name}
	order by e.Id
go

<#
	end for
end for
#>
