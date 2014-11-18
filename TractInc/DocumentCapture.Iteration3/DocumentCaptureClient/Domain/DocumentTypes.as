package Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    public dynamic class DocumentTypes
    {
        public var Items:ArrayCollection;
        
        public function DocumentTypes():void{
            Items = new ArrayCollection();
            Items.addItem(new String("Company"));
            Items.addItem(new String("Individual"));
            Items.addItem(new String("Affidavit of Heirship"));
            Items.addItem(new String("Agreement"));
            Items.addItem(new String("Correction Warranty Deed"));
            Items.addItem(new String("Deed of Trust"));
            Items.addItem(new String("Designation of Unit"));
            Items.addItem(new String("Easement"));
            Items.addItem(new String("Executor's Deed"));
            Items.addItem(new String("Lease Memo"));
            Items.addItem(new String("License"));
            Items.addItem(new String("Lien"));
            Items.addItem(new String("Marshal's Deed"));
            Items.addItem(new String("Mechanic's Lien"));
            Items.addItem(new String("Mineral Deed"));
            Items.addItem(new String("Oil Gas Mineral Lease"));
            Items.addItem(new String("Option and Agreement"));
            Items.addItem(new String("Probate"));
            Items.addItem(new String("Quit Claim Deed"));
            Items.addItem(new String("Release of Lien"));
            Items.addItem(new String("Right of Way"));
            Items.addItem(new String("Sheriff's Deed"));
            Items.addItem(new String("Special Warranty Deed"));
            Items.addItem(new String("Warranty Deed"));
        }
    }
}