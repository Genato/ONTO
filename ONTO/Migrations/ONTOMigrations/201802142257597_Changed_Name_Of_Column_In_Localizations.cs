namespace ONTO.Migrations.ONTOMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Name_Of_Column_In_Localizations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "onto.Localizations", newName: "Localization");
            RenameColumn(table: "onto.Localization", name: "_Localization", newName: "Localization");
        }
        
        public override void Down()
        {
            RenameColumn(table: "onto.Localization", name: "Localization", newName: "_Localization");
            RenameTable(name: "onto.Localization", newName: "Localizations");
        }
    }
}
