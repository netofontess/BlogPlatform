using FluentMigrator;

namespace BlogPlatform.Infrastructure.Migrations
{
    [Migration(202501251330)]
    public class _202501251330_Init_tables : Migration
    {
        public override void Up()
        {
            Create.Table("BlogPost")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString().Nullable()
                .WithColumn("Content").AsString().Nullable();

            Create.Table("Comment")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Content").AsString().Nullable()
                .WithColumn("BlogPostId").AsInt32().ForeignKey("BlogPost", "Id").OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Comment");
            Delete.Table("BlogPost");
        }
    }
}
