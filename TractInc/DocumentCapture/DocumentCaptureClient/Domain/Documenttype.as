
package Domain
{

    import Domain.Codegen.*;

    [Bindable]
    [RemoteClass(alias="TractInc.DocCapture.Domain.Documenttype")]
    public dynamic class Documenttype extends _Documenttype
    {
        public function get label():String {
            return Name;
        }
    }
}
    