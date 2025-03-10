using FluentMigrator;

namespace ShopCore.Infrastructure.Migrations.Orders;

[Migration(2025_03_10_1650)]
public class Orders_2025_03_10_1650 : Migration
{
    public override void Up()
    {
        Create.Table("Orders")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .WithColumn("Quantity").AsInt32().NotNullable()
            .WithColumn("Status").AsInt32().NotNullable()
            .WithColumn("CreatedDate").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Orders");
    }
}