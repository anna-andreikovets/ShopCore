using FluentMigrator;

namespace ShopCore.Infrastructure.Migrations.Products;

[Migration(2025_03_10_1656)]
public class Products_2025_03_10_1656 : Migration
{
    public override void Up()
    {
        Create.Table("Products")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("Price").AsDecimal().NotNullable()
            .WithColumn("Stock").AsInt32().NotNullable()
            .WithColumn("DeleteDate").AsDateTime().Nullable(); 
    }

    public override void Down()
    {
        Delete.Table("Products");
    }
}