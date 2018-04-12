namespace Demo_NMM.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "AlcoholByVolume", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "AlcoholByVolume");
        }
    }
}
