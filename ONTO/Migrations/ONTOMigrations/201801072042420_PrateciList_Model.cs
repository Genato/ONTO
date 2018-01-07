namespace ONTO.Migrations.ONTOMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrateciList_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "onto.Prateci_List",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("onto.Prateci_List");
        }
    }
}
