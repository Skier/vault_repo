set ANSI_NULLS on
set QUOTED_IDENTIFIER on
go

create procedure [dbo].[EC_GetSubmittals] (@modelId as integer) as

declare @newmodelid as integer

select top 1 @newmodelid= submodel_id from jnctModSubMod where model_id=@modelid

if isnull(@newmodelid,0)=0

begin

            set @newmodelid=@modelid

end

           

select

                        FC.filecategoryname as [filecategory],

                        FC.filecategoryid as [filecategoryid],

                        FC.secure as [secure],

                        Ftype.name as [filetype],

                        JFC.sortorder  as [filetypesort],

                        TF.id as [fileid],

                        TF.description as [filename]

                        from tFileCategoryTypes FC,tblFileType Ftype,tblfile TF,JnctFile JF,JNCTFileTYPECAT JFC

                        where isnull(FC.secure,0)<=1 and JF.file_id=TF.id

                        and TF.filetype_id=Ftype.id

                        and ftype.id=JFC.filetypeid

                        and JFC.FILECATEGORYID=FC.FILECATEGORYID

 

                        and jf.objtype='Model' and jf.objid=@newmodelid and ftype.brand_code='tts' and TF.active=1
