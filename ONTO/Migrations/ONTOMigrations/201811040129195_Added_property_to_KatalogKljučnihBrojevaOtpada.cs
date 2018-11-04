namespace ONTO.Migrations.ONTOMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_property_to_KatalogKljučnihBrojevaOtpada : DbMigration
    {
        public override void Up()
        {
            AddColumn("onto.Katalog_Kljucnih_Brojeva_Otpada", "KeyNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("onto.Katalog_Kljucnih_Brojeva_Otpada", "KeyNumber");
        }
    }
}
