namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rodzaj", "ParentId");
            DropColumn("dbo.Rodzaj", "MetaTytul");
            DropColumn("dbo.Rodzaj", "MetaOpis");
            DropColumn("dbo.Rodzaj", "MetaSlowa");
            DropColumn("dbo.Rodzaj", "Tresc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rodzaj", "Tresc", c => c.String(maxLength: 500));
            AddColumn("dbo.Rodzaj", "MetaSlowa", c => c.String(maxLength: 160));
            AddColumn("dbo.Rodzaj", "MetaOpis", c => c.String(maxLength: 160));
            AddColumn("dbo.Rodzaj", "MetaTytul", c => c.String(maxLength: 72));
            AddColumn("dbo.Rodzaj", "ParentId", c => c.Int(nullable: false));
        }
    }
}
