package truetract.domain
{
public interface IFileAttachment extends IMemento
{
    function get fileId():int;
    function set fileId(value:int):void;

    function get file():File;
    function set file(value:File):void;
    
    function get attachmentTypeId():int;
    function set attachmentTypeId(value:int):void;
}
}