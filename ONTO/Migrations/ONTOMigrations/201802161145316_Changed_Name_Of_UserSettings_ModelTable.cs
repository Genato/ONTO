namespace ONTO.Migrations.ONTOMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Name_Of_UserSettings_ModelTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "onto.User_Profile_Settings", newName: "User_Settings");
        }
        
        public override void Down()
        {
            RenameTable(name: "onto.User_Settings", newName: "User_Profile_Settings");
        }
    }
}
