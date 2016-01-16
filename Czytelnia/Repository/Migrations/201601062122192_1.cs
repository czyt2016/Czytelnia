namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ksiazka", "Autor", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Ksiazka", "Tytul", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Ksiazka", "Gatunek", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ksiazka", "Gatunek", c => c.String(maxLength: 30));
            AlterColumn("dbo.Ksiazka", "Tytul", c => c.String(maxLength: 100));
            AlterColumn("dbo.Ksiazka", "Autor", c => c.String(maxLength: 40));
        }
    }
}
