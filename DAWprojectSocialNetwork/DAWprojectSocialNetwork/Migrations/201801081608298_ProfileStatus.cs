namespace DAWprojectSocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfileStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ProfileStatus");
        }
    }
}
