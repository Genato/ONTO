namespace ONTO.Migrations.ONTOMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserProfileSettings_model2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("onto.User_Profile_Settings", "LocalizationID", c => c.Int(nullable: false));
            DropColumn("onto.User_Profile_Settings", "Localization");
        }
        
        public override void Down()
        {
            AddColumn("onto.User_Profile_Settings", "Localization", c => c.String());
            DropColumn("onto.User_Profile_Settings", "LocalizationID");
        }
    }
}
